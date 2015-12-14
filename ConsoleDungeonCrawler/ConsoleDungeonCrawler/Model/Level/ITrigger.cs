using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Interface for all TriggerObjects. One TriggerObject is ONE way to trigger said Object.
/// </summary>
public interface ITrigger
{
    string OnTriggerEnter();
}
