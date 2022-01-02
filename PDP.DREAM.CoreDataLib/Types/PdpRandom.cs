// PdpRandom.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Linq;

namespace PDP.DREAM.CoreDataLib.Types
{
  public static class PdpRandom
  {
    public static string RandGuidString(short numChars = 11)
    {
      var rndgen = new Random();
      var firstchar = (char)rndgen.Next('A', 'Z');
      var guidstr = Guid.NewGuid().ToString();
      guidstr = guidstr.Replace("-", "").ToUpper();
      var rndstr = (firstchar + guidstr.Substring(1, numChars - 1));
      return rndstr;
    }

    public static string RandString(string charList, short numChars)
    {
      var maxIndex = (short)(charList.Length - 1);
      var rndgen = new Random();
      var randChars = Enumerable.Repeat(charList, numChars).Select(s => s[rndgen.Next(0, maxIndex)]).ToArray();
      var rndstr = new string(randChars);
      return rndstr;
    }

    public static string RandLowerString(short numChars = 11)
    {
      var charList = "abcdefghijklmnopqrstuvwxyz";
      return RandString(charList, numChars);
    }

    public static string RandUpperString(short numChars = 11)
    {
      var charList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
      return RandString(charList, numChars);
    }

  } // class

} // namespace
