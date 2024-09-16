using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constain.PLAYER))
        {
            OnCropSelling e = new OnCropSelling();
            EventManager.TriggerEvent(e);
        }
    }
}
