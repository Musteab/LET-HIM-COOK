using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartcountdownUI : MonoBehaviour
{
    private const string NUMBER_POPUP = "NumberPopup";

    [SerializeField] private TextMeshProUGUI countdownText;

    private Animator animator;
    private int previousCountDownNumber;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

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
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();

        if (previousCountDownNumber != countdownNumber)
        {
            previousCountDownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.instance.PlayCountDownSound();
        }
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
