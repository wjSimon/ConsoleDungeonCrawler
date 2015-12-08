using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsoleMapSelectionController : IBaseController
{
    public GameData data;
    public GameState state;
    public MapStates current;
    public List<MapStates> states;
    public int index;

    public ConsoleMapSelectionController()
    {
        data = Application.GetData();
        state = Application.GetState();
        current = new MapStates();
        current = MapStates.RANDOM;

        states = new List<MapStates>();
        foreach (MapStates value in Enum.GetValues(typeof(MapStates)))
        {
            states.Add(value);
        }

        index = 0;
    }

    public void Execute()
    {
        data = Application.GetData();
        state = Application.GetState();

        ConsoleKey input = Console.ReadKey().Key;

        switch (input)
        {
            case ConsoleKey.W:
                if (index <= 0)
                {
                    index = states.Count - 1;
                }
                else
                {
                    index--;
                }

                current = states[index];
                break;
            case ConsoleKey.S:
                if (index >= states.Count - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }

                current = states[index];
                break;
            case ConsoleKey.Enter:

                if (current == MapStates.RANDOM)
                {
                    Application.ChangeGameState(GameStates.GAME);
                    Application.NewGame();
                }
                if (current == MapStates.TINY)
                {
                    MasterControlProgram.map = "tiny.bmp";
                    Application.ChangeGameState(GameStates.GAME);
                    Application.NewGame();
                }
                if (current == MapStates.SMALL)
                {
                    MasterControlProgram.map = "small.bmp";
                    Application.ChangeGameState(GameStates.GAME);
                    Application.NewGame();
                }
                if (current == MapStates.MEDIUM)
                {
                    Application.ChangeGameState(GameStates.GAME);
                    Application.NewGame();
                }
                if (current == MapStates.LARGE)
                {
                    Application.ChangeGameState(GameStates.GAME);
                    Application.NewGame();
                }
                if (current == MapStates.VERYLARGE)
                {
                    Application.ChangeGameState(GameStates.GAME);
                    Application.NewGame();
                }
                if (current == MapStates.WHYWOULDYOU)
                {
                    Application.ChangeGameState(GameStates.GAME);
                    Application.NewGame();
                }
                break;

            case ConsoleKey.Escape:
                Application.ChangeGameState(GameStates.MENU);
                break;

            case ConsoleKey.Backspace:
                Application.ChangeGameState(GameStates.MENU);
                break;
        }
    }
}
