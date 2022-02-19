// PdpCiDictionary.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;

namespace PDP.DREAM.CoreDataLib.Types;

// PDP Case Insensitive Dictionary
public class PdpCiDictionary : Dictionary<string, string>
{
  public PdpCiDictionary() : base(StringComparer.OrdinalIgnoreCase)  
  {
    KeyComparer = StringComparer.OrdinalIgnoreCase;
  }
  public PdpCiDictionary(StringComparer sc) : base(sc) 
  { 
    KeyComparer = sc;
  }
  public StringComparer KeyComparer { get; }
}
