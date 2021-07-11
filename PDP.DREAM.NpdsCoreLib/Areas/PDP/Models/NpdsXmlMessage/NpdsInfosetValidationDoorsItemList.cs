// NpdsInfosetValidationDoorsItemList.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsInfosetValidationDoorsItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsInfosetValidationDoorsItem : ANpdsXsgValidationItem
  {
    public NpdsInfosetValidationDoorsItem() : base() { this.Initialize(); }
    public NpdsInfosetValidationDoorsItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsInfosetValidationDoorsItem(NpdsConst.FieldRule rul, string status) : base(status) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.DoorsValidationItemXnam, NpdsConst.DoorsValidationListXnam); }
  }

  [KnownType(typeof(NpdsInfosetValidationDoorsList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsInfosetValidationDoorsList : ANpdsXsgItemList<NpdsInfosetValidationDoorsItem>
  {
    public NpdsInfosetValidationDoorsList() : base() { }
    public NpdsInfosetValidationDoorsList(NpdsConst.FieldRule rul) : base(rul) { }
  }
}