
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Used for the Armor + Weapon Slots for the Actors, but very expandable
/// </summary>
/// <typeparam name="T"></typeparam>
public class Slot<T> where T : Item
{

    public Slot()
    {
    }

    private string TYPE;
    public T content;

    public T Equip(T item)
    {
        // TODO implement here
        return null;
    }
    public T Unequip()
    {
        // TODO implement here
        return null;
    }
    public T Get()
    {
        // TODO implement here
        return null;
    }

}