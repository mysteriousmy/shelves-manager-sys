using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shelves_manager_sys.Entity.Context;
using shelves_manager_sys.Entity;

namespace Tags_manager_sys.DAL
{
    class TagsDAL
    {
        public static async Task<TagsPageData> GetTagsByPage(int pageIndex, int pageSize)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = await db.tags.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                long count = await db.tags.LongCountAsync();
                long pageCount = (long)Math.Ceiling(count * 1.0 / pageSize);
                TagsPageData data = new();
                data.TagList = items;
                data.count = pageCount;
                return data;
            };

        }

        internal static void DeleteDataById(int id)
        {
            ShelvesDbContext db = new();
            var entity = db.tags.Find(id);
            db.tags.Remove(entity);
            db.SaveChanges();
        }


        internal static void Insert(Tags Tags)
        {
            ShelvesDbContext db = new();
            db.tags.Add(Tags);
            db.SaveChanges();
        }

        internal static void Update(Tags Tags)
        {
            ShelvesDbContext db = new();
            var tag = db.tags.First(a => a.tgaId == Tags.tgaId);
            tag.tagCode = Tags.tagCode;
            db.SaveChanges();
        }

        internal static Tags findyById(int v)
        {
            ShelvesDbContext db = new();
            return db.tags.FirstOrDefault(a => a.tgaId == v);
        }
    }
    public class TagsPageData
    {
        public List<Tags>? TagList { get; set; }
        public long count;
    }
}
