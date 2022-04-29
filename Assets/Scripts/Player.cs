using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Set this speed via the Unity Inspector Window
    public float speed = 1f;
    public GameObject bulletPrefab;
    public Text scoreText;
    //public TMP_Text scoreText;
    public GameObject wonLostPanel;
    public Text wonLostText;
    //public TMP_Text wonLostText;

    private int MaxScore = 0;
    
    private void Awake()
    {
        var enemySpawner = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        var gridSize = enemySpawner.GridSize;
        MaxScore = gridSize.x * gridSize.y; // enemy count equals maximum score
    }

    private int _score = 0;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleShooting();
    }
    
    void HandleMovement()
    {
        // Get the horizontal axis.
        // By default this is mapped to the arrow keys but also to A and D.
        // The value is in the range -1 to 1
        float horz = Input.GetAxis("Horizontal");
        // Mulitply with speed factor
        horz *= speed;
        // Convert from meters per frame to meters per second
        horz *= Time.deltaTime; 
        
        // Move translation along the object's z-axis
        transform.Translate(horz, 0, 0);
    }

    void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Spawn Bullet
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"Player - OnTriggerEnter2D: {other.gameObject.name}");
        if (other.CompareTag("Enemy"))
        {
            // game lost
            Lost();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log($"Player - OnCollisionEnter2D: {other.gameObject.name}");
    }

    public void IncreaseScore()
    {
        _score++;
        scoreText.text = $"Score: {_score * 100}";

        if (_score == MaxScore)
        {
            Won();
        }
    }

    public void Won()
    {
        wonLostPanel.SetActive(true);
        wonLostText.text = "You Won!";
    }

    public void Lost()
    {
        wonLostPanel.SetActive(true);
        wonLostText.text = "You Lost!";
    }
}
