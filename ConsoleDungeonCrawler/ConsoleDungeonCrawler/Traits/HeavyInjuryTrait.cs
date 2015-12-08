using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HeavyInjuryTrait : ITraitBehaviour
{
    private int dmg;

    public HeavyInjuryTrait(int dmg)
    {
        this.dmg = dmg;
    }
    public void Execute(Actor actor)
    {
        actor.Weapon.content.accuracy -= 0.15f;
    }

    public void OnRemove(Actor actor)
    {
        actor.TakeDamage(dmg, "true", 0);
    }
}
