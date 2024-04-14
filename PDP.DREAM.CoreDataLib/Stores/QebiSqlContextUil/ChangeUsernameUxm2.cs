// ChangeUsernameUxm2.cs 
// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class ChangeUsernameUxm2 : ConfirmTokenUxm
{
  // required parameterless constructor
  public ChangeUsernameUxm2() { }
  public ChangeUsernameUxm2(string password) { PassWord = password; }

} // end class

// end file