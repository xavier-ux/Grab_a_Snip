using Codehub.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codehub.Models
{
   public class CodehubListItem
    {
            public int CodehubId { get; set; }
            public string Title { get; set; }

            public DateTimeOffset CreatedUtc { get; set; }
        public string Description { get; set; }
        //Add this on tuesday marker here just incase I need to remove
      public int BootstrapId { get; set; }
        public int CssId { get; set; }


    }

}
