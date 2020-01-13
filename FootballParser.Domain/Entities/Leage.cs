using System.Collections.Generic;

namespace FootballParser.Domain.Entities
{
    public class Leage : BaseEntity
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
    }
}
