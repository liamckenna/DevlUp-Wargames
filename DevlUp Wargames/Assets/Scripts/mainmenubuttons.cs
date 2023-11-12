using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainmenubuttons : MonoBehaviour
{
    public void PlayButton(){
        SceneManager.LoadScene("Eric");
    }

        public void OptionsButton(){
        SceneManager.LoadScene("options");
    }

        public void BackButton(){
        SceneManager.LoadScene("menu");
    }

        public void QuitButton(){
        Application.Quit();
    }

        public void clickanywhereButton(){
        SceneManager.LoadScene("menu");
    }
}
