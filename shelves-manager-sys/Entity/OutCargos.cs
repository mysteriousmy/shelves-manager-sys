using System;
using System.ComponentModel.DataAnnotations;
//出库货物实体模型
namespace shelves_manager_sys.Entity
{
    public class OutCargos
    {
        [Key]
        public int outCargoId { get; set; }
        [Required]
        public int cargoId { get; set; }
        public Cargos? cargos { get; set; }
        [Required]
        public int tagId { get; set; }
        public Tags? tags { get; set; }
        public DateTime createTime { get; set; }
    }
}
