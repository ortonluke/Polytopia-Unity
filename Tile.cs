using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private SpriteRenderer render;

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        float green = Random.Range(100f, 150f);
        Debug.Log("Green" + green);

        render.color = new Color(0f, green / 255f, 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
