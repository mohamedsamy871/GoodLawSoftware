﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.Entities
{
    public class LoginItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? Remarks { get; set; }
    }
}
