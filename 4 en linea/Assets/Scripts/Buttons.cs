using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
   public void ButtonStartGame()
    {
        SceneManager.LoadScene("SceneGame");
    }
   public void ButtonReset()
    {
        SceneManager.LoadScene("SceneGame");
    }
   public  void ButtonReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void ButtonExit()
    {
        Application.Quit();
    }
}