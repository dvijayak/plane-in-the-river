using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 1f;

    public float cooldownSeconds = 1f;
    private float cooldownTimer = 0;

    protected string bulletTag;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (HasAttemptedFire())
        {
            if (!IsCoolingDown())
            {                
                SpawnBullet();

                RefreshCooldownTimer();
            }
        }

        Cooldown();
    }

    protected abstract bool HasAttemptedFire();

    void SpawnBullet()
    {
        GameObject go = Instantiate(bulletPrefab);
        go.tag = bulletTag;
        go.transform.SetParent(transform);
        go.transform.position = transform.position;
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);
        Destroy(go, 3f);
    }

    void Cooldown()
    {
        if (IsCoolingDown())
        {
            cooldownTimer -= Time.deltaTime;
        }
    }

    void RefreshCooldownTimer()
    {
        cooldownTimer = cooldownSeconds;        
    }

    bool IsCoolingDown()
    {
        return cooldownTimer > 0;
    }
}
