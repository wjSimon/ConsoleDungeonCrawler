using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// TriggerObject that triggers once the player position overlays with the object position
/// </summary>
public class TriggerObject : GameObject, ITrigger
{
    public TriggerObject()
    {
        Application.GetData().level.trigger.Add(this);
    }
    public TriggerObject(string name, Vector2 position)
    {
        this.name = name;
        this.position = position;
        Application.GetData().level.trigger.Add(this);
    }

    //Using a naming "tag" system aswell to allow multiple triggers without multiple classes. Only 'when' it triggers is set by the class
    public string OnTriggerEnter()
    {
        Console.Write("TRIGGERED");
        if (this.name == "endoflevel")
        {
            Application.ChangeGameState(GameStates.FINISH); 
        }
        if (this.name == "subsystem")
        {
            Application.GetData().ActivateSubsystem();
            Application.GetData().level.trigger.Remove(this);
            Application.GetData().score.AddScore(100);
        }

        return name;
    }
}
