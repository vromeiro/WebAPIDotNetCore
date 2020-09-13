using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPIDotNetCore.Domain.Entities
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        [Required]
        [MaxLength(50)]
        public string School { get; set; }

        [Required]
        [MaxLength(50)]
        public string HouseId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Patronus { get; set; }
    }
}
