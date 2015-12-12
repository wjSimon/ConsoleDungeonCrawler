
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Inventory class is assigned to GameData. The player character has one universal inventory containing all items in possesion
/// </summary>
public class Inventory
{
    public List<ItemWrapper> content = new List<ItemWrapper>();
    public Inventory()
    {

    }

    public void Add(Item item, int count)
    {
        //Handles the ItemWrappers to stack items. New items get a new Wrapper, mutliples instead increase the count of the itemWrappper with the item already existing
        bool newItem = true;
        ItemWrapper wrapper = new ItemWrapper(item, count);

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
        //Checks if the item is in the inventory already, used in PickUp()
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

    //Not used because we handle it via the list directly. If you need more functionality than JUST the removal when removing an item, switch to this function
    public void Remove(Item item)
    {
        // TODO implement here
    }

    public ItemWrapper Get(int index)
    {
        return content[index];
    }

}