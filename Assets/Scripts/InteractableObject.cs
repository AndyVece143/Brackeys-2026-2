using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Player player;
    public string[] dialogue1;
    public string[] dialogue2;
    public InspectBox inspectBox;

    public BoxCollider2D boxCollider;
    public int react;
    public CameraController mainCamera;
    public bool checker = false;
    private int progress = 0;
    public bool death;

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
            if (Input.GetKeyDown(KeyCode.Space) && player.state != Player.State.NoMove && player.canInteract == true)
            {
                player.inspectIcon.enabled = false;
                mainCamera.state = CameraController.State.StayStill;
                player.StopMoving(react);
                InspectBox newInspectBox = Instantiate(inspectBox);

                if (progress == 0)
                {
                    newInspectBox.lines = dialogue1;
                    if (death == true)
                    {
                        newInspectBox.kill = true;
                    }
                    progress++;
                    if (dialogue2 == null)
                    {
                        checker = true;
                    }
                }

                else if (progress == 1 && dialogue2 != null)
                {
                    newInspectBox.lines = dialogue2;
                    checker = true;

                }
            }
        }
    }
}