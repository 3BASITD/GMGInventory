using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Data
{
    public class SalesOrderCounter
    {
        [Key]
        [MaxLength(10)]
        [Column(TypeName = "varchar(10)")]   
        public string Prefix { get; set; } = "";

        public int LastNumber { get; set; }
    }
}
