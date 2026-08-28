using UnityEngine;

public class PuzzlePlant : MonoBehaviour
{
    public Player player;
    public string[] lookAtPlant;
    public string[] waterPlant;
    public string[] plantDone;
    public InspectBox inspectBox;

    public BoxCollider2D boxCollider;
    public CameraController mainCamera;
    public bool checker = false;
    public Room1Manager manager;

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
                InspectBox newInspectBox = Instantiate(inspectBox);

                if (manager.puzzleProgress < 4)
                {
                    newInspectBox.lines = lookAtPlant;
                }
                else if (manager.puzzleProgress == 4)
                {
                    newInspectBox.lines = waterPlant;
                    manager.puzzleProgress++;
                    player.escapeBool = true;
                    checker = true;
                }
                else
                {
                    newInspectBox.lines = plantDone;
                }
            }
        }
    }
}
