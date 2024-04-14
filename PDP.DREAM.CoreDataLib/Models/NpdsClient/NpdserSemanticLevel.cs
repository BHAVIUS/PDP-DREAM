// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

public partial class NpdsClientWrace
{
  // ATTN: differentiate static singleton types
  // from types supporting multiple instance

  // requested values

  private string? reqSemanticLevel = ESS;
  public string? SemanticLevelReqst
  {
    set {
      reqSemanticLevel = value;
      if (!string.IsNullOrEmpty(reqSemanticLevel))
      { semanticLevel = ValidateSemanticLevel(reqSemanticLevel); }
    }
    get { return reqSemanticLevel; }
  }

  // validated values

  private NpdsSemanticLevel? semanticLevel = NPDSCD.SemanticLevelDefault;
  public NpdsSemanticLevel? SemanticLevel
  {
    set { semanticLevel = ValidateSemanticLevel(value); }
    get {
      if (semanticLevel == null)
      { SemanticLevel = NPDSCD.SemanticLevelDefault; }
      return semanticLevel;
    }
  }

  // validators

  private NpdsSemanticLevel ValidateSemanticLevel(string strValue)
  {
    // NpdsServerDefaults.SemanticLevel enmValue = NpdsSemanticLevel.None;
    //switch (strValue.ToLower())
    //{
    //  default:
    //    break;
    //}
    //enmValue = ValidateSemanticLevel(enmValue);
    // var enmValue = (NpdsServerDefaults.SemanticLevel)strValue;
    var enmValue = NPDSCD.SemanticLevelDefault;
    return enmValue;
  }
  private NpdsSemanticLevel ValidateSemanticLevel(NpdsSemanticLevel enmValue)
  {
    // validates search filter and resets default for ResrepFormat
    //switch (enmValue)
    //{
    //    default:
    //    throw new Exception($"case not implemented for SemanticLevel value {enmValue.ToString()} in method {MethodBase.GetCurrentMethod().Name}");
    //}
    return enmValue;
  }

} // end class

public partial class NpdsClientDefaults
{
  // SemanticLevel addresses use of "None, Metadata, Triples, Reified" from BHA-2020-41
  // Table 2 Categorizing Examples of Semantic Markup of Adam Craig's SARSE paper at
  // https://www.brainiacsjournal.org/arc/pub/Craig2020SARSE

  public void InitSemanticLevels()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsSemanticLevel>
    {
    new NpdsSemanticLevel(0,"None", "SemanticLevel (unspecified level)"),
    new NpdsSemanticLevel(1,"LexMeta", "lexical metadata (unspecified format)"),
    new NpdsSemanticLevel(2,"SemMeta", "SemanticLevel (unspecified format)"),
    new NpdsSemanticLevel(3,"Triples", "SemanticLevel (subject-verb-object)"),
    new NpdsSemanticLevel(4,"Quads", "SemanticLevel (reified triples)"),
    };
    SemanticLevelList = recItms;
    SemanticLevelItem = recItms[0];
    SemanticLevelDefault = recItms[1];
  }
  public void ResetSemanticLevelDefault(string enam, NpdsSemanticLevel edef)
  {
    try
    {
      SemanticLevelDefault = SemanticLevelList?
         .Where(itm => itm.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch
    {
      InitSearchScopeEnums();
    }
  }

  public List<NpdsSemanticLevel>? SemanticLevelList { get; set; } = null;
  public NpdsSemanticLevel? SemanticLevelItem { get; set; } = null;
  public NpdsSemanticLevel? SemanticLevelDefault { get; set; } = null;

} // end class

// end file

