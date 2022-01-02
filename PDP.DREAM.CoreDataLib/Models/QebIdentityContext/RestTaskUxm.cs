// RestTaskUxm.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;

using PDP.DREAM.CoreDataLib.Models;

namespace PDP.DREAM.CoreDataLib.Models
{
  public class RestTaskUxm
  {
    public bool TaskCompleted { get; set; }

    public bool ErrorOccurred { get; set; }

    public string? Message { get; set; } = string.Empty;

    public Exception? Error { get; set; } = null;

    // TODO: eliminate this method and its overrides ???
    public virtual void UpdateRestContext(PdpRestContext prc) { }

  }

}
