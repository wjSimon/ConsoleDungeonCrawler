using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Contains all information that is only avaible for the current Game session. Gets newed once you start a new map etc.
/// </summary>
public class GameData
{
    public Actor player;
    public Inventory inventory;
    public bool inv = false;
    public int currentItem; //The currently selected item in the inventory. Used for stuff all over so I put it here (controller needs it, view needs it, etc.)
    public bool combat;
    public int subsystems;
    public Score score;
    public Level level;
    public Vector2 levelfinish = new Vector2();
    public List<Actor> collision = new List<Actor>();
    public List<string> combatlog = new List<string>()
    {
        "|============|",
        "|////////////|",
        "|//PR0T0L0G//|",
        "|////////////|",
        "|============|"
    };

    public GameData()
    {
        this.player = new Actor();
        this.inventory = new Inventory();
        this.score = new Score();
        this.level = new Level();
        this.combat = new bool();
        this.score = new Score();
        this.subsystems = 0;
        this.currentItem = -1;
    }

    public void SpawnPlayer()
    {
        player = new Actor();
        player.health = 10;
        player.maxHealth = 10;
        
        Application.GetData().inventory.Add(player.Weapon.content, 1); //Player gets initiated with Weapon/Armor in slot already, this makes sure he can
        Application.GetData().inventory.Add(player.Armor.content, 1); //actually access them

        Application.GetData().inventory.Add(ItemLibrary.Get().usableList[1], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().weaponList[2], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().grenadeList[1], 1);

        //DEBUGGING ONLY, DONT TOUCH IF YOU DONT KNOW WHAT YOU'RE DOING
        /*
        Application.GetData().inventory.Add(ItemLibrary.Get().items[0], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().weaponList[5], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().grenadeList[0], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().grenadeList[1], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().usableList[0], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().usableList[1], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().usableList[2], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().weaponList[1], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().weaponList[2], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().weaponList[3], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().weaponList[4], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().weaponList[5], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().armorList[0], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().armorList[1], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().armorList[2], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().armorList[3], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().keyList[0], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().keyList[1], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().keyList[2], 1);
        Application.GetData().inventory.Add(ItemLibrary.Get().keyList[3], 1);
        //
        /**/

        Random rng = new Random();
        player.position = level.playerSpawnPoints[rng.Next(0, 4)];

        Application.GetData().collision.Add(player);
    }

    /// <summary>
    /// Little help here, since they're named a little confusing.
    /// Those actually don't activate them in level or anything, but happen AFTER a subsystem has been triggered.
    /// If you want anything special to happen when the player activates a certain number of subsystems, do it here.
    /// If you have levels that use more/less than 3 subsystems mixed with levels that do use 3, you could rewrite so
    /// it uses the amount of subsystems in the level found in level.trigger.count(-1 if it has the levelfinish active).
    /// </summary>
    public void ActivateSubsystem()
    {
        subsystems++;
        if (subsystems == 1)
        {
            Application.GetData().inventory.Add(ItemLibrary.Get().items[0], 1);
            Application.GetData().inventory.Add(ItemLibrary.Get().items[2], 1);
        }
        if (subsystems == 2)
        {
            Application.GetData().inventory.Add(ItemLibrary.Get().keyList[4], 1);
        }
        if (subsystems >= 3)
        {
            for (int i = 0; i < Application.GetData().inventory.content.Count; i++)
            {
                if (Application.GetData().inventory.content[i].item.type == "weap")
                {
                    Weapon w = (Weapon)Application.GetData().inventory.content[i].item;
                    w.currentammo = w.clipsize;
                    w.ammo = w.maxAmmo;
                }
            }

            ActivateLevelFinish();
        }
    }
    //Since its dynamic, we have to store it here instead of the LevelGenerator, the position (levelfinish) is set during the generation process however
    public void ActivateLevelFinish()
    {
        level.trigger.Add(new TriggerObject("endoflevel", levelfinish));
    } 
}