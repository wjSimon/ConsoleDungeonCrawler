using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ITraitBehaviour
{
    void Execute(Actor actor);
    void OnRemove(Actor actor);
}
