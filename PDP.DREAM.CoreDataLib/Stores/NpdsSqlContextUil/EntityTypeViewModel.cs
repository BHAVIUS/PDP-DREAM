﻿// EntityTypeViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class EntityTypeViewModel
{
  public short CodeKey { get; set; } = 0;
  public string? TypeName { get; set; } = string.Empty;
  public string? TypeDescription { get; set; } = string.Empty;
  public bool TypeIsComponent { get; set; } = false;
  public bool TypeIsConstituent { get; set; } = false;
  public bool TypeEditedByAgent { get; set; } = false;
  public bool TypeEditedByAuthor { get; set; } = false;
  public bool TypeEditedByEditor { get; set; } = false;
  public bool TypeEditedByAdmin { get; set; } = false;
}

