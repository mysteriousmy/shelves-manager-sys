using Microsoft.EntityFrameworkCore;
using shelves_manager_sys.Entity;
using shelves_manager_sys.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace shelves_manager_sys.DAL
{
    class InventoryDAL
    {
        public static async Task<PutCargosFinal> getBasePageQueryData(int pageIndex, int pageSize)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = await db.putCargos.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                long count = await db.putCargos.LongCountAsync();
                long pageCount = (long)Math.Ceiling(count * 1.0 / pageSize);
                List<PutCargosList> putCargosList = new();
                items.ForEach(i =>
                {
                    var tag = db.tags.First(x => x.tgaId == i.tagId);
                    var cargo = db.cargos.First(x => x.cargoId == i.cargoId);
                    PutCargosList cargosList = new()
                    {
                        putCargoId = i.putCargoId,
                        position = i.position,
                        tagCode = tag.tagCode,
                        cargoName = cargo.cargoName,
                        createTime = i.createTime.ToString("G")
                    };
                    putCargosList.Add(cargosList);
                });
                PutCargosFinal putCargosFinal = new()
                {
                    cargosLists = putCargosList,
                    count = pageCount,
                };
                return putCargosFinal;
            }
        }
        public static async Task<OutCargosFinal> getBasePageQueryOutData(int pageIndex, int pageSize)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = await db.outCargos.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
                long count = await db.outCargos.LongCountAsync();
                long pageCount = (long)Math.Ceiling(count * 1.0 / pageSize);
                List<OutCargosList> outCargosList = new();
                items.ForEach(i =>
                {
                    var tag = db.tags.First(x => x.tgaId == i.tagId);
                    var cargo = db.cargos.First(x => x.cargoId == i.cargoId);
                    OutCargosList cargosList = new()
                    {
                        outCargoId = i.outCargoId,
                        tagCode = tag.tagCode,
                        cargoName = cargo.cargoName,
                        createTime = i.createTime.ToString("G")
                    };
                    outCargosList.Add(cargosList);
                });
                OutCargosFinal outCargosFinal = new()
                {
                    cargosLists = outCargosList,
                    count = pageCount,
                };
                return outCargosFinal;
            }
        }
        //添加入库货物信息
        public static async void addPutCargos(PutCargosDto cargosDto)
        {
            Boolean checkNumRe = await checkPutCargosNum(cargosDto);
            Boolean checkInRe = await checkPutCargosIsIn(cargosDto);
            if (!checkNumRe)
            {
                MessageBox.Show("该货架层已满，请选择别的层数或货架。");
            }else if (!checkInRe)
            {
                MessageBox.Show("该货物已经添加到了货架中，只能更改位置。");
            }
            else
            {
                ShelvesCargos cargos = new()
                {
                    layer = cargosDto.layer,
                    cargoId = cargosDto.cargoId,
                    shelveId = cargosDto.shelveId,
                    isUse = true
                };
                using (ShelvesDbContext db = new ShelvesDbContext())
                {
                    var shelves = await db.shelves.FirstOrDefaultAsync(x => x.shelveId == cargos.shelveId);
                    await db.shelvesCargos.AddAsync(cargos);
                    string position = $"{shelves.shelveName}, 第{cargos.layer}层";
                    PutCargos putCargos = new()
                    {
                        cargoId = cargos.cargoId,
                        tagId = cargosDto.tagId,
                        position = position,
                        createTime = DateTime.Now,
                    };
                    await db.putCargos.AddAsync(putCargos);
                    await db.SaveChangesAsync();
                }
            }
        }
        //更新入库货物信息
        public static void UpdatePutCargos(int putCargoId, PutCargosDto cargosDto)
        {
                ShelvesDbContext db = new ShelvesDbContext();
                var item = db.shelvesCargos.FirstOrDefault(x => x.cargoId == cargosDto.cargoId);
                item.shelveId = cargosDto.shelveId;
                item.layer = cargosDto.layer;

                db.SaveChanges();
                var item2 = db.putCargos.First(x => x.putCargoId == putCargoId);
                item2.tagId = cargosDto.tagId;
                var shelves = db.shelves.First(x => x.shelveId == cargosDto.shelveId);
                string position = $"{shelves.shelveName}, 第{cargosDto.layer}层";
                item2.position = position;
                db.SaveChanges();

        }
        //删除入库货物
        public static async void DeletePutCargos(int putCargoId, PutCargosDto cargosDto)
        {
            using(ShelvesDbContext db = new ShelvesDbContext())
            {
                var item = await db.shelvesCargos.FirstOrDefaultAsync(x => x.shelveId == cargosDto.shelveId && x.cargoId == cargosDto.cargoId);
                db.shelvesCargos.Remove(item);
                await db.SaveChangesAsync();
                var item2 = await db.putCargos.FirstOrDefaultAsync(x => x.putCargoId == putCargoId);
                db.putCargos.Remove(item2);
                await db.SaveChangesAsync();
            }
        }
        //出库
        public static  void OutPutCargos(int putCargoId, PutCargosDto cargosDto)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var item = db.shelvesCargos.First(x => x.shelveId == cargosDto.shelveId && x.cargoId == cargosDto.cargoId);
                db.shelvesCargos.Remove(item);
                db.SaveChanges();
                var item2 = db.putCargos.First(x => x.putCargoId == putCargoId);
                OutCargos outCargos = new()
                {
                    cargoId = item2.cargoId,
                    tagId = item2.tagId,
                    createTime = DateTime.Now,
                };
                db.outCargos.Add(outCargos);
                db.putCargos.Remove(item2);
                db.SaveChanges();
            }
        }
        public static async Task<Boolean> checkPutCargosIsIn(PutCargosDto cargosDto)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = await db.shelvesCargos.Where(x => x.cargoId == cargosDto.cargoId).ToListAsync();
                if (items.Count() != 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            };
        }
        //检查入库货物数量是否达到一层的限制（10个）
        public static async Task<Boolean> checkPutCargosNum(PutCargosDto cargosDto)
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                var items = await db.shelvesCargos.Where(x => x.shelveId == cargosDto.shelveId && x.layer == cargosDto.layer).ToListAsync();
                if(items.Count() < 10)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            };
        }
    }
    public class PutCargosDto
    {
        public int cargoId { get; set; }
        public int tagId { get; set; }
        public int shelveId { get; set; }
        public int layer { get; set; }
    }
    public class PutCargosList
    {
        public int putCargoId   { get; set; }
        public string tagCode { get; set; }
        public string cargoName { get; set; }
        public string position { get; set; }
        public string createTime { get; set; }
    }
    public class OutCargosList
    {
        public int outCargoId { get; set; }
        public string tagCode { get; set; }
        public string cargoName { get; set; }
        public string createTime { get; set; }
    }
    public class PutCargosFinal
    {
        public List<PutCargosList> cargosLists { get; set; }
        public long count;
    }
    public class OutCargosFinal
    {
        public List<OutCargosList> cargosLists { get; set; }
        public long count;
    }
}
