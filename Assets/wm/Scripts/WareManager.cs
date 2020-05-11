using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WareManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] scenes;
    public Text WinText;
    public Transform lives;

    private int wins = 0;
    private int loss = 0;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        for (int i = 0; i < lives.childCount; i++)
        {
            lives.GetChild(i).gameObject.SetActive(true);
        }
        WinText.text = "Games Won: 0";
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Prototype");
    }

    public void Success()
    {
        wins += 1;
        StartCoroutine("NextScene");
    }

    public void Failure()
    {
        if(loss >= lives.childCount)
        {
            SceneManager.LoadScene("Menu");
        }
        lives.GetChild(loss).gameObject.SetActive(false);
        loss += 1;
        StartCoroutine("NextScene");
    }

    public IEnumerator NextScene()
    {
        SceneManager.LoadScene("Transition");
        WinText.text = "Games Won: " + wins;
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Prototype");
    }
    
}
