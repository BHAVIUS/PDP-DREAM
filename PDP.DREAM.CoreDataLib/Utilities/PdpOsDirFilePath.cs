// PdpOsDirFilePath.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class PdpOsDirFilePath
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
    string compath = Path.Combine(dirpath, filenam + fileext);
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

} // end class

// end file