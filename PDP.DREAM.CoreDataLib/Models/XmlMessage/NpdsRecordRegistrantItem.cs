// NpdsRecordRegistrantItem.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordRegistrantItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordRegistrantItem : ANpdsXsgTypedLabelItem
{
  public NpdsRecordRegistrantItem() : base() { this.Initialize(); }
  public NpdsRecordRegistrantItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordRegistrantItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.RegistrantItemXnam, PdpAppConst.RegistrantListXnam); }

  public string Registrant
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

