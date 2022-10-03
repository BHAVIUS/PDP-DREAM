// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Net;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsMetadataInfosetItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsMetadataInfosetItem : ANpdsXsgBaseItem<XElement>, INpdsMetadataInfosetNexus
{
  public NpdsMetadataInfosetItem() : base() { this.Initialize(); }
  public NpdsMetadataInfosetItem(PdpAppConst.NpdsResrepFormat rrf) : base() { this.Initialize(rrf); }

  // initialize only in this private method, not with the private fields of properties
  private void Initialize(PdpAppConst.NpdsResrepFormat rrf = default(PdpAppConst.NpdsResrepFormat))
  {
    // initialize base
    base.InitNpdsItem(PdpAppConst.NpdsFieldRule.Required, PdpAppConst.InfosetItemXnam, PdpAppConst.InfosetListXnam, PdpAppConst.InfosetKeyXnam);

    // pdsroot.xsd group G_InfosetNexusCore

    // pdsroot.xsd group G_InfosetPortal
    if (rrf == PdpAppConst.NpdsResrepFormat.Nexus || rrf == PdpAppConst.NpdsResrepFormat.PORTAL)
    {
      this.InfosetPortalValidation = new NpdsInfosetValidationPortalItem(PdpAppConst.NpdsFieldRule.Permitted);
    }
    else
    {
      this.InfosetPortalValidation = new NpdsInfosetValidationPortalItem(PdpAppConst.NpdsFieldRule.Prohibited);
    }

    // pdsroot.xsd group G_InfosetDoors
    if (rrf == PdpAppConst.NpdsResrepFormat.Nexus || rrf == PdpAppConst.NpdsResrepFormat.DOORS)
    {
      this.InfosetDoorsValidation = new NpdsInfosetValidationDoorsItem(PdpAppConst.NpdsFieldRule.Permitted);
      this.InfosetNexusEntailment = new NpdsInfosetEntailmentNexusItem(PdpAppConst.NpdsFieldRule.Permitted);
    }
    else
    {
      this.InfosetDoorsValidation = new NpdsInfosetValidationDoorsItem(PdpAppConst.NpdsFieldRule.Prohibited);
      this.InfosetNexusEntailment = new NpdsInfosetEntailmentNexusItem(PdpAppConst.NpdsFieldRule.Prohibited);
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
    var writer = (NpdsXmlWrappingWriter)xWriter;
    writer.WriteStartElement(ItemXnam);
    if (ItemHasKey && writer.QURC.ItemDoesArchive)
    {
      writer.WriteAttributeString(ItemKeyXnam, InfosetGuid.ToString());
    }
    if (writer.QURC.ItemDoesVerbose || writer.QURC.ItemDoesArchive)
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
    var reader = (NpdsXmlWrappingReader)xReader;
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
      if (reader.QURC.ResrepFormat == PdpAppConst.NpdsResrepFormat.Nexus ||
          reader.QURC.ResrepFormat == PdpAppConst.NpdsResrepFormat.PORTAL)
      {
        InfosetPortalValidation.ReadXml(reader);
      }

      // pdsroot.xsd group G_InfosetDoors
      if (reader.QURC.ResrepFormat == PdpAppConst.NpdsResrepFormat.Nexus ||
          reader.QURC.ResrepFormat == PdpAppConst.NpdsResrepFormat.DOORS)
      {
        InfosetDoorsValidation.ReadXml(reader);
        InfosetNexusEntailment.ReadXml(reader);
      }

      reader.Read();
    }
  }

}

