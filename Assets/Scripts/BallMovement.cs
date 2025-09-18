using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();      

        // random starting direction
        velocity = Random.insideUnitCircle.normalized * speed;
    }

    void FixedUpdate()
    {
        // Move the ball
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

        // Camera bounds
        Vector3 camPos = Camera.main.transform.position;
        float camHeight = Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;

        float left = camPos.x - camWidth;
        float right = camPos.x + camWidth;
        float bottom = camPos.y - camHeight;
        float top = camPos.y + camHeight;

        float radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;

        float offset = 0.01f;
        // Check collisions with screen edges and reflect vector
        if (rb.position.x - radius <= left)
        {
            velocity = Vector2.Reflect(velocity, Vector2.right);
            rb.position = new Vector2(left + radius + offset, rb.position.y);
        }
        else if (rb.position.x + radius >= right)
        {
            velocity = Vector2.Reflect(velocity, Vector2.left);
            rb.position = new Vector2(right - radius - offset, rb.position.y);
        }
          
        if (rb.position.y - radius <= bottom)
        {
            velocity = Vector2.Reflect(velocity, Vector2.up);
            rb.position = new Vector2(rb.position.x, bottom + radius +  offset);
        }
        else if (rb.position.y + radius >= top)
        {
            velocity = Vector2.Reflect(velocity, Vector2.down);
            rb.position = new Vector2(rb.position.x, top - radius - offset);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("mushroom"))
        {
            // Reflect the velocity depending on where it hits
            Vector2 normal = collision.contacts[0].normal;
            velocity = Vector2.Reflect(velocity, normal).normalized * speed;

            // (Optional) Add a little "angle control" based on where it hits on paddle
            float hitPoint = transform.position.x - collision.transform.position.x;
            velocity.x += hitPoint * 2f; // tweak multiplier for more/less curve
        }
    }
}
