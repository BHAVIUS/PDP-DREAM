// NpdsEntityNameItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityNameItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityNameItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityNameItem() : base() { this.Initialize(); }
  public NpdsEntityNameItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityNameItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.NameItemXnam, PdpAppConst.NameListXnam); }

  public string Name
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

[KnownType(typeof(NpdsEntityNameList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityNameList : ANpdsXsgItemList<NpdsEntityNameItem>
{
  public NpdsEntityNameList() : base() { }
  public NpdsEntityNameList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}
