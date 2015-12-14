using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initializes everything necessary to run the application independently
            MasterControlProgram MCP = new MasterControlProgram();
            ConsoleMenuController menuController = new ConsoleMenuController();
            ConsolePlayerController playerController = new ConsolePlayerController();
            ConsoleView view = new ConsoleView();

            //Console.SetWindowSize(236, 80);
            Console.SetWindowSize(60, 60);
            MasterControlProgram.SetController(menuController);
            MasterControlProgram.SetView(view);
            MCP.Run();
            return;
        }
    }
}
