using System.Reflection;
using DbUp;


var connectionString = args.FirstOrDefault()
    ?? "Server=ALINA\\TEW_SQLEXPRESS;Database=TaskTrackerDatabase;Trusted_Connection=true;TrustServerCertificate=true;";



// cr db
EnsureDatabase.For.SqlDatabase(connectionString);

// Configuring the upgrade
var upgrader = DeployChanges.To
    .SqlDatabase(connectionString)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly()) 
    .LogToConsole()
    .Build();

// Running migrat
var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
return 0;
