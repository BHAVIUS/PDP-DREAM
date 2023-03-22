// NpdsEntityPrincipalTagItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityPrincipalTagItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityPrincipalTagItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityPrincipalTagItem() : base() { this.Initialize(); }
  public NpdsEntityPrincipalTagItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityPrincipalTagItem(PdpAppConst.NpdsFieldRule rul, string val) : base(NpdsParsers.ParsePrincipalTag(val)) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.PrincipalTagItemXnam, PdpAppConst.PrincipalTagListXnam); }

  public string PrincipalTag
  {
    get { return ItemValue; }
    set { ItemValue = NpdsParsers.ParsePrincipalTag(value); }
  }
}

[KnownType(typeof(NpdsEntityPrincipalTagList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityPrincipalTagList : ANpdsXsgItemList<NpdsEntityPrincipalTagItem>
{
  public NpdsEntityPrincipalTagList() : base() { }
  public NpdsEntityPrincipalTagList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}
