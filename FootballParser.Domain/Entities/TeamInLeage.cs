using System;

namespace FootballParser.Domain.Entities
{
    public class TeamInLeage : BaseEntity
    {
        public Guid LeageId { get; set; }
        public Guid TeamId { get; set; }
    }
}
