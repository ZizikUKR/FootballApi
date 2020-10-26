using System.Collections.Generic;

namespace FootballParser.Domain.Entities
{
    public class LiveMatch
    {
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string TimeLines { get; set; }
        public int AwayGoals { get; set; }
        public int HomeGoals { get; set; }
        public List<LiveStatistic> Statistics { get; set; } = new List<LiveStatistic>();

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
        public string AwayTeamValue { get; set; }
        public string HomeTeamValue { get; set; }
        public string StatisticName { get; set; }

        public string Value { get; set; }
    }
}
