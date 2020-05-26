using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    int numScenes;

    public static LevelManager Instance;

    // Start is called before the first frame update
    void Start()
    {
        numScenes = SceneManager.sceneCountInBuildSettings;
        Instance = this;
    }

    public void done()
    {
        SceneManager.LoadScene(Random.Range(1, numScenes));
    }
}
