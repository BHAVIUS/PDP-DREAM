// EntityLabelViewModel.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Models;

public class EntityLabelViewModel : CoreResrepModelBase
{
  public EntityLabelViewModel()
  {
    itemXnam = PdpAppConst.EntityLabelItemXnam;
  }

  public bool IsResolvable { get; set; }
  public bool IsGenerating { get; set; }
  public short ServiceTypeCode { get; set; }

  public virtual string? TagToken { get; set; } = string.Empty;
  public virtual string? LabelUri { get; set; } = string.Empty;

  // not input by user, should be ReadOnly in UIL
  public string? EntityLabel { get; set; } = string.Empty;

  private string ssElabHtml = string.Empty;
  public string EntityLabelHtml
  {
    get {
      if (string.IsNullOrEmpty(EntityLabel)) { ssElabHtml = string.Empty; }
      else
      {
        ssElabHtml =
          ((IsResolvable) ? $"<a href='{EntityLabel256}' rel='external'>{EntityLabel128}</a>" : EntityLabel128)
          .StringEscapeHashLiteral();
      }
      return ssElabHtml;
    }
  }

  private string ssElab128 = string.Empty;
  public string EntityLabel128
  {
    get {
      if (string.IsNullOrEmpty(EntityLabel)) { ssElab128 = string.Empty; }
      else { ssElab128 = EntityLabel.ToHoverHideHtml(128).StringEscapeHashLiteral(); }
      return ssElab128;
    }
  }
  private string ssElab256;
  public string EntityLabel256
  {
    get {
      if (string.IsNullOrEmpty(EntityLabel)) { ssElab256 = string.Empty; }
      else { ssElab256 = EntityLabel.ToHoverHideHtml(256).StringEscapeHashLiteral(); }
      return ssElab256;
    }
  }

} // end class

// end file
