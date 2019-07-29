using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroller : MonoBehaviour
{

    public float scrollSpeed;
    public float someLength;

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat (Time.time * scrollSpeed, someLength);
        
        transform.position = startPos + Vector3.forward * newPosition;   
    }

    public void speedUp()
    {
        scrollSpeed = -5;
    }
}
