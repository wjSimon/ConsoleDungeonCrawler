using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConsoleMenuController : IBaseController
{
    public GameData data;
    public GameState state;
    public MenuStates current;
    public List<MenuStates> states;
    public int index;

    public ConsoleMenuController()
    {
        data = Application.GetData();
        state = Application.GetState();
        current = new MenuStates();
        current = MenuStates.NEW;

        states = new List<MenuStates>();
        foreach (MenuStates value in Enum.GetValues(typeof(MenuStates)))
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
                if (current == MenuStates.NEW)
                {
                    Application.ChangeGameState(GameStates.MAPS);
                }
                if (current == MenuStates.CLOSE)
                {
                    Environment.Exit(0);
                }
                break;
        }
    }
}