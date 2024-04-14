// PORTAL-DOORS Project Copyright (c) 2006-2024 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.CoreDataLib.Models;

// TODO: revise for better conventions on difference between
//  defaults for filtering/selecting records and defaults for creating new records
public partial class NpdsClientWrace
{
  // requested value (nullable string)

  private string? reqFieldFormat = ESS;
  public string? FieldFormatReqst
  {
    set {
      reqFieldFormat = value;
      if (!string.IsNullOrEmpty(reqFieldFormat))
      { fieldFormat = ValidateFieldFormat(reqFieldFormat); }
    }
    get { return reqFieldFormat; }
  }

  // validated value (non-nullable typed)

  private NpdsFieldFormat fieldFormat = NPDSCD.FieldFormatDefault;
  public NpdsFieldFormat FieldFormat
  {
    set { fieldFormat = ValidateFieldFormat(value); }
    get {
      if (fieldFormat == null)
      { fieldFormat = NPDSCD.FieldFormatDefault; }
      return fieldFormat;
    }
  }

  // validators

  private NpdsFieldFormat ValidateFieldFormat(string recName)
  {
    var recItem = NPDSCD.ParseFieldFormat(recName);
    return ValidateFieldFormat(recItem);
  }
  private NpdsFieldFormat ValidateFieldFormat(NpdsFieldFormat recItem)
  {
    // TODO: code AI rules
    return recItem;

  } // end method

}  // end class

// TODO: migrate from static enums to dynamic db enumerators
// to dynamic db records initialized on app startup

public static partial class PdpAppConst
{
  // FieldFormat determines the structured syntax format for content
  // especially on DOORS side for semantic analysis
  // but also on PORTAL side for lexical analysis (in OtherText only)

  public static class DdeFieldFormat
  {
    public const string None = "None"; // 0
    public const string Unknown = "Unknown "; // 99
    public const string AnyAndAll = "AnyAndAll"; // 100

    // generic terms (odds true, evens false)

    public const string FreeForm = "FreeForm"; // 1
    public const string URL = "URL"; // 2 Uniform Resource Locator
    public const string UNC = "UNC"; // 3 Uniform Naming Convention
    public const string OSFNE = "OSFNE"; // 4 Operating System File Name with Extension (without file path)
    public const string OSFRP = "OSFRP"; // 5 Operating System File Relative Path (with file name and extension)
    public const string OSFAP = "OSFAP"; // 6 Operating System File Absolute Path (with file name and extension)
    public const string OSDRP = "OSDRP"; // 7 Operating System Directory Relative Path (with dirname and pathsep)
    public const string OSDAP = "OSDAP"; // 8 Operating System Directory Absolute Path (with dirname and pathsep)
    public const string JSON = "JSON"; // 10
    public const string XML = "XML"; // 11
    public const string RDF = "RDF"; // 12
    public const string OWL = "OWL"; // 13
    public const string HTML = "HTML"; // 14
    public const string XHTML = "XHTML"; // 15

    //  Craig and Taswell 2021 Brainiacs Edoc HA46280EF
    public const string NpdsQuad = "NpdsQuad"; // 20
    public const string NpdsQuadMin = "NpdsQuadMin"; // 21
    public const string NpdsQuadMod = "NpdsQuadMod"; // 22
    public const string NpdsQuadMax = "NpdsQuadMax"; // 23

    // BibTex, BibLaTeX, BabbleNewt BibCitRef formats
    public const string PdpBibtex = "PdpBibtex";
    // 30 PDP variant similar to BibTeX, interoperable with BibTeX format
    public const string PdpBibtexgen = "PdpBibtexgen";
    // 31 PDP variant similar to BibTeX, interoperable with BibTeX format, transition to PdpBiblatex
    public const string PdpBiblatex = "PdpBiblatex";
    // 32 PDP variant similar to BibLaTeX, interoperable with BibLaTeX format
    public const string PdpBiblatexgen = "PdpBiblatexgen";
    // 33 PDP variant generalized from BibLaTeX, interoperable with BibLaTeX format, transition to PdpBabblenewt
    public const string PdpBabblenewt = "PdpBabblenewt";
    // 34 PDP BabbleNewt format, universally interoperable with other bibliographic formats (MARC, BibFrame, etc)
    public const string PdpBibdefault = "PdpBibdefault";
    // 35 for setting preferred *.bib default if *.bib file used
    public const string VCF = "VCF";
    // 42 Virtual Contact File (VCF) aka vCard

  } // end class

} // end class

