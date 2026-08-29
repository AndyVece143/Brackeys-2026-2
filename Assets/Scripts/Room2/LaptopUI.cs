using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class LaptopUI : MonoBehaviour
{
    public GameObject screen;
    public GameObject textBox;
    public TMP_InputField nameField;
    public TMP_InputField colorField;
    public TMP_InputField bedField;
    public TMP_InputField animalField;
    public float duration;
    public float dampSpeed;

    private Vector3 screenPosition;
    private Vector3 screenEndPosition;
    private Vector3 tBoxPosition;
    private Vector3 tBoxEndPosition;
    private bool correctAnswer = false;
    public Player player;
    public CameraController mainCamera;
    public Canvas canvas;
    public Laptop laptop;
    public AudioClip correctNoise;
    public AudioClip wrongNoise;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        player = Player.FindAnyObjectByType<Player>();
        mainCamera = CameraController.FindAnyObjectByType<CameraController>();
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetPosition()
    {
        screenPosition = screen.transform.position;

        screen.transform.position = new Vector2(screenPosition.x, screenPosition.y - 10f);
        screenEndPosition = screen.transform.position;
        tBoxPosition = textBox.transform.position;
        tBoxEndPosition = new Vector3(tBoxPosition.x, tBoxPosition.y + 3, tBoxPosition.z);
        textBox.transform.position = tBoxEndPosition;

        StartCoroutine(MoveBeginning());
    }

    IEnumerator MoveBeginning()
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;

            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            screen.transform.position = Vector3.Lerp(screen.transform.position, screenPosition, t);
            yield return null;
        }
    }

    IEnumerator TextBoxMove()
    {
        laptop.checker = true;
        player.escapeBool = true;
        SoundManager.instance.PlaySound(correctNoise);
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            textBox.transform.position = Vector3.Lerp(textBox.transform.position, tBoxPosition, t);
            yield return null;
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(MoveEnd());
    }

    IEnumerator MoveEnd()
    {
        float time = 0;
        while (time < duration)
        {
            time += Time.deltaTime;

            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            screen.transform.position = Vector3.Lerp(screen.transform.position, screenEndPosition, t);
            textBox.transform.position = Vector3.Lerp(textBox.transform.position, tBoxEndPosition, t);
            yield return null;
        }

        player.StartMoving();

        mainCamera.state = mainCamera.initialState;
        Destroy(gameObject);
    }

    public void ConfirmButton()
    {
        if (correctAnswer != true)
        {
            if (nameField.text == "Lilith")
            {
                if (colorField.text == "Pink")
                {
                    if (animalField.text == "Rabbit" || animalField.text == "Rabbits")
                    {
                        if (bedField.text == "Bedsheet" || bedField.text == "Bedsheets")
                        {
                            correctAnswer = true;
                            StartCoroutine(TextBoxMove());
                        }
                    }
                }
            }

            else
            {
                SoundManager.instance.PlaySound(wrongNoise);
                nameField.text = "";
                colorField.text = "";
                animalField.text = "";
                bedField.text = "";
            }
        }
    }

    public void ExitButton()
    {
        StartCoroutine(MoveEnd());
    }
}
