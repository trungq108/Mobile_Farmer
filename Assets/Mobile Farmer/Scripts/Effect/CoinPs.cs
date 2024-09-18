using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPs : MonoBehaviour
{
    [SerializeField] private ParticleSystem coinPS;
    [SerializeField] private Transform coinRectTranform;
    [SerializeField] private float moveSpeed = 5f;

    private void OnEnable()
    {
        EventManager.AddListener<OnCoinCollect>(PlayCoinParticle);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener<OnCoinCollect>(PlayCoinParticle);
    }

    public void PlayCoinParticle(OnCoinCollect e)
    {
        int amount = e.amount;
        if (coinPS.isPlaying) return;

        ParticleSystem.Burst burst = coinPS.emission.GetBurst(0);
        burst.count = amount;
        coinPS.emission.SetBurst(0, burst);

        ParticleSystem.MainModule main = coinPS.main;
        main.gravityModifier = 1;
        coinPS.Play();

        StartCoroutine(nameof(MoveCoins), amount);
    }

    IEnumerator MoveCoins(int amount)
    {
        yield return new WaitForSeconds(1);

        ParticleSystem.MainModule main = coinPS.main;
        main.gravityModifier = 0;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[amount];

        while (coinPS.isPlaying)
        {
            coinPS.GetParticles(particles);            
            for (int i = 0; i < particles.Length; i++)
            {
                particles[i].position = Vector3.MoveTowards(particles[i].position, coinRectTranform.position, moveSpeed * Time.deltaTime);
                if(Vector3.Distance(particles[i].position, coinRectTranform.position) < 0.01f)
                {
                    particles[i].position = Vector3.one * 1000f;
                    CurrencyManager.Instance.ChangeCurrency(1);
                }
            }
            coinPS.SetParticles(particles);
            yield return null;
        }
    }
}
