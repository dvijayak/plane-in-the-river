using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : MonoBehaviour
{
    public delegate void FuelCollectHandler();
    public event FuelCollectHandler OnFuelCollect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 viewSpacePos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewSpacePos.y < 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);

            OnFuelCollect?.Invoke();
        }
    }
}
