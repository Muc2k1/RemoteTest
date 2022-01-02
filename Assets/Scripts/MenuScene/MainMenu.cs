using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
    }
    public void QuitGame(){
        Application.Quit();
    }
    public void LoadSceneIndex(int i)
    {
        SceneManager.LoadScene(i);
    }
    public void isClassic(bool isClassic)
    {
        if(isClassic)
            PlayerPrefs.SetString("GameMode","Classic");
        else
            PlayerPrefs.SetString("GameMode","New School");
    }
}
