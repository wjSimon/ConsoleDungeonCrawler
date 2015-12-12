
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Saves an item and a count for said item, that way we can store multiples of an item without multiple instances of the same item
/// </summary>
public class ItemWrapper
{
    public ItemWrapper(Item item, int count)
    {
        this.item = item;
        this.count = count;
    }

    public Item item;
    public int count;
    public int maxstack;


    public Item Get()
    {
        // TODO implement here
        return null;
    }
}