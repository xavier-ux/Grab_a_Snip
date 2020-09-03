using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codehub.Data
{
    public class Hubs
    {
        [Key]
        public int CodehubId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        //Add this on tuesday marker here just incase I need to remove

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
        public int CssId { get; set; }
        [ForeignKey("CssId")]
        public virtual Css Css { get; set; }
        public int BootstrapId { get; set; }

        [ForeignKey("BootstrapId")]
        public virtual Bootstrap Bootstrap { get; set; }
    }

}
