using System.Threading.Tasks;

namespace VueMSFramework.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
