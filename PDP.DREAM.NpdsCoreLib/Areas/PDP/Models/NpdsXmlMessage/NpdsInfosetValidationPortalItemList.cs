// NpdsInfosetValidationPortalItemList.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsInfosetValidationPortalItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsInfosetValidationPortalItem : ANpdsXsgValidationItem
  {
    public NpdsInfosetValidationPortalItem() : base() { this.Initialize(); }
    public NpdsInfosetValidationPortalItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsInfosetValidationPortalItem(NpdsConst.FieldRule rul, string status) : base(status) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.PortalValidationItemXnam, NpdsConst.PortalValidationListXnam); }
  }

  [KnownType(typeof(NpdsInfosetValidationPortalList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsInfosetValidationPortalList : ANpdsXsgItemList<NpdsInfosetValidationPortalItem>
  {
    public NpdsInfosetValidationPortalList() : base() { }
    public NpdsInfosetValidationPortalList(NpdsConst.FieldRule rul) : base(rul) { }
  }
}