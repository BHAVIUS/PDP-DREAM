using System;

namespace PDP.DREAM.NpdsDataLib.Models
{
  public interface INexusSupportingTagEditModel : INexusEditModelBase
  {
    string SupportingTag { get; set; }
  }

}