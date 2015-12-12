
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PickUp : GameObject
{
    private Random rng = new Random();

    public PickUp()
    {
        this.item = new Item();
        this.count = 1;
    }

    public PickUp(Item item, int count)
    {
        this.item = item;
        this.count = count;
        this.type = item.type;
    }

    public PickUp(Item item, string type, int count)
    {
        this.item = item;
        this.type = type;
        this.count = count;
    }

    public Item item;
    public string type;
    public int count;

    public void OnPickup()
    {
        GameData data = Application.GetData();
        //Sequence spawn conditions
        //Checks for the pre-assigned item-type, then processes
        #region Ammo
        //Refills ammo, does not add any item
        if (item.type == "ammo")
        {
            data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */"Ammunition crate found.");
            for (int i = 0; i < data.inventory.content.Count; i++)
            {
                if (data.inventory.content[i].item.type == "weap")
                {
                    Weapon temp = (Weapon)data.inventory.content[i].item;

                    if (temp.ammo == temp.maxAmmo)
                        continue;

                    temp.ammo += (int)(temp.clipsize * 1.5);

                    if (temp.ammo > temp.maxAmmo)
                        temp.ammo = temp.maxAmmo;

                    data.inventory.content[i].item = temp;

                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ '"' + data.inventory.content[i].item.name + '"' + " ammunition restored.");
                    data.score.AddScore(10);
                }
            }
        }
        #endregion
        #region Medkit
        //Refills health, does not add any item
        else if (item.type == "med")
        {
            data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */"Medkit found. Player health restored.");
            data.player.health = data.player.maxHealth;
            data.score.AddScore(10);
        }
        #endregion
        #region Weapon
        //Checks if you have all weapons already, if not it randomly rolls you one weapon that you do not have yet
        else if (item.type == "weap")
        {
            bool added = false;
            bool NotAllWeapons = true;
            int count = 0;

            for (int i = 0; i < data.inventory.content.Count; i++)
            {
                if (data.inventory.content[i].item.type == "weap") count++;
                if (count == ItemLibrary.Get().weaponList.Count) NotAllWeapons = false;
            }

            count = 0;

            while (NotAllWeapons)
            {
                int current = rng.Next(0, ItemLibrary.Get().weaponList.Count);
                this.item = ItemLibrary.Get().weaponList[current];

                if (!data.inventory.Contains(this.item))
                {
                    data.inventory.Add(this.item, this.count);
                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Weaponcase found. " + this.item.name + " added to inventory.");

                    added = true;
                    data.score.AddScore(150);
                    break;
                }

                count++;
            }
            if (!added)
            {
                data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Empty Weaponcase found. Proceeding...");
            }
        }
        #endregion
        #region Armor
        else if (item.type == "armor")
        {
            bool added = false;
            int count = 0;

            for (int i = 0; i < data.inventory.content.Count; i++)
            {
                if (data.inventory.content[i].item.type == "armor") count++;
                if (count == ItemLibrary.Get().armorList.Count) return;
            }

            count = 0;

            while (true)
            {
                int current = rng.Next(0, ItemLibrary.Get().armorList.Count);
                this.item = ItemLibrary.Get().armorList[current];

                if (!data.inventory.Contains(this.item))
                {
                    data.inventory.Add(this.item, this.count);
                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Armor found. " + this.item.name + " was added to the inventory.");

                    added = true;
                    data.score.AddScore(150);
                    break;
                }

                count++;
            }
            if (!added)
            {
                data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Debug no armor found. Proceeding...");
            }
        }
        #endregion
        #region Grenades
        else if (item.type == "grenade")
        {
            int current = rng.Next(0, ItemLibrary.Get().grenadeList.Count);
            this.item = ItemLibrary.Get().grenadeList[current];
            data.inventory.Add(this.item, this.count);
            data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Grenade found. Added " + this.item.name + " to inventory.");
            data.score.AddScore(10);
        }
        #endregion
        #region ammo_mods
        else if (item.type == "use")
        {
            int current = rng.Next(0, ItemLibrary.Get().usableList.Count);
            this.item = ItemLibrary.Get().usableList[current];
            data.inventory.Add(this.item, this.count);
            data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Mod found. Added " + this.item.name + " to inventory.");
            data.score.AddScore(10);
        }
        #endregion
        #region Keycards
        else if (item.type == "key")
        {
            bool added = false;
            int count = 0;

            for (int i = 0; i < data.inventory.content.Count; i++)
            {
                if (data.inventory.content[i].item.type == "key") count++;
                if (count == ItemLibrary.Get().keyList.Count) return;
            }

            while (true)
            {
                int current = rng.Next(0, ItemLibrary.Get().keyList.Count);
                this.item = ItemLibrary.Get().keyList[current];

                if (!data.inventory.Contains(this.item) && this.item.name != "master_key")
                {
                    data.inventory.Add(this.item, this.count);
                    data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ this.item.name + " was added to the inventory.");

                    added = true;
                    data.score.AddScore(50);
                    break;
                }

                count++;
            }
            if (!added)
            {
                data.combatlog.Add(/*DateTime.Now.Hour + ":" + DateTime.Now.Minute + */ "Keycard already in possesion.");
            }
        }
        #endregion

        //If it some other item (which should not happen), it gets added to the inventory raw
        else
        {
            data.inventory.Add(this.item, this.count);
        }

        //Removes the pickUp from the level
        data.level.pickUps.Remove(this);
    }

}