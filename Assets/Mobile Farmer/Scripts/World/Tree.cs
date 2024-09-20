using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private GameObject treeVCam;

    private void Awake()
    {
        treeVCam.SetActive(false);
    }

    public void OnTree()
    {
        treeVCam.SetActive(true);
    }

    public void OffTree()
    {
        treeVCam.SetActive(false);
    }
}

