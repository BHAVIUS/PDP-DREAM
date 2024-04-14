// NpdsInfosetValidationDoorsItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsInfosetValidationDoorsItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetValidationDoorsItem : ANpdsXsgValidationItem
{
  public NpdsInfosetValidationDoorsItem() : base() { this.Initialize(); }
  public NpdsInfosetValidationDoorsItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsInfosetValidationDoorsItem(NpdsFieldRule rul, string status) : base(status) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, DoorsValidationItemXnam, DoorsValidationListXnam); }
}

[KnownType(typeof(NpdsInfosetValidationDoorsList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetValidationDoorsList : ANpdsXsgItemList<NpdsInfosetValidationDoorsItem>
{
  public NpdsInfosetValidationDoorsList() : base() { }
  public NpdsInfosetValidationDoorsList(NpdsFieldRule rul) : base(rul) { }
}
