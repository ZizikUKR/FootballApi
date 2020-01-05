using DbUp;
using System.Reflection;

namespace FootballParser.Domain
{
    public class DbUP
    {
        public static void Update(string connString)
        {
            var assemblyName = Assembly.GetExecutingAssembly();
            var upgrader = DeployChanges.To
                .SqlDatabase(connString)
                .WithScriptsEmbeddedInAssembly(assemblyName)
                .LogToAutodetectedLog()
                .Build();
            var result = upgrader.PerformUpgrade();
        }
    }
}
