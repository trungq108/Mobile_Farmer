using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticle : MonoBehaviour
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

        WaterSeed e = new WaterSeed();
        e.collisionPositions = collisionPositions;
        EventManager.TriggerEvent(e);
    }
}
