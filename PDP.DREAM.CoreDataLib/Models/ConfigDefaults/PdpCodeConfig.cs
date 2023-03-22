// PdpCodeConfig.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

[AttributeUsage(AttributeTargets.Assembly)]
public class PdpCodeDateAttribute : Attribute
{
  public PdpCodeDateAttribute(string value)
  {
    DateTime = DateTime.ParseExact(value, PdpDateTimeNowSortFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
  }

  public DateTime DateTime { get; }
}

public class PdpCodeConfig
{
  public PdpCodeConfig(Type progType,
    string projRoot = "", string webRoot = PdpSiteDefaultWebroot,
    AcgtCodeRazor codeRazor = AcgtRazorDefault, AcgtCodeBranch codeBranch = AcgtBranchDefault)
  {
    if (progType == null) { throw new ArgumentNullException(nameof(progType)); }
    if (string.IsNullOrEmpty(projRoot)) { PdpCodeProjroot = Environment.CurrentDirectory; }
    else { PdpCodeProjroot = projRoot; }
    if (string.IsNullOrEmpty(webRoot)) { PdpCodeWebroot = PdpSiteDefaultWebroot; }
    else { PdpCodeWebroot = webRoot; }
    PdpCodeRazor = codeRazor;
    PdpCodeBranch = codeBranch;

    // info from the program assembly
    var progAsmbly = Assembly.GetAssembly(progType);
    var progName = progAsmbly.GetName();
    PdpCodeAsmdat = GetPdpCodeDate(progAsmbly);
    // PdpCodeAppnam = progName.FullName; // includes description
    PdpCodeAppnam = progName.Name;
    // PdpCodeAsmver = progName.Version.ToString(3);
    PdpCodeAsmver = progName.Version.ToString();
    PdpCodeAppver = FileVersionInfo.GetVersionInfo(progAsmbly.Location).FileVersion;
    PdpCodeNamspc = progType.Namespace;
    PdpCodeErrmsg = $"PDP-DREAM error in {PdpCodeNamspc} for {PdpCodeBranch} {PdpCodeAppnam} {PdpCodeAppver}";
    PdpCodeBldstr = $"Code Branch {PdpCodeBranch} Version {PdpCodeAsmver} Date {PdpCodeAsmdat.ToLocalTime()}<br />";
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
  public string PdpCodeBldstr { get; init; }
  public string PdpCodeProjroot { get; init; }
  public string PdpCodeWebroot { get; init; }

} // end class

// end file