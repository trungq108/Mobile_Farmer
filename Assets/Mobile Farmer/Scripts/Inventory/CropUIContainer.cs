using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropUIContainer : MonoBehaviour
{
    [SerializeField] private Image cropIcon;
    [SerializeField] private TextMeshProUGUI amountText;

    private Item item;  

    public void Configue(Item item)
    {
        this.item = item;
        this.cropIcon.sprite = item.cropIcon;
        this.amountText.text = item.amount.ToString();
    }


}
