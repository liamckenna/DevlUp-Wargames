using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSceneManager : MonoBehaviour
{

        
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.F11)) {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

    //This function should be called to other scripts so that way you have the transition working
    public void LoadScene(int SceneIndex)
    {
        StartCoroutine(LoadAsyncScene(SceneIndex));
    }

    IEnumerator LoadAsyncScene(int SceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
