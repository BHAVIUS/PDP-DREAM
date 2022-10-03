// NpdsEntityNatureItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityNatureItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityNatureItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityNatureItem() : base() { this.Initialize(); }
  public NpdsEntityNatureItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityNatureItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.NatureItemXnam, PdpAppConst.NatureListXnam); }

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
  public NpdsEntityNatureList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

