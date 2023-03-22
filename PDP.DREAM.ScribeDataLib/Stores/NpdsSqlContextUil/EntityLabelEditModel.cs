// EntityLabelEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.ScribeDataLib.Models;

public class EntityLabelEditModel : EntityLabelViewModel
{
  public EntityLabelEditModel()
  {
    itemXnam = PdpAppConst.EntityLabelItemXnam;
  }


  // SqlDataType is nvarchar(64), non-nullable
  [StringLength(64, MinimumLength = 4, ErrorMessage = "String must be 4 - 64 characters.")]
  [RegularExpression(PdpAppConst.RegexPrincipalTag, ErrorMessage = "String not a valid TagToken.")]
  public override string? TagToken { get; set; } = string.Empty;

  // SqlDataType is nvarchar(128), non-nullable
  // [StringLength(128, ErrorMessage = "String must be <= 128 characters.")]
  // ATTN: requires fix for KendoUI regex problems caused by # in MVC grid client templates
  // [RegularExpression(NpdsConst.RegexLabelUri, ErrorMessage = "String not a valid independent LabelUri.")]
  public override string? LabelUri { get; set; } = string.Empty;

	
} // end class

// end file