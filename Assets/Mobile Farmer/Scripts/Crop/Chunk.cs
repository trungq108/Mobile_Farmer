using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private GameObject lock_State_GO;
    [SerializeField] private GameObject unlock_State_GO;
    [SerializeField] private TextMeshPro priceText;
    [SerializeField] private Transform[] checkPoints;

    [Header("Setting")]
    [SerializeField] private int initPrice;
    [SerializeField] private LayerMask chunkLayerMask;

    private int currentPrice;
    private bool isUnlock = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Constain.PLAYER))
        {
            if (isUnlock) return;
            if (!CurrencyManager.Instance.IsEnougtCoin()) return;
            TryUnlockChunk();
        }
    }

    private void TryUnlockChunk()
    {
        currentPrice--;
        CurrencyManager.Instance.ChangeCurrency(-1);
        priceText.text = currentPrice.ToString();
        if (currentPrice <= 0)
            UnlockChunk();
    }

    private void UnlockChunk()
    {
        unlock_State_GO.SetActive(true);
        lock_State_GO.SetActive(false);
        isUnlock = true;

        for(int i = 0; i < checkPoints.Length; i++)
        {
            bool check = Physics.Raycast(checkPoints[i].position + Vector3.up, Vector3.down, 5f, chunkLayerMask);
            if (!check)
            {
                CreatNewChunk e = new CreatNewChunk();
                e.newChunkPosition = checkPoints[i].position;
                EventManager.TriggerEvent(e);
            }
        }
    }

    public void OnInit()
    {
        currentPrice = initPrice;   
        priceText.text = currentPrice.ToString();
    }

    public void LoadChunk(string key)
    {
        transform.position = ES3.Load<Vector3>(key + "_currentPosition", transform.position);
        currentPrice = ES3.Load<int>(key + "_currentPrice", initPrice);
        priceText.text = currentPrice.ToString();
        if (currentPrice <= 0)
        {
            unlock_State_GO.SetActive(true);
            lock_State_GO.SetActive(false);
            isUnlock = true;
        }
    }

    public void SaveChunk(string key)
    {
        ES3.Save<int>(key + "_currentPrice", currentPrice);
        ES3.Save<Vector3>(key + "_currentPosition", transform.position);
    }

}