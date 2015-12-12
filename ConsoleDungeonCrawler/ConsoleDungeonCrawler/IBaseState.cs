
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Gamestate Interface. Dont touch.
public interface IBaseState
{
    void Enter(GameStates state);
    void Exit();
    void Execute();
}