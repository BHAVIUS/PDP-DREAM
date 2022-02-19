// OtherTextEditModel.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models
{
  public class OtherTextEditModel : NexusEditModelBase
  {
    public OtherTextEditModel()
    {
      itemXnam = NpdsConst.OtherTextItemXnam;
    }

    public string? OtherText { get; set; }

  }

}
