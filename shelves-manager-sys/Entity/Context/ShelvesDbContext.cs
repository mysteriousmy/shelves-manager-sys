using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shelves_manager_sys.Entity.Context
{
    internal class ShelvesDbContext : DbContext
    {
        public ShelvesDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;database=smart-shelves;user=root;password=12345678");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 管理员信息表
            modelBuilder.Entity<Admins>().Property(x => x.adminId).ValueGeneratedOnAdd();//设置主键自增
            #endregion

            #region 标签信息表
            modelBuilder.Entity<Tags>().Property(x => x.tgaId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Tags>().HasOne(x => x.putCargos).WithOne(t => t.tags).HasForeignKey<PutCargos>(h => h.tagId);
            modelBuilder.Entity<Tags>().HasOne(x => x.outCargos).WithOne(t => t.tags).HasForeignKey<OutCargos>(h => h.tagId);
            #endregion

            #region 货物信息表
            modelBuilder.Entity<Cargos>().Property(x => x.cargoId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Cargos>().HasOne(x => x.shelveCargos).WithOne(c => c.cargos).HasForeignKey<ShelvesCargos>(h => h.cargoId);
            modelBuilder.Entity<Cargos>().HasOne(x => x.putCargos).WithOne(t => t.cargos).HasForeignKey<PutCargos>(h => h.cargoId);
            modelBuilder.Entity<Cargos>().HasOne(x => x.outCargos).WithOne(t => t.cargos).HasForeignKey<OutCargos>(h => h.cargoId);
            #endregion

            #region 入库信息表
            modelBuilder.Entity<PutCargos>().Property(x => x.putCargoId).ValueGeneratedOnAdd();
            #endregion

            #region 出库信息表
            modelBuilder.Entity<OutCargos>().Property(x => x.outCargoId).ValueGeneratedOnAdd();
            #endregion

            #region 货架信息表
            modelBuilder.Entity<Shelves>().Property(x => x.shelveId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Shelves>().HasOne(x => x.shelvesCargos).WithOne(t => t.shelves).HasForeignKey<ShelvesCargos>(h => h.shelveId);
            #endregion

            #region 货架货物信息表
            modelBuilder.Entity<ShelvesCargos>().Property(x => x.recordId).ValueGeneratedOnAdd();
            #endregion
        }
        public DbSet<Admins> admins { get; set; }
        public DbSet<Cargos> cargos { get; set; }
        public DbSet<Tags> tags { get; set; }
        public DbSet<OutCargos> outCargos { get; set; }
        public DbSet<PutCargos> putCargos { get; set; }
        public DbSet<Shelves> shelves { get; set; }
        public DbSet<ShelvesCargos> shelvesCargos { get; set; }

    }
}
