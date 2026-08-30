using UnityEngine;
using System.Collections;

public class EpilogueManager : MonoBehaviour
{
    public Player player;
    public CameraController mainCamera;
    public SoloBigDialogue dialogue1;
    public BigDialogue dialogue2;
    public SoloBigDialogue dialogue3;
    private int progress;
    public GameObject ghost;
    public SpriteRenderer ghostSprite;
    public Rigidbody2D ghostBody;
    private Color fullColor;
    private Color emptyColor;
    public LevelLoader loader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fullColor = ghostSprite.color;
        ghostSprite.color = new Color(ghostSprite.color.r, ghostSprite.color.g, ghostSprite.color.b, 0);
        emptyColor = ghostSprite.color;
        StartCutscene();
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
                StartCoroutine(Cutscene4());
                break;
        }
    }

    private IEnumerator Cutscene1()
    {
        player.StopMoving(0);
        yield return new WaitForSeconds(1.5f);
        player.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(1);
        player.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(1);
        SoloBigDialogue newDialogue = Instantiate(dialogue1);
    }

    private IEnumerator Cutscene2()
    {
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
        StartCoroutine(MoveGhost());

        float time = 0;
        float duration = 7;
        while (time < duration)
        {
            time += Time.deltaTime;
            //float t = 1.0f - Mathf.Exp(-3 * Time.deltaTime);
            float t = time / duration;
            //ghostSprite.color = Color.Lerp(ghostSprite.color, fullColor, time / duration);
            ghostSprite.color = Color.Lerp(fullColor, emptyColor, t);
            if (ghostSprite.color == emptyColor)
            {
                Debug.Log("Sans");
            }
            yield return null;
        }
        ghostSprite.color = emptyColor;

        yield return new WaitForSeconds(1.5f);
        SoloBigDialogue newDialogue = Instantiate(dialogue3);
    }

    private IEnumerator MoveGhost()
    {
        float time = 0;
        float duration = 7;
        Vector2 location = new Vector2(9.8f, 6.44f);
        Vector2 startingPos = ghost.transform.position;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            //ghostSprite.color = Color.Lerp(ghostSprite.color, fullColor, time / duration);
            ghost.transform.position = Vector2.Lerp(startingPos, location, t);
            if (ghostSprite.color == emptyColor)
            {
                Debug.Log("Sans");
            }
            yield return null;
        }
    }

    private IEnumerator Cutscene4()
    {
        StartCoroutine(player.GoToPlace(new Vector2(10, -1.75f), 4));
        yield return new WaitForSeconds(1);
        loader.LoadNextLevel("Title");
    }
}
