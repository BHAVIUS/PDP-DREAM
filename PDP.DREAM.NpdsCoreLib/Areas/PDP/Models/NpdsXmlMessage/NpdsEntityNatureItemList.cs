﻿using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  [KnownType(typeof(NpdsEntityNatureItem)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityNatureItem : ANpdsXsgBaseItem<string>
  {
    public NpdsEntityNatureItem() : base() { this.Initialize(); }
    public NpdsEntityNatureItem(NpdsConst.FieldRule rul) : base() { this.Initialize(rul); }
    public NpdsEntityNatureItem(NpdsConst.FieldRule rul, string val) : base(val) { this.Initialize(rul); }

    private void Initialize(NpdsConst.FieldRule rul = default(NpdsConst.FieldRule))
    { base.InitNpdsItem(rul, NpdsConst.NatureItemXnam, NpdsConst.NatureListXnam); }

    public string Nature
    {
      get { return ItemValue; }
      set { ItemValue = value; }
    }
  }

  [KnownType(typeof(NpdsEntityNatureList)), XmlSchemaProvider(null, IsAny = true)]
  public class NpdsEntityNatureList : ANpdsXsgItemList<NpdsEntityNatureItem>
  {
    public NpdsEntityNatureList() : base() { }
    public NpdsEntityNatureList(NpdsConst.FieldRule rul) : base(rul) { }
  }

}
