// NpdsRecordCrossReferenceItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsRecordCrossReferenceItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordCrossReferenceItem : ANpdsXsgBaseItem<Uri>
{
  public NpdsRecordCrossReferenceItem() : base() { this.Initialize(); }
  public NpdsRecordCrossReferenceItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsRecordCrossReferenceItem(PdpAppConst.NpdsFieldRule rul, Uri val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default)
  { base.InitNpdsItem(rul, PdpAppConst.CrossReferenceItemXnam, PdpAppConst.CrossReferenceListXnam); }

  public Uri CrossReference
  {
    get { return ItemValue; }
    set { if (value != null) { ItemValue = value; } }
  }

  public void ParseCrossReference(string value)
  {
    ItemValue = value.ToUri();
  }
}

[KnownType(typeof(NpdsRecordCrossReferenceList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsRecordCrossReferenceList : ANpdsXsgItemList<NpdsRecordCrossReferenceItem>
{
  public NpdsRecordCrossReferenceList() : base() { }
  public NpdsRecordCrossReferenceList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}

