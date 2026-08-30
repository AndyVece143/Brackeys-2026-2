using UnityEngine;

public class GhostPrologue : MonoBehaviour
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
    public PrologueManager manager;
    public AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = State.OffCamera;
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
        transform.position = new Vector2(player.transform.position.x +11, player.transform.position.y);
        spriteRenderer.enabled = false;
    }

    private void JumpscareMovement()
    {
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
        //body.linearVelocity = new Vector2(-speed, transform.position.y - player.transform.position.y);
        //transform.position = new Vector2(transform.position.x, player.transform.position.y);

    }

    public void SwitchToJumpscare()
    {
        SoundManager.instance.PlaySound(jumpscare);
        state = State.Jumpscare;
        spriteRenderer.enabled = true;
        source.Stop();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Sans1");
            manager.GetAttackedFool();
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Sans32");
    //        manager.GetAttackedFool();
    //    }
    //}
}
