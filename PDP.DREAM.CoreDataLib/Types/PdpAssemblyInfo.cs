namespace PDP.DREAM.CoreDataLib.Types;

public class PdpAssemblyInfo
{
  public PdpAssemblyInfo() // zero-param constructor
    : this(Assembly.GetExecutingAssembly()) { }

  public PdpAssemblyInfo(Assembly asmbly) // one-param constructor
  {
    AsmblyDate = GetPdpCodeDate(asmbly);

    var asmblyName = asmbly.GetName();
    AsmblyName = asmblyName.Name;
    // FullName includes culture, version, tokenkey
    AsmblyFullName = asmblyName.FullName;
    AsmblyVersion = asmblyName.Version.ToString();
    // AssemblyVersion = asmblyName.Version.ToString(3);

    var asmblyLocation = asmbly.Location;
    FileLocation = asmblyLocation.ToString();
    FileVersion = FileVersionInfo.GetVersionInfo(asmblyLocation).FileVersion;

    AssemblyCompanyAttribute compAttr =
        asmbly.GetCustomAttribute<AssemblyCompanyAttribute>();
    AsmblyCompany = (compAttr != null ? compAttr.Company : "");

    AssemblyCopyrightAttribute copyAttr =
        asmbly.GetCustomAttribute<AssemblyCopyrightAttribute>();
    AsmblyCopyright = (copyAttr != null ? copyAttr.Copyright : "");

    AssemblyDescriptionAttribute descAttr =
    asmbly.GetCustomAttribute<AssemblyDescriptionAttribute>();
    AsmblyDescription = (descAttr != null ? descAttr.Description : "");

    AssemblyProductAttribute prodAttr =
    asmbly.GetCustomAttribute<AssemblyProductAttribute>();
    AsmblyProduct = (prodAttr != null ? prodAttr.Product : "");

    AssemblyTitleAttribute titlAttr =
    asmbly.GetCustomAttribute<AssemblyTitleAttribute>();
    AsmblyTitle = (titlAttr != null ? titlAttr.Title : "");

    AssemblyTrademarkAttribute tradAttr =
        asmbly.GetCustomAttribute<AssemblyTrademarkAttribute>();
    AsmblyTrademark = (tradAttr != null ? tradAttr.Trademark : "");
  }

  public DateTime AsmblyDate { get; init; }
  public string AsmblyName { get; init; } = "";
  public string AsmblyFullName { get; init; } = "";
  public string AsmblyVersion { get; init; } = "";
  public string AsmblyCompany { get; init; } = "";
  public string AsmblyCopyright { get; init; } = "";
  public string AsmblyDescription { get; init; } = "";
  public string AsmblyProduct { get; init; } = "";
  public string AsmblyTitle { get; init; } = "";
  public string AsmblyTrademark { get; init; } = "";
  public string FileVersion { get; init; } = "";
  public string FileLocation { get; init; } = "";
  // TODO: review and rebuild these App* properties
  public string AppDescription { get { return AsmblyDescription; } }
  public string AppVersion { get { return PdpCodeDefaultAppversion; } }

  public static DateTime GetPdpCodeDate(Assembly assembly)
  {
    var attribute = assembly.GetCustomAttribute<PdpCodeDateAttribute>();
    return attribute != null ? attribute.DateTime : default(DateTime);
  }

} // end class

// See https://aka.ms/dotnet/msbuild/customize for more details on customizing your build
//
// Microsoft Visual Studio Build code generator works correctly
// for "product", "trademark", "description"
// but does not generate correct info for "title"
// as requested in *.csproj file
//
// TODO: too many MS bugs in *AssemblyInfo.cs generator called
// MSBuild WriteCodeFragment class with source code at
// https://github.com/dotnet/msbuild/blob/main/src/Tasks/WriteCodeFragment.cs
// which is based on Runtime version 4.0.30319.42000 ??? from the Net Framework ???
// therefore must build own PdpAssemblyInfo generator or use more reliable build infrastructure
// https://stackoverflow.com/questions/11582250/is-there-any-msbuild-alternative
// https://www.jetbrains.com/help/rider/Build_Process.html
// VS Code calls dotnet build.... which calls MSBuild
// https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build
// https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-build#msbuild


[AttributeUsage(AttributeTargets.Assembly)]
public class PdpCodeDateAttribute : Attribute
{
  public PdpCodeDateAttribute(string value)
  {
    DateTime = DateTime.ParseExact(value, PdpDateTimeSortFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
  }

  public DateTime DateTime { get; }

} // end class

// end file