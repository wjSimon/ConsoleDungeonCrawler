
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Tile {

    public string terrain;
    public ClipType substance;


    public Tile()
    {

    }
    public Tile(string terrain, ClipType substance)
    {
        this.terrain = terrain;
        this.substance = substance;
    }

}