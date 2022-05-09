using shelves_manager_sys.Entity;
using shelves_manager_sys.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shelves_manager_sys.DAL
{
    internal class AdminsDAL
    {
        public static Admins LoginCheck(string adminName, string adminPwd)
        {
            ShelvesDbContext db = new ShelvesDbContext();
            return db.admins.SingleOrDefault(s => s.adminName == adminName && s.adminPassword == adminPwd);
        }
    }
}
