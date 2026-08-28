using Unity.VisualScripting;
using UnityEngine;

public class ReadableNote : MonoBehaviour
{
    public Player player;

    [TextArea]
    public string text;
    public StickyNote note;

    public BoxCollider2D boxCollider;
    public CameraController mainCamera;
    public bool checker = false;

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

                StickyNote newNote = Instantiate(note);
                newNote.text = text;
                checker = true;
            }
        }
    }
}
