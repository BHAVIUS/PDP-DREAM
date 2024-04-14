// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System.Collections.Generic;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static partial class QebFile
{
  // convention: implement all methods as extensions

  public static string EnvCurDir()
  {
    // TODO: test return values for .NET 8 release
    // returns location of dll executable for app
    string curdir = Environment.CurrentDirectory;
    return curdir;
  }

  // TODO: retest return value for NET 8
  public static string GetPathRoot(this string filepath)
  {
    // extension wrapper for System.IO.Path.GetPathRoot
    string pathroot = Path.GetPathRoot(filepath);
    return pathroot;
  }
  // TODO: retest return value for NET 8
  public static string GetFullPath(this string filepath)
  {
    // extension wrapper for System.IO.Path.GetFullPath
    string fullpath = Path.GetFullPath(filepath);
    return fullpath;
  }

  public static string PathToSourceFile(this string callingFile, [CallerFilePath] string callingPath = "")
  {
    return callingPath;
  }
  public static string PathToSourceFolder(this string callingFile, [CallerFilePath] string callingPath = "")
  {
    string folderName = Path.GetDirectoryName(callingPath).AppendPathSeparator();
    return folderName;
  }
  public static string PathToSourceFolderParent(this string callingFile, [CallerFilePath] string callingPath = "")
  {
    string folderName = Path.GetDirectoryName(callingPath);
    string folderPath = Directory.GetParent(folderName).FullName.AppendPathSeparator();
    return folderPath;
  }

  // ATTN: this method currently only called from unit test projects
  public static string PathToSolutionDataFolder(this string dataFolderName, [CallerFilePath] string tstFilePath = "")
  {
  // extension method assumes the data folder is subfolder of solution folder
    string projpath = Path.GetDirectoryName(tstFilePath); // from the test file to project folder
    string solnpath = Directory.GetParent(projpath).FullName; // from project folder to solution folder
    string datapath = Path.Combine(solnpath, dataFolderName).AppendPathSeparator();
    return datapath;
  }
  // ATTN: this method currently only called from BbnLexerTester
  public static string PathToFileCombinedWithExt(this string dirpath, string filenam, string fileext, bool fullpath = false)
  {
    string compath = Path.Combine(dirpath, $"{filenam}.{fileext}");
    if (fullpath) { Path.GetFullPath(compath); }
    return compath;
  }

  public static string AppendFileExtension(this string filepath, string fileext)
  {
    // Path.HasExtension(filepath)
    var extpath = $"{filepath}.{fileext}";;
    return extpath;
  }

  public static string AppendPathSeparator(this string filepath)
  {
    var seppath = filepath;
    if (!Path.EndsInDirectorySeparator(filepath))
    {
      seppath += Path.DirectorySeparatorChar.ToString();
    }
    return seppath;
  }

  public static string AppendCsext(this string filename)
  {
    return filename + ".cs";
  }

  // filnam without path
  public static string AppendTimeStampFilExt(this string filnam, string filver = "", string filext = "", string subdir = "")
  {
    ArgumentNullException.ThrowIfNull(filnam);
    if (string.IsNullOrEmpty(filver)) { filver = "z"; }
    if (string.IsNullOrEmpty(filext)) { filext = "log"; } // file extension as string without the separator "."
    if (string.IsNullOrEmpty(subdir)) { subdir = "TestLogs";  } // subdirectory as string without the separator "/"
    var revfil = $"{subdir.AppendPathSeparator()}{filnam}.{filver}.{DateTimeNowSortString()}.{filext}";
    return revfil;
  }

} // end class

// end file