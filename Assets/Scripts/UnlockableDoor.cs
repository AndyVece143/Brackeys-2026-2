using UnityEngine;

public class UnlockableDoor : MonoBehaviour
{
    public Player player;
    public string[] lockedDoor;
    public InspectBox inspectBox;

    public BoxCollider2D boxCollider;
    public CameraController mainCamera;
    public LevelLoader loader;
    public string sceneName;
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
                player.goIcon.enabled = false;
                mainCamera.state = CameraController.State.StayStill;
                player.StopMoving(1);


                if (player.escapeBool == false)
                {
                    InspectBox newInspectBox = Instantiate(inspectBox);
                    newInspectBox.lines = lockedDoor;
                }

                else
                {
                    loader.LoadNextLevel(sceneName);
                }

            }
        }
    }
}
