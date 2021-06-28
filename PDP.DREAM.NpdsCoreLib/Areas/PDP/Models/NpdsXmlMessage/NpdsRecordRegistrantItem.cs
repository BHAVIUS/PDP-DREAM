using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordRegistrantItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordRegistrantItem : ANpdsXsgTypedLabelItem
  {
    public NpdsRecordRegistrantItem() : base() { this.Initialize(); }
    public NpdsRecordRegistrantItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordRegistrantItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.RegistrantItemXnam, NpdsConst.RegistrantListXnam); }

    public string Registrant
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

}
