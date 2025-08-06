using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Tile : Clickable
{
    private Transform tform;
    private SpriteRenderer render;

    private float greenIntensity;
    private Color ogColor;

    [SerializeField] private string tileType;
    // Start is called before the first frame update
    void Start()
    {
        tform = GetComponent<Transform>();
        render = GetComponent<SpriteRenderer>();
        render.color = GetShade(); //Want to move to SetTileType(), but throws errors?

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTileType(string type)
    {
        tileType = type;
        //render.color = GetShade();     //doesn't work, referencing a null instance of an object?
    }

    private Color GetShade()
    {
        if (tileType == "Land")
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
            ogColor = new Color(0, greenIntensity / 255, 0, 1);
        }
        else if (tileType == "Water")
        {
            ogColor = new Color(0, 160f / 255f, 1, 1);
        }
        else if (tileType == "Mountain")
        {
            ogColor = new Color(70f/255f, 70f/255f, 70f/255f, 1);
        }
        else
        {
            ogColor = new Color(1, 0, 0, 1);
        }

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

    public override void OnClick()
    {
        Debug.Log("Tile Clicked: " + this.transform.position);
    }
}
