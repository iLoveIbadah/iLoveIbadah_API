using iLoveIbadah.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.Contracts.Infrastructure
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(Email email);
    }
}
