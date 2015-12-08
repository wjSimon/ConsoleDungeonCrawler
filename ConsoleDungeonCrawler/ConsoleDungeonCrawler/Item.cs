
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Item : IImpactBehaviour
{

    public Item()
    {
        this.name = "empty_item";
        this.type = "debug";
    }

    public Item(string name, string type)
    {
        this.name = name;
        this.type = type;
    }

    public string type;
    public string name;

    public void Execute()
    {
        throw new NotImplementedException();
    }
}