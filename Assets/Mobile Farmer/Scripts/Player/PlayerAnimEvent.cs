using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour
{
    [SerializeField] private PlayerAbility ability;

    public void ThrowSeedAnimEvent() => ability.ThrowSeed();
    public void ThrowWaterAnimEnvent() => ability.ThrowWater();

    public void HarvestAnimEvent() => ability.ScytheDamage();

}
