using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    private const string POP_UP = "PopUp";

    [SerializeField] private Image backgorundImage;
    [SerializeField] private Image IconImage;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private Color suceesColor;
    [SerializeField] private Color failedcolor;
    [SerializeField] private Sprite SucessSprite;
    [SerializeField] private Sprite failedsprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        DeliveryManager.Instance.OnDeliverySucess += Delivery_OnDeliverySucess;
        DeliveryManager.Instance.OnDeliveryfailed += Delivery_OnDeliveryfailed;

        gameObject.SetActive(false);
    }

    private void Delivery_OnDeliveryfailed(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POP_UP);
        backgorundImage.color = failedcolor;
        IconImage.sprite = failedsprite;
        messageText.text = "RIZZ\nLOST-";
    }

    private void Delivery_OnDeliverySucess(object sender, System.EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(POP_UP);
        backgorundImage.color = suceesColor;
        IconImage.sprite = SucessSprite;
        messageText.text = "RIZZ\nGAINED+";
    }
}
