using PDP.DREAM.NpdsCoreLib.Types;

namespace PDP.DREAM.NpdsCoreLib.Models
{
  // TODO: revise for better conventions on difference between
  //  defaults for filtering/selecting records and defaults for creating new records
  public partial class PdpRestContext
  {
    // default values

    public NpdsConst.EntityType EntityTypeDeflt // for filtering/selecting records
    { get { return defEntityType; } }
    private NpdsConst.EntityType defEntityType = NpdsServiceDefaults.GetValues.NpdsDefaultEntityType;
    public NpdsConst.EntityType EntityTypeNewDeflt // for creating new records
    { get { return defNewEntityType; } }
    private NpdsConst.EntityType defNewEntityType = NpdsServiceDefaults.GetValues.NpdsDefaultNewEntityType;

    // requested values
    public string EntityTypeReqst
    {
      set
      {
        reqEntityType = value;
        EntityType = ValidateEntityType(reqEntityType);
      }
      get { return reqEntityType; }
    }
    private string reqEntityType = string.Empty;

    // validated values
    public NpdsConst.EntityType EntityType
    {
      set { entityType = ValidateEntityType(value); }
      get
      {
        if (entityType == 0)
        { EntityType = EntityTypeDeflt; }
        return entityType;
      }
    }
    private NpdsConst.EntityType entityType = default;

    // validators

    private NpdsConst.EntityType ValidateEntityType(string strValue)
    {
      NpdsConst.EntityType enmValue = PdpEnum<NpdsConst.EntityType>.Parse(strValue, NpdsConst.EntityType.AnyAndAll);
      return enmValue;
    }
    private NpdsConst.EntityType ValidateEntityType(NpdsConst.EntityType value)
    {
      // TODO: finish coding this virtual implementation with AI rules
      return value;
    }

  }

}
