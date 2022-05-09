using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// 管理员实体模型类
namespace shelves_manager_sys.Entity
{
    public class Admins
    {
        [Key]
        public int adminId { get; set; }
        [MaxLength(30)] //代表其属性在数据库和代码中都将被限制最大长度为30
        [Required]// 代表必须要给该属性赋值
        public string? adminName { get; set; }
        [Required, MaxLength(50)]
        public string? adminPassword { get; set; }
        [Required, MaxLength (1)]
        public int adminRole { get; set; }
        [Required]
        public Boolean autoLogin { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime createTime { get; set; }

    }
}
