using System;
using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  public partial class PdpRestContext
  {
    // ATTN: if nothing in request options and nothing in server parameter constraints
    //       then leave as nothing in response settings !!! (otherwise incorrect filtering results)

    //   ResponseSettings should have NPDS values
    //      which may change on every request, and which may be null as follows:
    //      if requested value null and constraint value null, then null (do not use default!!!);
    //      TODO: if requested value null and constraint value non-null, then any of the constraint values;
    //      if requested value non-null and constraint value null, then the requested value;
    //      if requested value non-null and constraint value non-null, then the requested value if valid value else first constraint value;

    // default values from service parameters

    public string RegistrarTagDeflt
    {
      set
      {
        defRegistrarTag = value;
        defRegistrarGuid = NpdsServiceDefaults.GetValues.NpdsServiceCache.GetByTag(defRegistrarTag);
      }
      get { return defRegistrarTag; }
    }
    private string defRegistrarTag = NpdsServiceDefaults.GetValues.NpdsDefaultRegistrarTag;

    public Guid RegistrarGuidDeflt
    { get { return defRegistrarGuid; } }
    private Guid defRegistrarGuid = NpdsServiceDefaults.GetValues.NpdsDefaultRegistrarGuid;

    // requested values from request options (URL routes or querystrings)

    public string RegistrarTagReqst
    {
      set
      {
        reqRegistrarTag = value;
        if (!string.IsNullOrEmpty(reqRegistrarTag)) { RegistrarTag = reqRegistrarTag; }
      }
      get { return reqRegistrarTag; }
    }
    private string reqRegistrarTag = string.Empty;

    public string RegistrarGuidReqst
    {
      set
      {
        reqRegistrarGuid = value;
        if (!string.IsNullOrEmpty(reqRegistrarGuid)) { RegistrarGuid = PdpGuid.Parse(reqRegistrarGuid, RegistrarGuidDeflt); }
      }
      get { return reqRegistrarGuid; }
    }
    private string reqRegistrarGuid = string.Empty;

    public void RegistrarGuidResetFromReqst()
    {
      RegistrarGuid = PdpGuid.Parse(RegistrarGuidReqst, RegistrarGuidDeflt);
    }

    // selected values for response settings

    public string RegistrarTag
    {
      set
      {
        registrarTag = ValidateServiceTag(value, NpdsServiceDefaults.GetValues.NpdsConstraintRegistrarTag);
        registrarGuid = NpdsServiceDefaults.GetValues.NpdsServiceCache.GetByTag(registrarTag);
      }
      get
      {
        if (string.IsNullOrEmpty(registrarTag)) { registrarTag = RegistrarTagDeflt; }
        return registrarTag;
      }
    }
    private string registrarTag = string.Empty;

    public Guid? RegistrarGuid
    {
      set
      {
        registrarGuid = value;
        registrarTag = NpdsServiceDefaults.GetValues.NpdsServiceCache.GetByNullableGuid(registrarGuid);
      }
      get
      {
        if (PdpGuid.IsNullOrEmpty(registrarGuid)) { registrarGuid = RegistrarGuidDeflt; }
        return registrarGuid;
      }
    }
    private Guid? registrarGuid = Guid.Empty;

  }

}
