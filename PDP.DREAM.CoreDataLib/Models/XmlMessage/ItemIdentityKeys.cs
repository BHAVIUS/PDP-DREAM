// ItemIdentityKeys.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

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

