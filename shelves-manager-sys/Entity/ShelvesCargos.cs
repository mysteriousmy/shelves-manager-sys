using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 货架货物信息表
namespace shelves_manager_sys.Entity
{
    public class ShelvesCargos
    {
        [Key]
        public int recordId { get; set; }
        //导航属性
        public int shelveId { get; set; }
        public Shelves? shelves { get; set; }
        public int cargoId { get; set; }
        public Cargos? cargos { get; set; }

        [Required, MaxLength(1)]
        public int layer { get; set; }
        [Required]
        public Boolean isUse { get; set; }
    }
}
