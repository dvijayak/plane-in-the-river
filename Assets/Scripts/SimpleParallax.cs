using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleParallax : MonoBehaviour
{
    public GameObject parallaxTarget;    
    public float tileHeight;    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject currentTile = transform.GetChild(i).gameObject;
            if (parallaxTarget.transform.position.y - currentTile.transform.position.y >= tileHeight)
            {
                currentTile.transform.position = new Vector3(
                    0,
                    currentTile.transform.position.y + transform.childCount * tileHeight);
            }
        }
    }
}
