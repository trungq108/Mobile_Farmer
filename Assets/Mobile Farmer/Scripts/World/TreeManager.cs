using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TreeManager : Singleton<TreeManager>
{
    [SerializeField] private GameObject ToolButtonsGroup;
    [SerializeField] private GameObject TreeButton;
    [SerializeField] private Slider treeModeSlider;

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
        treeModeSlider.value = 1f;
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
        currentTree = null;
        TreeButton.SetActive(false);
        ToolButtonsGroup.SetActive(true);
    }

    public void UpdateSlider(float value)
    {
        treeModeSlider.value = value;
        if(treeModeSlider.value <= 0)
        {
            ExitTreeMode();
        }
    }

    private void ExitTreeMode()
    {
        currentTree.ExitTreeMode();
        ExitTreeMode e = new ExitTreeMode();
        EventManager.TriggerEvent(e);
    }
}
