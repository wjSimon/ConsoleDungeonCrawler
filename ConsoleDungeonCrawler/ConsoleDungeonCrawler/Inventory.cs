
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Inventory
{
    public List<ItemWrapper> content = new List<ItemWrapper>();
    public Inventory()
    {

    }

    public void Add(Item item, int count)
    {
        bool newItem = true;
        ItemWrapper wrapper = new ItemWrapper(item, count);

        /*if(item.type == "cons")
        {
            return;
        }
        if(item.type == "ammo")
        {
        }
         * */

        //if item exists within inventory, count up
        for (int i = 0; i < content.Count; i++)
        {
            if (content[i].item.name == wrapper.item.name)
            {
                content[i].count += 1;
                newItem = false;
            }
        }

        //if item not found, add it as new item with count 1
        if (newItem)
        {
            content.Add(wrapper);
        }
    }

    public bool Contains(Item item)
    {
        bool contains = false;

        for (int i = 0; i < content.Count; i++)
        {
            if (content[i].item.name == item.name)
            {
                contains = true;
            }
        }

        return contains;
    }

    public void Remove(Item item)
    {
        // TODO implement here
    }

    public ItemWrapper Get(int index)
    {
        return content[index];
    }

}