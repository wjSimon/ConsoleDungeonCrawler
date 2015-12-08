using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RangeTrait : ITraitBehaviour
{
    private float range;


    public RangeTrait(float range)
    {
        this.range = range;
    }
    public void Execute(Actor actor)
    {
        actor.Weapon.content.range += range;
    }

    public void OnRemove(Actor actor)
    {
        actor.Weapon.content.range -= range;
    }
}
