using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PrankGhost : MonoBehaviour
{
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
    public Light2D globalLight;
    public Color redColor;
    public Color ogColor;
    public BigDialogue dialogue;
    public CameraController mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.OffCamera;
        mainCamera = CameraController.FindAnyObjectByType<CameraController>();
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
                JumpScareMovement();
                break;
        }
    }

    private void OffCameraMovement()
    {
        transform.position = new Vector2(player.transform.position.x - 11, player.transform.position.y);
        spriteRenderer.enabled = false;

    }

    private void JumpScareMovement()
    {
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
    }

    public void SwitchToJumpscare()
    {
        SoundManager.instance.PlaySound(jumpscare);
        state = State.Jumpscare;
        spriteRenderer.enabled = true;

        if (globalLight)
        {
            globalLight.color = redColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.StopMoving(2);
            globalLight.color = ogColor;
            mainCamera.state = CameraController.State.StayStill;
            BigDialogue newBigDialogue = Instantiate(dialogue);
            Destroy(gameObject);
        }
    }
}
