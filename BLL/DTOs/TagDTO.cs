using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class TagDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<FileDTO> Files { get; set; }
    }
}
