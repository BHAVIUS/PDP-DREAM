// NpdsEntityOtherEntityItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityOtherEntityItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityOtherEntityItem : ANpdsXsgTypedLabelItem
{
  public NpdsEntityOtherEntityItem() : base() { this.Initialize(); }
  public NpdsEntityOtherEntityItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityOtherEntityItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.OtherEntityItemXnam, PdpAppConst.OtherEntityListXnam); }

  public string OtherEntity
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

[KnownType(typeof(NpdsEntityOtherEntityList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityOtherEntityList : ANpdsXsgItemList<NpdsEntityOtherEntityItem>
{
  public NpdsEntityOtherEntityList() : base() { }
  public NpdsEntityOtherEntityList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

