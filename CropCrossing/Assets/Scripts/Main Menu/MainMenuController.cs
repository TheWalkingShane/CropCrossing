using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{
    public string playButtonLeadsTo;
    


    public void PlayButton()
    {
        SceneManager.LoadScene(playButtonLeadsTo);
    }
}
