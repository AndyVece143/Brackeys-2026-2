using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public Animator anim;
    public float duration;
    public AudioClip gore;
    public LevelLoader loader;

    public void YouDied()
    {
        anim.SetTrigger("died");
        StartCoroutine(GoreNoise());
    }

    public void RestartLevel()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        loader.LoadNextLevel(currentScene);
    }

    public void GiveUp()
    {
        loader.LoadNextLevel("Title");
    }

    private IEnumerator GoreNoise()
    {
        yield return new WaitForSeconds(duration);
        SoundManager.instance.PlaySound(gore);
        yield return new WaitForSeconds(1);
        SoundManager.instance.PlaySound(gore);
    }
}
