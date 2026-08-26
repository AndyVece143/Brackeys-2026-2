using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    public PrologueManager manager;
    public Player player;
    public SoloBigDialogue dialogue1;
    public InspectBox inspectBox;
    public string[] dialogue2;
    public BoxCollider2D boxCollider;
    public bool checker = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollider.IsTouching(player.boxCollider))
        {
            if (Input.GetKeyDown(KeyCode.Space) && player.state != Player.State.NoMove)
            {
                switch (manager.progress)
                {
                    case 0:
                        player.StopMoving(1);
                        player.inspectIcon.enabled = false;
                        SoloBigDialogue newDialogue = Instantiate(dialogue1);
                        manager.progress++;
                        manager.PrepareForJumpscare();
                        break;

                    case 1:
                        player.StopMoving(1);
                        player.inspectIcon.enabled = false;
                        InspectBox newInspectBox = Instantiate(inspectBox);
                        newInspectBox.lines = dialogue2;
                        checker = true;
                        break;
                }
            }
        }
    }
}
