using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HookedTrait : ITraitBehaviour
{
    public void Execute(Actor actor)
    {
        Application.GetData().combatlog.Add("Enemy succesfully hooked");
    }

    public void OnRemove(Actor actor)
    {
        actor.actions = 0;
    }
}
