using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool fadeOut = false;
    public float fadeOutSpeed = 3f;

    private Color myColor;
    private Color alphaColor;

    private void Start()
    {
        Color startColor = this.gameObject.GetComponent<Renderer>().material.color;
        myColor = new Color(startColor.r, startColor.g, startColor.b, startColor.a);
        alphaColor = new Color(myColor.r, myColor.g, myColor.b, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"Enemy - OnTriggerEnter2D: {other.gameObject.name}");
    }

    private void Update()
    {
        if(fadeOut)
        {
            Color newColor = 
                Color.Lerp(this.gameObject.GetComponent<Renderer>().material.color, 
                alphaColor, fadeOutSpeed * Time.deltaTime);
            foreach (var renderer in this.gameObject.GetComponentsInChildren<Renderer>())
            {
                renderer.material.color = newColor;
            }
        }
    }
}
