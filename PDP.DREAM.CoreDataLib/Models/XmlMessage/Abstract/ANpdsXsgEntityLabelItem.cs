// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public abstract class ANpdsXsgEntityLabelItem : ANpdsXsgBaseItem<Uri?>
{
  protected ANpdsXsgEntityLabelItem() : base() { }
  protected ANpdsXsgEntityLabelItem(Uri? val) : base(val) { }

  public bool IsGenerating { set; get; } = false;
  public string? LabelUri { set; get; } = ESS;
  public string? TagToken { set; get; } = ESS;

  public void ParseEntityLabel(string? entityLabel)
  {
    ItemValue = NpdsParsers.ParseEntityLabel(entityLabel);
  }
  public void ParseEntityLabel(string? labelUri, string? tagToken)
  {
    LabelUri = labelUri;
    TagToken = tagToken;
    ItemValue = NpdsParsers.ParseEntityLabel(LabelUri + TagToken);
  }

  public string? EntityLabel
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }

  private Guid? srGuid;
  public Guid? SelfrefGuidKey
  {
    get { return srGuid; }
    set { srGuid = value; }
  }

  private byte entTypCod;
  public byte EntityTypeCode
  {
    get { return entTypCod; }
    set { entTypCod = value; }
  }

  private string entTypNam;
  public string EntityTypeName
  {
    get { return entTypNam; }
    set {
      entTypNam = value;
      if (string.IsNullOrEmpty(entTypNam)) { hasEntTypNam = false; }
      else { hasEntTypNam = true; }
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
    if (ItemHasValue || writer.WRACE.ItemDoesVerbose)
    {
      // write open tag for element
      writer.WriteStartElement(ItemXnam);
      if (ItemHasValue && writer.WRACE.ItemCanBeAccessed)
      {
        bool doesVerbOrArch = (writer.WRACE.ItemDoesVerbose || writer.WRACE.ItemDoesArchive);
        // write attributes for element
        if (ItemIndexKeys.Priority.HasValue && (writer.WRACE.ItemDoesVerbose || writer.WRACE.ItemDoesArchive))
        {
          writer.WriteAttributeString(ItemFacetPriorityXnam, ItemIndexKeys.Priority.ToString());
        }
        if (HasEntityTypeName && doesVerbOrArch)
        { writer.WriteAttributeString(EntityTypeNameXnam, EntityTypeName); }
        if (SelfrefGuidKey.HasValue && doesVerbOrArch)
        { writer.WriteAttributeString(InfosetKeyXnam, SelfrefGuidKey.ToString()); }
        if (writer.WRACE.ItemDoesArchive)
        {
          if (ItemIndexKeys.GuidForeignKey.HasValue)
          { writer.WriteAttributeString(ItemForeignKeyXnam, ItemIndexKeys.GuidForeignKey.ToString()); }
          if (ItemIndexKeys.GuidPrimaryKey.HasValue)
          { writer.WriteAttributeString(ItemPrimaryKeyXnam, ItemIndexKeys.GuidPrimaryKey.ToString()); }
          writer.WriteAttributeString(ItemIsPrincipalXnam, IsPrincipal.ToString().ToLower());
          writer.WriteAttributeString(ItemIsPrivateXnam, IsPrivate.ToString().ToLower());
          writer.WriteAttributeString(ItemIsResolvableXnam, IsResolvable.ToString().ToLower());
          writer.WriteAttributeString(ItemIsGeneratingXnam, IsGenerating.ToString().ToLower());
        }
        // write content for element
        if (ItemValue != null)
        { writer.WriteValue(ItemValue.AbsoluteUri.ToString()); }
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
          switch (attrNam)
          {
            case EntityTypeNameXnam:
              EntityTypeName = attrVal;
              break;
            case InfosetKeyXnam:
              SelfrefGuidKey = PdpGuid.ParseToNonNullable(attrVal);
              break;
            case ItemForeignKeyXnam:
              ItemIndexKeys.GuidForeignKey = PdpGuid.ParseToNonNullable(attrVal);
              break;
            case ItemPrimaryKeyXnam:
              ItemIndexKeys.GuidPrimaryKey = PdpGuid.ParseToNonNullable(attrVal);
              break;
            case ItemFacetIndexXnam:
              ItemIndexKeys.Index = Convert.ToInt16(attrVal);
              break;
            case ItemFacetPriorityXnam:
              ItemIndexKeys.Priority = Convert.ToInt16(attrVal);
              break;
            case ItemIsPrincipalXnam:
              IsPrincipal = Convert.ToBoolean(attrVal);
              break;
            case ItemIsPrivateXnam:
              IsPrivate = Convert.ToBoolean(attrVal);
              break;
            case ItemIsGeneratingXnam:
              IsGenerating = Convert.ToBoolean(attrVal);
              break;
            case ItemIsResolvableXnam:
              IsResolvable = Convert.ToBoolean(attrVal);
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
        ItemValue = NpdsParsers.ParseEntityLabel(elemVal);
      }
    }

    // TODO: implement for TagToken and LabelUri
    // reader.Read();
    // TagToken = ReadItemPropAsString(TagTokenXnam, reader)
    // LabelUri = ReadItemPropAsString(LabelUriXnam, reader)
    // ParseEntityLabel(ReadItemPropAsString(EntityLabelItemXnam, reader));
    // reader.Read();
  }

} // class

// end file