// NpdsInfosetValidationPortalItemList.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
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