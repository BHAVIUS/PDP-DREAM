// PrcQueryFormat.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class QebUserRestContext
{
  // TODO: currently QueryFormat is a bool rather than an enum
  //  variable name QueryFormat is OK if plan to enhance to enum
  //  but if remains just bool true/false then change variable name
  //  currently used only to distinguish exact case-sensitive match from partial case-insensitive match on query
  // alternative name might just be QueryType

  // default values

  public bool QueryFormatDeflt
  { get { return defQuery; } }
  private bool defQuery = PdpAppStatus.NPDSSD.NpdsDefaultQueryFormat;

  // requested values

  public bool QueryFormatReqst
  {
    set { reqQuery = value; }
    get { return reqQuery; }
  }
  private bool reqQuery = false;

  // validated values

  public bool QueryFormat
  {
    get { return QueryFormatReqst; }
  }

}

