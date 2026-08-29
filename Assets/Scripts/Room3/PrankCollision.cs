using UnityEngine;

public class PrankCollision : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public PrankGhost ghost;
    private bool pranked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && pranked == false)
        {
            pranked = true;
            ghost.SwitchToJumpscare();
        }
    }
}
