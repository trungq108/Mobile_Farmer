using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player Player {  get; private set; }

    private void Awake()
    {
        Player = FindObjectOfType<Player>();
    }
}
