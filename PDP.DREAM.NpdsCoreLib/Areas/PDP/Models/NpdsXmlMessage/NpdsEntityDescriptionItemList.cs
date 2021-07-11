// NpdsEntityDescriptionItemList.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsEntityDescriptionItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityDescriptionItem : ANpdsXsgBaseItem<string>
  {
    public NpdsEntityDescriptionItem() : base() { this.Initialize(); }
    public NpdsEntityDescriptionItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntityDescriptionItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.DescriptionItemXnam, NpdsConst.DescriptionListXnam); }

    public string Description
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }

  }

  [KnownType(typeof(NpdsEntityDescriptionList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityDescriptionList : ANpdsXsgItemList<NpdsEntityDescriptionItem>
  {
    public NpdsEntityDescriptionList() : base() { }
    public NpdsEntityDescriptionList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}