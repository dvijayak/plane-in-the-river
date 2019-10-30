using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireController : FireController
{
    public float fireChance = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        bulletTag = "EnemyBullet";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override bool HasAttemptedFire()
    {
        return Random.Range(0, 1) < 0.7f;
    }
}
