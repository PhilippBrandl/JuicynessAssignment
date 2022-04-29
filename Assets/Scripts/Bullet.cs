using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public AudioSource audioSource;
    public AudioClip explosionSound;

    private Player player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        audioSource = Camera.main.GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var movementY = speed * Time.deltaTime;
        // Move the bullet along the y-axis
        transform.Translate(0f, movementY, 0f);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"Bullet - OnTriggerEnter: {other.name}");
        if (other.CompareTag("Enemy"))
        {
            // hit enemy, hence increase score
            audioSource.PlayOneShot(explosionSound, 3f);
            //other.gameObject.GetComponent<Explodable>().explode();
            Destroy(other.gameObject);
            player.IncreaseScore();
            Destroy(gameObject);
        }
        if (other.CompareTag("Top"))
        { // bullet self-destroys when hitting top screen bounds collider
            Destroy(gameObject);
        }
    }
}
