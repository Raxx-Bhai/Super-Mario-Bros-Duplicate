using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float force = 3f; //movement force
    public float coefficient_of_friction = 0.2f; //friction force = coefficient of friction x normal reaction from ground
    public float maxspeed = 6.0f; //speed limit
    public float jumpforce = 350f; //jump with the following force

    private int direction = 0; //direction of movement
    private float speed = 0f; //movement speed
    private bool isjumping = false; //track if player is jumping
    private float direction_of_movement = 0; //direction in which player is moving, irrespective of where the force is being applied
    private float velocity = 0f; //movement speed and direction of movement

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Direction();
        Movement();
        Friction();
    }

    void Direction()
    {
        if (Input.GetKey(KeyCode.D))
        {
            direction = 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            direction = -1;
        }
    }

    void Movement()
    {
        if ((Input.GetKey(KeyCode.D)) || (Input.GetKey(KeyCode.A)))
        {
            if (speed <= maxspeed)
            {
                rb.AddForce(new Vector2(direction * force, 0));
                speed = Mathf.Abs(rb.linearVelocityX);
                velocity = rb.linearVelocityX;
            }

            else
            {
                speed = maxspeed;
            }

            direction_of_movement = velocity / speed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isjumping)
            {
                rb.AddForce(new Vector2(0, jumpforce));
            }
        }
        
    }

    void Friction()
    {
        if (speed > 0)
        {
            rb.AddForce(new Vector2(-direction_of_movement * coefficient_of_friction * rb.mass, 0));
        }
        else
        {
            
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            isjumping = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isjumping = true;
        }
    }
}