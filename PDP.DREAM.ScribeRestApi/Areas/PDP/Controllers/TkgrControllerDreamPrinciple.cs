// TkgrControllerDreamPrinciple.cs 
// Copyright (c) 2007 - 2021 Brain Health Alliance. All Rights Reserved. 
// Licensed per the OSI approved MIT License (https://opensource.org/licenses/MIT).

using Microsoft.AspNetCore.Mvc;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Types;
using PDP.DREAM.NpdsDataLib.Models.NpdsSqlDatabase;

namespace PDP.DREAM.ScribeRestApi.Controllers
{
  public partial class TkgrControllerBase
  {

    [HttpGet]
    public virtual IActionResult ImportPrinciple()
    {
      PRC.PageTitle = PRC.RecordAccess.ToString() + " Import Principle for PDP-DREAM";
      var dreamp = new DreamPrincipleEditModel();
      return View(dreamp);
    }

    [HttpPut, HttpPost, ValidateAntiForgeryToken] // Put/Post for Rest, Post for Ajax
    public virtual IActionResult ImportPrinciple(DreamPrincipleEditModel editObj)
    {
      var entyType = NpdsConst.EntityType.Principle.ToString();
      var srvcType = NpdsConst.ServiceType.Nexus.ToString();
      var srvcGuid = editObj.DiristryGuid;
      var srvcTag = NpdsServiceDefaults.GetValues.NpdsServiceCache.GetByGuid(srvcGuid);
      // parse and reset the PRC and PSDC
      PRC.ParseNpdsServTagEntity(srvcType, srvcTag, entyType);
      ResetScribeRepository(); // use PSDC

      // ResRepRecord object
      var rrrObj = new NexusResrepEditModel()
      {
        RecordDiristryGuid = PdpGuid.ParseToNonNullable(editObj.DiristryGuid, PRC.DiristryGuidDeflt),
        RecordRegistrarGuid = PdpGuid.ParseToNonNullable(editObj.RegistrarGuid, PRC.RegistrarGuidDeflt)
      };
      // ATTN: must reclarify/refactor/recode convention for transition from Nexus 8 to Nexus 9 on
      // ATTN:   current use of Nexus, PORTAL, DOORS such that Nexus takes priority over PORTAL and DOORS
      // ATTN: maintain priority for this import utility on bibtex records
      rrrObj.RecordRegistryGuid = rrrObj.RecordDiristryGuid;
      rrrObj.RecordDirectoryGuid = rrrObj.RecordDiristryGuid;
      rrrObj.EntityTypeCode = (short)NpdsConst.EntityType.Principle;
      rrrObj.EntityInitialTag = editObj.EntityTag;
      rrrObj.EntityName = editObj.EntityName;
      rrrObj.EntityNature = editObj.EntityNature;
      rrrObj.InfosetIsAuthorPrivate = true;
      rrrObj.InfosetIsAgentShared = true;
      rrrObj = PSDC.EditResrepStem(rrrObj);

      var rrrGuidRef = rrrObj.RRRecordGuid;
      // var rrrPdpMsg = rrrObj.PdpStatusMessage;
      var rrrStored = rrrObj.PdpStatusItemStored;

      if ((rrrStored == true) && (!PdpGuid.IsInvalidGuid(rrrGuidRef)))
      {
        var st = new SupportingTagEditModel()
        {
          SupportingTag = editObj.SupportingTag,
          RRRecordGuid = rrrGuidRef,
          HasPriority = 0,
          IsPrincipal = true
        };
        PSDC.EditSupportingTag(st);

        var cr = new CrossReferenceEditModel()
        {
          CrossReference = editObj.CrossReference,
          RRRecordGuid = rrrGuidRef,
          HasPriority = 0,
          IsPrincipal = true
        };
        PSDC.EditCrossReference(cr);

        var ot = new OtherTextEditModel()
        {
          OtherText = editObj.OtherText,
          RRRecordGuid = rrrGuidRef,
          HasPriority = 0,
          IsPrincipal = true
        };
        PSDC.EditOtherText(ot);

        var loc = new LocationEditModel()
        {
          UrlWebAddress = editObj.Location,
          RRRecordGuid = rrrGuidRef,
          HasPriority = 0,
          IsPrincipal = true
        };
        PSDC.EditLocation(loc);

        var des = new DescriptionEditModel()
        {
          Description = editObj.Description,
          RRRecordGuid = rrrGuidRef,
          HasPriority = 0,
          IsPrincipal = true
        };
        PSDC.EditDescription(des);

        var controller = ControllerContext.ActionDescriptor.ControllerName;
        return RedirectToAction("NpdsEdit", controller, new { serviceType = "Nexus", serviceTag = "PDP-DREAM" });
      }

      PRC.PageTitle = PRC.RecordAccess.ToString() + " Import Principle for PDP-DREAM";
      return View(editObj);
    }

  }

}
