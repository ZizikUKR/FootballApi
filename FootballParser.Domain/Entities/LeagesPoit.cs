using System;

namespace FootballParser.Domain.Entities
{
    public class LeagesPoit
    {
        public Guid TeamId  { get; set; }

        public int ScoredGoals { get; set; }

        public int MissedGoals { get; set; }

        public int Points { get; set; }
    }
}
