using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DamageTrait : ITraitBehaviour
{
    private int dmg;

    public DamageTrait(int dmg)
    {
        this.dmg = dmg;
    }
    public void Execute(Actor actor)
    {
        actor.Weapon.content.damage += dmg;
    }

    public void OnRemove(Actor actor)
    {
        actor.Weapon.content.damage -= dmg;
    }
}


