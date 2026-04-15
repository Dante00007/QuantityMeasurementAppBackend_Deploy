// See https://aka.ms/new-console-template for more information

using QuantityMeasurementAppConsole.Menu;
using QuantityMeasurementAppModelLayer.Util;
using Microsoft.Extensions.Configuration;

class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        AppConfig.ConnectionString = configuration.GetConnectionString("DefaultConnection");

        Menu menu = new Menu();
        menu.ShowMenu();
    }
}