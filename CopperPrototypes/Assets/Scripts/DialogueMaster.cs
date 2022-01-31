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
        "There he is!"
    };

    public static string GetPlayerSeenLine()
    {
        int index = RandomIndex(0, playerSeenLines.Count - 1);

        return playerSeenLines.ElementAt(index);
    }

    public static int RandomIndex(int min, int max)
    {
        return Random.Range(min, max);
    }
}
