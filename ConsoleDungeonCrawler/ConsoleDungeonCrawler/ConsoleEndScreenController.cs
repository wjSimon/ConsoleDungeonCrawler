using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsoleEndScreenController : IBaseController
{
    public void Execute()
    {
        Console.ReadKey();
        Application.ChangeGameState(GameStates.MENU);
    }
}
