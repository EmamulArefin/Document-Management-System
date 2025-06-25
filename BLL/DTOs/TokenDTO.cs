using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class TokenDTO
    {
        public int Id { get; set; }
        public string Token_Key { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime? Expire_At { get; set; }
        public int UserId { get; set; }
    }
}
