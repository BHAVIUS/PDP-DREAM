// NpdsInfosetValidationDoorsItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsInfosetValidationDoorsItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetValidationDoorsItem : ANpdsXsgValidationItem
{
  public NpdsInfosetValidationDoorsItem() : base() { this.Initialize(); }
  public NpdsInfosetValidationDoorsItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsInfosetValidationDoorsItem(PdpAppConst.NpdsFieldRule rul, string status) : base(status) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.DoorsValidationItemXnam, PdpAppConst.DoorsValidationListXnam); }
}

[KnownType(typeof(NpdsInfosetValidationDoorsList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetValidationDoorsList : ANpdsXsgItemList<NpdsInfosetValidationDoorsItem>
{
  public NpdsInfosetValidationDoorsList() : base() { }
  public NpdsInfosetValidationDoorsList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}
