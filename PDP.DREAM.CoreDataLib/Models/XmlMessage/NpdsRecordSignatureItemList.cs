// NpdsRecordSignatureItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordSignatureItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordSignatureItem : ANpdsXsgBaseItem<XElement>
{
  public NpdsRecordSignatureItem() : base() { this.Initialize(); }
  public NpdsRecordSignatureItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordSignatureItem(PdpAppConst.NpdsFieldRule rul, XElement val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.SignatureItemXnam, PdpAppConst.SignatureListXnam); }

  public XElement Signature
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }
}

[KnownType(typeof(NpdsRecordSignatureList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordSignatureList : ANpdsXsgItemList<NpdsRecordSignatureItem>
{
  public NpdsRecordSignatureList() : base() { }
  public NpdsRecordSignatureList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}
