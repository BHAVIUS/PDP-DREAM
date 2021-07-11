// ItemIdentityKeys.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  // intended for use with list items that are listed with integer priority indexed ordering
  [KnownType(typeof(ItemIdentityKeys)), XmlSchemaProvider(null, IsAny = true)]
  public class ItemIdentityKeys
  {
    public ItemIdentityKeys() { }

    public int? Priority { set; get; } // non-unique in sequence

    public int? Index { set; get; } // unique in sequence

    public Guid? GuidPrimaryKey { set; get; }

    public Guid? GuidForeignKey { set; get; }


  }

}