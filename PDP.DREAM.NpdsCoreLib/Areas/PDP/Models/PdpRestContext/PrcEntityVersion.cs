// PrcEntityVersion.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System.Text.RegularExpressions;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // default values

    // requested values

    public string EntityVersionReqst
    {
      set
      {
        reqEntityVersion = value;
        // TODO: consider alternative regex check on version such as alphanumeric only or with . and -
        if (!string.IsNullOrEmpty(reqEntityVersion) && Regex.IsMatch(reqEntityVersion, NpdsConst.RegexSafeGeneric))
        {
          entityVersion = reqEntityVersion;
        }
        else
        {
          entityVersion = string.Empty;
        }
      }
      get { return reqEntityVersion; }
    }
    private string reqEntityVersion = string.Empty;

    // validated values

    public string EntityVersion
    {
      set { entityVersion = value; }
      get { return entityVersion; }
    }
    private string entityVersion = string.Empty;

  }

}
