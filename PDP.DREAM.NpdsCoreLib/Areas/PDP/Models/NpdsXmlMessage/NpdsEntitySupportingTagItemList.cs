using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsEntitySupportingTagItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntitySupportingTagItem : ANpdsXsgBaseItem<string>
  {
    public NpdsEntitySupportingTagItem() : base() { this.Initialize(); }
    public NpdsEntitySupportingTagItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntitySupportingTagItem(NpdsConst.FieldRule rul, string val) : base(NpdsParsers.ParseSupportingTag(val)) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.SupportingTagItemXnam, NpdsConst.SupportingTagListXnam); }

    public string SupportingTag
    {
      get { return ItemValue; }
      set { ItemValue = NpdsParsers.ParseSupportingTag(value); }
    }

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
          }
          // write content for element
          writer.WriteValue(ItemValue);
        }
        // write close tag for element
        writer.WriteEndElement();
      }
    }

  }

  [KnownType(typeof(NpdsEntitySupportingTagList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntitySupportingTagList : ANpdsXsgItemList<NpdsEntitySupportingTagItem>
  {
    public NpdsEntitySupportingTagList() : base() { }
    public NpdsEntitySupportingTagList(NpdsConst.FieldRule rul) : base(rul) { }
  }
}