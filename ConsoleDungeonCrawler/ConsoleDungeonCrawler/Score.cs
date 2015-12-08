
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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