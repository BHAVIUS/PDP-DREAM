// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class QebFile
{
  // TODO: recode a custom PdpPathCombine

  public static string EnvCurDir()
  {
    string curdir = Environment.CurrentDirectory; // returns location of dll executable
    return curdir;
  }
  public static string PathRoot(string filepath)
  {
    string pathroot = Path.GetPathRoot(filepath);
    return pathroot;
  }
  public static string FullPath(string filepath)
  {
    string fullpath = Path.GetFullPath(filepath);
    return fullpath;
  }
  public static string SourceFilePath([CallerFilePath] string filepath = "")
  {
    return filepath;
  }
  public static string SourceCurrentDirPath([CallerFilePath] string filepath = "")
  {
    string dirpath = Path.GetDirectoryName(filepath);
    return dirpath;
  }
  public static string SourceParentDirPath([CallerFilePath] string srcfilepath = "")
  {
    string dirpath = Path.GetDirectoryName(srcfilepath);
    string parpath = Directory.GetParent(dirpath).FullName;
    return parpath;
  }
  public static string CombineSrcParentDirPath(string desfilepath, [CallerFilePath] string srcfilepath = "")
  {
    string dirpath = Path.GetDirectoryName(srcfilepath);
    string parpath = Directory.GetParent(dirpath).FullName;
    string compath = Path.Combine(parpath, desfilepath);
    return compath;
  }
  public static string CombineDirFilExtPath(string dirpath, string filenam, string fileext)
  {
    string compath = Path.Combine(dirpath, $"{filenam}.{fileext}");
    return compath;
  }
  public static string CombineDirNameWithAbsPath(this string dirname, [CallerFilePath]  string filepath = "")
  {
    var dirpath = Path.GetDirectoryName(filepath);
    if (string.IsNullOrEmpty(dirpath) && !string.IsNullOrEmpty(filepath))
    {
      dirpath = Directory.GetParent(filepath).Parent.FullName;
    }
    if ((!Path.IsPathRooted(dirpath) || !Path.IsPathFullyQualified(dirpath)))
    {
      dirpath = Path.GetFullPath(dirpath);
    }
    // TODO: enhance for different OS directory separators
    if (dirname.Contains("/")) { dirname = dirname.Replace("/","\\"); }
    string abspath = dirpath + dirname;
    return abspath;
  }

  // TODO: must test/debug feature with subdirectory folder
  public static string AppendTimeStampFilExt(this string filnam, string filver = "", string filext = "")
  {
    ArgumentNullException.ThrowIfNull(filnam);
    if (string.IsNullOrEmpty(filver)) { filver = "z"; }
    if (string.IsNullOrEmpty(filext)) { filext = "log";  } // file extension as string without the separator "."
    // if (string.IsNullOrEmpty(subdir)) { subdir = "log";  } // subdirectory as string without the separator "/"
    // var revfil = $"{subdir}/{filnam}{DateTimeNowSortString()}.{filext}";
    var revfil = $"{filnam}.{filver}.{DateTimeNowSortString()}.{filext}";
    return revfil;
  }

} // end class

// end file