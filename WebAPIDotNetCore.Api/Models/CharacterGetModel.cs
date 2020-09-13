using System;

namespace WebAPIDotNetCore.Api.Models
{
    public class CharacterGetModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public string School { get; set; }

        public HouseModel House { get; set; }

        public string Patronus { get; set; }
    }
}
