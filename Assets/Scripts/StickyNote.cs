using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class StickyNote : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string text;
    public float textSpeed;
    public GameObject letter;
    public Image letterImage;

    public Player player;
    public Canvas canvas;
    private Vector3 letterPosition;
    private Vector3 letterEndPosition;
    private Color fullColor;
    private Color emptyColor;
    public float duration;
    public AudioClip audioClip;
    private const string HTML_ALPHA = "<color=#00000000>";
    public bool ready = false;
    public CameraController mainCamera;
    public float dampSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        player = Player.FindAnyObjectByType<Player>();
        mainCamera = CameraController.FindAnyObjectByType<CameraController>();
        textComponent.text = string.Empty;
        SetPositionAndTrans();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (ready == true)
            {
                StartCoroutine(MovePaperEnd());
            }

        }
    }

    void SetPositionAndTrans()
    {
        letterPosition = letter.transform.position;
        fullColor = letterImage.color;
        letter.transform.position = new Vector3(letter.transform.position.x, letter.transform.position.y - 1f, letter.transform.position.z);
        letterImage.color = new Color(letterImage.color.r, letterImage.color.g, letterImage.color.b, 0);
        letterEndPosition = letter.transform.position;
        emptyColor = letterImage.color;
        StartCoroutine(MovePaperBeginning());
    }

    IEnumerator TypeLine()
    {
        int i = 4;
        string originalText = text;
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in text)
        {
            alphaIndex++;
            textComponent.text = originalText;
            displayedText = textComponent.text.Insert(alphaIndex, HTML_ALPHA);
            textComponent.text = displayedText;

            i++;
            if (i == 5)
            {
                SoundManager.instance.PlaySound(audioClip);
                i = 0;
            }

            yield return new WaitForSeconds(textSpeed);
        }

        ready = true;
    }

    IEnumerator MovePaperBeginning()
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            letter.transform.position = Vector3.Lerp(letter.transform.position, letterPosition, t);
            letterImage.color = Color.Lerp(letterImage.color, fullColor, time / duration);
            yield return null;
        }

        StartCoroutine(TypeLine());
    }

    IEnumerator MovePaperEnd()
    {
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            letter.transform.position = Vector3.Lerp(letter.transform.position, letterEndPosition, t);
            letterImage.color = Color.Lerp(letterImage.color, emptyColor, time / duration);
            textComponent.color = Color.Lerp(textComponent.color, emptyColor, time / duration);
            yield return null;
        }
        player.StartMoving();
        mainCamera.state = mainCamera.initialState;
        Destroy(gameObject);
    }
}
