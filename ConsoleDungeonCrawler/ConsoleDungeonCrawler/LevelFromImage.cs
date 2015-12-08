
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

public class LevelFromImage : ILevelBuilder
{
    public int pickUpCount = 5;
    public int enemyCount = 3;
    public LevelFromImageOutputInfo output = new LevelFromImageOutputInfo();
    string path = "layout_all.bmp";
    Random rng = new Random(); 
    Bitmap btm;

    public LevelFromImage()
    {
        if (MasterControlProgram.map != "nomap")
        {
            path = MasterControlProgram.map;
        }
        Init();
    }

    public LevelFromImage(string path)
    {
        this.path = path;
        Init();
    }

    public void Init()
    {
        btm = new Bitmap(new FileStream(path, FileMode.Open));
    }

    public Level Generate()
    {
        Level levelGen = new Level();
        Random rng = new Random();

        levelGen.structure = BuildStructure();
        AddObjectsFromImage(levelGen);
        levelGen.playerSpawnPoints = SetPlayerSpawnPoints();

        /**/
        levelGen.pickUps.Add(SpawnPickup(new Vector2(1, 2), 0));
        levelGen.pickUps.Add(SpawnPickup(new Vector2(1, 3), 1));
        levelGen.pickUps.Add(SpawnPickup(new Vector2(2, 3), 2));
        levelGen.pickUps.Add(SpawnPickup(new Vector2(3, 3), 3));
        return levelGen;
    }

    public Tile[,] BuildStructure()
    {
        Tile[,] levelGenStructure = new Tile[btm.Width, btm.Height];
        output.width = btm.Width;
        output.height = btm.Height;

        for (int i = 0; i < btm.Width; i++)
        {
            for (int j = 0; j < btm.Height; j++)
            {
                //Console.Write(btm.GetPixel(i, j).ToArgb());
                if (btm.GetPixel(i, j).ToArgb() == Color.Black.ToArgb())
                {
                    levelGenStructure[i, j] = new Tile("wall", ClipType.WALL);
                    output.walls += 1;
                }
                else
                {
                    levelGenStructure[i, j] = new Tile("floor", ClipType.FLOOR);
                    output.floors += 1;
                }
            }
        }

        return levelGenStructure;
    }

    public void AddObjectsFromImage(Level level)
    {
        for (int i = 0; i < btm.Width; i++)
        {
            for (int j = 0; j < btm.Height; j++)
            {
                if (btm.GetPixel(i, j).ToArgb() == Color.Red.ToArgb())
                {
                    level.doors.Add(new Door("door", "red", new Vector2(i, j), false));
                    output.doors += 1;
                }
                if (btm.GetPixel(i, j).ToArgb() == Color.Blue.ToArgb())
                {
                    level.doors.Add(new Door("door", "blue", new Vector2(i, j), false));
                    output.doors += 1;
                }
                if (btm.GetPixel(i, j).ToArgb() == Color.Lime.ToArgb())
                {
                    level.doors.Add(new Door("door", "green", new Vector2(i, j), false));
                    output.doors += 1;
                }
                if (btm.GetPixel(i, j).ToArgb() == Color.Yellow.ToArgb())
                {
                    level.doors.Add(new Door("door", "yellow", new Vector2(i, j), false));
                    output.doors += 1;
                }
                if (btm.GetPixel(i, j).ToArgb() == Color.Aqua.ToArgb())
                {
                    Application.GetData().levelfinish.x = i;
                    Application.GetData().levelfinish.y = j;
                }
                //RESERVED FOR SUBSYSTEMS
                if (btm.GetPixel(i, j).ToArgb() == Color.Orange.ToArgb())
                {
                    level.trigger.Add(new TriggerObject("subsystem", new Vector2(i, j)));
                }
                if (btm.GetPixel(i, j).ToArgb() == Color.LightSlateGray.ToArgb())
                {
                    level.playerSpawnPoints.Add(new Vector2(i, j));
                    output.playerSpawns += 1;
                }
            }
        }
    }

    private PickUp SpawnPickup(Vector2 pos, int i)
    {
        Random rng = new Random();

        PickUp pickUp = new PickUp(ItemLibrary.Get().generics[i], 1); //rng.Next(1,3));
        pickUp.position = pos;

        return pickUp;
    }
    private Actor SpawnEnemy(Vector2 pos, int h)
    {
        Actor enemy = new Actor();

        enemy.position = pos;
        enemy.health = h;
        enemy.Weapon.content = new Weapon();

        Application.GetData().collision.Add(enemy);

        return enemy;
    }
    /**/
    private List<Vector2> SetPlayerSpawnPoints()
    {
        List<Vector2> spawns = new List<Vector2>();

        spawns.Add(new Vector2(0, 0));
        spawns.Add(new Vector2(0, 19));
        spawns.Add(new Vector2(19, 0));
        spawns.Add(new Vector2(19, 19));

        return spawns;
    }

    private List<Vector2> SetPickupSpawnPoints()
    {
        List<Vector2> spawns = new List<Vector2>();

        spawns.Add(new Vector2(5, 5));
        spawns.Add(new Vector2(5, 15));
        spawns.Add(new Vector2(15, 5));
        spawns.Add(new Vector2(15, 15));
        spawns.Add(new Vector2(5, 10));
        spawns.Add(new Vector2(10, 5));
        spawns.Add(new Vector2(10, 10));

        return spawns;
    }

    private List<Vector2> SetEnemySpawnPoints()
    {
        List<Vector2> spawns = new List<Vector2>();

        spawns.Add(new Vector2(15, 3));
        spawns.Add(new Vector2(15, 6));
        spawns.Add(new Vector2(15, 9));

        return spawns;
    }
}