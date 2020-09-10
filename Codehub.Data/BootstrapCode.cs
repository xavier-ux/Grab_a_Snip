using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codehub.Data
{
    public class BootstrapCode
    {
        [Key]
        public int BootstrapId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public virtual ICollection<CodeHub1> Codehubs { get; set; }

        [Required]
        public DateTimeOffset CreatedUtc { get; set; }

        public DateTimeOffset? ModifiedUtc { get; set; }
        public BootstrapCode()
        {
            Codehubs = new HashSet<CodeHub1>();
        }
    }
}
