// using Unity.VisualScripting;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float laserSpeed = 20f;
    private Rigidbody2D rb;
    private float xSpeed;

    PlayerMovement player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerMovement>();
        xSpeed = player.transform.localScale.x;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(Mathf.Sign(xSpeed) * laserSpeed, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
