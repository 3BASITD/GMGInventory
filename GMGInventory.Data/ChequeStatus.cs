using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMGInventory.Data
{
    public class ChequeStatus : BaseTypeEntity
    {
        public ChequeStatus()
        {
            this.Cheques = new HashSet<Cheque>();
        }

        public ICollection<Cheque> Cheques { get; set; }
    }
}
