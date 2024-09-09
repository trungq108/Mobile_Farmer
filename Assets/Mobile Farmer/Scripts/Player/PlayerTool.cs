using Sirenix.OdinInspector.Editor.GettingStarted;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTool : MonoBehaviour
{
    [SerializeField] private Button[] toolButtons;
    private Image[] toolImages;
    public Tool CurrentTool {  get; private set; }

    private void Awake()
    {
        toolImages = new Image[toolButtons.Length];
        for (int i = 0; i < toolButtons.Length; i++)
        {
            int index = i;
            toolImages[index] = toolButtons[index].GetComponent<Image>();
            toolButtons[index].onClick.AddListener(() => OnToolSeclection(index));
        }
        OnToolSeclection(0);
    }

    public void OnToolSeclection(int toolIndex)
    {
        CurrentTool = (Tool)toolIndex;
        for(int i = 0;i < toolImages.Length;i++)
        {
            int index = i;
            toolImages[index].color = index == toolIndex ? Color.yellow : Color.white;
        }

        ChangeTool e = new ChangeTool();
        e.toolChange = CurrentTool;
        EventManager.TriggerEvent(e);
    }
}
