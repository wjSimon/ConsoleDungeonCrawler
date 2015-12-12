using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsolePseudoRaycast
{
    //THIS RAYCAST SUCKS HARD; REALLY HARD.
    public ConsolePseudoRaycast()
    {
    }

    public static bool CastRay(Vector2 source, Vector2 target_position)
    {
        bool hit = false;
        bool run = true;
        Vector2 distance = GetDistanceVector(source, target_position);
        Vector2 top = Vector2.ZERO;
        Vector2 bot = Vector2.ZERO;
        Vector2 storage = new Vector2(0,0);

        float x = distance.x;
        float y = distance.y;

        while (run)
        {
            int count = 0;

            //Has to run each Raycast twice for consistency. Has some questionable results with corners...
            top = new Vector2(Vector2.Equalize(target_position, source));
            //bot = new Vector2(Vector2.Equalize(source, target_position));

            //PLEASE FIND BETTER WAY - GOOD FUCKING JOB, LOSER
            if (x > 0) x -= 1;
            if (x < 0) x += 1;
            if (y > 0) y -= 1;
            if (y < 0) y += 1;

            if (Application.GetData().level.structure[(int)(top.x), (int)(top.y)].substance == ClipType.WALL)
            {
                hit = true;
                break;
            }

            count++;

            if (x == 0 && y == 0)
            {
                run = false;
            }
        }

        return hit;
    }

    private static Vector2 GetDistanceVector(Vector2 a, Vector2 b)
    {
        Vector2 dist_vec = new Vector2();

        dist_vec.x = Math.Abs(a.x - b.x);
        dist_vec.y = Math.Abs(a.y - b.y);

        return dist_vec;
    }
}