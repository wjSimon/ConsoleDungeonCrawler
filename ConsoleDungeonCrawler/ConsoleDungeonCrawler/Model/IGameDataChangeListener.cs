
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//You know how it is by now. Dont touch interfaces.
public interface IGameDataChangeListener
{
    void OnGameDataChange(GameData data);
}