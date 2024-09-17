using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderInteract : MonoBehaviour
{
    [SerializeField] private Material cropMaterial;
    private Player player;

    private void Start()
    {
        player = GameManager.Instance.Player;
    }

    private void Update()
    {
        cropMaterial.SetVector("_PlayerPosition", player.transform.position);
    }

}
