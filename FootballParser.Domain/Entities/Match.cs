using System;

namespace FootballParser.Domain.Entities
{
    public class Match : BaseEntity
    {
        public TeamMatchStatistics Statistics { get; set; }
        public Guid StatisticId { get; set; }
        public DateTime FootballDate { get; set; }
        public Guid TeamId { get; set; }
        public Guid LeageId  { get; set; }
    }
}
