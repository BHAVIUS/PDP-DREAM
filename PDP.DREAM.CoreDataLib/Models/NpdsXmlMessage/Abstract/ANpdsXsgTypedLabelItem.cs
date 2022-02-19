// ANpdsXsgTypedLabelItem.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Xml;

namespace PDP.DREAM.CoreDataLib.Models
{
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

    private Type entTypEnum = typeof(NpdsConst.EntityType);
    private NpdsConst.EntityType entTypCod;
    public NpdsConst.EntityType EntityTypeCode
    {
      get
      {
        if (!hasEntTypNam)
        {
          entTypCod = NpdsConst.EntityType.Untyped;
          entTypNam = Enum.GetName(entTypEnum, entTypCod);
          hasEntTypNam = true;
        }
        return entTypCod;
      }
      set
      {
        if (Enum.IsDefined(entTypEnum, value)) { entTypCod = value; }
        else { entTypCod = NpdsConst.EntityType.Untyped; }
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
        NpdsConst.EntityType cod = 0;
        try { cod = (NpdsConst.EntityType)(Enum.Parse(entTypEnum, value)); }
        catch { cod = NpdsConst.EntityType.Untyped; }
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
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      if (ItemHasValue || writer.PRC.ItemDoesVerbose)
      {
        writer.WriteStartElement(ItemXnam);
        if (ItemHasValue && writer.PRC.ItemCanBeAccessed)
        {
          bool doesVerbOrArch = (writer.PRC.ItemDoesVerbose || writer.PRC.ItemDoesArchive);
          if (HasEntityTypeName && doesVerbOrArch)
          { writer.WriteAttributeString(NpdsConst.EntityTypeNameXnam, EntityTypeName); }
          if (SelfrefGuidKey.HasValue && doesVerbOrArch)
          { writer.WriteAttributeString(NpdsConst.InfosetKeyXnam, SelfrefGuidKey.ToString()); }
          writer.WriteString(ItemValue.ToString());
        }
        writer.WriteEndElement();
      }
    }

    public override void ReadXml(XmlReader xReader)
    {
      var reader = (PdpPrcXmlWrappingReader)xReader;
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
              case NpdsConst.InfosetKeyXnam:
                SelfrefGuidKey = new Guid(attrVal);
                break;
              case NpdsConst.EntityTypeNameXnam:
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

} // namespace