using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DamageTypeTrait : ITraitBehaviour
{
    private string type;
    private string storage;

    public DamageTypeTrait(string type)
    {
        this.type = type;
    }
    public void Execute(Actor actor)
    {
        storage = actor.Weapon.content.damagetype;
        actor.Weapon.content.damagetype = type;
    }

    public void OnRemove(Actor actor)
    {
        actor.Weapon.content.damagetype = storage;
    }
}
