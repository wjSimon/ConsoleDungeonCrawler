using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// LIBRARY CONTAINING ALL ITEMS USED IN THE GAME, SORTED BY VARIOUS LISTS
/// COMPARABLE TO UNITY'S PREFAB SYSTEM.
/// 
/// If you want items to appear in the game that are not randomized in stats, add them here.
/// Just make sure they're in the correct list.
/// 
/// Makes it very easy to randomize items from an existing pool, like this system a lot.
/// Made it a singleton so we dont have hundereds of lists with LOTS of "new" stuff in them,
/// Memory might not like it. Same system is used for all enemies, since they're random too.
/// Just like this comment. ALL the comments in this project, actually.
/// Maybe there's a list of them somewhere too?
/// </summary>
public class ItemLibrary
{
    private static ItemLibrary instance;
    public List<Item> items = new List<Item>();
    public List<Item> generics = new List<Item>();
    public List<Item> keyList = new List<Item>();
    public List<Weapon> weaponList = new List<Weapon>();
    public List<Weapon> enemyweaponList = new List<Weapon>();
    public List<Armor> armorList = new List<Armor>();
    public List<Armor> enemyarmorList = new List<Armor>();
    public List<Throwable> grenadeList = new List<Throwable>();
    public List<Item> usableList = new List<Item>();

    public void Init()
    {
        //Generics for the pickups - Generics are used for marking the item-type on the map before an actual ingame item is rolled.
        //Allows us to control which item is rolled within the Pickup without having to make the visualization completly random.
        generics.Add(new Item("med_kit", "med"));
        generics.Add(new Weapon("empty_weap", "weap"));
        generics.Add(new Armor("empty_armor", "armor"));
        generics.Add(new Item("ammo_box", "ammo"));
        generics.Add(new Item("keycard", "key"));
        generics.Add(new Throwable("grenade_shell", "grenade"));
        generics.Add(new Usable("modification", "use"));

        //Gonna be used for additional pickups, like Ammo Mods maybe or other usable that arent grenades (can still be coded like them) -So much for that...
        //Unique items that can not be obtained by random pickups. Right now they are rewards for activating subsystems, and therefore are REALLY broken. Have fun.
        items.Add(new Throwable("terra_former", "grenade", new TerraformImpact()));
        items.Add(AmmoFacade.Get().Create("wrath_of_the_gods", damage: 3, accuracy: 1, range: 1, penetration: 1, type: "true"));
        items.Add(new Item("master_key", "key"));

        //Weapons - ALL Weapons in the game
        //PLAYER WEAPONS - split the weapons in enemy and player weapons so the Pickups dont randomize enemy weapons(they're really bad and boring)
        //new Weapon(string n, string t, int d, float r, float a, int ammo, int maxammo, int clip, string ammotype, damagetype, pen) <- super easy, right?
        weaponList.Add(new Weapon("handgun", "weap", 3, 3, 0.7f, 24, 24, 12, "9mm", "bullet", 0));
        weaponList.Add(new Weapon("assault_rifle", "weap", 3, 4, 0.8f, 30, 90, 10, "9mm", "bullet", 0));
        weaponList.Add(new Weapon("combat_shotgun", "weap", 6, 2, 0.95f, 4, 16, 4, "12-gauge", "bullet", 0));
        weaponList.Add(new Weapon("sniper_rifle", "weap", 12, 9, 0.99f, 2, 6, 1, ".50", "bullet", 0));
        weaponList.Add(new Weapon("submachine_gun", "weap", 3, 3, 0.6f, 150, 300, 60, "5.7x28mm", "bullet", 0));
        weaponList.Add(new Weapon("EHF_osc_blade", "weap", 5, 1, 1.0f, -1, -1, -1, "mechanical", "sharp", 1));

        //ENEMY WEAPONS - Weapons used by the enemies. Not needed, but makes it alot easier to use the constructor for new enemies if you split 'em up a little
        enemyweaponList.Add(new Weapon("claws", "weap", 3, 1, 0.95f, -1, -1, -1, "none", "sharp", 1));
        enemyweaponList.Add(new Weapon("bolter", "weap", 2, 3, 0.7f, -1, -1, -1, "raw", "bullet", 1));
        enemyweaponList.Add(new Weapon("bearpaw", "weap", 6, 1, 0.6f, -1, -1, -1, "none", "blunt", 1));

        //Armor - ALL Armor in the game - Same as with the weapons. Split player and enemy for convenience
        armorList.Add(new Armor("uniform", "armor", 0, "fabric"));
        armorList.Add(new Armor("hardshell_suit", "armor", 20, "plate"));
        armorList.Add(new Armor("command_suit", "armor", 30, "aramid"));
        armorList.Add(new Armor("strike_suit", "armor", 40, "hybrid"));
        armorList.Add(new Armor("hcombat_armor", "armor", 80, "hybrid", new Trait(-1, "equip", new ActionTrait(-1))));
        armorList.Add(new Armor("phasing_armor", "armor", 10, "molecular", new Trait(-1, "equip", new ActionTrait(1))));

        //ENEMY ARMOR - See above
        enemyarmorList.Add(new Armor("thin_plating", "armor", 10, "plate"));
        enemyarmorList.Add(new Armor("cyber_fur", "armor", 30, "fluffy"));

        //Throwable - ALL Grenades in the game - Lots of grenades, as anyone can see. But ever since TraitBehaviours and the possibility to infinitely chain
        //them with the grenades ImpactBehaviours I've been scared by the possibilities. Technically everything should be possible. EVERYTHING. NO EXCEPTIONS.
        grenadeList.Add(new Throwable("frag_grenade", "grenade", new DamageImpact(2.0f, 8.0f)));
        grenadeList.Add(new Throwable("flashbang", "grenade", new AccuracyImpact(2.0f)));

        //Usables - ALL Character Buff Items in the game (technically all usables, but a little difficult to use the system)
        //Hollow-tips, explosive, cake. You know how it goes yo.
        //You can build any item you want here really, just make sure you use the Behaviour and tags for them correctly. Other than that, you can do anything with
        //Usables. They're very obedient slaves. 
        //Also save yourself the time and use the AmmoFacade whereever possible, the constructor chain for this stuff is an abomination.
        usableList.Add(AmmoFacade.Get().Create("tracer_ammo", accuracy: 1));
        usableList.Add(AmmoFacade.Get().Create("slug_shells", damage: 1, accuracy: -0.2f, range: 1, penetration: 1, type: "blunt"));
        usableList.Add(AmmoFacade.Get().Create("flechet_shells", damage: 1, penetration: 0.5f, type: "flechet"));
        usableList.Add(AmmoFacade.Get().Create("hooking_device", type: "hook"));

        //Keys - Ultimately not worth an extra list, but it helps with the way Pickups are rolled + visualized. 
        //It's 4 cards. With different colors. Creativity is through the roof on this one.
        keyList.Add(new Item("red_keycard", "key"));
        keyList.Add(new Item("blue_keycard", "key"));
        keyList.Add(new Item("green_keycard", "key"));
        keyList.Add(new Item("yellow_keycard", "key"));

        //Uses - ALL items that are used instantly when picked up and are not ammo? Maybe we should drop this one
        //"Maybe" he said. Yeah, those are dead. Not even a list for it. Kept the comment, because I liked it anyways.
    }

    public static ItemLibrary Get()
    {
        if (instance == null)
        {
            instance = new ItemLibrary();
            instance.Init();
        }

        return instance;
    }
}
