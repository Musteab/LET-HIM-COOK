using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsUI : MonoBehaviour
{
    public void OpenUrl()
    {
        Application.OpenURL("https://youtu.be/dQw4w9WgXcQ");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
