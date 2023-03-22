// ChangeUsernameUxm2.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ChangeUsernameUxm2 : ConfirmTokenUxm, IFormTaskUxm
{
  // required parameterless constructor
  public ChangeUsernameUxm2() { }
  public ChangeUsernameUxm2(string password) { PassWord = password; }

} // end class

// end file