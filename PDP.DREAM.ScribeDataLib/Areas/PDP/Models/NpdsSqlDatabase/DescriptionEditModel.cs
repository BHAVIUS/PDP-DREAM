using System;
using System.Text;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase
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
