// EntityTypeViewModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class EntityTypeViewModel
  {
    public int CodeKey { get; set; } = 0;
    public string? TypeName { get; set; } = string.Empty;
    public string? TypeDescription { get; set; } = string.Empty;
    public bool TypeIsComponent { get; set; } = false;
    public bool TypeIsConstituent { get; set; } = false;
    public bool TypeEditedByAgent { get; set; } = false;
    public bool TypeEditedByAuthor { get; set; } = false;
    public bool TypeEditedByEditor { get; set; } = false;
    public bool TypeEditedByAdmin { get; set; } = false;
  }

}
