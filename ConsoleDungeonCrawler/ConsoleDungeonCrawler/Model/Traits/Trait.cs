
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Trait
{
    public int duration;
    public string name;
    public List<ITraitBehaviour> behaviour = new List<ITraitBehaviour>();

    public Trait()
    {
    }

    public Trait(ITraitBehaviour behaviour)
    {
        this.behaviour.Add(behaviour);
    }
    public Trait(int duration, string name, ITraitBehaviour behaviour)
    {
        this.duration = duration;
        this.name = name; //VERY IMPORTANT; The name of a trait is the "tag" that is used to remove then at various points in the program. So make sure you choose the correct one for what you're trying to do
        this.behaviour.Add(behaviour);
    }
    public Trait(int duration, string name, List<ITraitBehaviour> behaviour)
    {
        this.duration = duration;
        this.name = name;
        this.behaviour.AddRange(behaviour);
    }
    public void AddTrait(ITraitBehaviour trait)
    {
        behaviour.Add(trait);
    }
    public void Remove(Actor actor)
    {
        //Calls the OnRemove() for all Behaviours on this Trait for the Actor this Trait is associated with
        for (int i = 0; i < behaviour.Count; i++)
        {
            behaviour[i].OnRemove(actor);
        }
    }
}