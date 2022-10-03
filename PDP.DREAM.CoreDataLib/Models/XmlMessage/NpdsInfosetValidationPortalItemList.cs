// NpdsInfosetValidationPortalItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsInfosetValidationPortalItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetValidationPortalItem : ANpdsXsgValidationItem
{
  public NpdsInfosetValidationPortalItem() : base() { this.Initialize(); }
  public NpdsInfosetValidationPortalItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsInfosetValidationPortalItem(PdpAppConst.NpdsFieldRule rul, string status) : base(status) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.PortalValidationItemXnam, PdpAppConst.PortalValidationListXnam); }
}

[KnownType(typeof(NpdsInfosetValidationPortalList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetValidationPortalList : ANpdsXsgItemList<NpdsInfosetValidationPortalItem>
{
  public NpdsInfosetValidationPortalList() : base() { }
  public NpdsInfosetValidationPortalList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}
