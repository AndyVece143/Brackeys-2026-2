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
    private Rigidbody2D body;
    public BoxCollider2D boxCollider;

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
                    
        }
    }

    private void OffCameraMovement()
    {
        transform.position = new Vector2(player.transform.position.x -11, player.transform.position.y);
    }
}
