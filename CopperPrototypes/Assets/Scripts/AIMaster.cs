using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AIMaster
{
    public static HashSet<GameObject> takenObjects = new HashSet<GameObject>();

    public static Scene currentScene = SceneManager.GetActiveScene();

    public static int currentLevel = 1;
}
