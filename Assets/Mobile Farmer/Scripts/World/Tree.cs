using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private GameObject treeVCam;
    [SerializeField] private GameObject treeModel;
    [field: SerializeField] public Transform PlayerShakeTreePos {  get; private set; }
    [SerializeField] private List<Fruit> fruits = new List<Fruit>();

    private float _maxShakeIndex = 100f;
    private float _currentShakeIndex;


    [Header("Setting")]
    [SerializeField] private float shakeDuration ;
    [SerializeField] private float shakePositionStrength;
    [SerializeField] private float shakeRotationStrength;
    [SerializeField] private int vibrato;
    [SerializeField] private float randomness;
    private Vector3 originTreeModelPos;
    private Quaternion originTreeModelRot;

    private void Awake()
    {
        treeVCam.SetActive(false);
        originTreeModelPos = treeModel.transform.localPosition;
        originTreeModelRot = treeModel.transform.localRotation;
        _currentShakeIndex = _maxShakeIndex;
    }

    public void EnterTreeMode()
    {
        treeVCam.SetActive(true);
    }

    public void ExitTreeMode()
    {
        treeVCam.SetActive(false);
    }

    public void ShakeTree()
    {
        // Animation
        DOTween.Kill(treeModel.transform);
        treeModel.transform.localPosition = originTreeModelPos;
        treeModel.transform.localRotation = originTreeModelRot;
        treeModel.transform.DOShakePosition(shakeDuration, shakePositionStrength, vibrato, randomness, fadeOut: true)
                           .OnUpdate(() => treeModel.transform.DOShakeRotation(shakeDuration, shakeRotationStrength, vibrato, randomness, fadeOut: true));

        // Logic
        _currentShakeIndex--;
        TreeManager.Instance.UpdateSlider(_currentShakeIndex / _maxShakeIndex);
        if(_currentShakeIndex % 10 == 0)
        {
            DropFruit();
        } 
    }

    public void DropFruit()
    {
        for(int i = 0; i < fruits.Count; i++)
        {
            if (fruits[i].IsDrop) continue;
            fruits[i].Dropping(); 
            return;
        }
    }
}

