// MvcErrorUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class DotnetErrorModel
{
  public string RequestId { get; set; } = string.Empty;
  public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
