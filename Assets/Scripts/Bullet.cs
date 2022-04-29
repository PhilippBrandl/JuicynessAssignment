using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1f;
    public AudioClip explosionSound;

    private Player player;
    private AudioSource audioSource;

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
            other.enabled = false;
            Camera.main.GetComponent<CameraRumble>().rumble = 2f;
            other.gameObject.GetComponent<Enemy>().fadeOut = true;
            Destroy(other.gameObject, 1 / other.gameObject.GetComponent<Enemy>().fadeOutSpeed);
            player.IncreaseScore();
            ParticleSystem particles = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
            particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            this.transform.DetachChildren();
            Destroy(particles.gameObject, particles.main.startLifetime.constant);
            Destroy(gameObject);
        }
        if (other.CompareTag("Top"))
        { // bullet self-destroys when hitting top screen bounds collider
            Destroy(gameObject);
        }
    }
}
