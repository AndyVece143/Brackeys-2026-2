using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LampPost : MonoBehaviour
{
    public Light2D light1;
    public Light2D light2;

    public float timer1;
    public float timer2;

    private float timer1Max;
    private float timer2Max;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer1Max = timer1;
        timer2Max = timer2;
    }

    // Update is called once per frame
    void Update()
    {
        timer1 -= Time.deltaTime;
        if (timer1 <= 0)
        {
            Light1Check();
        }

        timer2 -= Time.deltaTime;
        if (timer2 <= 0)
        {
            Light2Check();
        }
    }

    private void Light1Check()
    {
        int i = Random.Range(0, 7);

        if (i == 6)
        {
            StartCoroutine(Light1Flicker());
        }

        timer1 = timer1Max;
    }

    private void Light2Check()
    {
        int i = Random.Range(0, 11);

        if (i == 10)
        {
            StartCoroutine(Light2Flicker());
        }
        timer2 = timer2Max;
    }

    private IEnumerator Light1Flicker()
    {
        light1.enabled = false;
        yield return new WaitForSeconds(0.1f);
        light1.enabled = true;
    }

    private IEnumerator Light2Flicker()
    {
        light2.enabled = false;
        yield return new WaitForSeconds(0.1f);
        light2.enabled = true;
    }
}
