// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityLabelItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityLabelItem : ANpdsXsgEntityLabelItem
{
  public NpdsEntityLabelItem() : base()
  { this.Initialize(); }
  public NpdsEntityLabelItem(NpdsFieldRule rul) : base()
  { this.Initialize(rul); }
  public NpdsEntityLabelItem(NpdsFieldRule rul, Uri? val) : base(val)
  { this.Initialize(rul); }

  private void Initialize(NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, AliasLabelItemXnam, AliasLabelListXnam); }

} // end class

[KnownType(typeof(NpdsEntityLabelList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityLabelList : ANpdsXsgItemList<NpdsEntityLabelItem>
{
  public NpdsEntityLabelList() : base() { }
  public NpdsEntityLabelList(NpdsFieldRule rul) : base(rul) { }
} // end class

// end file