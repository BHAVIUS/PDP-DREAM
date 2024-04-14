// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // requested values

  private string? reqResrepFormat = ESS;
  public string? ResrepFormatReqst
  {
    set {
      reqResrepFormat = value;
      if (!string.IsNullOrEmpty(reqResrepFormat))
      { resrepFormat = ValidateResrepFormat(reqResrepFormat); }
    }
    get { return reqResrepFormat; }
  }

  // validated values

  private NpdsResrepFormat resrepFormat = NPDSCD.ResrepFormatDefault;
  public NpdsResrepFormat ResrepFormat
  {
    set { resrepFormat = ValidateResrepFormat(value); }
    get {
      if (resrepFormat == null)
      { ResrepFormat = NPDSCD.ResrepFormatDefault; }
      return resrepFormat;
    }
  }

  // validators

  private NpdsResrepFormat ValidateResrepFormat(string strValue)
  {
    NpdsResrepFormat enmValue = NPDSCD.ParseResrepFormat(strValue);
    return ValidateResrepFormat(enmValue);
  }
  private NpdsResrepFormat ValidateResrepFormat(NpdsResrepFormat enmValue)
  {
    var chkValue = NPDSCD.ResrepFormatCore;
    switch (DatabaseType.EName)
    {
      // allows Core, PORTAL, DOORS, Nexus
      case DdeDatabaseType.Nexus:
      case DdeDatabaseType.Cache:
        if (enmValue == NPDSCD.ResrepFormatCore || enmValue == NPDSCD.ResrepFormatPORTAL ||
          enmValue == NPDSCD.ResrepFormatDOORS || enmValue == NPDSCD.ResrepFormatNexus)
        { chkValue = enmValue; }
        break;
      // allows Core and PORTAL
      case DdeDatabaseType.PORTAL:
        if (enmValue == NPDSCD.ResrepFormatCore || enmValue == NPDSCD.ResrepFormatPORTAL)
        { chkValue = enmValue; }
        break;
      // allows Core and DOORS
      case DdeDatabaseType.DOORS:
        if (enmValue == NPDSCD.ResrepFormatCore ||
          enmValue == NPDSCD.ResrepFormatDOORS)
        { chkValue = enmValue; }
        break;
      // allows Core, PORTAL, DOORS, Nexus, Scribe
      case DdeDatabaseType.Scribe:
      case DdeDatabaseType.ACMS:
      case DdeDatabaseType.Bridge:
      case DdeDatabaseType.Vocab:
        if (enmValue == NPDSCD.ResrepFormatCore ||
          enmValue == NPDSCD.ResrepFormatPORTAL ||
          enmValue == NPDSCD.ResrepFormatDOORS ||
          enmValue == NPDSCD.ResrepFormatNexus ||
          enmValue == NPDSCD.ResrepFormatScribe)
        { chkValue = enmValue; }
        break;
      case DdeDatabaseType.Core:
        // chkValue = NPDSCD.ResrepFormatCore;
        break;
      case DdeDatabaseType.QEBI:
      case DdeDatabaseType.None:
        chkValue = NPDSCD.ResrepFormatNone;
        break;
      default:
        throw new Exception("Invalid value for NpdsConstants.DatabaseType");
    }
    return chkValue;

  } // end method

}  // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  // ResrepFormat is the format for the NPDS resource representation
  // ATTN: do not conflate ResrepFormat with MessageFormat
  // ATTN: do not conflate either format with the Resrep Entity metadata content
  // TODO: consider renaming Core as CCCC = Cyberinfrastructure Component/Constituent Core 

  public static class DdeResrepFormat
  {
    public const string None = "None"; // 0
    public const string Nexus = "Nexus"; // 1
    public const string PORTAL = "PORTAL"; // 2
    public const string DOORS = "DOORS"; // 3
    public const string Scribe = "Scribe"; // 4
    public const string Core = "Core"; // 5
  };

} // end class

public partial class NpdsClientDefaults
{
  public NpdsResrepFormat ResrepFormatNone =
     new NpdsResrepFormat(0, DdeResrepFormat.None, DdeResrepFormat.None);
  public NpdsResrepFormat ResrepFormatNexus =
     new NpdsResrepFormat(1, DdeResrepFormat.Nexus, DdeResrepFormat.Nexus);
  public NpdsResrepFormat ResrepFormatPORTAL =
     new NpdsResrepFormat(2, DdeResrepFormat.PORTAL, DdeResrepFormat.PORTAL);
  public NpdsResrepFormat ResrepFormatDOORS =
      new NpdsResrepFormat(3, DdeResrepFormat.DOORS, DdeResrepFormat.DOORS);
  public NpdsResrepFormat ResrepFormatScribe =
      new NpdsResrepFormat(4, DdeResrepFormat.Scribe, DdeResrepFormat.Scribe);
  public NpdsResrepFormat ResrepFormatCore =
      new NpdsResrepFormat(5, DdeResrepFormat.Core, DdeResrepFormat.Core);

  protected void InitResrepFormatEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsResrepFormat>
    {
      ResrepFormatNone,
      ResrepFormatNexus,
      ResrepFormatPORTAL,
      ResrepFormatDOORS,
      ResrepFormatScribe,
      ResrepFormatCore,
    };
    ResrepFormatList = recItms;
    ResrepFormatNewItem = recItms[0];
    ResrepFormatDefault = recItms[1];
  }

  //public void ResetResrepFormatDefault(string enam)
  //{
  //  try
  //  {
  //    ResrepFormatDefault = ResrepFormatList?
  //       .Where(r => r.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch
  //  {
  //    InitResrepFormats();
  //  }
  //}

  // nullable lists
  public List<NpdsResrepFormat>? ResrepFormatList { get; set; } = null;
  public List<string>? ResrepFormatNames
  {
    get {
      if (ResrepFormatList == null) { InitResrepFormatEnums(); }
      return ResrepFormatList.Select(itm => itm.EName).ToList<string>();
    }
  }
  // non-nullable items
  public NpdsResrepFormat ParseResrepFormat(byte recCod)
  {
    NpdsResrepFormat? recItm = null;
    if (ResrepFormatList == null) { InitResrepFormatEnums(); }
    try
    {
      recItm = ResrepFormatList?
         .Where(r => r.ECode == recCod)
         .FirstOrDefault();
    }
    catch { }
    if (recItm == null) { recItm = ResrepFormatDefault; }
    return recItm;
  }
  public NpdsResrepFormat ParseResrepFormat(string enam)
  {
    NpdsResrepFormat? recItm;
    if (ResrepFormatList == null) { InitResrepFormatEnums(); }
    try
    {
      recItm = ResrepFormatList
         .Where(r => r.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch
    {
      recItm = ResrepFormatDefault;
    }
    if (recItm == null) { recItm = ResrepFormatDefault; }
    return recItm;
  }
  public NpdsResrepFormat ResrepFormatDefault { get; set; }
  public NpdsResrepFormat ResrepFormatNewItem { get; set; }

} // end class

// end file