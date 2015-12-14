
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Autmatically assigned during MasterControlProgram.Run() while the game is running. Controls all enemies in the current level.7
/// </summary>
public class EnemyController : IBaseController
{
    public static bool done = false;
    private Random rng = new Random();
    private readonly Dictionary<int, Direction> DIR_LIB = new Dictionary<int, Direction>();

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

        //Checks what "AI" algorithm should be used based on the enemy type is currently holds. For more enemies, consider implementing EnemyBehaviours instead and executing them here
        for (int i = 0; i < enemies.Count; i++)
        {
            //melee
            if (enemies[i].type == "melee_calm")
            {
                random = false;
                hit = false;
                //Leashing range for this enemy type
                if (Vector2.Distance(new Vector2(enemies[i].position.x, enemies[i].position.y), p_pos) <= 5)
                {
                    //Checks if the enemy can see the player
                    if ((ConsolePseudoRaycast.CastRay(new Vector2(enemies[i].position.x, enemies[i].position.y), new Vector2(p_pos.x, p_pos.y))))
                    {
                        //ConsoleView.errorMessage = "target obscured (RayCast)";
                        hit = true;
                        random = true;
                    }

                    //If he can, start action
                    if (!hit)
                    {
                        //Complicated array stuff. Modify DirectionTowards() instead of this if you want to modify movement around walls/edges
                        Direction[] dirArray = new Direction[2];
                        dirArray = enemies[i].DirectionTowards(p_pos);

                        //If player is within attacking range, attack him
                        if (Vector2.Distance(new Vector2(enemies[i].position.x, enemies[i].position.y), p_pos) <= enemies[i].Weapon.content.range)
                        {
                            enemies[i].Weapon.content.Attack(enemies[i].position, data.player.position);
                        }
                        //Not in range? Than check if we can move towards him.
                        else
                        {                        
                            //Can't move towards him. Randomly run about, with your arms flailing above your head.
                            if (!DirectionMove(dirArray, enemies[i]))
                            {
                                random = true;
                            }
                        }
                    }   
                    //If there's a wall, do so aswell.
                    else random = true;
                }
                //Also when he can't see you. No need to act scary and enemy-y when he's not around yo.
                else random = true;

                if (random == true)
                {
                    int counter = 0;
                    //TECHNICALLY this can mean that he won't move. Since there's a .VOID Direction. But if one of them actually rolls 100 Voids in a row, he might aswell chill for a turn. 
                    while (counter < 100)
                    {
                        counter++;
                        //FLAILING
                        if (RandomMove(enemies[i])) break; //RandomMove() returns true once it has rolled a direction the enemy is allowed to move, so walls/other enemies can fuck it up aswell
                    }
                }
            }
            // ranged
            if (enemies[i].type == "ranged")
            {
                //Same stuff for ranged enemies, hardly different
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
                //Different range the _calm. Nothing else. Genius coder here.
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

    //the function doing all the flailing
    private bool RandomMove(Actor enemy)
    {
        Direction dir;
        int tmp = rng.Next(0, 4);
        DIR_LIB.TryGetValue(tmp, out dir);

        return enemy.Move(dir);
    }

    //Little more complicated stuff because of the moved bool
    private bool DirectionMove(Direction[] dirArray, Actor enemy)
    {
        bool moved = false;
        if (!moved)
        {
            //Tries to move enemy on the X-Axis if it can, otherwise jumps to the Y-Axis
            moved = enemy.Move(dirArray[0]);

            if (moved && dirArray[0] == Direction.VOID)
            {
                moved = false;
            }
        }
        if (!moved)
        {
            //Can't move enemy on the Y-Axis either? Well, sucks to be him. (It'll go ahead and try random actually)
            moved = enemy.Move(dirArray[1]);

            if (moved && dirArray[1] == Direction.VOID)
            {
                moved = false;
            }
        }

        return moved;
    }
}