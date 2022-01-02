// INpdsXsgItemList.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models
{
  public interface INpdsXsgListItem
  {
    NpdsConst.FieldRule ItemRule { get; }
    string ItemXnam { get; }
    string ItemListXnam { get; }
  }

}