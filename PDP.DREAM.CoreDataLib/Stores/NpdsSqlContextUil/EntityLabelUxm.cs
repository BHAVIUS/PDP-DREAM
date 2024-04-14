// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class EntityLabelUxm : ResrepRootModelBase
{
  public EntityLabelUxm()
  {
    itemXnam = EntityLabelItemXnam;
  }

  public bool IsResolvable { get; set; }
  public bool IsGenerating { get; set; }
  public byte ServiceTypeCode { get; set; }

  // ATTN: move RegEx processing to data layer until rebuilt in Blazor
  // [RegularExpression(RegexPrincipalTag, ErrorMessage = "String not a valid TagToken.")]

  // SqlDataType is nvarchar(64), non-nullable
  [StringLength(64, MinimumLength = 4, ErrorMessage = "String must be 4 - 64 characters.")]
  public string? TagToken { get; set; } = ESS;

  // ATTN: move RegEx processing to data layer until rebuilt in Blazor
  // ATTN: requires fix for KendoUI regex problems caused by # in MVC grid client templates
  // [RegularExpression(NpdsConst.RegexLabelUri, ErrorMessage = "String not a valid independent LabelUri.")]

  // SqlDataType is nvarchar(128), non-nullable
  [StringLength(128, ErrorMessage = "String must be <= 128 characters.")]
  public string? LabelUri { get; set; } = ESS;


  // not input by user, should be ReadOnly in UIL
  public string? EntityLabel { get; set; } = ESS;

  private string ssElabHtml = ESS;
  public string EntityLabelHtml
  {
    get {
      if (string.IsNullOrEmpty(EntityLabel)) { ssElabHtml = ESS; }
      else
      {
        ssElabHtml =
          ((IsResolvable) ? $"<a href='{EntityLabel256}' rel='external'>{EntityLabel128}</a>" : EntityLabel128)
          .StringEscapeHashLiteral();
      }
      return ssElabHtml;
    }
  }

  private string ssElab128 = ESS;
  public string EntityLabel128
  {
    get {
      if (string.IsNullOrEmpty(EntityLabel)) { ssElab128 = ESS; }
      else { ssElab128 = EntityLabel.TruncateForTkgr(128).StringEscapeHashLiteral(); }
      return ssElab128;
    }
  }
  private string ssElab256;
  public string EntityLabel256
  {
    get {
      if (string.IsNullOrEmpty(EntityLabel)) { ssElab256 = ESS; }
      else { ssElab256 = EntityLabel.TruncateForTkgr(256).StringEscapeHashLiteral(); }
      return ssElab256;
    }
  }

} // end class

// end file
