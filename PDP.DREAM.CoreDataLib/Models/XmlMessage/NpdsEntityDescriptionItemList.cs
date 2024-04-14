// NpdsEntityDescriptionItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityDescriptionItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityDescriptionItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityDescriptionItem() : base() { this.Initialize(); }
  public NpdsEntityDescriptionItem(NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityDescriptionItem(NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default(NpdsFieldRule))
  { base.InitNpdsItem(rul, DescriptionItemXnam, DescriptionListXnam); }

  public string Description
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

}

[KnownType(typeof(NpdsEntityDescriptionList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityDescriptionList : ANpdsXsgItemList<NpdsEntityDescriptionItem>
{
  public NpdsEntityDescriptionList() : base() { }
  public NpdsEntityDescriptionList(NpdsFieldRule rul) : base(rul) { }
}

