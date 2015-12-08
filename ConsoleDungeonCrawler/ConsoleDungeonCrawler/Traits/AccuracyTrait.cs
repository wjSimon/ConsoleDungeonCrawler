using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class AccuracyTrait : ITraitBehaviour
{
    private float acc;

    public AccuracyTrait()
    {

    }
    public AccuracyTrait(float a)
    {
        acc = a;
    }

    public void Execute(Actor actor)
    {
        actor.Weapon.content.accuracy += acc;
    }

    public void OnRemove(Actor actor)
    {
        actor.Weapon.content.accuracy -= acc;
    }
}
