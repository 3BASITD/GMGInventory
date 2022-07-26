using System.ComponentModel.DataAnnotations.Schema;

namespace GMGInventory.Data
{
    public class CreditDebitMemoType : BaseTypeEntity
    {
        public CreditDebitMemoType()
        {
            this.CreditDebitMemos = new HashSet<CreditDebitMemo>();
        }

        public ICollection<CreditDebitMemo> CreditDebitMemos { get; set; }
    }
}
