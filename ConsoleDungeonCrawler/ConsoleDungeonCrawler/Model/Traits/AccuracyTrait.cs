using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// ITraitBehaviour that increases the targets Accuracy
/// </summary>
class AccuracyTrait : ITraitBehaviour
{
    private float acc;

    /// <summary>
    /// AccuracyTraitConstructor
    /// </summary>
    /// <param name="a">Value for accuracy increment</param>
    public AccuracyTrait(float a)
    {
        acc = a;
    }

    //Gets called when the Trait it's stored in gets applied to an Actor
    public void Execute(Actor actor)
    {
        actor.Weapon.content.accuracy += acc;
    }

    //All TraitBehaviours have the OnRemove(), gets called when the Trait it'S stored in gets removed from an Actor - Usually just gets rid of the Execute() modification, sometimes does some extra stuff
    public void OnRemove(Actor actor)
    {
        actor.Weapon.content.accuracy -= acc;
    }
}
