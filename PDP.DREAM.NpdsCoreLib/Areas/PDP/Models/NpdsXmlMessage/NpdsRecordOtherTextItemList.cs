﻿using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordOtherTextItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordOtherTextItem : ANpdsXsgBaseItem<string>
  {
    public NpdsRecordOtherTextItem() : base() { this.Initialize(); }
    public NpdsRecordOtherTextItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordOtherTextItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.OtherTextItemXnam, NpdsConst.OtherTextListXnam); }

    public string OtherText
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }

  }

  [KnownType(typeof(NpdsRecordOtherTextList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordOtherTextList : ANpdsXsgItemList<NpdsRecordOtherTextItem>
  {
    public NpdsRecordOtherTextList() : base() { }
    public NpdsRecordOtherTextList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}
