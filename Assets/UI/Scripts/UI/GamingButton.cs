using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamingButton : MonoBehaviour
{
    public void Retry()
    {
        //加载当前场景
        Debug.Log("Retry!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        //加载主界面场景
        Debug.Log("Back To Menu!");

    }

    public void EnterNextLevel()
    {
        //加载下一关场景
        Debug.Log("Enter Next Level!");

    }
}
