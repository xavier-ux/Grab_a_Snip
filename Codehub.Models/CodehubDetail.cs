﻿using Codehub.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codehub.Models
{
    public class CodehubDetail
    {
        public int CodehubId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "ModifiedUtc")]
        public DateTimeOffset? ModifiedUtc { get; set; }

        //Add this on tuesday marker here just incase I need to remove
        public int CssId { get; set; }
        public int BootstrapId { get; set; }

        public IEnumerable<BootstrapListItem> bootstrapListItems { get; set; }
    }
}
