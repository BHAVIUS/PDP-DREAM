// NpdsRecordUpdatedByItem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordUpdatedByItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordUpdatedByItem : ANpdsXsgTypedLabelItem
{
  public NpdsRecordUpdatedByItem() : base() { this.Initialize(); }
  public NpdsRecordUpdatedByItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordUpdatedByItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.UpdatedByItemXnam, PdpAppConst.UpdatedByListXnam); }

  public string UpdatedBy
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

