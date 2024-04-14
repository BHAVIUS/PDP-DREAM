// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public class PdpCodeConfig
{
  public PdpCodeConfig(Type programType, string programPath,
    AcgtCodeRazor codeRazor = AcgtRazorDefault, AcgtCodeBranch codeBranch = AcgtBranchDefault)
  {
    if (programType == null) { throw new ArgumentNullException(nameof(programType)); }
    if (string.IsNullOrEmpty(programPath)) { throw new ArgumentNullException(nameof(programPath)); }
    PdpCodePrgroot = programPath;
    PdpCodeRazor = codeRazor;
    PdpCodeBranch = codeBranch;
    PdpCodeNamspc = programType.Namespace;
    var progAsmbly = Assembly.GetAssembly(programType);
    var progInfo = new PdpAssemblyInfo(progAsmbly);
    PdpCodeAsmdat = progInfo.AsmblyDate;
    PdpCodeAsmnam = progInfo.AsmblyName;
    PdpCodeAsmver = progInfo.AsmblyVersion;
    PdpCodeAppdesc = progInfo.AppDescription;
    PdpCodeAppvers = progInfo.AppVersion;
    PdpCodeErrmsg = $"PDP-DREAM error in {PdpCodeNamspc} for {PdpCodeBranch} {PdpCodeAsmnam} {PdpCodeAppvers}";
    PdpCodeBldstr = $"Code Branch {PdpCodeBranch} Version {PdpCodeAsmver} Date {PdpCodeAsmdat.ToLocalTime()}";
  }

  public AcgtCodeBranch PdpCodeBranch { get; init; }
  public AcgtCodeRazor PdpCodeRazor { get; init; }
  public DateTime PdpCodeAsmdat { get; init; } // assembly date
  public string PdpCodeAsmnam { get; init; } // assembly name
  public string PdpCodeAsmver { get; init; } // assembly version
  public string PdpCodeErrmsg { get; init; } // error message
  public string PdpCodeNamspc { get; init; } // namespace
  public string PdpCodeBldstr { get; init; } // build string
  public string PdpCodePrgroot { get; init; } // program root path
  public string PdpCodeAppdesc { get; init; } // application description
  public string PdpCodeAppvers { get; init; } // application version

} // end class

// end file