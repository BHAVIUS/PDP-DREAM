// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityAliasLabelItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityAliasLabelItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityAliasLabelItem() : base() { this.Initialize(); }
  public NpdsEntityAliasLabelItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityAliasLabelItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.AliasLabelItemXnam, PdpAppConst.AliasLabelListXnam); }

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
    var writer = (NpdsXmlWrappingWriter)xWriter;
    if (ItemHasValue || writer.QURC.ItemDoesVerbose)
    {
      // write open tag for element
      writer.WriteStartElement(ItemXnam);
      if (ItemHasValue && writer.QURC.ItemCanBeAccessed)
      {
        // write attributes for element
        if (ItemIndexKeys.Priority.HasValue && (writer.QURC.ItemDoesVerbose || writer.QURC.ItemDoesArchive))
        {
          writer.WriteAttributeString(PdpAppConst.ItemPriorityXnam, ItemIndexKeys.Priority.ToString());
        }
        if (writer.QURC.ItemDoesArchive)
        {
          if (ItemIndexKeys.GuidForeignKey.HasValue)
          { writer.WriteAttributeString(PdpAppConst.ItemForeignKeyXnam, ItemIndexKeys.GuidForeignKey.ToString()); }
          if (ItemIndexKeys.GuidPrimaryKey.HasValue)
          { writer.WriteAttributeString(PdpAppConst.ItemPrimaryKeyXnam, ItemIndexKeys.GuidPrimaryKey.ToString()); }
          writer.WriteAttributeString(PdpAppConst.ItemIsPrivateXnam, IsPrivate.ToString().ToLower());
          writer.WriteAttributeString(PdpAppConst.ItemIsResolvableXnam, IsResolvable.ToString().ToLower());
          writer.WriteAttributeString(PdpAppConst.ItemIsPrincipalXnam, IsPrincipal.ToString().ToLower());
          writer.WriteAttributeString(PdpAppConst.ItemIsGeneratingXnam, IsGenerating.ToString().ToLower());
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
          string attrNam = reader.LocalName;
          string attrVal = reader.GetAttribute(attrNam);
          if (attrNam == PdpAppConst.ItemPriorityXnam)
          {
            ItemIndexKeys.Priority = Convert.ToInt16(attrVal);
          }
          else if (attrNam == PdpAppConst.ItemIndexXnam)
          {
            ItemIndexKeys.Index = Convert.ToInt16(attrVal);
          }
          else if (attrNam == PdpAppConst.ItemIsPrivateXnam)
          {
            IsPrivate = Convert.ToBoolean(attrVal);
          }
          else if (attrNam == PdpAppConst.ItemForeignKeyXnam)
          {
            ItemIndexKeys.GuidForeignKey = PdpGuid.ParseToNonNullable(attrVal);
          }
          else if (attrNam == PdpAppConst.ItemPrimaryKeyXnam)
          {
            ItemIndexKeys.GuidPrimaryKey = PdpGuid.ParseToNonNullable(attrVal);
          }
          else if (attrNam == PdpAppConst.ItemIsPrincipalXnam)
          {
            IsPrincipal = Convert.ToBoolean(attrVal);
          }
          else if (attrNam == PdpAppConst.ItemIsResolvableXnam)
          {
            IsResolvable = Convert.ToBoolean(attrVal);
          }
          else if (attrNam == PdpAppConst.ItemIsGeneratingXnam)
          {
            IsGenerating = Convert.ToBoolean(attrVal);
          }
        }
      }
      reader.Read();
      // TagToken = ReadItemPropAsString(NpdsConstants.TagTokenXnam, reader)
      // LabelUri = ReadItemPropAsString(NpdsConstants.LabelUriXnam, reader)
      AliasLabel = ReadItemPropAsString(PdpAppConst.EntityLabelItemXnam, reader);
      reader.Read();
    }
  }
}

[KnownType(typeof(NpdsEntityAliasLabelList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityAliasLabelList : ANpdsXsgItemList<NpdsEntityAliasLabelItem>
{
  public NpdsEntityAliasLabelList() : base() { }
  public NpdsEntityAliasLabelList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}
