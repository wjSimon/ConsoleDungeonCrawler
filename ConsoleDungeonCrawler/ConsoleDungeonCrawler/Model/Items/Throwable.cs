
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Grenade shell. Assign Impactbehaviours and execute Use()
/// </summary>
public class Throwable : Item
{
    public List<IImpactBehaviour> behaviour = new List<IImpactBehaviour>();
    
    public Throwable()
    {
    }
    public Throwable(string n, string t)
    {
        name = n;
        type = t;
    }
    public Throwable(string n, string t, IImpactBehaviour b)
    {
        name = n;
        type = t;
        behaviour.Add(b);
    }
    public Throwable(string n, string t, List<IImpactBehaviour> b)
    {
        name = n;
        type = t;
        behaviour = b;
    }

    public bool Use()
    {
        GameData data = Application.GetData();

        if (!data.combat)
        {
            return false;
        }

        for (int i = 0; i < behaviour.Count; i++)
        {
            behaviour[i].Execute();
        }
        return true;
    }

    public void AddBehaviour(IImpactBehaviour b)
    {
        behaviour.Add(b);
    }
}