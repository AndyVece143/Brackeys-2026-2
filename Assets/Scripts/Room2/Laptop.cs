using UnityEngine;

public class Laptop : MonoBehaviour
{
    public Player player;
    public LaptopUI laptopUI;
    public InspectBox inspectBox;
    public string[] dialogue;

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
                switch (player.escapeBool)
                {
                    case true:
                        InspectBox newInspectBox = Instantiate(inspectBox);
                        newInspectBox.lines = dialogue;
                        break;
                    case false:
                        LaptopUI newLaptopUI = Instantiate(laptopUI);
                        newLaptopUI.laptop = this;
                        break;
                }
            }
        }
    }
}
