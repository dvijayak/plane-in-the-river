using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireController : FireController
{
    // Start is called before the first frame update
    void Start()
    {
        bulletTag = "PlayerBullet";
    }

    // Update is called once per frame
    void Update()
    {        
    }

    protected override bool HasAttemptedFire()
    {        
        return Input.GetAxis("Fire1") >= 1f;
    }
}
