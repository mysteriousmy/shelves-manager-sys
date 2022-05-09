using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using shelves_manager_sys.Entity.Context;
using shelves_manager_sys.Entity;

namespace Cargos_manager_sys.DAL
{
    class CargosDAL
    {
        public static async Task<CargosPageData> GetCargosByPage(int pageIndex, int pageSize)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = await db.cargos.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                long count = await db.cargos.LongCountAsync();
                long pageCount = (long)Math.Ceiling(count * 1.0 / pageSize);
                CargosPageData data = new();
                data.TagList = items;
                data.count = pageCount;
                return data;
            };

        }
        public static List<MoreCargoInfo> GetCargosMoreInfoByPage(string searchWords)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = db.cargos.Where(e => EF.Functions.Like(e.cargoName, "%" + searchWords + "%")).ToList();
                List<MoreCargoInfo> moreinfos = new();
                items.ForEach(i =>
                {
                    var tmp = db.putCargos.FirstOrDefault(s => s.cargoId == i.cargoId);
                    if(tmp != null)
                    {
                        MoreCargoInfo moreinfo = new()
                        {
                            cargoId = i.cargoId,
                            cargoName = i.cargoName,
                            position = tmp.position
                        };
                        moreinfos.Add(moreinfo);
                    }
                    
                });
                return moreinfos;
            };

        }
        internal static void DeleteDataById(int id)
        {
            ShelvesDbContext db = new();
            var entity = db.cargos.Find(id);
            db.cargos.Remove(entity);
            db.SaveChanges();
        }


        internal static void Insert(Cargos Cargos)
        {
            ShelvesDbContext db = new();
            db.cargos.Add(Cargos);
            db.SaveChanges();
        }

        internal static void Update(Cargos Cargos)
        {
            ShelvesDbContext db = new();
            var cargo = db.cargos.First(a => a.cargoId == Cargos.cargoId);
            cargo.cargoName = Cargos.cargoName;
            cargo.cargoId = Cargos.cargoId;
            db.SaveChanges();
        }

        internal static List<MoreCargoInfo> GetCargosMoreInfoByShevNum(string searchWords)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = db.shelvesCargos.Where(e => e.shelveId == int.Parse(searchWords)).ToList();
                List<MoreCargoInfo> moreinfos = new();
                items.ForEach(i =>
                {
                    var tmp = db.cargos.FirstOrDefault(s => s.cargoId == i.cargoId);
                    var po = db.putCargos.FirstOrDefault(s => s.cargoId == i.cargoId);
                    if (tmp != null && po != null)
                    {
                        MoreCargoInfo moreinfo = new()
                        {
                            cargoId = i.cargoId,
                            cargoName = tmp.cargoName,
                            position = po.position
                        };
                        moreinfos.Add(moreinfo);
                    }

                });
                return moreinfos;
            };
        }

        internal static Cargos findyById(int v)
        {
            ShelvesDbContext db = new();
            return db.cargos.FirstOrDefault(a => a.cargoId == v);
        }

        internal static List<MoreCargoInfo> GetCargosMoreInfoByTagCode(string searchWords)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = db.tags.FirstOrDefault(e => e.tagCode == searchWords);
                List<MoreCargoInfo> moreinfos = new();
                if (items != null)
                {
                    var po = db.putCargos.Where(s => s.tagId == items.tgaId).ToList();
                    po.ForEach(i =>
                    {
                        var tmp = db.cargos.FirstOrDefault(s => s.cargoId == i.cargoId);
                        if (tmp != null)
                        {
                            MoreCargoInfo moreinfo = new()
                            {
                                cargoId = i.cargoId,
                                cargoName = tmp.cargoName,
                                position = i.position
                            };
                            moreinfos.Add(moreinfo);
                        }

                    });
                    return moreinfos;
                }
                else
                {
                    return null;
                }
            
            };
        }

        internal static List<MoreCargoInfo> GetCargosMoreInfoByShevName(string searchWords)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = db.shelves.Where(e => EF.Functions.Like(e.shelveName, "%" + searchWords + "%")).ToList();
                List<MoreCargoInfo> moreinfos = new();
                
                items.ForEach(i =>
                {
                    var tmp = db.shelvesCargos.Where(s => s.shelveId == i.shelveId).ToList();
                    
                    if (tmp != null )
                    {
                        tmp.ForEach(t =>
                        {
                            var cargo = db.cargos.FirstOrDefault(c => c.cargoId == t.cargoId);
                            var po = db.putCargos.FirstOrDefault(s => s.cargoId == cargo.cargoId);
                            MoreCargoInfo moreinfo = new()
                            {
                                cargoId = cargo.cargoId,
                                cargoName = cargo.cargoName,
                                position = po.position
                            };
                            moreinfos.Add(moreinfo);
                        });
                     
                    }

                });
                return moreinfos;
            };
        }
    }
    public class CargosPageData
    {
        public List<Cargos>? TagList { get; set; }
        public long count;
    }
    public class MoreCargoInfo
    {
        public int cargoId { get; set; }
        public string cargoName { get; set; }
        public string position  { get; set; }

    }
}
