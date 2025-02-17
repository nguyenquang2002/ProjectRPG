using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SwordSpawner : BaseSpawner<SwordController>
{
    [SerializeField] private float spawnInterval = 2f;
    public override SwordController CreateObject()
    {
        SwordController sword = Instantiate(prefab);
        sword.gameObject.SetActive(false);
        return sword;
    }

    public override void OnGet(SwordController sword)
    {
        sword.gameObject.SetActive(true);
    }

    public override void OnRelease(SwordController sword)
    {
        sword.gameObject.SetActive(false);
    }

    protected override void Update()
    {
        base.Update();

        if (objectCount < maxObjetctsAtOnce && timer >= spawnInterval)
        {
            // Get a sword from the pool
            SwordController sword = objectPool.Get();
            if (sword != null)
            {
                objectCount++;
            }
            timer = 0f;
        }
    }
}
