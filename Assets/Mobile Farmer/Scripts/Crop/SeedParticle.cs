using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedParticle : MonoBehaviour
{
    private ParticleSystem ps;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> events = new List<ParticleCollisionEvent>();
        int collisionAmount = ps.GetCollisionEvents(other, events); 
        Vector3[] collisionPositions = new Vector3[collisionAmount];
        for (int i = 0; i < collisionAmount; i++)
        {
            collisionPositions[i] = events[i].intersection;          
        }

        SowSeed e = new SowSeed();
        e.collisionPositions = collisionPositions;
        EventManager.TriggerEvent(e);
    }
}
