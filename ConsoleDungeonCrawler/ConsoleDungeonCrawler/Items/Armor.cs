
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Armor : Item
{
    public int value;
    public string armortype;
    public Trait trait;

    public Armor()
    {
        value = 5;
        type = "none";
        trait = new Trait();
    }

    public Armor(Armor armor)
    {
        this.name = armor.name;
        this.type = armor.type;
        this.value = armor.value;
        this.armortype = armor.armortype;
        this.trait = armor.trait;
    }

    public Armor(string name, string type)
    {
        this.name = name;
        this.type = type;
        value = 5;
        armortype = "none";
    }

    public Armor(string name, string type, int value, string armortype)
    {
        this.name = name;
        this.type = type;
        this.value = value;
        this.armortype = armortype;
        this.trait = new Trait();
    }
    public Armor(string name, string type, int value, string armortype, Trait trait)
    {
        this.name = name;
        this.type = type;
        this.value = value;
        this.armortype = armortype;
        this.trait = trait;
    }
}