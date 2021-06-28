using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordRegistryItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordRegistryItem : ANpdsXsgTypedLabelItem
  {
    public NpdsRecordRegistryItem() : base() { this.Initialize(); }
    public NpdsRecordRegistryItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordRegistryItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.RegistryItemXnam, NpdsConst.RegistryListXnam); }

    public string Registry
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

}
