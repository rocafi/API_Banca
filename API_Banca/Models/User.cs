using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Banca.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }
        public string? Name { get; set; }
        public DateOnly Birthday { get; set; }
        public char Gender { get; set; }
        public decimal? Incommes { get; set; }
        public DateOnly CreatedAt { get; set; }
        public string? AccountNumber { get; set; }

    }
    public class UserDTO
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no puede exceder los 20 caracteres")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Range(typeof(DateOnly), "1900-01-01", "2100-12-31", ErrorMessage = "La fecha de nacimiento debe estar entre 1900 y 2100")]
        public DateOnly Birthday { get; set; }

        [Required(ErrorMessage = "El género es obligatorio")]
        [RegularExpression("^[MF]$", ErrorMessage = "El género debe ser 'M' o 'F'")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Los ingresos son requeridos")]
        [Range(0, double.MaxValue, ErrorMessage = "Los ingresos deben ser un valor positivo")]
        public decimal? Incommes { get; set; }

    }

}