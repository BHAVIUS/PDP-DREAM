using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordManagedByItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordManagedByItem : ANpdsXsgTypedLabelItem
  {
    public NpdsRecordManagedByItem() : base() { this.Initialize(); }
    public NpdsRecordManagedByItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordManagedByItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.ManagedByItemXnam, NpdsConst.ManagedByListXnam); }

    public string ManagedBy
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

}