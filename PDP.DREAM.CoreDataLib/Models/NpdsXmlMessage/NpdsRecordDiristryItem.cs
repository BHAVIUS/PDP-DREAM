// NpdsRecordDiristryItem.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsRecordDiristryItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordDiristryItem : ANpdsXsgTypedLabelItem
  {
    public NpdsRecordDiristryItem() : base() { this.Initialize(); }
    public NpdsRecordDiristryItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordDiristryItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.DiristryItemXnam, NpdsConst.DiristryListXnam); }

    public string Diristry
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

}
