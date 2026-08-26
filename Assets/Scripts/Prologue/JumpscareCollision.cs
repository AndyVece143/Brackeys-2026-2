using UnityEngine;
using UnityEngine.Rendering.Universal;

public class JumpscareCollision : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public GhostPrologue ghost;
    public Light2D globalLight;
    public string hexColor;
    public string hexColorLamp;
    public LampPost lamp1;
    public LampPost lamp2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Boo!");
            ghost.SwitchToJumpscare();
            Color newColor;

            if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
            {
                globalLight.color = newColor;
            }

            Color newerColor;

            if (ColorUtility.TryParseHtmlString(hexColorLamp, out newerColor))
            {
                lamp1.light1.color = newerColor;
                lamp1.light2.color = newerColor;

                lamp2.light1.color = newerColor;
                lamp2.light2.color = newerColor;
            }
        }
    }
}
