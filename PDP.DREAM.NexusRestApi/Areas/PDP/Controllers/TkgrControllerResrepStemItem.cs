using System;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;

namespace PDP.DREAM.NexusRestApi.Controllers
{
  public partial class TkgrControllerBase
  {
    private const string eidResrepStemStatus = "span#ResrepStemStatus";

    [HttpGet, HttpPost] // Get for Rest, Post for Ajax
    public ActionResult<string> SummarizeNexusResrepStem(Guid recordGuid)
    {
      ResetNexusRepository();  // use PNDC
      NexusResrepViewModel? nre = PNDC.GetViewableResrepStemByKey(recordGuid);
      var result = string.Empty;
      if (nre?.RRRecordGuid == recordGuid) { result = nre.NexusStatusSummary; }
      return result;
    }

  } // class

} // namespace
