using Dapper.Contrib.Extensions;
using System;

namespace FootballParser.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
