using UnityEngine;
using System.Collections;

public class Disclaimer : MonoBehaviour
{
    public LevelLoader loader;
    private bool transition = false;
    public SpriteRenderer sprite;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && transition == false)
        {
            transition = true;
            loader.LoadNextLevel("Title");
            StartCoroutine(SpookyGhost());
        }
    }

    IEnumerator SpookyGhost()
    {
        yield return new WaitForSeconds(0.5f);
        sprite.enabled = true;
    }
}
