using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class TerraformImpact : IImpactBehaviour
{
    GameData data;

    public TerraformImpact()
    {
    }

    public void Execute()
    {
        data = Application.GetData();

        for (int i = 1; i < data.level.structure.GetLength(0)-1; i++)
        {
            if (data.level.structure[i, (int)data.player.selector.position.y].substance == ClipType.WALL)
            {
                data.level.structure[i, (int)data.player.selector.position.y].substance = ClipType.FLOOR;
                data.level.structure[i, (int)data.player.selector.position.y].terrain = "floor";
            }
        }

        for (int i = 1; i < data.level.structure.GetLength(1) - 1; i++)
        {
            if (data.level.structure[(int)data.player.selector.position.x, i].substance == ClipType.WALL)
            {
                data.level.structure[(int)data.player.selector.position.x, i].substance = ClipType.FLOOR;
                data.level.structure[(int)data.player.selector.position.x, i].terrain = "floor";
            }
        }

        for (int i = 0; i < data.collision.Count; i++)
        {
            if (data.collision[i].position.x == data.player.selector.position.x || data.collision[i].position.y == data.player.selector.position.y)
            {
                data.collision[i].TakeDamage(data.collision[i].health, "true", 0);
            }
        }
        for (int i = 0; i < data.level.doors.Count; i++)
        {
            if (data.level.doors[i].position.x == data.player.selector.position.x || data.level.doors[i].position.y == data.player.selector.position.y)
            {
                data.level.doors.Remove(data.level.doors[i]);
            }
        }
    }
}
