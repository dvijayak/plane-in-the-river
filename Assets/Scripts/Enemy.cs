using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;

    public delegate void KillHandler(); // define a function type
    public event KillHandler OnKill; // reference to a function of this type

    // Start is called before the first frame update
    void Start()
    {

    }

    void OnEnable()
    {
        // Put `Start` logic here instead
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);

        Vector2 viewSpacePos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewSpacePos.y < 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("PlayerBullet"))
        {
            gameObject.SetActive(false);
            Destroy(otherCollider.gameObject);

            // Inform observers that we have died
            OnKill?.Invoke();
        }
    }
}
