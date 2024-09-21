using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Market"))
        {
            OnCropSelling e = new OnCropSelling();
            EventManager.TriggerEvent(e);
        }

        if (other.CompareTag("Tree"))
        {
            EnterTreeZone e = new EnterTreeZone();
            e.tree = other.GetComponent<Tree>();
            EventManager.TriggerEvent(e);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree"))
        {
            ExitTreeZone e = new ExitTreeZone();
            e.tree = other.GetComponent<Tree>();
            EventManager.TriggerEvent(e);
        }
    }
}
