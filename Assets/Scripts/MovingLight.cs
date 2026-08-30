using UnityEngine;

public class MovingLight : MonoBehaviour
{
    public CapsuleCollider2D capCollider;
    public Rigidbody2D body;
    public float speed;
    public Transform ledgeDetector;
    public float wallDistance;
    public LayerMask groundLayer;
    private bool facingRight = true;
    private Vector2 forwards;
    public bool sideways;
    public DeathGhost ghost;
    public bool stationary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        capCollider  = GetComponent<CapsuleCollider2D>();
        ghost = DeathGhost.FindAnyObjectByType<DeathGhost>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (stationary == false)
        {
            switch (sideways)
            {
                case true:
                    body.linearVelocity = new Vector2(speed, body.linearVelocity.y);
                    if (facingRight)
                    {
                        forwards = Vector2.right;
                    }
                    else
                    {
                        forwards = Vector2.left;
                    }

                    RaycastHit2D hitWall = Physics2D.Raycast(ledgeDetector.position, forwards, wallDistance, groundLayer);

                    if (hitWall == true)
                    {
                        Rotate();
                    }
                    break;

                case false:
                    body.linearVelocity = new Vector2(body.linearVelocity.x, speed);

                    if (facingRight)
                    {
                        forwards = Vector2.up;
                    }
                    else
                    {
                        forwards = Vector2.down;
                    }
                    RaycastHit2D hitWall2 = Physics2D.Raycast(ledgeDetector.position, forwards, wallDistance, groundLayer);


                    if (hitWall2 == true)
                    {
                        Rotate();
                    }
                    break;
            }
        }
    }

    void Rotate()
    {
        transform.Rotate(0, 180, 0);
        speed = -speed;

        if (facingRight)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && ghost.state != DeathGhost.State.Jumpscare)
        {
            ghost.SwitchToJumpscare();
        }
    }
}
