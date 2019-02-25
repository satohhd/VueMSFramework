using System.Threading.Tasks;

namespace VueMSFramework.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
