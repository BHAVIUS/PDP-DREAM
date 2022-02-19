// EntityLabelViewModel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.NexusDataLib.Models
{
  public class EntityLabelViewModel : NexusViewModelBase
  {
    public EntityLabelViewModel()
    {
      itemXnam = NpdsConst.EntityLabelItemXnam;
    }

    public bool IsResolvable { get; set; }
    public bool IsGenerating { get; set; }
    public byte ServiceTypeCode { get; set; }
    public string? TagToken { get; set; } = string.Empty;
    public string? LabelUri { get; set; } = string.Empty;
    public string? EntityLabel { get; set; } = string.Empty;

    public string? EntityLabelHtml
    {
      get
      {
        return ((IsResolvable) ? $"<a href='{EntityLabel}' target='_blank'>{EntityLabel}</a>" : EntityLabel);
      }
    }
	
  } // class

} // namespace