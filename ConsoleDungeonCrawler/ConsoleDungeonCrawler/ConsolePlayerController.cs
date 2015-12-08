
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class ConsolePlayerController : IBaseController, IGameDataChangeListener, IGameStateChangeListener
{
    public GameData data;
    public static bool done = false;

    public ConsolePlayerController()
    {
        data = Application.GetData();
        done = false;
        Application.Add((IGameDataChangeListener)this);
    }

    public void Execute()
    {
        ConsoleKey input = Console.ReadKey().Key;

        switch (input)
        {
            case ConsoleKey.W:
                //Console.WriteLine("\nw");
                data.player.Move(Direction.UP);
                break;
            case ConsoleKey.A:
                //Console.WriteLine("\na");
                data.player.Move(Direction.LEFT);
                break;
            case ConsoleKey.S:
                //Console.WriteLine("\ns");
                data.player.Move(Direction.DOWN);
                break;
            case ConsoleKey.D:
                //Console.WriteLine("\nd");
                data.player.Move(Direction.RIGHT);
                break;
            case ConsoleKey.C:
                //Console.WriteLine("\nc");
                if (data.player.EnterCombat())
                {
                    CombatSwitch();
                }
                break;
            case ConsoleKey.Q:
                //Console.WriteLine("\nq");
                data.player.Weapon.content.Attack();
                break;
            case ConsoleKey.Backspace:
                //Console.WriteLine("\nbackspace");
                data.player.Undo();
                break;
            case ConsoleKey.I:
                //Console.WriteLine("\ni");
                InventorySwitch();
                break;
            case ConsoleKey.Enter:
                //Console.WriteLine("\nenter");
                if (data.combat)
                {
                    return;
                }
                End();
                break;
            case ConsoleKey.R:
                //Console.WriteLine("\nr");
                data.player.Weapon.content.Reload();
                break;
            case ConsoleKey.O:
                //Console.WriteLine("\no");
                OpenDoor();
                break;
            case ConsoleKey.E:
                //Console.WriteLine("\ne");
                Analyze();
                break;
            case ConsoleKey.Tab:
                //Console.WriteLine("\ntab");
                Application.auto = !Application.auto;
                break;
        }

    }

    private void OpenDoor()
    {
        for (int i = 0; i < data.level.doors.Count; i++)
        {
            if (Vector2.Adjacent(data.level.doors[i].position, data.player.position))
            {
                data.level.doors[i].Switch();
            }
        }
    }

    private void Analyze()
    {
        //PUT FUNCTION INTO ACTOR
        Actor type = null;
        for (int i = 0; i < data.level.enemies.Count; i++)
        {
            if (Vector2.Distance(data.level.enemies[i].position, data.player.selector.position) <= 0)
            {
                type = data.level.enemies[i];
            }
        }

        if (type != null)
        {
            for (int i = 0; i < data.level.enemies.Count; i++)
            {
                if (type.name == data.level.enemies[i].name)
                {
                    data.level.enemies[i].info = true;
                }
            }

            data.player.actions -= 1;
        }
    }

    private void CombatSwitch()
    {
        data.combat = !(data.combat);
    }
    private void InventorySwitch()
    {
        MasterControlProgram.SetController(new ConsoleInventoryController());
        data.inv = !data.inv;
        data.currentItem = 0;
    }

    private void End()
    {
        data.player.actions = 0;
        data.player.path.Clear();
        Application.GetEnemyController().Execute();

        done = true;
    }

    public void OnGameDataChange(GameData data)
    {
        this.data = data;
    }

    public void OnGameStateChange()
    {
        throw new NotImplementedException();
    }
}