// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Xml;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Models;

public abstract class ANpdsXsgTypedLabelItem : ANpdsXsgBaseItem<string>
{
  protected ANpdsXsgTypedLabelItem() : base() { }
  protected ANpdsXsgTypedLabelItem(string val) : base(val) { }

  public string EntityTypedLabel
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

  private Guid? srGuid;
  public Guid? SelfrefGuidKey
  {
    get { return srGuid; }
    set { srGuid = value; }
  }

  private Type entTypEnum = typeof(PdpAppConst.NpdsEntityType);
  private PdpAppConst.NpdsEntityType entTypCod;
  public PdpAppConst.NpdsEntityType EntityTypeCode
  {
    get
    {
      if (!hasEntTypNam)
      {
        entTypCod = PdpAppConst.NpdsEntityType.Untyped;
        entTypNam = Enum.GetName(entTypEnum, entTypCod);
        hasEntTypNam = true;
      }
      return entTypCod;
    }
    set
    {
      if (Enum.IsDefined(entTypEnum, value)) { entTypCod = value; }
      else { entTypCod = PdpAppConst.NpdsEntityType.Untyped; }
      entTypNam = Enum.GetName(entTypEnum, entTypCod);
      if (string.IsNullOrEmpty(entTypNam)) { hasEntTypNam = false; }
      else { hasEntTypNam = true; }
    }
  }

  private string entTypNam;
  public string EntityTypeName
  {
    get { return entTypNam; }
    set
    {
      PdpAppConst.NpdsEntityType cod = 0;
      try { cod = (PdpAppConst.NpdsEntityType)(Enum.Parse(entTypEnum, value)); }
      catch { cod = PdpAppConst.NpdsEntityType.Untyped; }
      EntityTypeCode = cod;
    }
  }

  private bool hasEntTypNam = false;
  public bool HasEntityTypeName
  {
    get { return hasEntTypNam; }
  }

  public override void WriteXml(XmlWriter xWriter)
  {
    var writer = (NpdsXmlWrappingWriter)xWriter;
    if (ItemHasValue || writer.QURC.ItemDoesVerbose)
    {
      writer.WriteStartElement(ItemXnam);
      if (ItemHasValue && writer.QURC.ItemCanBeAccessed)
      {
        bool doesVerbOrArch = (writer.QURC.ItemDoesVerbose || writer.QURC.ItemDoesArchive);
        if (HasEntityTypeName && doesVerbOrArch)
        { writer.WriteAttributeString(PdpAppConst.EntityTypeNameXnam, EntityTypeName); }
        if (SelfrefGuidKey.HasValue && doesVerbOrArch)
        { writer.WriteAttributeString(PdpAppConst.InfosetKeyXnam, SelfrefGuidKey.ToString()); }
        writer.WriteString(ItemValue.ToString());
      }
      writer.WriteEndElement();
    }
  }

  public override void ReadXml(XmlReader xReader)
  {
    var reader = (NpdsXmlWrappingReader)xReader;
    reader.MoveToContent();
    if (reader.IsStartElement(ItemXnam))
    {
      if (reader.HasAttributes)
      {
        while (reader.MoveToNextAttribute())
        {
          string attrNam = reader.LocalName;
          string attrVal = reader.GetAttribute(attrNam);
          switch (attrNam)
          {
            case PdpAppConst.InfosetKeyXnam:
              SelfrefGuidKey = new Guid(attrVal);
              break;
            case PdpAppConst.EntityTypeNameXnam:
              EntityTypeName = attrVal;
              break;
            default:
              break;
          }
        }
        reader.MoveToElement();
      }
      string elemNam = reader.LocalName;
      if (elemNam == ItemXnam)
      {
        string elemVal = reader.ReadInnerXml();
        ItemValue = elemVal;
      }
    }
  }

} // class

