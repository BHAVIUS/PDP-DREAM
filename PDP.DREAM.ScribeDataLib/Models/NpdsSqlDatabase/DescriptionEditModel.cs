// DescriptionEditModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;
using PDP.DREAM.CoreDataLib.Types;

namespace PDP.DREAM.ScribeDataLib.Models
{
  public class DescriptionEditModel : NexusEditModelBase
  {
    public DescriptionEditModel()
    {
      itemXnam = NpdsConst.DescriptionItemXnam;
    }

    public string? Description { get; set; } = string.Empty;

    public string? Description128
    {
      get
      {
        if (string.IsNullOrEmpty(Description)) { desc128 = string.Empty; }
        else
        {
          desc128 = ((Description.Length > 128) ?
           Description.Substring(0, 128).ToColorSpan("partial") : Description);
        }
        return desc128;
      }
    }
    private string? desc128;

  }

}
