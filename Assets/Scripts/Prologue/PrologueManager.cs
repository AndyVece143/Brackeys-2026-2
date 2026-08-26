using System.Collections;
using TMPro;
using UnityEngine;

public class PrologueManager : MonoBehaviour
{
    public Player player;
    public GhostPrologue ghost;
    public CameraController mainCamera;
    public SoloBigDialogue dialogue1;
    public GameObject entranceWall;

    public GameObject jumpscareCollision;
    public int progress = 0;
    public GameObject panel;
    public TextMeshProUGUI ominousText;
    private Color fullColor;
    private Color emptyColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jumpscareCollision.SetActive(false);
        fullColor = ominousText.color;
        ominousText.color = new Color(ominousText.color.r, ominousText.color.g, ominousText.color.b, 0);
        emptyColor = ominousText.color;
        ominousText.enabled = false;
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrepareForJumpscare()
    {
        jumpscareCollision.SetActive(true);
    }

    public void GetAttackedFool()
    {
        StartCoroutine(WasAttacked());
        player.StopMoving(1);
    }

    private IEnumerator WasAttacked()
    {
        panel.SetActive(true);
        yield return new WaitForSeconds(5);
        ominousText.enabled = true;
        float time = 0;
        while (time < 2)
        {
            time += Time.deltaTime;
            ominousText.color = Color.Lerp(ominousText.color, fullColor, time / 2);
            yield return null;
        }

        yield return new WaitForSeconds(2);

        time = 0;

        while (time < 2)
        {
            time += Time.deltaTime;
            ominousText.color = Color.Lerp(ominousText.color, emptyColor, time / 2);
            yield return null;
        }
    }

    private IEnumerator Cutscene()
    {
        //player.StopMoving(1);
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(player.GoToPlace(new Vector2(-6, -1.75f), 3));
        yield return new WaitForSeconds(4);
        entranceWall.SetActive(true);
        SoloBigDialogue newDialogue = Instantiate(dialogue1);
    }
}
