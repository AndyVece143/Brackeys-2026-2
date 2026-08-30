using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DeathGhost : MonoBehaviour
{
    public DeathScreen screen;
    public Player player;
    public enum State
    {
        OffCamera,
        Jumpscare,
    }
    public State state;

    public float speed;
    public Rigidbody2D body;
    public BoxCollider2D boxCollider;
    public SpriteRenderer spriteRenderer;
    public AudioClip jumpscare;

    public bool comingFromLeft;
    public Light2D globalLight;
    public Color lightColor;
    public AudioSource source;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.OffCamera;

        if (comingFromLeft != true)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.OffCamera:
                OffCameraMovement();
                break;
            case State.Jumpscare:
                JumpscareMovement();
                break;
        }
    }

    private void OffCameraMovement()
    {
        switch (comingFromLeft)
        {
            case true:
                transform.position = new Vector2(player.transform.position.x - 11, player.transform.position.y);
                spriteRenderer.enabled = false;
                break;
            case false:
                transform.position = new Vector2(player.transform.position.x + 11, player.transform.position.y);
                spriteRenderer.enabled = false;
                break;
        }

    }

    private void JumpscareMovement()
    {
        switch (comingFromLeft)
        {
            case true:
                spriteRenderer.enabled = true;
                if (transform.position.y - player.transform.position.y > 0)
                {
                    body.linearVelocity = new Vector2(speed, -2f);
                }

                else if (transform.position.y - player.transform.position.y < 0)
                {
                    body.linearVelocity = new Vector2(speed, 2f);
                }

                else
                {
                    body.linearVelocity = new Vector2(speed, 0f);
                }
                break;

            case false:
                spriteRenderer.enabled = true;
                if (transform.position.y - player.transform.position.y > 0)
                {
                    body.linearVelocity = new Vector2(-speed, -2f);
                }

                else if (transform.position.y - player.transform.position.y < 0)
                {
                    body.linearVelocity = new Vector2(-speed, 2f);
                }

                else
                {
                    body.linearVelocity = new Vector2(-speed, 0f);
                }
                break;
        }

        //body.linearVelocity = new Vector2(-speed, transform.position.y - player.transform.position.y);
        //transform.position = new Vector2(transform.position.x, player.transform.position.y);

    }

    public void SwitchToJumpscare()
    {
        source.Stop();
        SoundManager.instance.PlaySound(jumpscare);
        state = State.Jumpscare;
        spriteRenderer.enabled = true;

        if (globalLight)
        {
            globalLight.color = lightColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            screen.YouDied();
            player.StopMoving(2);
        }
    }
}
