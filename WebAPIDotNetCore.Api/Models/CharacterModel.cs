using System.ComponentModel.DataAnnotations;

namespace WebAPIDotNetCore.Api.Models
{
    public class CharacterModel
    {
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Role { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string School { get; set; }

        [Required]
        [MaxLength(50)]
        public string House { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Patronus { get; set; }
    }
}
