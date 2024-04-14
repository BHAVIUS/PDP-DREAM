// NpdsInfosetValidationPortalItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsInfosetValidationPortalItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetValidationPortalItem : ANpdsXsgValidationItem
{
  public NpdsInfosetValidationPortalItem() : base() { this.Initialize(); }
  public NpdsInfosetValidationPortalItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsInfosetValidationPortalItem(NpdsFieldRule rul, string status) : base(status) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, PortalValidationItemXnam, PortalValidationListXnam); }
}

[KnownType(typeof(NpdsInfosetValidationPortalList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsInfosetValidationPortalList : ANpdsXsgItemList<NpdsInfosetValidationPortalItem>
{
  public NpdsInfosetValidationPortalList() : base() { }
  public NpdsInfosetValidationPortalList(NpdsFieldRule rul) : base(rul) { }
}
