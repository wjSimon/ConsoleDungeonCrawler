using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Can be applied to Player + Enemies, its hilariously OP though if enemies would have it, so I didn't give them a way to apply it.
// If you want to, make sure to edit the combatlog entry accordingly
/// <summary>
/// Sets the targets NEXT turn's actions to 0. Choose "temp" Trait name when initiating a Trait with this behaviour.
/// </summary>
public class HookedTrait : ITraitBehaviour
{
    public void Execute(Actor actor)
    {
        Application.GetData().combatlog.Add("Enemy succesfully hooked");
    }

    public void OnRemove(Actor actor)
    {
        //This is a very specific Traitbehaviour, only works correctly with the "temp" Trait tag
        actor.actions = 0;
    }


    //This + HeavyInjuryTrait kind of prove that you can make ANYTHING out of this system if you know how to use it.
}
