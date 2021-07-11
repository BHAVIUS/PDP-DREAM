// NpdsRecordUpdatedByItem.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

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