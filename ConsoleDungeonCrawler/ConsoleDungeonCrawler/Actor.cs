
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Actor : GameObject
{

    public Actor()
    {
        this.name = "player";
        this.position = new Vector2();
        this.selector = new GameObject();
        this.selector.position = new Vector2(0, 0);

        this.actions = maxActions;
        this.Weapon = new Slot<Weapon>();
        this.Armor = new Slot<Armor>();
        Weapon.content = new Weapon(ItemLibrary.Get().weaponList[0]);
        Armor.content = new Armor(ItemLibrary.Get().armorList[0]);

        XselecRange = Weapon.content.range;
    }
    /// <summary>
    /// Constructor for enemies
    /// </summary>
    /// <param name="name"></param>
    /// <param name="health"></param>
    /// <param name="weapon"></param>
    /// <param name="armor"></param>
    public Actor(string name, string type, int health, Weapon weapon, Armor armor)
    {
        this.name = name;
        this.actions = maxActions;
        this.type = type;
        this.health = health;
        this.position = new Vector2();
        this.selector = new GameObject();
        this.selector.position = new Vector2(0, 0);

        this.Weapon = new Slot<Weapon>();
        this.Armor = new Slot<Armor>();
        Weapon.content = weapon;
        Armor.content = armor;
    }
    public Actor(Actor enemy)
    {
        this.name = enemy.name;
        this.actions = 1;
        this.type = enemy.type;
        this.health = enemy.health;
        this.position = new Vector2();
        this.selector = new GameObject();
        this.selector.position = new Vector2(0, 0);

        this.Weapon = new Slot<Weapon>();
        this.Armor = new Slot<Armor>();
        Weapon.content = enemy.Weapon.content;
        Armor.content = enemy.Armor.content;
    }

    public string type;
    public int health;
    public int maxHealth;
    public int actions;
    public int maxActions = 3;
    public int level = 1;
    public int experience = 0;
    public Slot<Weapon> Weapon;
    public Slot<Armor> Armor;
    public List<Trait> traits = new List<Trait>();
    private GameData data;
    public GameObject selector;
    public bool info = false;

    public bool usingItem = false;
    public float XselecRange;

    public List<Vector2> path = new List<Vector2>();

    public bool Move(Direction dir)
    {
        Vector2 pos = new Vector2();
        data = Application.GetData();
        bool moved = false;
        XselecRange = Weapon.content.range;

        if (actions <= 0)
        {
            return false;
        }

        switch (dir)
        {
            case Direction.VOID:

                return true;

            case Direction.UP:
                //SMARTGIT DEMONSTRATION COMMENT
                //change -5/+5 to temporary range, based on what is being used for weapon ranges and stuff

                //------------------------------------------------------------
                if (!(data.combat) && position.x - 1 < 0)
                {
                    return false;
                }
                {
                    //------------------------------------------------------------
                }
                if (data.combat && (selector.position.x - 1 < position.x - XselecRange || selector.position.x - 1 < 0))
                {
                    return false;
                }
                //------------------------------------------------------------
                for (int i = 0; i < data.collision.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.x == position.x - 1 && data.collision[i].position.y == position.y))
                    {
                        return false;
                    }
                }
                //------------------------------------------------------------              
                if (!(data.combat) && (data.level.structure[(int)position.x - 1, (int)position.y].substance == ClipType.WALL))
                {
                    return false;
                }

                //------------------------------------------------------------
                pos.x -= 1;

                break;
            //------------------------------------------------------------


            case Direction.DOWN:

                if (!(data.combat) && position.x + 1 > data.level.structure.GetLength(0) - 1)
                {
                    return false;
                }
                if (data.combat && (selector.position.x + 1 > position.x + XselecRange || selector.position.x + 1 > data.level.structure.GetLength(0) - 1))
                {
                    return false;
                }
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.x == position.x + 1 && data.collision[i].position.y == position.y))
                    {
                        return false;
                    }
                }
                if (!(data.combat) && (data.level.structure[(int)position.x + 1, (int)position.y].substance == ClipType.WALL))
                {
                    return false;
                }
                pos.x += 1;

                break;

            case Direction.LEFT:

                if (!(data.combat) && position.y - 1 < 0)
                {
                    return false;
                }
                if (data.combat && (selector.position.y - 1 < position.y - XselecRange || selector.position.y - 1 < 0))
                {
                    return false;
                }
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.y == position.y - 1 && data.collision[i].position.x == position.x))
                    {
                        return false;
                    }
                }
                if (!(data.combat) && (data.level.structure[(int)position.x, (int)position.y - 1].substance == ClipType.WALL))
                {
                    return false;
                }
                pos.y -= 1;

                break;

            case Direction.RIGHT:

                if (!(data.combat) && position.y + 1 > data.level.structure.GetLength(1) - 1)
                {
                    return false;
                }
                if (data.combat && (selector.position.y + 1 > position.y + XselecRange || selector.position.y + 1 > data.level.structure.GetLength(1) - 1))
                {
                    return false;
                }
                for (int i = 0; i < data.level.enemies.Count; i++)
                {
                    if (!(data.combat) && (data.collision[i].position.y == position.y + 1 && data.collision[i].position.x == position.x))
                    {
                        return false;
                    }
                }
                if (!(data.combat) && (data.level.structure[(int)position.x, (int)position.y + 1].substance == ClipType.WALL))
                {
                    return false;
                }
                pos.y += 1;

                break;
        }

        if (!data.combat)
        {
            path.Add(position);
            position = new Vector2((int)(position.x + pos.x), (int)(position.y + pos.y));
            actions -= 1;
            moved = true;

            if (this == data.player)
            {
                for (int i = 0; i < data.level.trigger.Count; i++)
                {
                    if (position.x == data.level.trigger[i].position.x && position.y == data.level.trigger[i].position.y)
                    {
                        if (data.level.trigger[i].name == "subsystem")
                        {
                            data.combatlog.Add("Ship system reached... End turn to activate");
                        }
                        if (data.level.trigger[i].name == "endoflevel")
                        {
                            data.combatlog.Add("Escape pod reached... End turn to leave ship");
                        }
                    }
                }
            }
        }

        if (data.combat)
        {
            selector.position = new Vector2((int)(selector.position.x + pos.x), (int)(selector.position.y + pos.y));
            moved = true;
        }

        for (int i = 0; i < data.level.pickUps.Count; i++)
        {
            if (position.x == data.level.pickUps[i].position.x && position.y == data.level.pickUps[i].position.y)
            {
                if (this == data.player)
                {
                    data.level.pickUps[i].OnPickup();
                }
            }
        }
        return moved;
    }
    /// <summary>
    /// Move position A towards position B
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public Direction[] DirectionTowards(Vector2 b)
    {
        Direction[] dir = new Direction[] { Direction.VOID, Direction.VOID };

        if (position.x < b.x) dir[0] = Direction.DOWN;
        if (position.x > b.x) dir[0] = Direction.UP;
        if (position.y < b.y) dir[1] = Direction.RIGHT;
        if (position.y > b.y) dir[1] = Direction.LEFT;

        return dir;
    }

    public bool EnterCombat()
    {
        if (actions <= 0)
        {
            if (!data.combat)
            {
                return false;
            }
        }

        selector = new GameObject();
        selector.position = new Vector2();

        selector.position = this.position;

        return true;
    }

    public void TakeDamage(float value, string dmgtype, float pen)
    {
        data = Application.GetData();
        Armor armor = this.Armor.content;

        //DAMAGE CALC WITH DAMAGE TYPE AND ARMOR TYPE AND STUFF
        value = ApplyArmor(value, dmgtype, armor, pen);
        health -= Math.Max((int)value, 0);

        if (this != data.player)
        {
            data.combatlog.Add("...Enemy took " + (int)value + " damage");
        }
        if (this == data.player)
        {
            data.combatlog.Add("Hit. " + (int)value + " damage taken");
        }

        data.score.AddScore((int)value);

        if (this != data.player && health <= 0)
        {
            data.combatlog.Add(name + " defeated");
            #region scores
            if (this.name == "alien_assaulter")
            {
                data.score.AddScore(25);
                data.player.AddExperience(10);
            }
            if (this.name == "alien_trooper")
            {
                data.score.AddScore(20);
                data.player.AddExperience(10);
            }
            if (this.name == "cyberbear")
            {
                data.score.AddScore(75);
                data.player.AddExperience(50);
            }
            #endregion

            data.level.enemies.Remove(this);
            data.collision.Remove(this);
        }
        if (this == data.player && health <= 0)
        {
            data.collision.Remove(data.player);
            data.player = new Actor();
            data.SpawnPlayer();

            Console.WriteLine("YOU DIED");
            Console.WriteLine("Press any key to continue\n");
            Application.ChangeGameState(GameStates.FINISH);
        }
    }

    private float ApplyArmor(float value, string dmgtype, Armor armor, float pen)
    {
        float result = value;

        if (armor.armortype == "none" || armor.armortype == "fabric") return value;
        if (dmgtype == "true") return value;

        #region plate
        if (armor.armortype == "plate")
        {
            if (dmgtype == "sharp") result = (value * 0.3f) - ((armor.value / 10));
            if (dmgtype == "bullet") result = (value * 0.9f) - ((armor.value / 10) * (1 - pen / 1.3f));
            if (dmgtype == "flechet")
            {
                result = (value * 0.9f) - ((armor.value / 10) * (1 - pen));
                AddTrait(2, "temp", new HeavyInjuryTrait(1));
            }
            if (dmgtype == "hook")
            {
                result = (value * 0.5f) - ((armor.value / 10));
                AddTrait(0, "temp", new HookedTrait());
            }

            if (dmgtype == "blunt") result = (value * 1.5f) + ((armor.value / 20) * (1 - pen));
        }
        #endregion
        #region aramid
        if (armor.armortype == "aramid")
        {
            if (dmgtype == "sharp") result = (value) - ((armor.value / 10));
            if (dmgtype == "bullet") result = (value * 0.5f) - ((armor.value / 10) * (1 - pen / 1.8f));
            if (dmgtype == "flechet")
            {
                result = (value * 0.9f) - ((armor.value / 10) * (1 - pen / 1.1f));
                AddTrait(2, "temp", new HeavyInjuryTrait(1));
            }
            if (dmgtype == "hook")
            {
                result = (value * 0.5f) - ((armor.value / 10));
                AddTrait(0, "temp", new HookedTrait());
            }

            if (dmgtype == "blunt") result = (value * 1.0f) + ((armor.value / 10) * (1 - pen));
        }
        #endregion
        #region hybrid
        if (armor.armortype == "hybrid")
        {
            if (dmgtype == "sharp") result = (value * 0.65f) - ((armor.value / 10) * (1 - pen / 1.4f));
            if (dmgtype == "bullet") result = (value * 0.65f) - ((armor.value / 10) * (1 - pen / 1.4f));
            if (dmgtype == "flechet")
            {
                result = (value) - ((armor.value / 10) * (1 - pen));
                AddTrait(2, "temp", new HeavyInjuryTrait(1));
            }
            if (dmgtype == "hook")
            {
                result = (value * 0.5f) - ((armor.value / 10));
                AddTrait(0, "temp", new HookedTrait());
            }

            if (dmgtype == "blunt") result = (value * 1.2f) + ((armor.value / 10) * (1 - pen));
        }
        #endregion
        #region fluffy
        if (armor.armortype == "fluffy")
        {
            if (dmgtype == "sharp" || dmgtype == "flechet") result = (value * 3.0f) + ((armor.value) * (1 - pen));
            if (dmgtype == "bullet") result = (value) - ((armor.value / 10) * (1 - pen * 0.8f));
            if (dmgtype == "blunt") result = (value) + ((armor.value));
        }
        #endregion
        #region molecular
        if (armor.armortype == "molecular")
        {
            Random rng = new Random();
            double current = Math.Round(rng.NextDouble(), 2);

            if (current <= 0.1)
            {
                data.combatlog.Add("Enemy attack absorbed(Molecular armor).");
                return 0;
            }


            if (dmgtype == "sharp" && current >= 0.3) result = (value) - ((armor.value / 100) * (1 - pen));
            else if (dmgtype == "sharp" && current <= 0.3) result = 0;

            if (dmgtype == "bullet" && current >= 0.7) result = (value * 0.5f) - ((armor.value / 10) * (1 - pen / 1.8f));
            else if (dmgtype == "bullet" && current >= 0.7) result = 0;

            if (dmgtype == "flechet" && current >= 0.7)
            {
                result = (value * 0.9f) - ((armor.value / 10) * (1 - pen / 1.1f));
                AddTrait(2, "temp", new HeavyInjuryTrait(1));
            }
            else if (dmgtype == "flechet" && current >= 0.7) result = 0;

            if (dmgtype == "hook")
            {
                result = (value * 0.5f) - ((armor.value / 10));
                AddTrait(0, "temp", new HookedTrait());
            }

            if (dmgtype == "blunt" && current >= 0.7) result = (value * 1.0f) + ((armor.value / 10) * (1 - pen));
        }
        #endregion

        return result;
    }

    /*
    private void ApplyTraits()
    {
        //Reset();
        for (int i = 0; i < traits.Count; i++)
        {
            for (int j = 0; j < traits[i].behaviour.Count; j++)
            {
                traits[i].behaviour[j].Execute(this);
            }
        }
    }
    */

    public void AddTrait(Trait trait)
    {
        traits.Add(trait);
        for (int i = 0; i < trait.behaviour.Count; i++)
        {
            trait.behaviour[i].Execute(this);
        }
    }
    public void AddTrait(int duration, string name, ITraitBehaviour trait)
    {
        traits.Add(new Trait(duration, name, trait));
        trait.Execute(this);
    }

    public void RemoveTrait(string name)
    {
        Trait trait = null;

        for (int i = 0; i < traits.Count; i++)
        {
            if (name == traits[i].name)
            {
                trait = traits[i];
            }
        }

        if (trait == null) return;

        trait.Remove(this);
        traits.Remove(trait);
    }

    public void RemoveTrait(Trait trait)
    {
        trait.Remove(this);
        traits.Remove(trait);
    }

    public void EquipWeapon(Weapon r)
    {
        if (Application.GetData().combat)
        {
            data.combat = false;
        }

        Weapon.content = r;
        actions--;
    }

    public void EquipArmor(Armor a)
    {
        if (Application.GetData().combat)
        {
            data.combat = false;
        }

        for (int i = 0; i < traits.Count; i++)
        {
            if (traits[i].name == "equip")
            {
                RemoveTrait(traits[i]);
            }
        }

        Armor.content = a;
        actions--;
        AddTrait(Armor.content.trait);
    }

    public void AddExperience(int exp)
    {
        data.combatlog.Add(exp.ToString() + " experience gained.");
        experience += exp;

        if (experience >= level * 50 + ((level - 1) * 50))
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        experience -= level * 50 + ((level - 1) * 50);
        level++;

        data.combatlog.Add("LEVEL UP! Level " + level.ToString() + " reached");
        /*
        data.combatlog.Add("Max health increased!");
        data.combatlog.Add("Accuracy increased!");
        */

        if (level > 10)
        {
            return;
        }

        AddTrait(new Trait(new AccuracyTrait(0.05f)));
        maxHealth++;
        health++;
        
        if(level == 3)
        {
            maxActions++;
        }

        if(level == 6)
        {
            maxActions++;
        }

        if (level == 10)
        {
            data.combatlog.Add("Max enhancement reached.");
            data.combatlog.Add("Further leveling will not increase stats.");
        }
    }

    public void Undo()
    {
        if (actions == maxActions)
        {
            return;
        }
        if (data.combat)
        {
            return;
        }

        position = path[path.Count - 1];
        path.RemoveAt(path.Count - 1);
        actions += 1;
    }

    /*
    public void Reset()
    {
        for (int i = 0; i < ItemLibrary.Get().weaponList.Count; i++)
        {
            if (ItemLibrary.Get().weaponList[i].name == this.Weapon.content.name)
            {
                this.Weapon.content = new Weapon(ItemLibrary.Get().weaponList[i]);
            }
        }
    }
    */

}