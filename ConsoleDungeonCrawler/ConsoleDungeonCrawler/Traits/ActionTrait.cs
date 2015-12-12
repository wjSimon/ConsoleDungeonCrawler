using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Pretty much the same as AccuracyTrait, just with Actions. DamageTrait, PenetrationTrait, RangeTrait share this exact same functionality
/// <summary>
/// ITraitBehaviour that increases the targets Actions
/// </summary>
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

