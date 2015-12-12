
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
/// <summary>
/// Stores the player's score as single values and multipliers
/// </summary>
public class Score {

    public static List<int> score = new List<int>();
    public static List<float> multiplier = new List<float>();

    public void AddScore(int value)
    {
        score.Add(value);
    }

    public void AddMultiplier(float mult)
    {
        multiplier.Add(mult);
    }

    //Calculates and returns the actual score
    public int GetScore()
    {
        int result = 0;
        float mult = 1.0f;

        for (int i = 0; i < score.Count; i++)
        {
            for (int j = 0; j < multiplier.Count; j++)
            {
                mult += multiplier[j];
            }
            result += score[i];
        }

        return result*(int)mult;
    }
}