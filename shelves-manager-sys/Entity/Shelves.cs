using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 货架信息实体模型
namespace shelves_manager_sys.Entity
{
    public class Shelves
    {
        [Key]
        public int shelveId { get; set; }
        [Required]
        public string? shelveName { get; set; }
        [Required]
        public Boolean isOnline { get; set; }

        // 导航属性
        public ShelvesCargos? shelvesCargos { get; set; }
    }
}
