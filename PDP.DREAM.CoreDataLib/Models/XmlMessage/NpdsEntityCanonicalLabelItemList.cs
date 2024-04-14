// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityCanonicalLabelItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityCanonicalLabelItem : ANpdsXsgEntityLabelItem
{
  public NpdsEntityCanonicalLabelItem() : base()
  { this.Initialize(); }
  public NpdsEntityCanonicalLabelItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsEntityCanonicalLabelItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, CanonicalLabelItemXnam, CanonicalLabelListXnam); }

  public string? CanonicalLabel
  {
    get { return ItemValue.AbsoluteUri; }
    set { ParseEntityLabel(value); }
  }

} // end class

[KnownType(typeof(NpdsEntityCanonicalLabelList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityCanonicalLabelList : ANpdsXsgItemList<NpdsEntityCanonicalLabelItem>
{
  public NpdsEntityCanonicalLabelList() : base() { }
  public NpdsEntityCanonicalLabelList(NpdsFieldRule rul) : base(rul) { }
} // end class

// end file
