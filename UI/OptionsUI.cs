using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI instance {  get; private set; }

    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI soundEffectsTexts;
    [SerializeField] private TextMeshProUGUI musicTexts;
    [SerializeField] private Button optionsCloseButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button altInteractButton;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI altInteractText;
    [SerializeField] private Transform pressToRebindKey;

    private void Awake()
    {
         instance = this;
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.instance.ChangeVolumne();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolumne();
            UpdateVisual();
        });
        optionsCloseButton.onClick.AddListener(() =>
        {
            Hide();
        });
        moveUpButton.onClick.AddListener(() =>
        {
            RebindingKey(inputscript.Binding.Move_Up);
        });
        moveDownButton.onClick.AddListener(() =>
        {
            RebindingKey(inputscript.Binding.Move_Down);
        });
        moveLeftButton.onClick.AddListener(() =>
        {
            RebindingKey(inputscript.Binding.Move_Left);
        });
        moveRightButton.onClick.AddListener(() =>
        {
            RebindingKey(inputscript.Binding.Move_Right);
        });
        interactButton.onClick.AddListener(() =>
        {
            RebindingKey(inputscript.Binding.Interact);
        });
        altInteractButton.onClick.AddListener(() =>
        {
            RebindingKey(inputscript.Binding.AltInteract);
        });


    }
    private void Start()
    {
        GameManager.Instance.OnGammeUnPaused += game_OnGammeUnPaused;
        UpdateVisual();
        Hide();
        HidePressToRebindKey();
    }

    private void game_OnGammeUnPaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsTexts.text = "Sound Effects: "+ Mathf.Round(SoundManager.instance.GetVolume() * 10f);
        musicTexts.text = " Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);

        moveUpText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Move_Up);
        moveDownText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Move_Down);
        moveLeftText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Move_Left);
        moveRightText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Move_Right);
        interactText.text = inputscript.Instance.GetBindingText(inputscript.Binding.Interact);
        altInteractText.text = inputscript.Instance.GetBindingText(inputscript.Binding.AltInteract);


    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false); 
    }

    private void ShowPressToRebindKey()
    {
        pressToRebindKey.gameObject.SetActive(true);
    }
    private void HidePressToRebindKey()
    {
        pressToRebindKey.gameObject.SetActive(false);
    }

    private void RebindingKey(inputscript.Binding binding)
    {
        ShowPressToRebindKey();
        inputscript.Instance.RebindBinding(binding, () =>
        {
            HidePressToRebindKey();
            UpdateVisual();
        });
    }
}
