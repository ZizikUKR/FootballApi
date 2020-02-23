using System.Collections.Generic;

namespace FootballParser.Domain.Entities
{
    public class LiveMatch
    {
        public LiveTeam HomeTeam { get; set; }
        public LiveTeam AwayTeam { get; set; }
    }

    public class LiveTeam
    {
        public string Name { get; set; }

        public List<LiveStatistic> Statistics { get; set; } = new List<LiveStatistic>();
    }

    public class LiveStatistic
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
