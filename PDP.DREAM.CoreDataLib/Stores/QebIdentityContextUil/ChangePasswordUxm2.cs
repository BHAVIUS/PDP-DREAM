// ChangePasswordUxm2.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ChangePasswordUxm2 : ConfirmTokenUxm, IFormTaskUxm
{
  // required parameterless constructor
  public ChangePasswordUxm2() { }
  public ChangePasswordUxm2(string username) { UserName = username; }

} // end class

// end file