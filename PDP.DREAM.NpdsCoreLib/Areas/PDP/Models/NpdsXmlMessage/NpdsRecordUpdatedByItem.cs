using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordUpdatedByItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordUpdatedByItem : ANpdsXsgTypedLabelItem
  {
    public NpdsRecordUpdatedByItem() : base() { this.Initialize(); }
    public NpdsRecordUpdatedByItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordUpdatedByItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.UpdatedByItemXnam, NpdsConst.UpdatedByListXnam); }

    public string UpdatedBy
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

}