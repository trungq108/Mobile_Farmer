using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private PlayerAnimation anim;
    [SerializeField] private ParticleSystem seedParticle;
    [SerializeField] private LayerMask cropLayerMask;


    private void Update()
    {
        Debug.DrawRay(transform.position + Vector3.up, Vector3.down * 5, Color.red);
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, 5f, cropLayerMask))
        {
            Sowing();
        }
        else StopSowing();
    }

    public void Sowing()
    {
        anim.PlaySowAnim();
    }

    public void StopSowing()
    {
        anim.StopSowAnim();
    }

    public void ThrowSeed()
    {
        seedParticle.Play();
    }
}
