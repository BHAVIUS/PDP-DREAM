// INpdsXsgItemList.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public interface INpdsXsgListItem
  {
    NpdsConst.FieldRule ItemRule { get; }
    string ItemXnam { get; }
    string ItemListXnam { get; }
  }

}