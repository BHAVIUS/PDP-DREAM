// CrossReferenceEditModel.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.ScribeDataLib.Models;

public class CrossReferenceEditModel : NexusEditModelBase
{
  public CrossReferenceEditModel()
  {
    itemXnam = NpdsConst.CrossReferenceItemXnam;
  }

  public string? CrossReference { get; set; }

}
