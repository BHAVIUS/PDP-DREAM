using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordCreatedByItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordCreatedByItem : ANpdsXsgTypedLabelItem
  {
    public NpdsRecordCreatedByItem() : base() { this.Initialize(); }
    public NpdsRecordCreatedByItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordCreatedByItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.CreatedByItemXnam, NpdsConst.CreatedByListXnam); }

    public string CreatedBy
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

}
