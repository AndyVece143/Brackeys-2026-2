using UnityEngine;

public class Developer : MonoBehaviour
{
    public Player player;
    public BigDialogue dialogue1;
    public BigDialogue dialogue2;

    public BoxCollider2D boxCollider;
    public CameraController mainCamera;
    public bool checker = false;
    private int progress = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        mainCamera = CameraController.FindAnyObjectByType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollider.IsTouching(player.boxCollider))
        {
            if (Input.GetKeyDown(KeyCode.Space) && player.state != Player.State.NoMove)
            {
                player.inspectIcon.enabled = false;
                mainCamera.state = CameraController.State.StayStill;
                player.StopMoving(1);

                if (progress == 0)
                {
                    BigDialogue newDialogue = Instantiate(dialogue1);
                    progress++;
                }
                else
                {
                    BigDialogue newDialogue = Instantiate(dialogue2);
                    checker = true;
                }
            }
        }
    }
}
