using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lamp : MonoBehaviour
{
    public Player player;
    public Light2D lampLight;
    public float distance;
    public AudioClip click;
    private bool lightOn = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lampLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= 5 && lightOn == false)
        {
            ChangeLight(true);
        }
        else if (distance >= 5 && lightOn == true)
        {
            ChangeLight(false);
        }
    }

    private void ChangeLight(bool turnOn)
    {
        if (turnOn)
        {
            lampLight.enabled = true;
            lightOn = true;
        }
        else
        {
            lampLight.enabled = false;
            lightOn = false;
        }

        SoundManager.instance.PlaySound(click);
    }
}
