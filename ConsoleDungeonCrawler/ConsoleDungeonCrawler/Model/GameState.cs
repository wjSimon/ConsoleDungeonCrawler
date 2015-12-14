using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameState : IBaseState
{
    public GameStates state = new GameStates();

    public GameState()
    {
    }

    public GameStates Get()
    {
        return state;
    }

    public void Enter(GameStates state)
    {
        //Only important function here, sets the correct controller for the correct gamestate
        this.state = state;

        if (state == GameStates.GAME) MasterControlProgram.SetController(new ConsolePlayerController());
        if (state == GameStates.MENU)
        {
            MasterControlProgram.SetController(new ConsoleMenuController());
        }
        if (state == GameStates.MAPS) MasterControlProgram.SetController(new ConsoleMapSelectionController());
        if (state == GameStates.FINISH)
        {
            MasterControlProgram.SetController(new ConsoleEndScreenController());
        }
    }

    public void Execute()
    {
        throw new NotImplementedException();
    }

    public void Exit()
    {
        throw new NotImplementedException();
    }

    public void GetEnemies()
    {
        // TODO implement here
    }

    public void GetPlayer()
    {
        // TODO implement here
    }

    public void Update()
    {
        // TODO implement here
    }
}