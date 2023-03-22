// SqldbcUilEntLabelViewList.cs 
// PORTAL-DOORS Project Copyright (c) 2007 - 2023 Brain Health Alliance. All Rights Reserved. 
// Software license: the OSI approved Apache 2.0 License (https://opensource.org/licenses/Apache-2.0).

namespace PDP.DREAM.NexusDataLib.Stores;

public partial class NexusDbsqlContext
{
  public IEnumerable<EntityLabelViewModel> ListViewableEntityLabels(Guid guidKey, bool isLimited = false)
  {
    IEnumerable<EntityLabelViewModel> result;
    try
    {
      IQueryable<NexusEntityLabel> qry = this.NexusEntityLabels;
      if (NPDSCP.ClientHasAdminAccess || NPDSCP.ClientHasEditorAccess)
      { qry = qry.Where(nn => (nn.RecordGuidRef == guidKey)); }
      else
      {
        if (isLimited)
        {
          qry = qry.Where(nn => (nn.RecordGuidRef == guidKey) &&
            (nn.IsDeleted == false) && (nn.UpdatedByAgentGuidRef == NPDSCP.ClientAgentGuid));
        }
        else
        {
          qry = qry.Where(nn => (nn.RecordGuidRef == guidKey) && 
		    (nn.IsDeleted == false));
        }
      }
      result = qry.ToViewable().AsEnumerable()
        .OrderBy(nn => nn.HasPriority)
        .ToList();
    }
    catch
    {
      result = Enumerable.Empty<EntityLabelViewModel>();
    }
    return result;
  }

} // end class

// end file