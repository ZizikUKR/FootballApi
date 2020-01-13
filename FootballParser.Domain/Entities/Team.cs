using System.Collections.Generic;

namespace FootballParser.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }
}
