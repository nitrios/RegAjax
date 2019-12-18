using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegAjax.Data.Entities
{
    public class Registration
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        
        [Required] public string FirstName { get; set; }
        
        [Required] public string SecondName { get; set; }
        
        [Required] public string Phone { get; set; }
    }
}