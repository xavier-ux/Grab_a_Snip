﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codehub.Models
{
    public class BootstrapListItem
    {
        public int BootstrapId { get; set; }
        public string Title { get; set; }

        public DateTimeOffset CreatedUtc { get; set; }
    }
}
