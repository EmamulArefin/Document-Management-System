using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class FileDTO
    {
        public int Id { get; set; }
        public string F_Name { get; set; }
        public string Path { get; set; }
        public System.DateTime Upload_Time { get; set; }
        public int User_Id { get; set; }
        public int Tag_Id { get; set; }

        public virtual Tag Tag { get; set; }
        public virtual User User { get; set; }
    }
}
