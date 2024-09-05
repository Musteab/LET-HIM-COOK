using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartcountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;


    private void Start()
    {
        GameManager.Instance.OnStatechanged += GameManager_OnStatechanged;
        Hide();
    }

    private void GameManager_OnStatechanged(object sender, System.EventArgs e)
    {
      if (GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountdownToStartTimer()).ToString();
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
