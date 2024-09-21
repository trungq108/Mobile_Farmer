using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private GameObject treeVCam;
    [SerializeField] private GameObject treeModel;
    [field: SerializeField] public Transform PlayerShakeTreePos {  get; private set; }

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
        DOTween.Kill(treeModel.transform);
        treeModel.transform.localPosition = originTreeModelPos;
        treeModel.transform.localRotation = originTreeModelRot;
        treeModel.transform.DOShakePosition(shakeDuration, shakePositionStrength, vibrato, randomness, fadeOut: true)
                            .OnUpdate(
                             () => treeModel.transform.DOShakeRotation(shakeDuration, shakeRotationStrength, vibrato, randomness, fadeOut: true));
    }
}

