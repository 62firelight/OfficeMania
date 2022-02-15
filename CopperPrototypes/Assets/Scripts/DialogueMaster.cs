using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class DialogueMaster
{
    public static List<string> playerSeenLines = new List<string>(){
        "It's the guy from the email!",
        "It's him!",
        "Get him!",
        "Watch out!",
        "Take him down!",
        "He's right there!",
        "There he is!",
        "Stop right there!",
        "Stop!",
        "Halt!"
    };

    public static List<string> enemyKOLines = new List<string>(){
        "Oof!",
        "Ow!",
        "Ah!",
        "Ouch!"
    };

    public static List<string> enemyStuckLines = new List<string>(){
        "I can't move!",
        "I'm stuck!"
    };

    public static List<string> bossSlowLines = new List<string>(){
        "Stop slowing me down!",
        "I'm too slow!",
        "I feel sluggish..."
    };

    public static string GetPlayerSeenLine()
    {
        int index = RandomIndex(0, playerSeenLines.Count - 1);

        return playerSeenLines.ElementAt(index);
    }

    public static string GetEnemyKOLine()
    {
        int index = RandomIndex(0, enemyKOLines.Count - 1);

        return enemyKOLines.ElementAt(index);
    }

    public static string GetEnemyStuckLine()
    {
        int index = RandomIndex(0, enemyStuckLines.Count - 1);

        return enemyStuckLines.ElementAt(index);
    }

    public static string GetBossSlowLine()
    {
        int index = RandomIndex(0, bossSlowLines.Count - 1);

        return bossSlowLines.ElementAt(index);
    }

    public static int RandomIndex(int min, int max)
    {
        return Random.Range(min, max);
    }
}
