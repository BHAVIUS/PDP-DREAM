// NpdsRecordCrossReferenceItemList.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordCrossReferenceItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordCrossReferenceItem : ANpdsXsgBaseItem<Uri>
  {
    public NpdsRecordCrossReferenceItem() : base() { this.Initialize(); }
    public NpdsRecordCrossReferenceItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordCrossReferenceItem(NpdsConst.FieldRule rul, Uri val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default)
    { base.InitNpdsItem(rul, NpdsConst.CrossReferenceItemXnam, NpdsConst.CrossReferenceListXnam); }

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
    public NpdsRecordCrossReferenceList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}