using System;

namespace FootballParser.Domain.Entities
{
    public class Player : BaseEntity
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public Guid TeamId { get; set; }
    }
}
