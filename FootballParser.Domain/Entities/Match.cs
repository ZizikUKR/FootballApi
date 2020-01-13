using System;
using System.Collections.Generic;

namespace FootballParser.Domain.Entities
{
    public class Match : BaseEntity
    {
        public List<TeamMatchStatistics> Statistics { get; set; } = new List<TeamMatchStatistics>();
        public Guid LeageId  { get; set; }
    }
}
