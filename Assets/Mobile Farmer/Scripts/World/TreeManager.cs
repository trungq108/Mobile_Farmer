using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : Singleton<TreeManager>
{
    [SerializeField] private GameObject ToolButtonsGroup;
    [SerializeField] private GameObject TreeButton;
    private Button treeButton;
    private Tree currentTree;

    private void OnEnable()
    {
        EventManager.AddListener<EnterTreeZone>(EnterTreeZoneCallBack);
        EventManager.AddListener<ExitTreeZone>(ExitTreeZoneCallBack);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<EnterTreeZone>(EnterTreeZoneCallBack);
        EventManager.RemoveListener<ExitTreeZone>(ExitTreeZoneCallBack);
    }

    private void Awake()
    {
        treeButton = TreeButton.GetComponent<Button>();
        treeButton.onClick.AddListener(() => StartTreeMode());
        TreeButton.SetActive(false);
        ToolButtonsGroup.SetActive(true);
    }

    private void StartTreeMode()
    {
        currentTree.EnterTreeMode();

        EnterTreeMode e = new EnterTreeMode();
        e.tree = currentTree;
        e.playerShakeTreePos = currentTree.PlayerShakeTreePos.position;
        EventManager.TriggerEvent(e);
    }

    private void EnterTreeZoneCallBack(EnterTreeZone e)
    {
        currentTree = e.tree;
        TreeButton.SetActive(true);
        ToolButtonsGroup.SetActive(false);
    }

    private void ExitTreeZoneCallBack(ExitTreeZone e)
    {
        currentTree.ExitTreeMode();
        currentTree = null;
        TreeButton.SetActive(false);
        ToolButtonsGroup.SetActive(true);
    }
}
