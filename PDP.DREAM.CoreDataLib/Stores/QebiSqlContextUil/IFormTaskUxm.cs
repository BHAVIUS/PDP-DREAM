// IFormTaskUxm.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public interface IFormTaskUxm
{
  string? FormTitle { get; set; }
  string? FormMessage { get; set; }
  bool FormCompleted { get; set; }
  bool ErrorOccurred { get; set; }
  Exception? Error { get; set; }
}