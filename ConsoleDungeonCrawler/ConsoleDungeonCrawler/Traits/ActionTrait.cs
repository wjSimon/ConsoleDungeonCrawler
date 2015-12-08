using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ActionTrait : ITraitBehaviour
{
    private int actions;

    public ActionTrait(int actions)
    {
        this.actions = actions;
    }

    public void Execute(Actor actor)
    {
        actor.maxActions += actions;
    }

    public void OnRemove(Actor actor)
    {
        actor.maxActions -= actions;
    }
}

