// NpdsRecordOtherTextItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordOtherTextItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordOtherTextItem : ANpdsXsgBaseItem<string>
{
  public NpdsRecordOtherTextItem() : base() { this.Initialize(); }
  public NpdsRecordOtherTextItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordOtherTextItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.OtherTextItemXnam, PdpAppConst.OtherTextListXnam); }

  public string OtherText
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

}

[KnownType(typeof(NpdsRecordOtherTextList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordOtherTextList : ANpdsXsgItemList<NpdsRecordOtherTextItem>
{
  public NpdsRecordOtherTextList() : base() { }
  public NpdsRecordOtherTextList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

