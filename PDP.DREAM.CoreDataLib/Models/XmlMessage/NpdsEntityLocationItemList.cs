// NpdsEntityLocationItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityLocationItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityLocationItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityLocationItem() : base() { this.Initialize(); }
  public NpdsEntityLocationItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityLocationItem(NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, LocationItemXnam, LocationListXnam); }

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
  public NpdsEntityLocationList(NpdsFieldRule rul) : base(rul) { }

} // class

