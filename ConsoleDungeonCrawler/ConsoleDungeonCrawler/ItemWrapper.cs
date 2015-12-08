
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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