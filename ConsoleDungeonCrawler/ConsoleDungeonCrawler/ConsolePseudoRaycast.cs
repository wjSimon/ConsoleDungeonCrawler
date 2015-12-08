using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ConsolePseudoRaycast
{
    //THIS RAYCAST ONLY WORKS FOR THIS PROJECT SPECIFICALLY; DONT PORT. UNITY'S IS BETTER ANYWAYS
    //MAYBE MAKE USUABLE BY OUTPUT STRUCT?!?!?!?!??!?!?
    public ConsolePseudoRaycast()
    {
    }

    public static bool CastRay(Vector2 source, Vector2 target_position)
    {
        bool hit = false;
        bool run = true;
        Vector2 distance = GetDistanceVector(source, target_position);
        Vector2 current = new Vector2(0,0);
        Vector2 storage = new Vector2(0,0);

        float x = distance.x;
        float y = distance.y;

        while (run)
        {
            int count = 0;
            current = Vector2.Equalize(target_position, source);

            //PLEASE FIND BETTER WAY
            if (x > 0) x -= 1;
            if (x < 0) x += 1;
            if (y > 0) y -= 1;
            if (y < 0) y += 1;

            //Console.ReadKey();

            if (Application.GetData().level.structure[(int)(current.x), (int)(current.y)].substance == ClipType.WALL)
            {
                Vector2 wall = new Vector2(current.x, current.y);
                hit = true;
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