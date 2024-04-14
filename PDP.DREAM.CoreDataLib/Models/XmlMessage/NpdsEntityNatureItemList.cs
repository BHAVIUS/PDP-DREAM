// NpdsEntityNatureItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityNatureItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityNatureItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityNatureItem() : base() { this.Initialize(); }
  public NpdsEntityNatureItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityNatureItem(NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, NatureItemXnam, NatureListXnam); }

  public string Nature
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

[KnownType(typeof(NpdsEntityNatureList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityNatureList : ANpdsXsgItemList<NpdsEntityNatureItem>
{
  public NpdsEntityNatureList() : base() { }
  public NpdsEntityNatureList(NpdsFieldRule rul) : base(rul) { }
}

