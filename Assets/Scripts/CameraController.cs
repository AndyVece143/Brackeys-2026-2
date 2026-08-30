using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public enum State
    {
        FollowPlayer,
        Room1,
        Room2,
        Room3,
        Room4,
        StayStill
    }
    public State state;
    public State initialState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialState = state;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.FollowPlayer:
                FollowPlayer();
                break;
            case State.Room1:
                Room1();
                break;
            case State.Room2:
                Room2();
                break;
            case State.Room3:
                Room3();
                break;
            case State.Room4:
                Room4();
                break;
            case State.StayStill:
                break;
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.y = 0;

        if (targetPosition.x < 0f)
        {
            targetPosition.x = 0f;
        }
        if (targetPosition.x > 12f)
        {
            targetPosition.x = 12f;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void Room1()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.y = 0;

        if (targetPosition.x < 0f)
        {
            targetPosition.x = 0f;
        }
        if (targetPosition.x > 28f)
        {
            targetPosition.x = 28f;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void Room2()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.x = 0;

        if (targetPosition.y > 0f)
        {
            targetPosition.y = 0;
        }

        if (targetPosition.y < -5f)
        {
            targetPosition.y = -5f;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void Room3()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.y = 0;

        if (targetPosition.x < 0f)
        {
            targetPosition.x = 0f;
        }
        if (targetPosition.x > 28.5f)
        {
            targetPosition.x = 28.5f;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    private void Room4()
    {
        Vector3 targetPosition = player.position + offset;
        if (targetPosition.x < 0f)
        {
            targetPosition.x = 0f;
        }

        if (targetPosition.x > 82)
        {
            targetPosition.x = 82;
        }

        if (targetPosition.y < 0)
        {
            targetPosition.y = 0;
        }

        if (targetPosition.y > 13.15f)
        {
            targetPosition.y = 13.15f;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public IEnumerator GoToPlace(Vector3 location, float duration)
    {
        state = State.StayStill;
        float time = 0;
        Vector3 startingPos = transform.position;

        while (time < duration)
        {
            //time += Time.deltaTime;
            //float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            //transform.position = Vector2.Lerp(transform.position, location, t);
            //yield return null;

            float t = time / duration;
            transform.position = Vector3.Lerp(startingPos, location, t);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = location;
    }
}