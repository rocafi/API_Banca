using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Banca.Models
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionID { get; set; }
        public string? AccountNumber { get; set; }
        public string? TransactionType { get; set; }
        public decimal Amount { get; set; }
        public decimal InitialBalance { get; set; }
        public decimal FinalBalance { get; set; }
        public DateOnly CreatedAt { get; set; }

    }

    public class TransactionDTO
    {
        public string? AccountNumber { get; set; }
        [Required(ErrorMessage = "El tipo de transacción es obligatorio")]
        public string? TransactionType { get; set; }
        [Required(ErrorMessage = "El monto es obligatorio")]
        public decimal Amount { get; set; }
    }
}
