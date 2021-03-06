// NpdsRecordUpdatedOnItem.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsRecordUpdatedOnItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordUpdatedOnItem : ANpdsXsgBaseItem<DateTime>
  {
    public NpdsRecordUpdatedOnItem() : base() { this.Initialize(); }
    public NpdsRecordUpdatedOnItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordUpdatedOnItem(NpdsConst.FieldRule rul, DateTime val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.UpdatedOnItemXnam, NpdsConst.UpdatedOnListXnam); }

    public DateTime UpdatedOn
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }
}