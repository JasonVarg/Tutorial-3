using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{

    public GameObject shot;
    public Transform shotSpawn1, shotSpawn2;
    public float fireRate;
    public float delay;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource> ();

        InvokeRepeating ("Fire", delay, fireRate);
    }

    void Fire()
    {
        Instantiate (shot, shotSpawn1.position, shotSpawn1.rotation);
        Instantiate (shot, shotSpawn2.position, shotSpawn2.rotation);

        audioSource.Play ();
    }
}
