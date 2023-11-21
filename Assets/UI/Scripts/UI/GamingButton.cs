using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamingButton : MonoBehaviour
{
    public void Retry()
    {
        //���ص�ǰ����
        Debug.Log("Retry!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        //���������泡��
        Debug.Log("Back To Menu!");

    }

    public void EnterNextLevel()
    {
        //������һ�س���
        Debug.Log("Enter Next Level!");

    }
}
