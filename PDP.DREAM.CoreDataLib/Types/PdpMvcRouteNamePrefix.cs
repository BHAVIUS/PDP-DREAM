// PdpMvcRouteNamePrefix.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

namespace PDP.DREAM.CoreDataLib.Types;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class PdpMvcNamePrefixAttribute : Attribute
{
  private string namePrefix;
  public PdpMvcNamePrefixAttribute(string np)
  {
    namePrefix = np;
  }
}
