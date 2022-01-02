// PdpBiDictionary.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Collections.Generic;

namespace PDP.DREAM.CoreDataLib.Types;

public class PdpBiDictionary<TLeft, TRight>
  where TLeft : notnull
  where TRight : notnull
{
  IDictionary<TLeft, TRight> leftToRight;
  IDictionary<TRight, TLeft> rightToLeft;

  public PdpBiDictionary()
  {
    if ((typeof(TLeft) == typeof(string)) && (typeof(TRight) == typeof(string)))
    {
      leftToRight = (IDictionary<TLeft, TRight>)new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
      rightToLeft = (IDictionary<TRight, TLeft>)new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }
    else if (typeof(TLeft) == typeof(string))
    {
      leftToRight = (IDictionary<TLeft, TRight>)new Dictionary<string, TRight>(StringComparer.OrdinalIgnoreCase);
      rightToLeft = (IDictionary<TRight, TLeft>)new Dictionary<TRight, string>();
    }
    else if (typeof(TRight) == typeof(string))
    {
      leftToRight = (IDictionary<TLeft, TRight>)new Dictionary<TLeft, string>();
      rightToLeft = (IDictionary<TRight, TLeft>)new Dictionary<string, TLeft>(StringComparer.OrdinalIgnoreCase);
    }
    else
    {
      leftToRight = new Dictionary<TLeft, TRight>();
      rightToLeft = new Dictionary<TRight, TLeft>();
    }
  }

  public void Add(TLeft left, TRight right)
  {
    if (leftToRight.ContainsKey(left))
    { throw new ArgumentException("Duplicate left in PdpBiDictionary"); }
    else if (rightToLeft.ContainsKey(right))
    { throw new ArgumentException("Duplicate right in PdpBiDictionary"); }

    leftToRight.Add(left, right);
    rightToLeft.Add(right, left);
  }

  public bool TryGetByLeft(TLeft left, out TRight right)
  { return leftToRight.TryGetValue(left, out right); }

  public bool TryGetByRight(TRight right, out TLeft left)
  { return rightToLeft.TryGetValue(right, out left); }

  public TRight GetByLeft(TLeft left)
  {
    if (left == null) return default(TRight);
    else return leftToRight[left];
  }

  public TLeft GetByRight(TRight right)
  {
    if (right == null) return default(TLeft);
    else return rightToLeft[right];
  }

} // class
