using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{


    public Animator anim;

    [HideInInspector]
    public int loadingScene;


    void Start()
    {
        loadingScene = PlayerPrefs.GetInt("LastSavedScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        GameData.instance.ClearData();
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(loadingScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ShowOpt()
    {
        anim.SetBool("Show", true);
    }
    public void HideOpt()
    {
        anim.SetBool("Show", false);
    }
}

