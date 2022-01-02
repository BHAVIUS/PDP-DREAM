// EntityLabelEditModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models
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
