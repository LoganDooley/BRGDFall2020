using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{

    public Color newColor;
    public Color original;
    SpriteRenderer m_SpriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        original = m_SpriteRenderer.color;
    }

    void OnMouseEnter()
    {
        m_SpriteRenderer.color = newColor;
        print("mouse entered");
    }

    void OnMouseExit()
    {
        m_SpriteRenderer.color = original;
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
