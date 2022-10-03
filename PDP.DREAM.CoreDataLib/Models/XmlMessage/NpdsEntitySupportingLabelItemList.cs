// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

using PDP.DREAM.CoreDataLib.Types;
using PDP.DREAM.CoreDataLib.Utilities;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntitySupportingLabelItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntitySupportingLabelItem : ANpdsXsgBaseItem<Uri>
{
  public NpdsEntitySupportingLabelItem() : base() { this.Initialize(); }
  public NpdsEntitySupportingLabelItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntitySupportingLabelItem(PdpAppConst.NpdsFieldRule rul, Uri val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.SupportingLabelItemXnam, PdpAppConst.SupportingLabelListXnam); }

  public Uri? SupportingLabel
  {
    get { return ItemValue; }
    set { if (value != null) { ItemValue = value; } }
  }

  public void ParseSupportingLabel(string value)
  {
    Uri? val = NpdsParsers.ParseLabel(value);
    if (val != null) { ItemValue = val; }
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
        // write content for element PdsItemValue
        writer.WriteValue(ItemValue.AbsoluteUri);
      }
      // write close tag for element
      writer.WriteEndElement();
    }
  }

}

[KnownType(typeof(NpdsEntitySupportingLabelList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntitySupportingLabelList : ANpdsXsgItemList<NpdsEntitySupportingLabelItem>
{
  public NpdsEntitySupportingLabelList() : base() { }
  public NpdsEntitySupportingLabelList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

