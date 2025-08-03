using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Transform tform;
    private SpriteRenderer render;

    private float greenIntensity;
    private Color ogColor;

    public string tileType;
    // Start is called before the first frame update
    void Start()
    {
        tform = GetComponent<Transform>();
        render = GetComponent<SpriteRenderer>();
        render.color = GetShade();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Color GetShade()
    {
        if (tform.position.x % 2 == 0)
        {
            if (tform.position.y % 2 == 0)
            {
                greenIntensity = 100f;
            }
            else
            {
                greenIntensity = 150f;
            }
            
        }
        else
        {
            if (tform.position.y % 2 == 0)
            {
                greenIntensity = 150f;
            }
            else
            {
                greenIntensity = 100f;
            }
            
        }
        ogColor = new Color(0f, greenIntensity / 255f, 0f, 1f);
        return ogColor;
    }

    public void SetFadedColor()
    {
        Color fadedColor = Color.Lerp(ogColor, Color.white, 0.3f);
        render.color = fadedColor;
    }
    public void ResetColor()
    {
        render.color = ogColor;
    }
}
