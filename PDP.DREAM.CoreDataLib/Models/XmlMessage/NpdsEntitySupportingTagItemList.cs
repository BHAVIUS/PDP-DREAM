// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntitySupportingTagItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntitySupportingTagItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntitySupportingTagItem() : base() { this.Initialize(); }
  public NpdsEntitySupportingTagItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntitySupportingTagItem(PdpAppConst.NpdsFieldRule rul, string val) : base(NpdsParsers.ParseSupportingTag(val)) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.SupportingTagItemXnam, PdpAppConst.SupportingTagListXnam); }

  public string SupportingTag
  {
    get { return ItemValue; }
    set { ItemValue = NpdsParsers.ParseSupportingTag(value); }
  }

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
  public NpdsEntitySupportingTagList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}
