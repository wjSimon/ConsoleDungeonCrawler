
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Class that stores the levels structural information - Walls, Floors, etc. are stored as terrain + substance for more (theoretical) variance
/// </summary>
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