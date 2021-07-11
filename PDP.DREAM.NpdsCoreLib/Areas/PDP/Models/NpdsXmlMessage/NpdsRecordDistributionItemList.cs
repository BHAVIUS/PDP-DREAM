﻿// NpdsRecordDistributionItemList.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsRecordDistributionItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordDistributionItem : ANpdsXsgBaseItem<string>
  {
    public NpdsRecordDistributionItem() : base() { this.Initialize(); }
    public NpdsRecordDistributionItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsRecordDistributionItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.DistributionItemXnam, NpdsConst.DistributionListXnam); }

    public string Distribution
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }

  }

  [KnownType(typeof(NpdsRecordDistributionList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsRecordDistributionList : ANpdsXsgItemList<NpdsRecordDistributionItem>
  {
    public NpdsRecordDistributionList() : base() { }
    public NpdsRecordDistributionList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}
