// NpdsEntityFairMetricItemList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models;

[KnownType(typeof(NpdsEntityFairMetricItem)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityFairMetricItem : ANpdsXsgBaseItem<string>
{
  public NpdsEntityFairMetricItem() : base() { this.Initialize(); }
  public NpdsEntityFairMetricItem(PdpAppConst.NpdsFieldRule rul) : base() { this.Initialize(rul); }
  public NpdsEntityFairMetricItem(PdpAppConst.NpdsFieldRule rul, string val) : base(val) { this.Initialize(rul); }

  private void Initialize(PdpAppConst.NpdsFieldRule rul = default(PdpAppConst.NpdsFieldRule))
  { base.InitNpdsItem(rul, PdpAppConst.FairMetricItemXnam, PdpAppConst.FairMetricListXnam); }

  public string FairMetric
  {
    get { return ItemValue; }
    set { ItemValue = value; }
  }

}

[KnownType(typeof(NpdsEntityFairMetricList)), XmlSchemaProvider(null, IsAny = true)]
public class NpdsEntityFairMetricList : ANpdsXsgItemList<NpdsEntityFairMetricItem>
{
  public NpdsEntityFairMetricList() : base() { }
  public NpdsEntityFairMetricList(PdpAppConst.NpdsFieldRule rul) : base(rul) { }
}
