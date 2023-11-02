using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solas.BL.IRepositories
{
    public interface IEmailService
    {
      
            Task SendEmailConfirmationAsync(string toEmail, string subject, string body);
        
    }
}
