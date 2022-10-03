// NpdsRecordUpdatedOnItem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordUpdatedOnItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordUpdatedOnItem : ANpdsXsgBaseItem<DateTime>
{
  public NpdsRecordUpdatedOnItem() : base() { this.Initialize(); }
  public NpdsRecordUpdatedOnItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordUpdatedOnItem(PdpAppConst.NpdsFieldRule rul, DateTime val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.UpdatedOnItemXnam, PdpAppConst.UpdatedOnListXnam); }

  public DateTime UpdatedOn
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}
