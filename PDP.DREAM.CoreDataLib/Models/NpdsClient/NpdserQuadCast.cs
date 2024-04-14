// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // ATTN: differentiate static singleton types
  // from types supporting multiple instance

  // requested values

  private string? reqQuadCast = ESS;
  public string? QuadCastReqst
  {
    set {
      reqQuadCast = value;
      if (!string.IsNullOrEmpty(reqQuadCast))
      { quadCast = ValidateQuadCast(reqQuadCast); }
    }
    get { return reqQuadCast; }
  }

  // validated values

  private NpdsQuadCast? quadCast = NPDSCD.QuadCastDefault;
  public NpdsQuadCast? QuadCast
  {
    set { quadCast = ValidateQuadCast(value); }
    get {
      if (quadCast == null)
      { QuadCast = NPDSCD.QuadCastDefault; }
      return quadCast;
    }
  }

  // validators

  private NpdsQuadCast ValidateQuadCast(string strValue)
  {
    // NpdsServerDefaults.QuadCast enmValue = NpdsQuadCast.None;
    //switch (strValue.ToLower())
    //{
    //  default:
    //    break;
    //}
    //enmValue = ValidateQuadCast(enmValue);
    // var enmValue = (NpdsServerDefaults.QuadCast)strValue;
    var enmValue = NPDSCD.QuadCastDefault;
    return enmValue;
  }
  private NpdsQuadCast ValidateQuadCast(NpdsQuadCast enmValue)
  {
    // validates search filter and resets default for ResrepFormat
    //switch (enmValue)
    //{
    //    default:
    //    throw new Exception($"case not implemented for QuadCast value {enmValue.ToString()} in method {MethodBase.GetCurrentMethod().Name}");
    //}
    return enmValue;
  }

} // end class

public partial class NpdsClientDefaults
{
  // NPDS <--> Quad Casts between formats
  public List<NpdsQuadCast> InitQuadCasts()
  {
    var recItms = new List<NpdsQuadCast>
    {
    new NpdsQuadCast(0,"None", "QuadCast (unspecified level)"),
    new NpdsQuadCast(1,"Npds2QuadMin", "lexical metadata (unspecified format)"),
    new NpdsQuadCast(2,"Npds2QuadMod", "QuadCast (unspecified format)"),
    new NpdsQuadCast(3,"Npds2QuadMax", "QuadCast (subject-verb-object)"),
    new NpdsQuadCast(4,"QuadMin2Npds", "QuadCast (reified triples)"),
    new NpdsQuadCast(5,"QuadMod2Npds", "QuadCast (reified triples)"),
    new NpdsQuadCast(6,"QuadMax2Npds", "QuadCast (reified triples)"),
    };
    QuadCastList = recItms;
    QuadCastItem = recItms[0];
    QuadCastDefault = recItms[1];
    return recItms;
  }

  public List<NpdsQuadCast>? QuadCastList { get; set; } = null;
  public NpdsQuadCast? QuadCastItem { get; set; } = null;
  public NpdsQuadCast? QuadCastDefault { get; set; } = null;

} // end class

// end file