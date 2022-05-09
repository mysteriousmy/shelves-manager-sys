using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//货物实体信息表
namespace shelves_manager_sys.Entity
{
    public class Cargos
    {
        [Key]
        public int cargoId { get; set; }
        [Required]
        public string? cargoName { get; set; }

        //导航属性
        public ShelvesCargos? shelveCargos { get; set; }
        public OutCargos? outCargos { get; set; }
        public PutCargos? putCargos { get; set; }
    }
}
