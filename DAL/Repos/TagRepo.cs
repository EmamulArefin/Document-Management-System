// TagRepo.cs
using DAL.EF;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repos
{
    internal class TagRepo : Repo, IRepo<Tag, int, Tag>
    {
        public Tag Create(Tag obj)
        {
            db.Tags.Add(obj);
            db.SaveChanges();
            return obj;
        }

        public void Delete(int id)
        {
            var tag = db.Tags.Find(id);
            if (tag != null)
            {
                db.Tags.Remove(tag);
                db.SaveChanges();
            }
        }

        public List<Tag> Get()
        {
            return db.Tags.ToList();
        }

        public Tag Get(int id)
        {
            return db.Tags.Find(id);
        }

        public Tag Update(Tag obj)
        {
            var existing = db.Tags.Find(obj.Id);
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