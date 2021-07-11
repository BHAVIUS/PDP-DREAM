﻿// NpdsRecordProvenanceItemList.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordProvenanceItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordProvenanceItem : ANpdsXsgBaseItem<string>
  {
    public NpdsRecordProvenanceItem() : base() { this.Initialize(); }
    public NpdsRecordProvenanceItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordProvenanceItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.ProvenanceItemXnam, NpdsConst.ProvenanceListXnam); }

    public string Provenance
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }

  }

  [KnownType(typeof(NpdsRecordProvenanceList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordProvenanceList : ANpdsXsgItemList<NpdsRecordProvenanceItem>
  {
    public NpdsRecordProvenanceList() : base() { }
    public NpdsRecordProvenanceList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}
