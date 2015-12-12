
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Makes sure that all Objects in the game have atleast a position and an identifier (even if empty)
public class GameObject
{

    public GameObject()
    {
    }

    public string name;
    public Vector2 position;

    public void Destroy()
    {
        // TODO implement here
    }

}