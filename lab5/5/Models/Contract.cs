using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _5.Models
{
    public partial class Contract
    {
        public int ContractId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Deadline { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
