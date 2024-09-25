

using InfinionProduct_Core.Entities;

namespace InfinionProduct_Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
        
    }
}
