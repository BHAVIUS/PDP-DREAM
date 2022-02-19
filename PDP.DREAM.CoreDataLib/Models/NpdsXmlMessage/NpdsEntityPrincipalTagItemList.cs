// NpdsEntityPrincipalTagItemList.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.CoreDataLib.Models
{
  [KnownType(typeof(NpdsEntityPrincipalTagItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityPrincipalTagItem : ANpdsXsgBaseItem<string>
  {
    public NpdsEntityPrincipalTagItem() : base() { this.Initialize(); }
    public NpdsEntityPrincipalTagItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntityPrincipalTagItem(NpdsConst.FieldRule rul, string val) : base(NpdsParsers.ParsePrincipalTag(val)) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.PrincipalTagItemXnam, NpdsConst.PrincipalTagListXnam); }

    public string PrincipalTag
    {
      get { return ItemValue; }
      set { ItemValue = NpdsParsers.ParsePrincipalTag(value); }
    }
  }

  [KnownType(typeof(NpdsEntityPrincipalTagList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityPrincipalTagList : ANpdsXsgItemList<NpdsEntityPrincipalTagItem>
  {
    public NpdsEntityPrincipalTagList() : base() { }
    public NpdsEntityPrincipalTagList(NpdsConst.FieldRule rul) : base(rul) { }
  }
}