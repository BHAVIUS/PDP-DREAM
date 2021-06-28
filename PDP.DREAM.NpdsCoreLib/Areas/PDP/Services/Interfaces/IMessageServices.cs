using System.Threading.Tasks;

namespace PDP.DREAM.NpdsCoreLib.Services
{
  public interface ISmsSender
  {
    Task SendSmsAsync(string number, string message);
  }

  public interface IEmailSender
  {
    Task SendEmailAsync(string email, string subject, string message);
  }

}
