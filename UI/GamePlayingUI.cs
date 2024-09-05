using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Update()
    {
        timerImage.fillAmount = GameManager.Instance.NormalizedGameTimer();
    }
}
