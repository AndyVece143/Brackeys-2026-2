using UnityEngine;

public class PuzzleBlood : MonoBehaviour
{
    public Player player;
    public string[] lookAtBlood;
    public string[] needACup;
    public string[] getBlood;
    public string[] bloodDone;
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
                player.StopMoving(2);
                InspectBox newInspectBox = Instantiate(inspectBox);

                if (manager.puzzleProgress < 2)
                {
                    newInspectBox.lines = lookAtBlood;
                }
                else if (manager.puzzleProgress == 2)
                {
                    newInspectBox.lines = needACup;

                }
                else if (manager.puzzleProgress == 3)
                {
                    newInspectBox.lines = getBlood;
                    manager.puzzleProgress++;
                    checker = true;
                }
                else
                {
                    newInspectBox.lines = bloodDone;
                }
            }
        }
    }
}
