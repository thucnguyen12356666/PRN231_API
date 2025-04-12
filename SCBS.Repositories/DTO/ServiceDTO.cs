using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCBS.Repositories.DTO
{
    public class ServiceDTO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Duration { get; set; }

        public double? Rating { get; set; }

        public string Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UserId { get; set; }
    }
}
