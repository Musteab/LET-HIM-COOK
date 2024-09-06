using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionsButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TooglePauseGame();
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionsButton.onClick.AddListener(() =>
        {
            OptionsUI.instance.Show();
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += Game_OnGamePaused;
        GameManager.Instance.OnGammeUnPaused += Game_OnGammeUnPaused;

        Hide();
    }

    private void Game_OnGammeUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void Game_OnGamePaused(object sender, System.EventArgs e)
    {
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);

    }
}
