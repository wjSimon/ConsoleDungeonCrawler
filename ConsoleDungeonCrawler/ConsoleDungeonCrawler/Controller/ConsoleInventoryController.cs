using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Inventory Functionality ingame, to add functionality, add keys into the switch
/// </summary>
class ConsoleInventoryController : IBaseController
{
    GameData data = Application.GetData();

    public void Execute()
    {
        int current = data.currentItem;
        ConsoleKey input = Console.ReadKey().Key;

        switch (input)
        {
            case ConsoleKey.W:
                //Console.WriteLine("\nw");
                if (current <= 0)
                {
                    current = data.inventory.content.Count() - 1;
                }
                else
                {
                    current = current - 1;
                }
                break;

            case ConsoleKey.S:
                //Console.WriteLine("\ns");
                if (current >= data.inventory.content.Count() - 1)
                {
                    current = 0;
                }
                else
                {
                    current = current + 1;
                }
                break;
            case ConsoleKey.Enter:
                //Console.WriteLine("\nenter");
                UseItem();
                break;

            case ConsoleKey.I:
                //Console.WriteLine("\ni");
                MasterControlProgram.SetController(new ConsolePlayerController());
                current = -1;
                data.inv = !data.inv;
                break;
        }

        data.currentItem = current;
    }

    private void UseItem()
    {
        Item item = data.inventory.content[data.currentItem].item;

        if (item is Weapon)
        {
            data.player.EquipWeapon((Weapon)item);
        }
        if (item is Armor)
        {
            data.player.EquipArmor((Armor)item);
        }
        if (item is Throwable)
        {
            Throwable t = (Throwable)item;

            data.inventory.content[data.currentItem].count -= 1;
            // == 0 for potential abuse with items that have a count of < 0 to allow easier unlimited use items
            bool temp = t.Use();

            if (data.inventory.content[data.currentItem].count == 0 && temp)
            {
                data.inventory.content.RemoveAt(data.currentItem);
                data.currentItem = -1;
            }

            if(temp)
            {
                data.player.actions--;
                QuitInventory();
            }
        }
        if (item is Usable)
        {
            Usable u = (Usable)item;

            data.inventory.content[data.currentItem].count -= 1;
            // == 0 for potential abuse with items that have a count of < 0 to allow easier unlimited use items

            bool temp = u.Use();

            if (data.inventory.content[data.currentItem].count == 0 && temp)
            {
                data.inventory.content.RemoveAt(data.currentItem);
                data.currentItem = -1;
            }

            if (temp)
            {
                data.player.actions--;
                QuitInventory();
            }
        }
    }

    public void QuitInventory()
    {
        MasterControlProgram.SetController(new ConsolePlayerController());
        data.currentItem = -1;
        data.inv = !data.inv;
    }
}
