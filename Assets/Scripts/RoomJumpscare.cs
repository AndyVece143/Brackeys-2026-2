using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomJumpscare : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public DeathGhost ghost;
    public Light2D globalLight;
    public string hexColor;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ghost.SwitchToJumpscare();
            Color newColor;

            if (ColorUtility.TryParseHtmlString(hexColor, out newColor))
            {
                globalLight.color = newColor;
            }
        }
    }
}
