// ANpdsXsgItemList.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  // Abstract class for PDS XmlSerializedGeneric object ITEM LIST
  [XmlSchemaProvider(null, IsAny = true)]
  public abstract class ANpdsXsgItemList<TItem> : List<TItem>, IXmlSerializable where TItem : INpdsXsgListItem, IXmlSerializable, new()
  {
    protected ANpdsXsgItemList() { InitNpdsItemList(); }
    protected ANpdsXsgItemList(NpdsConst.FieldRule rul) { InitNpdsItemList(rul); }

    protected void InitNpdsItemList(NpdsConst.FieldRule rul = default)
    {
      ListRule = rul;
      TItem itm = new TItem();
      // the list gets names from the item class
      ItemXnam = itm.ItemXnam;
      ListXnam = itm.ItemListXnam;
    }

    public NpdsConst.FieldRule ListRule { set; get; } = default;

    public string ItemXnam { set; get; } = string.Empty;

    public string ListXnam { set; get; } = string.Empty;

    public bool ListIsNullOrEmpty
    { get { return (this.Count <= 0); } }

    public bool ListMayExist
    { get { return (ListRule != NpdsConst.FieldRule.Prohibited); } }

    public bool ListDoesExist
    { get { return (ListMayExist && !ListIsNullOrEmpty); } }

    public virtual XmlSchema GetSchema()
    {
      var xs = new XmlSchema();
      return xs;
    }

    public virtual void WriteXml(XmlWriter writer)
    {
      PdpRestContext prc = ((PdpPrcXmlWrappingWriter)writer).PRC;
      bool lstHasValues = (Count > 0);
      if (lstHasValues || prc.VerboseFormat)
      {
        writer.WriteStartElement(ListXnam);
        if (prc.VerboseFormat)
        {
          writer.WriteAttributeString(NpdsConst.ListCountXnam, Count.ToString());
        }
        if (lstHasValues)
        {
          foreach (TItem child in this)
          { child.WriteXml(writer); }
        }
        writer.WriteEndElement();
      }
    }

    public virtual void ReadXml(XmlReader reader)
    {
      int expCount = 0; // expected count
      int obsCount = 0; // observed count

      //move to start of parent node
      reader.MoveToContent();
      if (reader.IsStartElement(ListXnam))
      {
        if (reader.HasAttributes)
        {
          while (reader.MoveToNextAttribute())
          {
            string attrnam = reader.LocalName;
            string attrval = reader.GetAttribute(attrnam);
            if (attrnam == NpdsConst.ListCountXnam)
            {
              expCount = Convert.ToInt32(attrval);
            }
          }
        }
        // move to start of child node
        reader.Read();
        while (reader.IsStartElement(ItemXnam))
        {
          TItem child = new TItem();
          child.ReadXml(reader);
          this.Add(child);
          obsCount += 1;
          reader.MoveToContent();
        }
        if (obsCount > 0) { reader.Read(); }
      }

      if ((expCount != 0) && (obsCount != expCount))
      {
        throw new Exception("Observed <> Expected");
      }
    }

  }

}