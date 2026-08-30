using UnityEngine;
using System.Collections;

public class Room4Manager : MonoBehaviour
{
    public Player player;
    public CameraController mainCamera;
    public SoloBigDialogue dialogue1;
    public BigDialogue dialogue2;
    public SoloBigDialogue dialogue3;
    private int progress;
    public GameObject ghost;
    public SpriteRenderer ghostSprite;
    private Color fullColor;
    private Color emptyColor;
    public AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullColor = ghostSprite.color;
        ghostSprite.color = new Color(ghostSprite.color.r, ghostSprite.color.g, ghostSprite.color.b, 0);
        emptyColor = ghostSprite.color;

        switch (StaticData.room4CutsceneWatch)
        {
            case true:
                source.Play();
                break;
            case false:
                StaticData.room4CutsceneWatch = true;
                StartCutscene();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartCutscene()
    {
        switch (progress)
        {
            case 0:
                StartCoroutine(Cutscene1());
                progress++;
                break;
            case 1:
                StartCoroutine(Cutscene2());
                progress++;
                break;
            case 2:
                StartCoroutine(Cutscene3());
                progress++;
                break;
            case 3:
                source.Play();
                break;
        }
    }

    private IEnumerator Cutscene1()
    {
        player.StopMoving(0);
        yield return new WaitForSeconds(1.5f);
        player.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(1);
        player.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(1);
        SoloBigDialogue newDialogue = Instantiate(dialogue1);
    }

    private IEnumerator Cutscene2()
    {
        StartCoroutine(player.GoToPlace(new Vector2(-6.22f, -1.22f), 2));
        yield return new WaitForSeconds(2);
        player.StopMoving(0);

        float time = 0;
        float duration = 2;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-3 * Time.deltaTime);
            //ghostSprite.color = Color.Lerp(ghostSprite.color, fullColor, time / duration);
            ghostSprite.color = Color.Lerp(ghostSprite.color, fullColor, t);
            if (ghostSprite.color == fullColor)
            {
                Debug.Log("Sans");
            }
            yield return null;
        }

        ghostSprite.color = fullColor;

        //yield return new WaitForSeconds(duration);
        BigDialogue newDialogue = Instantiate(dialogue2);
    }

    private IEnumerator Cutscene3()
    {
        player.StopMoving(0);
        float time = 0;
        float duration = 2;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-3 * Time.deltaTime);
            //ghostSprite.color = Color.Lerp(ghostSprite.color, fullColor, time / duration);
            ghostSprite.color = Color.Lerp(ghostSprite.color, emptyColor, t);
            if (ghostSprite.color == emptyColor)
            {
                Debug.Log("Sans");
            }
            yield return null;
        }
        ghostSprite.color = emptyColor;

        yield return new WaitForSeconds(1);
        SoloBigDialogue newDialogue = Instantiate(dialogue3);
    }
}
