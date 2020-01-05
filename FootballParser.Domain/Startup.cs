using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FootballParser.Domain
{
    public class Startup
    {
        public static void Configure(string connectionString, IServiceCollection services)
        {
            DbUP.Update(connectionString);
        }
    }
}
