// NpdsMetadataInfosetItem.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsMetadataInfosetItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsMetadataInfosetItem : ANpdsXsgBaseItem<XElement>, INpdsMetadataInfosetNexus
  {
    public NpdsMetadataInfosetItem() : base() { this.Initialize(); }
    public NpdsMetadataInfosetItem(NpdsConst.ResrepFormat rrf) : base() { this.Initialize(rrf); }

    // initialize only in this private method, not with the private fields of properties
    private void Initialize(NpdsConst.ResrepFormat rrf = default(NpdsConst.ResrepFormat))
    {
      // initialize base
      base.InitNpdsItem(NpdsConst.FieldRule.Required, NpdsConst.InfosetItemXnam, NpdsConst.InfosetListXnam, NpdsConst.InfosetKeyXnam);

      // pdsroot.xsd group G_InfosetNexusCore

      // pdsroot.xsd group G_InfosetPortal
      if (rrf == NpdsConst.ResrepFormat.Nexus || rrf == NpdsConst.ResrepFormat.PORTAL)
      {
        this.InfosetPortalValidation = new NpdsInfosetValidationPortalItem(NpdsConst.FieldRule.Permitted);
      }
      else
      {
        this.InfosetPortalValidation = new NpdsInfosetValidationPortalItem(NpdsConst.FieldRule.Prohibited);
      }

      // pdsroot.xsd group G_InfosetDoors
      if (rrf == NpdsConst.ResrepFormat.Nexus || rrf == NpdsConst.ResrepFormat.DOORS)
      {
        this.InfosetDoorsValidation = new NpdsInfosetValidationDoorsItem(NpdsConst.FieldRule.Permitted);
        this.InfosetNexusEntailment = new NpdsInfosetEntailmentNexusItem(NpdsConst.FieldRule.Permitted);
      }
      else
      {
        this.InfosetDoorsValidation = new NpdsInfosetValidationDoorsItem(NpdsConst.FieldRule.Prohibited);
        this.InfosetNexusEntailment = new NpdsInfosetEntailmentNexusItem(NpdsConst.FieldRule.Prohibited);
      }
    }

    public Guid? InfosetGuid
    {
      get { return ItemGuidKey; }
      set { ItemGuidKey = value; }
    }

    public string? InfosetHandle
    {
      get { return ItemHandleKey; }
      set { ItemHandleKey = value; }
    }

    public bool InfosetIsAuthorPrivate { set; get; } = false;
    public bool InfosetIsAgentShared { set; get; } = false;
    public bool InfosetIsUpdaterLimited { set; get; } = false;
    public bool InfosetIsManagerReleased { set; get; } = false;
    public bool InfosetIsConcise { set; get; } = false;
    public NpdsInfosetEntailmentNexusItem InfosetNexusEntailment { set; get; }
    public NpdsInfosetValidationPortalItem InfosetPortalValidation { set; get; }
    public NpdsInfosetValidationDoorsItem InfosetDoorsValidation { set; get; }

    public override void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      writer.WriteStartElement(ItemXnam);
      if (ItemHasKey && writer.PRC.ItemDoesArchive)
      {
        writer.WriteAttributeString(ItemKeyXnam, InfosetGuid.ToString());
      }
      if (writer.PRC.ItemDoesVerbose || writer.PRC.ItemDoesArchive)
      {
        if (InfosetPortalValidation.ItemMayExist) { InfosetPortalValidation.WriteXml(writer); }
        if (InfosetDoorsValidation.ItemMayExist) { InfosetDoorsValidation.WriteXml(writer); }
        if (InfosetNexusEntailment.ItemMayExist) { InfosetNexusEntailment.WriteXml(writer); }
      }
      else
      {
        if (InfosetPortalValidation.ItemDoesExist) { InfosetPortalValidation.WriteXml(writer); }
        if (InfosetDoorsValidation.ItemDoesExist) { InfosetDoorsValidation.WriteXml(writer); }
        if (InfosetNexusEntailment.ItemDoesExist) { InfosetNexusEntailment.WriteXml(writer); }
      }
      writer.WriteEndElement();
    }

    public override void ReadXml(XmlReader xReader)
    {
      var reader = (PdpPrcXmlWrappingReader)xReader;
      reader.MoveToContent();
      if (reader.IsEmptyElement)
      {
        reader.ReadStartElement();
      }
      else if (reader.IsStartElement(ItemXnam))
      {
        if (reader.HasAttributes)
        {
          while (reader.MoveToNextAttribute())
          {
            string attrnam = reader.LocalName;
            string attrval = reader.GetAttribute(attrnam);
            if (!string.IsNullOrEmpty(attrval))
            {
              if (attrnam == ItemKeyXnam)
              {
                ItemGuidKey = new Guid(WebUtility.HtmlDecode(attrval));
              }
            }
          }
        }
        reader.Read();

        // pdsroot.xsd group G_InfosetNexusCore

        // pdsroot.xsd group G_InfosetPortal
        if (reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.Nexus ||
            reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.PORTAL)
        {
          InfosetPortalValidation.ReadXml(reader);
        }

        // pdsroot.xsd group G_InfosetDoors
        if (reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.Nexus ||
            reader.PRC.ResrepFormat == NpdsConst.ResrepFormat.DOORS)
        {
          InfosetDoorsValidation.ReadXml(reader);
          InfosetNexusEntailment.ReadXml(reader);
        }

        reader.Read();
      }
    }

  }

}