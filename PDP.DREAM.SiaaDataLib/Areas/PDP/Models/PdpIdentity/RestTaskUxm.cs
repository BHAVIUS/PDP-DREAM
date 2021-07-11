// RestTaskUxm.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using System;

using PDP.DREAM.NpdsCoreLib.Models;

namespace PDP.DREAM.SiaaDataLib.Models.PdpIdentity
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
