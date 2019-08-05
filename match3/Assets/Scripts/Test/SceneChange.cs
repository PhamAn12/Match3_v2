using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
    
    public void SceneStart()
    {
        SceneManager.LoadScene("Main");

    }

    public void SceneBack()
    {
        StartCoroutine(DoChangeScene(0f));

    }

    IEnumerator DoChangeScene( float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Start");
    }
    public void BackToHome()
    {
        SceneManager.LoadScene("Start");
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
    