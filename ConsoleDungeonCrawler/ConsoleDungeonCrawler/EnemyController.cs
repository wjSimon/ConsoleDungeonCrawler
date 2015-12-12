
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EnemyController : IBaseController
{
    public static bool done = false;
    private Random rng = new Random();
    private readonly Dictionary<int, Direction> DIR_LIB = new Dictionary<int, Direction>();

    private int aggro = 0;

    public EnemyController()
    {
        DIR_LIB.Add(0, Direction.UP);
        DIR_LIB.Add(1, Direction.DOWN);
        DIR_LIB.Add(2, Direction.LEFT);
        DIR_LIB.Add(3, Direction.RIGHT);
        DIR_LIB.Add(4, Direction.VOID);
    }

    public void Execute()
    {
        GameData data = Application.GetData();
        List<Actor> enemies = data.level.enemies;
        Direction dir;
        bool hit = false;
        bool random = false;
        Vector2 p_pos = new Vector2(data.player.position.x, data.player.position.y);

        for (int i = 0; i < enemies.Count; i++)
        {
            //melee
            if (enemies[i].type == "melee_calm")
            {
                random = false;
                hit = false;
                if (Vector2.Distance(new Vector2(enemies[i].position.x, enemies[i].position.y), p_pos) <= 5)
                {
                    if ((ConsolePseudoRaycast.CastRay(new Vector2(enemies[i].position.x, enemies[i].position.y), new Vector2(p_pos.x, p_pos.y))))
                    {
                        //ConsoleView.errorMessage = "target obscured (RayCast)";
                        hit = true;
                        random = true;
                    }

                    if (!hit)
                    {
                        Direction[] dirArray = new Direction[2];
                        dirArray = enemies[i].DirectionTowards(p_pos);

                        if (Vector2.Distance(new Vector2(enemies[i].position.x, enemies[i].position.y), p_pos) <= enemies[i].Weapon.content.range)
                        {
                            enemies[i].Weapon.content.Attack(enemies[i].position, data.player.position);
                        }
                        else
                        {                        
                            if (!DirectionMove(dirArray, enemies[i]))
                            {
                                random = true;
                            }
                        }
                    }   
                    else random = true;
                }
                else random = true;

                if (random == true)
                {
                    int counter = 0;
                    while (counter < 100)
                    {
                        counter++;
                        if (RandomMove(enemies[i])) break;
                    }
                }
            }
            // ranged
            if (enemies[i].type == "ranged")
            {
                random = false;
                hit = false;
                if (Vector2.Distance(new Vector2(enemies[i].position.x, enemies[i].position.y), p_pos) <= enemies[i].Weapon.content.range)
                {
                    if ((ConsolePseudoRaycast.CastRay(new Vector2(enemies[i].position.x, enemies[i].position.y), new Vector2(p_pos.x, p_pos.y))))
                    {
                        //ConsoleView.errorMessage = "target obscured (RayCast)";
                        hit = true;
                    }
                    if (!hit)
                    {
                        Direction[] dirArray = new Direction[2];
                        bool moved = false;
                        if (Vector2.Distance(new Vector2(enemies[i].position.x, enemies[i].position.y), p_pos) <= enemies[i].Weapon.content.range)
                        {
                            enemies[i].Weapon.content.Attack(enemies[i].position, data.player.position);
                        }
                        else
                        {
                            DirectionMove(dirArray, enemies[i]);
                            if (!moved)
                            {
                                random = true;
                            }
                        }
                    }
                    else random = true;
                }
                else random = true;
                if (random == true)
                {
                    int counter = 0;
                    while (counter < 100)
                    {
                        counter++;
                        if (RandomMove(enemies[i])) break;
                    }
                }
            }

            // melee boss
            if (enemies[i].type == "melee_aggressive")
            {
                hit = false;
                random = false;

                if (Vector2.Distance(new Vector2(enemies[i].position.x, enemies[i].position.y), p_pos) <= 10)
                {
                    if ((ConsolePseudoRaycast.CastRay(new Vector2(enemies[i].position.x, enemies[i].position.y), new Vector2(p_pos.x, p_pos.y))))
                    {
                        //ConsoleView.errorMessage = "target obscured (RayCast)";
                        hit = true;
                        random = true;
                    }

                    if (!hit)
                    {
                        Direction[] dirArray = new Direction[2];
                        dirArray = enemies[i].DirectionTowards(p_pos);

                        if (Vector2.Distance(new Vector2(enemies[i].position.x, enemies[i].position.y), p_pos) <= enemies[i].Weapon.content.range)
                        {
                            enemies[i].Weapon.content.Attack(enemies[i].position, data.player.position);
                        }
                        else
                        {                           
                            if (DirectionMove(dirArray, enemies[i]))
                            {
                                random = true;
                            }
                        }
                    }

                    else random = true;
                }
                else random = true;

                if (random == true)
                {
                    int counter = 0;
                    while (counter < 100)
                    {
                        counter++;
                        if (RandomMove(enemies[i])) break;
                    }
                }
            }
            End();
        }
    }

    public void End()
    {
        done = true;
    }

    private bool RandomMove(Actor enemy)
    {
        aggro = 0;
        Direction dir;
        int tmp = rng.Next(0, 4);
        DIR_LIB.TryGetValue(tmp, out dir);

        return enemy.Move(dir);
    }

    private bool DirectionMove(Direction[] dirArray, Actor enemy)
    {
        bool moved = false;
        if (!moved)
        {
            moved = enemy.Move(dirArray[0]);

            if (moved && dirArray[0] == Direction.VOID)
            {
                moved = false;
            }
        }
        if (!moved)
        {
            moved = enemy.Move(dirArray[1]);

            if (moved && dirArray[1] == Direction.VOID)
            {
                moved = false;
            }
        }

        return moved;
    }
}