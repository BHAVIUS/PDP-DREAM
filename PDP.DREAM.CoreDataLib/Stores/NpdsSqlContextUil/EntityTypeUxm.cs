// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class EntityTypeUxm
{
  public short CodeKey { get; set; } = 0;
  public string? TypeName { get; set; } = ESS;
  public string? TypeDescription { get; set; } = ESS;
  public bool TypeIsComponent { get; set; } = false;
  public bool TypeIsConstituent { get; set; } = false;
  public bool TypeEditedByAgent { get; set; } = false;
  public bool TypeEditedByAuthor { get; set; } = false;
  public bool TypeEditedByEditor { get; set; } = false;
  public bool TypeEditedByAdmin { get; set; } = false;

} // end class

// end file