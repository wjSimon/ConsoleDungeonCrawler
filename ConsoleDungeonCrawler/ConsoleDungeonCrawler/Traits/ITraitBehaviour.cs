using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//TraitBehaviour Interface. Dont touch.
public interface ITraitBehaviour
{
    void Execute(Actor actor);
    void OnRemove(Actor actor);
}
