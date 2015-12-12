
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Application
{
    //Has the current state of the application stored. Data, Gamestate and Enemy AI are stored here, aswell as some functions
    public Application()
    {
    }

    private static GameData data;
    private static IBaseState currentState;
    private static EnemyController enemyController;
    private static readonly HashSet<IGameDataChangeListener> GAMEDATA_CHANGE_LISTENERS = new HashSet<IGameDataChangeListener>();
    private static Dictionary<GameStates, IBaseState> STATE_ARCHIVE;

    public static bool auto;

    public static void Load(string filename)
    {
        // TODO implement here
    }

    public static void Save(string filename)
    {
        // TODO implement here
    }

    public static void NewGame()
    {
        auto = false;
        data = new GameData();
        enemyController = new EnemyController();
        ILevelBuilder generator = new LevelGenerator();

        data.level = generator.Generate();
        for (int i = 0; i < data.level.doors.Count; i++)
        {
            data.level.doors[i].Init();
        }

        data.SpawnPlayer();
        //DEBUG STUFF
        //data.inventory.Add(ItemLibrary.Get().items[5], 1);


        foreach (IGameDataChangeListener listener in GAMEDATA_CHANGE_LISTENERS)
        {
            listener.OnGameDataChange(data);
        }

        //ConsolePseudoRaycast.CastRay(new Vector2(14, 1), new Vector2(16, 3));
        //Console.ReadKey();
    }

    public static void Add(IGameDataChangeListener listener)
    {
        GAMEDATA_CHANGE_LISTENERS.Add(listener);
    }

    public static void Remove(IGameDataChangeListener listener)
    {
        // TODO implement here
    }

    public static void Add(IGameStateChangeListener listener)
    {
        // TODO implement here
    }

    public static void Remove(IGameStateChangeListener listener)
    {
        // TODO implement here
    }

    public static void ChangeGameState(GameStates state)
    {
        if (currentState == null)
        {
            currentState = new GameState();
        }

        currentState.Enter(state);
    }

    public static void ChangeGameData(GameData newdata)
    {
        data = newdata;
        for (int i = 0; i < GAMEDATA_CHANGE_LISTENERS.Count; i++)
        {
            GAMEDATA_CHANGE_LISTENERS.ElementAt(i).OnGameDataChange(newdata);
        }
    }
    public static GameData GetData()
    {
        return data;
    }
    public static GameState GetState()
    {
        GameState temp = (GameState)currentState;
        return temp;
    }
    public static IBaseController GetEnemyController()
    {
        return enemyController;
    }
}