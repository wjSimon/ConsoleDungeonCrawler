using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Handles the Main Menu, gets assinged automatically when the state is switched to Menu.
/// </summary>
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

        //Creates a list with the content of the MenuStates() enum so we can access them via index
        states = new List<MenuStates>();
        foreach (MenuStates value in Enum.GetValues(typeof(MenuStates)))
        {
            states.Add(value);
        }

        index = 0;
    }

    public void Execute()
    {
        //inputs scroll through the list via increment/decrement of the index
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

                //current is set to the state corresponding to the index
                current = states[index];
                break;

            //Enter checks what menustate current is and performs according action
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