﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.AuthDTOs
{
    public class LoginResultDTO
    {
        //public string RefreshToken { get; set; }
        public string AccessToken { get; set; }
        public SmallUserDTO User { get; set; }
    }
}