public partial class NpdsClientDefaults
{
  public NpdsFieldFormat FieldFormatNone =
     new NpdsFieldFormat(0, DdeFieldFormat.None, DdeFieldFormat.None);
  public NpdsFieldFormat FieldFormatFreeForm =
     new NpdsFieldFormat(1, DdeFieldFormat.FreeForm, DdeFieldFormat.FreeForm);
  public static NpdsFieldFormat FieldFormatURL =
     new NpdsFieldFormat(2, DdeFieldFormat.URL, "Uniform Resource Locator");
  public NpdsFieldFormat FieldFormatUNC =
      new NpdsFieldFormat(3, DdeFieldFormat.UNC, "Uniform Naming Convention");
  public NpdsFieldFormat FieldFormatOSFNE =
      new NpdsFieldFormat(4, DdeFieldFormat.OSFNE, "Operating System File Name with Extension (without file path)");
  public NpdsFieldFormat FieldFormatOSFRP =
      new NpdsFieldFormat(5, DdeFieldFormat.OSFRP, "Operating System File Relative Path (with file name and extension)");
  public NpdsFieldFormat FieldFormatOSFAP =
      new NpdsFieldFormat(6, DdeFieldFormat.OSFAP, "Operating System File Absolute Path (with file name and extension)");
  public NpdsFieldFormat FieldFormatOSDRP =
      new NpdsFieldFormat(7, DdeFieldFormat.OSDRP, "Operating System Directory Relative Path (with dirname and pathsep)");
  public NpdsFieldFormat FieldFormatOSDAP =
      new NpdsFieldFormat(8, DdeFieldFormat.OSDAP, "Operating System Directory Absolute Path (with dirname and pathsep)");
  public NpdsFieldFormat FieldFormatNpdsQuad =
     new NpdsFieldFormat(20, DdeFieldFormat.NpdsQuad, DdeFieldFormat.NpdsQuad);
  public NpdsFieldFormat FieldFormatNpdsQuadMin =
     new NpdsFieldFormat(21, DdeFieldFormat.NpdsQuadMin, DdeFieldFormat.NpdsQuadMin);
  public NpdsFieldFormat FieldFormatNpdsQuadMod =
      new NpdsFieldFormat(22, DdeFieldFormat.NpdsQuadMod, DdeFieldFormat.NpdsQuadMod);
  public NpdsFieldFormat FieldFormatNpdsQuadMax =
      new NpdsFieldFormat(23, DdeFieldFormat.NpdsQuadMax, DdeFieldFormat.NpdsQuadMax);
  public NpdsFieldFormat FieldFormatPdpBibtex =
      new NpdsFieldFormat(30, DdeFieldFormat.PdpBibtex, DdeFieldFormat.PdpBibtex);
  public NpdsFieldFormat FieldFormatPdpBibtexgen =
      new NpdsFieldFormat(31, DdeFieldFormat.PdpBibtexgen, DdeFieldFormat.PdpBibtexgen);
  public NpdsFieldFormat FieldFormatPdpBiblatex =
      new NpdsFieldFormat(32, DdeFieldFormat.PdpBiblatex, DdeFieldFormat.PdpBiblatex);
  public NpdsFieldFormat FieldFormatPdpBiblatexgen =
      new NpdsFieldFormat(33, DdeFieldFormat.PdpBiblatexgen, DdeFieldFormat.PdpBiblatexgen);
  public NpdsFieldFormat FieldFormatPdpBabblenewt =
      new NpdsFieldFormat(34, DdeFieldFormat.PdpBabblenewt, DdeFieldFormat.PdpBabblenewt);
  public NpdsFieldFormat FieldFormatPdpBibdefault =
      new NpdsFieldFormat(35, DdeFieldFormat.PdpBibdefault, DdeFieldFormat.PdpBibdefault);
  public NpdsFieldFormat FieldFormatVCF =
      new NpdsFieldFormat(42, DdeFieldFormat.VCF, DdeFieldFormat.VCF);

  // TODO: migrate from static enums to dynamic db enumerators
  // to dynamic db records initialized on app startup

  protected void InitFieldFormatEnums()
  {
    // list of PdpEnumRecord typed record items
    var recItms = new List<NpdsFieldFormat>
    {
      FieldFormatNone, FieldFormatFreeForm, FieldFormatURL, FieldFormatUNC,
      FieldFormatOSFNE, FieldFormatOSFRP, FieldFormatOSFAP,FieldFormatOSDRP,FieldFormatOSDAP,
      FieldFormatNpdsQuad, FieldFormatNpdsQuadMin, FieldFormatNpdsQuadMod, FieldFormatNpdsQuadMax,
      FieldFormatPdpBibtex,FieldFormatPdpBibtexgen,FieldFormatPdpBiblatex,FieldFormatPdpBiblatexgen,
      FieldFormatPdpBabblenewt,FieldFormatPdpBibdefault, FieldFormatVCF,
    };
    FieldFormatList = recItms;
    FieldFormatNewItem = recItms[0];
    FieldFormatDefault = recItms[1];
  }

  //public void ResetFieldFormatDefault(string enam)
  //{
  //  try
  //  {
  //    FieldFormatDefault = FieldFormatList?
  //       .Where(r => r.EName.ToLower() == enam.ToLower())
  //       .FirstOrDefault();
  //  }
  //  catch
  //  {
  //    InitFieldFormatEnums();
  //  }
  //}

  // nullable lists
  public List<NpdsFieldFormat>? FieldFormatList { get; set; } = null;
  public List<string>? FieldFormatNames
  {
    get {
      if (FieldFormatList == null) { InitFieldFormatEnums(); }
      return FieldFormatList.Select(itm => itm.EName).ToList<string>();
    }
  }
  // non-nullable items
  public NpdsFieldFormat ParseFieldFormat(byte ecod)
  {
    NpdsFieldFormat? recItm = null;
    if (FieldFormatList == null) { InitFieldFormatEnums(); }
    try
    {
      recItm = FieldFormatList?
         .Where(r => r.ECode == ecod)
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = FieldFormatDefault; }
    return recItm;
  }
  public NpdsFieldFormat ParseFieldFormat(string enam)
  {
    NpdsFieldFormat? recItm = null;
    if (FieldFormatList == null) { InitFieldFormatEnums(); }
    try
    {
      recItm = FieldFormatList
         .Where(r => r.EName.ToLower() == enam.ToLower())
         .FirstOrDefault();
    }
    catch (Exception exc)
    {
#if DEBUG
      Debug.WriteLine(exc);
#endif
    }
    if (recItm == null) { recItm = FieldFormatDefault; }
    return recItm;
  }
  public NpdsFieldFormat FieldFormatDefault { get; set; }
  public NpdsFieldFormat FieldFormatNewItem { get; set; }

} // end class

// end file