﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.EmailServices
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
