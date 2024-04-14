// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // TODO: currently QueryFormat is a bool rather than an enumerator
  //  variable name QueryFormat is OK if plan to enhance to enumerator
  //  but if remains just bool true/false then change variable name
  //  currently used only to distinguish exact case-sensitive match from partial case-insensitive match on query
  // alternative name might just be QueryType

  // requested values

  //private bool reqQuery = false;
  //public bool QueryFormatReqst
  //{
  //  set { reqQuery = value; }
  //  get { return reqQuery; }
  //}

  // validated values

  private bool queryFormat = false;
  public bool QueryFormat
  {
    get { return queryFormat; }
    set { queryFormat = value; }
  }


} // end class

// end file