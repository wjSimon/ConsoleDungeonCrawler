using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsoleEndScreenController : IBaseController
{
    //Needs its own controller to ensure that view is not couppled
    public void Execute()
    {
        Console.ReadKey();
        Application.ChangeGameState(GameStates.MENU);
    }
}
