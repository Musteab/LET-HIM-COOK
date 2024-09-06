using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI RecipesDeliveredText;

    private void Awake()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
    }
    private void Start()
    {
        GameManager.Instance.OnStatechanged += GameManger_OnStatechanged;
        Hide();
    }

    private void GameManger_OnStatechanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            Show();

            RecipesDeliveredText.text = DeliveryManager.Instance.GetSuccesfulRecipesAmount().ToString();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {

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
