using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveUPText;
    [SerializeField] private TextMeshProUGUI moveDOWNText;
    [SerializeField] private TextMeshProUGUI moveLEFText;
    [SerializeField] private TextMeshProUGUI moveRIGHTText;
    [SerializeField] private TextMeshProUGUI INTERACTText;
    [SerializeField] private TextMeshProUGUI ALTINTERACTText;
    [SerializeField] private TextMeshProUGUI PAUSEText;

    private void Start()
    {
        inputscript.Instance.OnBindingRebind += input_OnBindingRebind;
        GameManager.Instance.OnStatechanged += game_OnStatechanged;
        UpdateVisual();
        Show();
    }

    private void game_OnStatechanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void input_OnBindingRebind(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        moveUPText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Move_Up);
        moveDOWNText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Move_Down);
        moveLEFText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Move_Left);
        moveRIGHTText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Move_Right);
        INTERACTText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Interact);
        ALTINTERACTText.text = inputscript.Instance.GetBindingText(inputscript.Binding.AltInteract);
        PAUSEText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Pause);

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
