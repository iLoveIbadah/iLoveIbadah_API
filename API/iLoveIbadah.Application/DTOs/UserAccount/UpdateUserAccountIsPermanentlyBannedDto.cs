﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iLoveIbadah.Application.DTOs.UserAccount
{
    public class UpdateUserAccountIsPermanentlyBannedDto
    {
        //public int Id { get; set; } //already passed in the url
        public bool IsPermanentlyBanned { get; set; }
    }
}
