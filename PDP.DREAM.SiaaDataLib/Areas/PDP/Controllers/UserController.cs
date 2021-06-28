using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using PDP.DREAM.NpdsCoreLib.Models;
using PDP.DREAM.NpdsCoreLib.Services;
using PDP.DREAM.NpdsDataLib.Stores.NpdsSqlDatabase;
using PDP.DREAM.SiaaDataLib.Stores.PdpIdentity;

namespace PDP.DREAM.SiaaDataLib.Controllers
{
  [Area(PdpConst.PdpMvcArea)]
  public partial class UserController : SiaaDataControllerBase
  {
    public UserController(ScribeDbsqlContext npdsCntxt, QebIdentityContext userCntxt,
      IEmailSender emlSndr, ISmsSender smsSndr, ILoggerFactory lgrFtry)
      : base(npdsCntxt, userCntxt, emlSndr, smsSndr, lgrFtry) { }

  } // class

} // namespace
