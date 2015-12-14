using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// ITraitBehaviour that modifies the targets DamageType
/// </summary>
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
        //need to store the old damagetype because it gets overwritten for the OnRemove()
        storage = actor.Weapon.content.damagetype;
        actor.Weapon.content.damagetype = type;
    }

    public void OnRemove(Actor actor)
    {
        actor.Weapon.content.damagetype = storage;
    }
}
