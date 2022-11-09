// NpdsEntityLocationItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityLocationItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityLocationItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityLocationItem() : base() { this.Initialize(); }
  public NpdsEntityLocationItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityLocationItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.LocationItemXnam, PdpAppConst.LocationListXnam); }

  public string Location
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

} // class

[KnownType(typeof(NpdsEntityLocationList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityLocationList : ANpdsXsgItemList<NpdsEntityLocationItem>
{
  public NpdsEntityLocationList() : base() { }
  public NpdsEntityLocationList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }

} // class

