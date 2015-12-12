
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Class containing all information regarding the levels structure and content. Built in LevelGenerator, stored in GameData
/// </summary>
public class Level
{

    public Level()
    {
    }

    public Tile[,] structure;
    public List<PickUp> pickUps = new List<PickUp>();
    public List<Actor> enemies = new List<Actor>();
    public List<Door> doors = new List<Door>();
    public List<Vector2> playerSpawnPoints = new List<Vector2>();
    public List<Vector2> subsystemSpawnPoints = new List<Vector2>();
    public List<TriggerObject> trigger = new List<TriggerObject>();

    public Tile[,] Get()
    {
        // TODO implement here
        return null;
    }
}