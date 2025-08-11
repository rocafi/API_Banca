using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Banca.Models
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountID { get; set; }
        public int ClientID { get; set; }
        public string? AccountNumber { get; set; }
        public decimal Balance { get; set; }

    }

    public class AccountDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "El saldo es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El saldo debe ser un valor positivo")]
        public decimal Balance { get; set; }
    }
}