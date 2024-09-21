using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject GAME_PANEL;
    [SerializeField] private GameObject TREE_PANEL;
    List<GameObject> panels = new List<GameObject>();

    private void OnEnable()
    {
        EventManager.AddListener<EnterTreeMode>(StartTreeModeCallBack);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<EnterTreeMode>(StartTreeModeCallBack);
    }

    private void Awake()
    {
        panels.AddRange(new GameObject[] { GAME_PANEL, TREE_PANEL, });
        ShowPanel(GAME_PANEL);
    }

    public void StartTreeModeCallBack(EnterTreeMode e)
    {
        ShowPanel(TREE_PANEL);
    }

    public void ShowPanel(GameObject panel)
    {
        for (int i = 0; i < panels.Count; i++)
        {
            panels[i].SetActive(panels[i] == panel);
        }
        //SoundManager.ButtonClick();
    }



}

