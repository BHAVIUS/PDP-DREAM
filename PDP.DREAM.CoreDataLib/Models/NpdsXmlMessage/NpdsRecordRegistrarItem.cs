// NpdsRecordRegistrarItem.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
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
