namespace FootballParser.Domain.Entities
{
    public class TeamMatchStatistics : BaseEntity
    {
        public int ShotsOnTarge { get; set; }
        public int BigChances { get; set; }
        public int ShotsInsideBox { get; set; }
        public int ShotsOutsideBox { get; set; }
        public int Crosses { get; set; }
        public int Goals { get; set; }
    }
}
