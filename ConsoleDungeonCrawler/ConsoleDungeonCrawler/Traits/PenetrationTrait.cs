using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PenetrationTrait : ITraitBehaviour
{
    private float pen;


    public PenetrationTrait(float pen)
    {
        this.pen = pen;
    }
    public void Execute(Actor actor)
    {
        actor.Weapon.content.penetration += pen;
    }

    public void OnRemove(Actor actor)
    {
        actor.Weapon.content.penetration -= pen;
    }
}
