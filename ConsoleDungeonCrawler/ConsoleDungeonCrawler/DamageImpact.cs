using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// IImpactBehaviour that damages all enemies in radius by the amount of dmg
/// </summary>
class DamageImpact : Item, IImpactBehaviour
{
    GameData data;
    float radius;
    float damage;

    public DamageImpact(float radius, float damage)
    {
        this.radius = radius;
        this.damage = damage;
    }

    public void Execute()
    {
        data = Application.GetData();
        Vector2[] radArray = CalcRadius(data.player.selector.position, radius);

        for (int i = 0; i < data.collision.Count; i++)
        {
            for (int j = 0; j < radArray.Length; j++)
            {
                if (data.collision[i].position.x == radArray[j].x && data.collision[i].position.y == radArray[j].y)
                {
                    data.collision[i].TakeDamage((int)(damage / (radius - 1) * 1.5), "explosive", 0.0f);
                }
            }
        }
    }

    private List<Actor> GetTarget()
    {
        List<Actor> inRange = new List<Actor>();
        return inRange;
    }

    public static Vector2[] CalcRadius(Vector2 pos, float radius)
    {
        Vector2[] result = new Vector2[(int)(Math.Pow((2 * radius) - 1, 2))];
        int counter = 0; 

        for (int i = 0; i < (2 * radius) - 1; i++)
        {
            for (int j = 0; j < (2 * radius) - 1; j++)
            {
                result[counter] = new Vector2(pos.x + (-radius + (1+i)), pos.y + (-radius + (1 + j)));
                counter++;
            }
        }


        /*MATH STUFF
        
        1,2,3,4,5 <- radius
        1,9,25,49,81 <- amount of fields covered
        1,3,5,7,9 <- exp base of ^

        n = radius
        a = amount of fields
        b = base

        a = b^2
        b = n+n-1
        ??

        25 = 5^2
        5 = 3+3-1

        49 = 7^2
        7 = 4+4-1

        > Good enough I guess

        a = (2*n - 1)^2

        (2n-1)^2 

-----------------------------------------------------

        /**/

        return result;
    }
}
