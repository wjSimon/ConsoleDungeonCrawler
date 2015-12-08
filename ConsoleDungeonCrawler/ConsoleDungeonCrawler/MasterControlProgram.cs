
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class MasterControlProgram : IGameDataChangeListener, IGameStateChangeListener
{
    public static string map = "";

    public MasterControlProgram()
    {
        turn = 0;
        map = "nomap";
        Application.Add((IGameDataChangeListener)this);
    }

    public static bool running = true;
    public GameData data;
    private static IBaseView view;
    private static IBaseController controller;

    private int turn = -1;

    public void Run()
    {
        //Application.NewGame();
        Application.ChangeGameState(GameStates.MENU);
        view.Execute();

        while (running)
        {
            if (controller != null) controller.Execute();

            if (Application.GetState().state != GameStates.FINISH)
            {
                if (ConsolePlayerController.done && EnemyController.done)
                {
                    EndTurn();
                }
                else if (Application.auto == true && data.player.actions <= 0)
                {
                    data.player.path.Clear();
                    Application.GetEnemyController().Execute();
                    EndTurn();
                }
            }

            view.Execute();
        }
    }

    public void Save()
    {
        // TODO implement here
    }

    public void Load()
    {
        // TODO implement here
    }

    public void GetCurrentState()
    {
        // TODO implement here
    }

    public void OnGameDataChange(GameData data)
    {
        this.data = data;
    }

    public void OnGameStateChange()
    {
        throw new NotImplementedException();
    }
    public void EndTurn()
    {
        turn++;
        data.player.actions = data.player.maxActions;
        for (int i = 0; i < data.collision.Count; i++)
        {
            data.collision[i].actions = data.collision[i].maxActions;
        }
        for (int i = 0; i < data.level.trigger.Count; i++)
        {
            if (data.level.trigger[i].position.x == data.player.position.x && data.level.trigger[i].position.y == data.player.position.y)
            {
                data.level.trigger[i].OnTriggerEnter();
            }
        }

        for (int i = 0; i < data.collision.Count; i++)
        {
            for (int j = 0; j < data.collision[i].traits.Count; j++)
            if (data.collision[i].traits[j].name == "temp")
            {
                    data.collision[i].traits[j].duration -= 1;
                    if (data.collision[i].traits[j].duration <= 0)
                    {
                        data.collision[i].RemoveTrait(data.collision[i].traits[j]);
                }
            }
        }

        ConsolePlayerController.done = false;
        EnemyController.done = false;
        ConsoleView.errorMessage = "";
    }

    public static void SetController(IBaseController c)
    {
        controller = c;
    }

    public static void SetView(IBaseView v)
    {
        view = v;
    }

    public static void UpdateView()
    {
        view.Execute();
    }

    public static IBaseController GetController()
    {
        return controller;
    }
}