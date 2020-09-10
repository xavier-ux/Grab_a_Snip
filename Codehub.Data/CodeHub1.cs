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
    public class CodeHub1
    {
        [Key]
        public int CodehubId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        //Add this on tuesday marker here just incase I need to remove
        
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
        public virtual ICollection<CssCode> CssCodes { get; set; }
        public virtual ICollection<BootstrapCode> BootstrapCodes { get; set; }
        public CodeHub1()
        {
            CssCodes = new HashSet<CssCode>();
            BootstrapCodes = new HashSet<BootstrapCode>();
        }
        
    }
}
