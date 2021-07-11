// EntityLabelEditModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
{
  public class EntityLabelEditModel : NexusEditModelBase
  {
    public EntityLabelEditModel()
    {
      itemXnam = NpdsConst.EntityLabelItemXnam;
    }

    public bool IsResolvable { get; set; }
    public bool IsGenerating { get; set; }
    public byte ServiceTypeCode { get; set; }

    public string? TagToken { get; set; } = string.Empty;

    public string? LabelUri { get; set; } = string.Empty;

    // not input by user, should be ReadOnly in UIL
    public string? EntityLabel { get; set; } = string.Empty;
    public string? EntityLabelHtml
    {
      get
      {
        entLabHtml = ((IsResolvable) ? $"<a href='{EntityLabel}' target='_blank'>{EntityLabel}</a>" : EntityLabel);
        return entLabHtml;
      }
    }
    private string? entLabHtml = string.Empty;

  }

}
