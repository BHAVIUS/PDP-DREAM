using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordCreatedOnItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordCreatedOnItem : ANpdsXsgBaseItem<DateTime>
  {
    public NpdsRecordCreatedOnItem() : base() { this.Initialize(); }
    public NpdsRecordCreatedOnItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordCreatedOnItem(NpdsConst.FieldRule rul, DateTime val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.CreatedOnItemXnam, NpdsConst.CreatedOnListXnam); }

    public DateTime CreatedOn
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }
}