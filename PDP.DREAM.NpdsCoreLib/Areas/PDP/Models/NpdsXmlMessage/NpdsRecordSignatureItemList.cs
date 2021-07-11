// NpdsRecordSignatureItemList.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordSignatureItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordSignatureItem : ANpdsXsgBaseItem<XElement>
  {
    public NpdsRecordSignatureItem() : base() { this.Initialize(); }
    public NpdsRecordSignatureItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordSignatureItem(NpdsConst.FieldRule rul, XElement val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.SignatureItemXnam, NpdsConst.SignatureListXnam); }

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
    public NpdsRecordSignatureList(NpdsConst.FieldRule rul) : base(rul) { }
  }
}