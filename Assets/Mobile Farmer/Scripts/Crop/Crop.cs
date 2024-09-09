using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crop : MonoBehaviour
{
    public void Grown()
    {
        transform.localScale = Vector3.one * 3;
    }
}
