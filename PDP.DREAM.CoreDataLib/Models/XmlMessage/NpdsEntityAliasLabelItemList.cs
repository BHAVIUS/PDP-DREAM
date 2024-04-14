// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityAliasLabelItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityAliasLabelItem : ANpdsXsgEntityLabelItem
{
  public NpdsEntityAliasLabelItem() : base()
  { this.Initialize(); }
  public NpdsEntityAliasLabelItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsEntityAliasLabelItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, AliasLabelItemXnam, AliasLabelListXnam); }

  public string? AliasLabel
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }

} // end class

[KnownType(typeof(NpdsEntityAliasLabelList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityAliasLabelList : ANpdsXsgItemList<NpdsEntityAliasLabelItem>
{
  public NpdsEntityAliasLabelList() : base() { }
  public NpdsEntityAliasLabelList(NpdsFieldRule rul) : base(rul) { }
} // end class

// end file