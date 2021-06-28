using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordRegistrarItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordRegistrarItem : ANpdsXsgTypedLabelItem
  {
    public NpdsRecordRegistrarItem() : base() { this.Initialize(); }
    public NpdsRecordRegistrarItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordRegistrarItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.RegistrarItemXnam, NpdsConst.RegistrarListXnam); }

    public string Registrar
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

}
