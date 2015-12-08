
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Vector2
{
    public static readonly Vector2 ZERO = new Vector2(0, 0);

    public Vector2()
    {
        this.x = 0;
        this.y = 0;
    }

    public Vector2(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public float x;
    public float y;

    public static Vector2 AddVectors(Vector2 a, Vector2 b)
    {
        Vector2 result = new Vector2();

        result.x = a.x + b.x;
        result.y = a.y + b.y;

        return result;
    }

    public static double CrossDistance(Vector2 a, Vector2 b)
    {
        double result = 0;

        double x = Math.Abs(a.x - b.x);
        double y = Math.Abs(b.x - b.y);

        if (x > y) result = x;
        if (y > x) result = y;
        else result = -1;

        return result;
    }

    public static Vector2 Equalize(Vector2 a, Vector2 b)
    {
        //throw new NotImplementedException();

        Vector2 result = a;

        if (a.x < b.x) result.x = result.x + 1;
        if (a.x > b.x) result.x = result.x - 1;
        if (a.y < b.y) result.y = result.y + 1;
        if (a.y > b.y) result.y = result.y - 1;

        return result;
    }

    public static bool Adjacent(Vector2 a, Vector2 b)
    {
        bool adjacent = false;


        if (Vector2.Distance(a,b) <= 1 && (a.x == b.x || a.y == b.y))
        {
            adjacent = true;
        }

        return adjacent;
    }

    public static float Distance(Vector2 a, Vector2 b)
    {
        float result = 0;

        float x;
        float y;

        x = Math.Abs(a.x - b.x);
        y = Math.Abs(a.y - b.y);

        if (x > y) result = x; else result = y; 

        return result;
    }
}