using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button creditButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.Gamescene);
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        creditButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(2);
        });
    }
}
