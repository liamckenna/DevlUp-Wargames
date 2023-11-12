using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickLoadScene : MonoBehaviour
{
    [Tooltip("The black screen transition that will be used")]
    public Animation anim;

    public int indexx;



    public void LoadByIndex(int sceneIndex)
    {
        indexx = sceneIndex;
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        anim.Play();
        yield return new WaitForSeconds(1f); 
        SceneManager.LoadScene("Eric");
        Cursor.visible = false;
        yield return new WaitForSeconds(1f);    
                
    
    }
}
