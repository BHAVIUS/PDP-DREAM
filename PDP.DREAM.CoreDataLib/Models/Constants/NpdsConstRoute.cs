// NpdsConstantsRegex.cs 
// Copyright (c) 2007 - 2022 Brain Health Alliance. All Rights Reserved. 
// Code license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// PdpConstants for PDP applications
// NpdsConstants for NPDS specification model

public static partial class NpdsConst
{
  public const string NpdsRoot = "NPDS-Root";

  // new for Net 6 with migration from convention routing to attribute routing
  // string constants for use in RestApi and WebApp controllers

  // TODO: complete migration from from convention routing to attribute routing
  // NP is NamePrefix, NS is NameSuffix, TS is TemplateSuffix
  public const string NPtkgr = "PdpTkgr";
  public const string NSget = "Get"; // may include post for Get/Post
  public const string NSput = "Put"; // may include post for Put/Post
  public const string NSdel = "Del"; // may include post for Delete/Post
  public const string NSpost = "Post"; // for post only
  public const string TSststet = "{serviceType}/{serviceTag}/{entityType?}";
  public const string TSrg = "{recordGuid}";
  public const string TSrgil = "{recordGuid}/{isLimited?}";

} // class
