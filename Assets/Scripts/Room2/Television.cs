using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Television : MonoBehaviour
{
    public Animator anim;
    public bool tvOn = false;
    public float distance;
    public Light2D tvLight;
    public AudioClip click;
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tvLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);

        if (distance <= 4 && tvOn == false)
        {
            ChangeChannel(true);
        }
        else if (distance >= 4 && tvOn == true)
        {
            ChangeChannel(false);
        }

        anim.SetBool("on", tvOn);
    }

    private void ChangeChannel(bool turnOn)
    {
        if (turnOn)
        {
            tvLight.enabled = true;
            tvOn = true;
        }
        else
        {
            tvLight.enabled = false;
            tvOn = false;
        }

        SoundManager.instance.PlaySound(click);
    }
}
