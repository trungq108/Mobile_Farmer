using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CropUIContainer : MonoBehaviour
{
    [SerializeField] private Image cropIcon;
    [SerializeField] private TextMeshProUGUI amountText;

    public Item Item {  get; private set; }  

    public void Configue(Item item)
    {
        this.Item = item;
        this.cropIcon.sprite = GameAsset.Instance.GetSpriteFromCropType(item.cropType);
        this.amountText.text = item.amount.ToString();
    }
}
