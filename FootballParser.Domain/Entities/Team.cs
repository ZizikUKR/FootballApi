using System;
using System.Collections.Generic;
using System.Text;

namespace FootballParser.Domain.Entities
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }

        public List<Player> Players { get; set; } = new List<Player>();
    }
}
