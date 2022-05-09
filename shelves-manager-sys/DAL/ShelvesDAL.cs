using Microsoft.EntityFrameworkCore;
using shelves_manager_sys.Entity;
using shelves_manager_sys.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shelves_manager_sys.DAL
{
    internal class ShelvesDAL
    {
        public static async Task<ShelvesPageData> GetShelvesByPage(int pageIndex, int pageSize)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = await db.shelves.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                long count = await db.shelves.LongCountAsync();
                long pageCount = (long)Math.Ceiling(count * 1.0 / pageSize);
                ShelvesPageData data = new();
                data.ShelveList = items;
                data.count = pageCount;
                return data;
            } ;

        }

        internal static void DeleteDataById(int id)
        {
            ShelvesDbContext db = new();
            var entity = db.shelves.Find(id);
            db.shelves.Remove(entity);
            db.SaveChanges();
        }

        internal static void StartOrStopShelves(int v, bool state)
        {
            ShelvesDbContext db = new();
            var shelves = db.shelves.First(a => a.shelveId == v);
            shelves.isOnline = state;
            db.SaveChanges();
        }

        internal static void Insert(Shelves shelves)
        {
            ShelvesDbContext db = new();
            db.shelves.Add(shelves);
            db.SaveChanges();
        }

        internal static void Update(Shelves shelves)
        {
            ShelvesDbContext db = new();
            var shelve = db.shelves.First(a => a.shelveId == shelves.shelveId);
            shelve.isOnline = shelves.isOnline;
            shelve.shelveName = shelves.shelveName;
            db.SaveChanges();
        }

        internal static Shelves findyById(int v)
        {
            ShelvesDbContext db = new();
            return db.shelves.FirstOrDefault(a => a.shelveId == v);
        }
    }
    public class ShelvesPageData
    {
        public List<Shelves>? ShelveList { get; set; }
        public long count;
    }
}
