using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpeed : MonoBehaviour
{

    private ParticleSystem ps;

    public float simulationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void speedUp()
    {
        
    }
}
