namespace PDP.DREAM.NpdsCoreLib.Models
{
  // constants grouped into PdpConstants and NpdsConstants
  // ie what is part of PDP application and what is part of NPDS spec model
  public static partial class NpdsConst
  {
    // this class contains nothing but constants and enums only
    // if enum value not set in code, it defaults to 0

    // Query String Keys with names beginning with Qsk

    // NPDS keys
    public const string QskDiristryTag = "nexus";
    public const string QskRegistryTag = "portal";
    public const string QskDirectoryTag = "doors";
    public const string QskRegistrarTag = "scribe";
    public const string QskDiristryGuid = "nexusgk";
    public const string QskRegistryGuid = "portalgk";
    public const string QskDirectoryGuid = "doorsgk";
    public const string QskRegistrarGuid = "scribegk";
    // NPDS keys with 5- or 6-character keys
    public const string QskLexLabAny = "labany"; // Any/All Labels
    public const string QskLexLabCan = "labcan"; // Canonical Label
    public const string QskLexLabAls = "labals"; // Alias Labels
    public const string QskLexLabSup = "labsup"; // Supporting Label
    public const string QskLexTagAny = "tagany"; // Tag Any/All
    public const string QskLexTagSup = "tagsup"; // Supporting Tag
    public const string QskLexOText = "otext"; // OtherText (= OtherMetadata)
    // NPDS keys with 4-character keys
    public const string QskServiceType = "styp";
    public const string QskServiceTag = "stag";
    public const string QskEntityType = "etyp";
    public const string QskEntityTag = "etag";
    public const string QskEntityName = "enam";
    public const string QskEntityNature = "enat";
    public const string QskInfosetType = "ityp";
    // NPDS keys with 3-character keys
    public const string QskDatabaseAccess = "dam"; // Database Access Mode
    public const string QskRecordAccess = "ram"; // Record Access Mode
    public const string QskUserName = "unk"; // UserName Key
    public const string QskPassWord = "pwk"; // PassWord Key
    public const string QskUserKey = "ugk"; // UserGuid Key
    public const string QskAgentKey = "agk"; // AgentGuid Key
    public const string QskSessionKey = "sgk"; // SessionGuid Key
    // NPDS keys with 2-character keys
    public const string QskArchiveFormat = "af";
    public const string QskCheckFormat = "cf";
    public const string QskEchoFormat = "ef";
    public const string QskListCount = "lc";
    public const string QskMessageFormat = "mf";
    public const string QskQueryFormat = "qf";
    public const string QskResrepFormat = "rf";
    public const string QskVerboseFormat = "vf";

  }

}