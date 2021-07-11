// NpdsRecordManagedByItem.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

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