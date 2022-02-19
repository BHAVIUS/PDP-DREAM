// NpdsEntitySupportingLabelItemList.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsEntitySupportingLabelItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntitySupportingLabelItem : ANpdsXsgBaseItem<Uri>
  {
    public NpdsEntitySupportingLabelItem() : base() { this.Initialize(); }
    public NpdsEntitySupportingLabelItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntitySupportingLabelItem(NpdsConst.FieldRule rul, Uri val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.SupportingLabelItemXnam, NpdsConst.SupportingLabelListXnam); }

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
    public NpdsEntitySupportingLabelList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}