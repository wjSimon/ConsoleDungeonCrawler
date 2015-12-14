using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Library containing all pre-made enemies that can then be assigned within LevelGenerator
/// </summary>
public class EnemyLibrary
{
    private static EnemyLibrary instance;
    public List<Actor> meleeList = new List<Actor>();
    public List<Actor> rangedList = new List<Actor>();
    public List<Actor> bossList = new List<Actor>();

    //Same style as the ItemLibrary, pretty straight forward
    public void Init()
    {
        meleeList.Add(new Actor("alien_assaulter", "melee_calm", 5, new Weapon(ItemLibrary.Get().enemyweaponList[0]), new Armor(ItemLibrary.Get().enemyarmorList[0])));
        rangedList.Add(new Actor("alien_trooper", "ranged", 3, new Weapon(ItemLibrary.Get().enemyweaponList[1]), new Armor(ItemLibrary.Get().enemyarmorList[0])));
        bossList.Add(new Actor("cyberbear", "melee_aggressive", 12, new Weapon(ItemLibrary.Get().enemyweaponList[0]), new Armor(ItemLibrary.Get().enemyarmorList[1])));
    }

    public static EnemyLibrary Get()
    {
        if (instance == null)
        {
            instance = new EnemyLibrary();
            instance.Init();
        }

        return instance;
    }
}
