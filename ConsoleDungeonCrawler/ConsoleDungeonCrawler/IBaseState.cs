
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IBaseState
{
    void Enter(GameStates state);
    void Exit();
    void Execute();
}