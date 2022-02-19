// PdpOsDirFilePath.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace PDP.DREAM.CoreDataLib.Utilities;

public static class PdpOsDirFilePath
{
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

  public static string FileNameWithPath(string filename, string dirname = "", bool absolute = false)
  {
    if (string.IsNullOrEmpty(dirname))
    {
      dirname = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
    }
    if ((absolute) && (!Path.IsPathRooted(dirname) || !Path.IsPathFullyQualified(dirname)))
    {
      dirname = Path.GetFullPath(dirname);
    }
    string filepath = Path.Combine(dirname, filename);
    return filepath;
  }

}
