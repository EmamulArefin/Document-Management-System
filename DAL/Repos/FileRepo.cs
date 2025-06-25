using DAL.EF;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    internal class FileRepo : Repo, IRepo<File_Info, int, File_Info>
    {
        public File_Info Create(File_Info obj)
        {
            db.File_Info.Add(obj);
            db.SaveChanges();
            return obj;
        }


        public void Delete(int id)
        {
            var file = db.File_Info.Find(id);
            if (file != null)
            {
                db.File_Info.Remove(file);
                db.SaveChanges();
            }
        }

        public List<File_Info> Get()
        {
            return db.File_Info.ToList();
        }

        public File_Info Get(int id)
        {
            return db.File_Info
             .Include("Tag")
             .Include("User")
             .FirstOrDefault(f => f.Id == id);
        }

        public File_Info Update(File_Info obj)
        {
            var existing = db.File_Info.Find(obj.Id);
            if (existing != null)
            {
                db.Entry(existing).CurrentValues.SetValues(obj);
                db.SaveChanges();
                return existing;
            }
            return null;
        }
    }
}
