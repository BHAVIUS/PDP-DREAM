using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsEntityAliasLabelItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityAliasLabelItem : ANpdsXsgBaseItem<string>
  {
    public NpdsEntityAliasLabelItem() : base() { this.Initialize(); }
    public NpdsEntityAliasLabelItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntityAliasLabelItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.AliasLabelItemXnam, NpdsConst.AliasLabelListXnam); }

    public string AliasLabel
    {
      get { return ItemValue; }
      set { if (value != null) { ItemValue = value; } }
    }

    public string TagToken { set; get; } = string.Empty;

    public string LabelUri { set; get; } = string.Empty;

    public bool IsGenerating { set; get; } = false;

    public override void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      if (ItemHasValue || writer.PRC.ItemDoesVerbose)
      {
        // write open tag for element
        writer.WriteStartElement(ItemXnam);
        if (ItemHasValue && writer.PRC.ItemCanBeAccessed)
        {
          // write attributes for element
          if (ItemIndexKeys.Priority.HasValue && (writer.PRC.ItemDoesVerbose || writer.PRC.ItemDoesArchive))
          {
            writer.WriteAttributeString(NpdsConst.ItemPriorityXnam, ItemIndexKeys.Priority.ToString());
          }
          if (writer.PRC.ItemDoesArchive)
          {
            if (ItemIndexKeys.GuidForeignKey.HasValue)
            { writer.WriteAttributeString(NpdsConst.ItemForeignKeyXnam, ItemIndexKeys.GuidForeignKey.ToString()); }
            if (ItemIndexKeys.GuidPrimaryKey.HasValue)
            { writer.WriteAttributeString(NpdsConst.ItemPrimaryKeyXnam, ItemIndexKeys.GuidPrimaryKey.ToString()); }
            writer.WriteAttributeString(NpdsConst.ItemIsPrivateXnam, IsPrivate.ToString().ToLower());
            writer.WriteAttributeString(NpdsConst.ItemIsResolvableXnam, IsResolvable.ToString().ToLower());
            writer.WriteAttributeString(NpdsConst.ItemIsPrincipalXnam, IsPrincipal.ToString().ToLower());
            writer.WriteAttributeString(NpdsConst.ItemIsGeneratingXnam, IsGenerating.ToString().ToLower());
          }
          // write content for element
          if (AliasLabel != null)
          { writer.WriteValue(AliasLabel); }
        }
        // write close tag for element
        writer.WriteEndElement();
      }
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
            string attrNam = reader.LocalName;
            string attrVal = reader.GetAttribute(attrNam);
            if (attrNam == NpdsConst.ItemPriorityXnam)
            {
              ItemIndexKeys.Priority = Convert.ToInt16(attrVal);
            }
            else if (attrNam == NpdsConst.ItemIndexXnam)
            {
              ItemIndexKeys.Index = Convert.ToInt16(attrVal);
            }
            else if (attrNam == NpdsConst.ItemIsPrivateXnam)
            {
              IsPrivate = Convert.ToBoolean(attrVal);
            }
            else if (attrNam == NpdsConst.ItemForeignKeyXnam)
            {
              ItemIndexKeys.GuidForeignKey = PdpGuid.Parse(attrVal);
            }
            else if (attrNam == NpdsConst.ItemPrimaryKeyXnam)
            {
              ItemIndexKeys.GuidPrimaryKey = PdpGuid.Parse(attrVal);
            }
            else if (attrNam == NpdsConst.ItemIsPrincipalXnam)
            {
              IsPrincipal = Convert.ToBoolean(attrVal);
            }
            else if (attrNam == NpdsConst.ItemIsResolvableXnam)
            {
              IsResolvable = Convert.ToBoolean(attrVal);
            }
            else if (attrNam == NpdsConst.ItemIsGeneratingXnam)
            {
              IsGenerating = Convert.ToBoolean(attrVal);
            }
          }
        }
        reader.Read();
        // TagToken = ReadItemPropAsString(NpdsConstants.TagTokenXnam, reader)
        // LabelUri = ReadItemPropAsString(NpdsConstants.LabelUriXnam, reader)
        AliasLabel = ReadItemPropAsString(NpdsConst.EntityLabelItemXnam, reader);
        reader.Read();
      }
    }
  }

  [KnownType(typeof(NpdsEntityAliasLabelList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityAliasLabelList : ANpdsXsgItemList<NpdsEntityAliasLabelItem>
  {
    public NpdsEntityAliasLabelList() : base() { }
    public NpdsEntityAliasLabelList(NpdsConst.FieldRule rul) : base(rul) { }
  }
}