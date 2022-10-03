// EntityLabelEditModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.ComponentModel.DataAnnotations;

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.NexusDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public class EntityLabelEditModel : EntityLabelViewModel
{
  public EntityLabelEditModel()
  {
    itemXnam = PdpAppConst.EntityLabelItemXnam;
  }

  public bool IsResolvable { get; set; }
  public bool IsGenerating { get; set; }
  public short ServiceTypeCode { get; set; }

  // SqlDataType is nvarchar(64), non-nullable
  [StringLength(64, MinimumLength = 4, ErrorMessage = "String must be 4 - 64 characters.")]
  [RegularExpression(PdpAppConst.RegexPrincipalTag, ErrorMessage = "String not a valid TagToken.")]
  public string? TagToken { get; set; } = string.Empty;

  // SqlDataType is nvarchar(128), non-nullable
  // [StringLength(128, ErrorMessage = "String must be <= 128 characters.")]
  // ATTN: requires fix for KendoUI regex problems caused by # in MVC grid client templates
  // [RegularExpression(NpdsConst.RegexLabelUri, ErrorMessage = "String not a valid independent LabelUri.")]
  public string? LabelUri { get; set; } = string.Empty;

  // not input by user, should be ReadOnly in UIL
  public string? EntityLabel { get; set; } = string.Empty;
  public string? EntityLabelHtml
  {
    get
    {
      return ((IsResolvable) ? $"<a href='{EntityLabel}' target='_blank'>{EntityLabel}</a>" : EntityLabel);
    }
  }
	
} // end class

// end file