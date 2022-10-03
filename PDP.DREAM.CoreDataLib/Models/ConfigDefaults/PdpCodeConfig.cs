// PdpCodeConfig.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

using static PDP.DREAM.CoreDataLib.Models.PdpAppConst;

namespace PDP.DREAM.CoreDataLib.Models;

[AttributeUsage(AttributeTargets.Assembly)]
public class PdpCodeDateAttribute : Attribute
{
  public PdpCodeDateAttribute(string value)
  {
    DateTime = DateTime.ParseExact(value, "yyyyMMddHHmmss", CultureInfo.InvariantCulture, DateTimeStyles.None);
  }

  public DateTime DateTime { get; }
}

public class PdpCodeConfig
{
  public PdpCodeConfig(Type progtype, string webroot = "",
    AcgtCodeBranch codebranch = AcgtBranchDefault, AcgtCodeRazor coderazor = AcgtRazorDefault)
  {
    if (string.IsNullOrEmpty(webroot)) { PdpCodeWebroot = PdpSiteDefaultWebroot; }
    else { PdpCodeWebroot = webroot; }
    PdpCodeBranch = codebranch;
    PdpCodeRazor = coderazor;
    PdpCodeProjdir = Directory.GetCurrentDirectory();

    // info from the program assembly
    var progAsmbly = Assembly.GetAssembly(progtype);
    var progName = progAsmbly.GetName();
    PdpCodeAsmdat = GetPdpCodeDate(progAsmbly);
    // PdpCodeAppnam = progName.FullName; // includes description
    PdpCodeAppnam = progName.Name;
    // PdpCodeAsmver = progName.Version.ToString(3);
    PdpCodeAsmver = progName.Version.ToString();
    PdpCodeAppver = FileVersionInfo.GetVersionInfo(progAsmbly.Location).FileVersion;
    PdpCodeNamspc = progtype.Namespace;
    PdpCodeErrmsg = $"PDP-DREAM error in {PdpCodeNamspc} for {PdpCodeBranch} {PdpCodeAppnam} {PdpCodeAppver}";
  }
  public static DateTime GetPdpCodeDate(Assembly assembly)
  {
    var attribute = assembly.GetCustomAttribute<PdpCodeDateAttribute>();
    return attribute != null ? attribute.DateTime : default(DateTime);
  }

  public AcgtCodeBranch PdpCodeBranch { get; init; }
  public AcgtCodeRazor PdpCodeRazor { get; init; }
  public DateTime PdpCodeAsmdat { get; init; }
  public string PdpCodeAppnam { get; init; }
  public string PdpCodeAppver { get; init; }
  public string PdpCodeAsmver { get; init; }
  public string PdpCodeErrmsg { get; init; }
  public string PdpCodeNamspc { get; init; }
  public string PdpCodeProjdir { get; init; }
  public string PdpCodeWebroot { get; init; }

} // end class

// end file