using System;
using System.Net;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public abstract class ANpdsXsgBaseItem<TValue> : IXmlSerializable, INpdsXsgListItem
  {
    #region Constructors

    protected ANpdsXsgBaseItem() { }
    protected ANpdsXsgBaseItem(TValue value) { ItemValue = value; }
    protected ANpdsXsgBaseItem(TValue value, string hdlKey) { ItemHandleKey = hdlKey; ItemValue = value; }
    protected ANpdsXsgBaseItem(TValue value, int? iidKey) { ItemIidKey = iidKey; ItemValue = value; }
    protected ANpdsXsgBaseItem(TValue value, Guid? guidKey) { ItemGuidKey = guidKey; ItemValue = value; }
    protected ANpdsXsgBaseItem(TValue value, ItemIdentityKeys itemKeys) { ItemIndexKeys = itemKeys; ItemValue = value; }

    protected void InitNpdsItem(NpdsConst.FieldRule itmRule, string itmXnam, string lstXnam = "", string keyXnam = "")
    {
      ItemRule = itmRule;
      if (!string.IsNullOrEmpty(itmXnam)) ItemXnam = itmXnam;
      if (!string.IsNullOrEmpty(lstXnam)) ItemListXnam = lstXnam;
      if (!string.IsNullOrEmpty(keyXnam)) ItemKeyXnam = keyXnam;
    }

    #endregion

    #region Rule and Existence Properties

    public NpdsConst.FieldRule ItemRule { set; get; } = default;

    public bool ItemMayExist
    {
      get { return (ItemRule != NpdsConst.FieldRule.Prohibited); }
    }

    public bool ItemDoesExist // only if item has at least either value or key
    {
      get { return (ItemMayExist && (ItemHasValue || ItemHasKey)); }
    }

    #endregion

    #region *Xnam and other related properties

    // properties named with prefix "NpdsItem" or "NpdsList" to identify more easily in derived classes
    //  all of these properties that are declared here in this abstract base class

    // ATTN: do not use constants for these three *Xname properties
    //        because of variable overrides in derived subclasses

    public string ItemXnam { set; get; } = "NpdsItemXnam";

    public string ItemKeyXnam { set; get; } = "NpdsItemKeyXnam";

    public string ItemListXnam { set; get; } = "NpdsItemListXnam";


    // TODO: review use of NpdsItemIsPrivate and NpdsItemIsConcise
    //  in both current file NpdsXsgBaseItem and file PdpRestContext


    public bool IsCached { set; get; } = false;

    // if item is concise then cannot be displayed by requests for verbosity
    public bool IsConcise { set; get; } = false;
    public bool IsMarked { set; get; } = false;
    public bool IsPrincipal { set; get; } = false;

    // if item is private then cannot be accessed by requests for public anonymous access
    public bool IsPrivate { set; get; } = false;
    public bool IsResolvable { set; get; } = false;

    // TODO: make generic, ie, move to base item so applicable to any derived item class
    public string DisplayText { set; get; } = string.Empty;
    public Uri? DisplayImageUrl { set; get; } = null;

    #endregion

    #region ItemValue Properties

    // ItemValues could be either C# value (eg, bool, int, float) or C# reference types (eg, string)

    public bool ItemHasValue { set; get; } = false;

    public Type ItemValueType { set; get; } = typeof(string);

    private TValue itmVal;
    public TValue ItemValue
    {
      set
      {
        itmVal = value;
        if ((itmVal == null) || ((itmVal.GetType() == typeof(string)) && string.IsNullOrWhiteSpace(itmVal.ToString())))
        {
          ItemHasValue = false;
          ItemValueType = typeof(string);
        }
        else
        {
          ItemHasValue = true;
          ItemValueType = itmVal.GetType();
        }
      }
      get { return itmVal; }
    }

    #endregion

    #region ItemKey Properties

    public bool ItemHasKey
    {
      get
      {
        return (ItemHasIidKey || ItemHasGuidKey || ItemHasHandleKey || ItemHasIndexKeys);
      }
    }

    public Type ItemKeyType { set; get; } = typeof(Guid);

    private int? itmIidKey = null;
    public int? ItemIidKey
    {
      get { return itmIidKey; }
      set
      {
        itmIidKey = value;
        if (itmIidKey.HasValue) { ItemHasIidKey = true; ItemKeyType = itmIidKey.GetType(); }
        else { ItemHasIidKey = false; }
      }
    }
    public bool ItemHasIidKey { set; get; } = false;

    private Guid? itmGuidKey = null;
    public Guid? ItemGuidKey
    {
      get { return itmGuidKey; }
      set
      {
        itmGuidKey = value;
        if (itmGuidKey.HasValue) { ItemHasGuidKey = true; ItemKeyType = itmGuidKey.GetType(); }
        else { ItemHasGuidKey = false; }
      }
    }
    public bool ItemHasGuidKey { set; get; } = false;


    private string itmHdlKey = string.Empty;
    public string ItemHandleKey
    {
      get { return itmHdlKey; }
      set
      {
        itmHdlKey = value;
        if (string.IsNullOrEmpty(itmHdlKey)) { ItemHasHandleKey = false; ItemKeyType = itmHdlKey.GetType(); }
        else { ItemHasHandleKey = true; }
      }
    }
    public bool ItemHasHandleKey { set; get; } = false;

    private ItemIdentityKeys itmKeys = new ItemIdentityKeys();
    public ItemIdentityKeys ItemIndexKeys
    {
      get { return itmKeys; }
      set
      {
        itmKeys = value;
        if (itmKeys.GuidPrimaryKey.HasValue || itmKeys.GuidForeignKey.HasValue || itmKeys.Index.HasValue)
        { ItemHasIndexKeys = true; ItemKeyType = itmKeys.GetType(); }
        else { ItemHasIndexKeys = false; }
      }
    }
    public bool ItemHasIndexKeys { set; get; } = false;

    #endregion

    #region IXmlSerializable Methods

    public virtual XmlSchema GetSchema()
    {
      XmlSchema xs = new XmlSchema();
      return xs;
    }

    public virtual void WriteXml(XmlWriter xWriter)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      if (ItemHasValue || writer.PRC.ItemDoesVerbose)
      {
        // write open tag for element
        writer.WriteStartElement(ItemXnam);
        if (ItemHasValue && writer.PRC.ItemCanBeAccessed)
        {
          // write all attributes for element
          if (ItemIndexKeys.Index.HasValue && (writer.PRC.ItemDoesVerbose || writer.PRC.ItemDoesArchive))
          {
            writer.WriteAttributeString(NpdsConst.ItemPriorityXnam, ItemIndexKeys.Index.ToString());
          }
          if (ItemIndexKeys.GuidForeignKey.HasValue && writer.PRC.ItemDoesArchive)
          {
            writer.WriteAttributeString(NpdsConst.ItemForeignKeyXnam, ItemIndexKeys.GuidForeignKey.ToString());
          }
          if (ItemIndexKeys.GuidPrimaryKey.HasValue && writer.PRC.ItemDoesArchive)
          {
            writer.WriteAttributeString(NpdsConst.ItemPrimaryKeyXnam, ItemIndexKeys.GuidPrimaryKey.ToString());
          }
          // write inner content for element
          if (ItemValue != null)
          {
            if (ItemValue is XElement)
            {
              writer.WriteRaw(ItemValue.ToString());
            }
            else
            {
              writer.WriteValue(ItemValue);
            }
          }
        }
        // write close tag for element
        writer.WriteEndElement();
      }
    }

    public virtual void ReadXml(XmlReader xReader)
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
              ItemIndexKeys.Index = Convert.ToInt16(attrVal);
            }
            else if (attrNam == NpdsConst.ItemPrimaryKeyXnam)
            {
              ItemIndexKeys.GuidPrimaryKey = PdpGuid.Parse(attrVal);
            }
            else if (attrNam == NpdsConst.ItemForeignKeyXnam)
            {
              ItemIndexKeys.GuidForeignKey = PdpGuid.Parse(attrVal);
            }
          }
          reader.MoveToElement();
        }
        string elemNam = reader.LocalName;
        if (elemNam == ItemXnam)
        {
          string elemVal = reader.ReadInnerXml();
          this.ItemValue = ParseToNpdsItemValueType(elemVal);
        }
      }
    }

    #endregion

    #region Other Methods

    public override bool Equals(object obj)
    {
      // Get the type t.
      Type clstyp = this.GetType();
      //Check for null and compare run-time types.
      if (obj == null || clstyp != obj.GetType()) return false;
      // initialize the properties
      PropertyInfo[] props = clstyp.GetProperties();
      // Cycle through the public properties.
      foreach (PropertyInfo prp in props)
      {
        //  var prptyp = prp.PropertyType();
        var x = prp.GetValue(this, null);
        bool xIsNull = (x == null);
        //  x = Convert.ChangeType(x, prptyp);
        var y = prp.GetValue(obj, null);
        bool yIsNull = (y == null);
        //  y = Convert.ChangeType(y, prptyp);
        // consider equal if both null
        if (!(xIsNull && yIsNull))
        {
          // consider unequal if only one is null
          if (!xIsNull && yIsNull) return false;
          if (xIsNull && !yIsNull) return false;
          // both not null, test property pair equality
          if (!x.Equals(y)) return false;
        }
      }
      // all property pairs are equal
      return true;
    }

    public override int GetHashCode()
    {
      // initialize the code c
      int c = 0;
      // Get the type t.
      Type t = this.GetType();
      // Cycle through the properties.
      foreach (PropertyInfo p in t.GetProperties())
      {
        c = c ^ p.GetHashCode();
      }
      return c;
    }

    protected TValue ParseToNpdsItemValueType(string strVal)
    {
      string decVal = WebUtility.HtmlDecode(strVal);
      Type valTyp = typeof(TValue);
      if (valTyp.Name == typeof(XElement).Name)
      {
        var elm = new XElement(valTyp.Name);
        if (!(string.IsNullOrEmpty(decVal)))
        {
          XmlDocument doc = new XmlDocument();
          doc.LoadXml(decVal);
          elm = XElement.Parse(doc.InnerXml);
        }
        return (TValue)(Convert.ChangeType(elm, valTyp));
      }
      else if (valTyp.Name == typeof(Uri).Name)
      {
        var vri = new Uri(valTyp.Name);
        if (!(string.IsNullOrEmpty(decVal)))
        {
          vri = new Uri(decVal);
        }
        return (TValue)(Convert.ChangeType(vri, valTyp));
      }
      else
      {
        return (TValue)(Convert.ChangeType(decVal, valTyp));
      }
    }

    protected void WriteItemPropAsElem(string elemValue, string elemName, XmlWriter xWriter, bool writAsRaw = false)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      if (!string.IsNullOrEmpty(elemValue))
      {
        if (writAsRaw)
        {
          writer.WriteStartElement(elemName);
          writer.WriteRaw(elemValue);
          writer.WriteEndElement();
        }
        else
        {
          writer.WriteElementString(elemName, elemValue);
        }
      }
      else if (writer.PRC.ItemDoesVerbose)
      {
        writer.WriteStartElement(elemName);
        writer.WriteEndElement();
      }
    }
    protected void WriteItemPropAsElem(Uri elemValue, string elemName, XmlWriter xWriter, bool writAsRaw = false)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      if (elemValue != null) { WriteItemPropAsElem(elemValue.ToString(), elemName, writer, writAsRaw); }
      else if (writer.PRC.ItemDoesVerbose) { WriteItemPropAsElem(string.Empty, elemName, writer, writAsRaw); }
    }
    protected void WriteItemPropAsElem(XElement elemValue, string elemName, XmlWriter xWriter, bool writAsRaw = true)
    {
      var writer = (PdpPrcXmlWrappingWriter)xWriter;
      if (elemValue != null) { WriteItemPropAsElem(elemValue.ToString(), elemName, writer, writAsRaw); }
      else if (writer.PRC.ItemDoesVerbose) { WriteItemPropAsElem(string.Empty, elemName, writer, writAsRaw); }
    }

    protected string ReadItemPropAsString(string elemName, XmlReader xReader)
    {
      var reader = (PdpPrcXmlWrappingReader)xReader;
      string elemVal = string.Empty;
      reader.MoveToContent();
      if (reader.IsStartElement(elemName))
      {
        elemVal = WebUtility.HtmlDecode(reader.ReadInnerXml());
      }
      else if (reader.NodeType == XmlNodeType.Text)
      {
        elemVal = WebUtility.HtmlDecode(reader.ReadContentAsString());
      }
      return elemVal;
    }

    protected Uri ReadItemPropAsUri(string elemName, XmlReader xReader)
    {
      var reader = (PdpPrcXmlWrappingReader)xReader;
      var elemVal = new Uri(elemName);
      string strVal = ReadItemPropAsString(elemName, reader);
      if (!(string.IsNullOrEmpty(strVal)))
      {
        elemVal = new Uri(strVal, UriKind.Absolute);
      }
      return elemVal;
    }

    protected XElement ReadItemPropAsXElement(string elemName, XmlReader xReader)
    {
      var reader = (PdpPrcXmlWrappingReader)xReader;
      var elemVal = new XElement(elemName);
      string strVal = ReadItemPropAsString(elemName, reader);
      if (!string.IsNullOrEmpty(strVal))
      {
        elemVal = XElement.Parse(strVal);
      }
      return elemVal;
    }

    #endregion
  }

}
