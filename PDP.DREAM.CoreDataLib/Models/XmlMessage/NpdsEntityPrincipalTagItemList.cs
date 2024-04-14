// NpdsEntityPrincipalTagItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityPrincipalTagItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityPrincipalTagItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityPrincipalTagItem() : base() { this.Initialize(); }
  public NpdsEntityPrincipalTagItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityPrincipalTagItem(NpdsFieldRule rul, string val) : base(NpdsParsers.ParsePrincipalTag(val)) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, PrincipalTagItemXnam, PrincipalTagListXnam); }

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
  public NpdsEntityPrincipalTagList(NpdsFieldRule rul) : base(rul) { }
}
