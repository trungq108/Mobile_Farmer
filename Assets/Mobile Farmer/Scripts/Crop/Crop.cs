using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public void Grown()
    {
        this.transform.DOScale(Vector3.one * 3, 1f);
    }
}
