using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 入库货物信息实体模型
namespace shelves_manager_sys.Entity
{
    public class PutCargos
    {
        [Key]
        public int putCargoId { get; set; }

        [Required]
        public int cargoId { get; set; }
        public Cargos? cargos { get; set; }
        //导航属性
        [Required]
        public int tagId { get; set; }
        public Tags? tags { get; set; }
        [Required]
        public string? position { get; set; }
        public DateTime createTime { get; set; }
    }
}
