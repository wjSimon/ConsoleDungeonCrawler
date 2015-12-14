using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// IImpactBehaviour that applies a temporary fixed-amount AccuracyTrait ITraitBehaviour to all actors within radius
/// </summary>
class AccuracyImpact : Item, IImpactBehaviour
{
    GameData data;
    float radius;

    public AccuracyImpact(float radius)
    {
        this.radius = radius;
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
                    //IImpactBehaviour applying Traitbehaviours. Did I mention that you can do like ANYTHING with this system?
                    data.collision[i].AddTrait(2, "temp", new AccuracyTrait(-0.5f));
                }
            }
        }
    }

    private List<Actor> GetTarget()
    {
        List<Actor> inRange = new List<Actor>();
        return inRange;
    }

    //Math stuff. Probably the thing I spent the most time on coding this game? Check the PseudoRayCast and you'll probably see why though.
    //Spoiler, My math is epic.
    public static Vector2[] CalcRadius(Vector2 pos, float radius)
    {
        Vector2[] result = new Vector2[(int)(Math.Pow((2 * radius) - 1, 2))];
        int counter = 0;

        for (int i = 0; i < (2 * radius) - 1; i++)
        {
            for (int j = 0; j < (2 * radius) - 1; j++)
            {
                result[counter] = new Vector2(pos.x + (-radius + (1 + i)), pos.y + (-radius + (1 + j)));
                counter++;
            }
        }

        return result;
    }
}
