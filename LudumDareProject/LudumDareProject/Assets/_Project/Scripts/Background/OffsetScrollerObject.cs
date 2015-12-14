using UnityEngine;
using System.Collections;

public class OffsetScrollerObject : MonoBehaviour
{
    public float scrollSpeed;
    public float scrollSpeedMultiplier = 1;
    public Vector3 startPosition;
    public Vector3 endPosition;

    void Start()
    {
        startPosition = new Vector3(Random.Range(50f, 300f), transform.position.y, transform.position.z);
        endPosition = new Vector3(Random.Range(-300f,-50f), transform.position.y, transform.position.z);
        transform.position = startPosition;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, scrollSpeed * scrollSpeedMultiplier);
        if (transform.position == endPosition)
        {
            startPosition = new Vector3(Random.Range(10, 20), transform.position.y, transform.position.z);
            endPosition = new Vector3(Random.Range(-300f, -50f), transform.position.y, transform.position.z);
            transform.position = startPosition;
        }
    }
}
