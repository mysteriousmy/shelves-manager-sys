using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shelves_manager_sys.Entity
{
    public class Tags
    {
        [Key]
        public int tgaId { get; set; }
        [Required]
        public string? tagCode{ get; set; }

        //导航属性
        public OutCargos? outCargos { get; set; }
        public PutCargos? putCargos { get; set; }
    }
}
