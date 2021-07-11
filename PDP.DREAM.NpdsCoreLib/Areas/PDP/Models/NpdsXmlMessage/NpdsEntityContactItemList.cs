// NpdsEntityContactItemList.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsEntityContactItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityContactItem : ANpdsXsgTypedLabelItem
  {
    public NpdsEntityContactItem() : base() { this.Initialize(); }
    public NpdsEntityContactItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntityContactItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.ContactItemXnam, NpdsConst.ContactListXnam); }

    public string Contact
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

  [KnownType(typeof(NpdsEntityContactList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityContactList : ANpdsXsgItemList<NpdsEntityContactItem>
  {
    public NpdsEntityContactList() : base() { }
    public NpdsEntityContactList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}
