using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
     public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
     public float speed;
     public float tilt;
     public int charges;
     public Boundary boundary;
     public GameObject shot;
     public Transform shotSpawn;
     public Text chargesText;
     public float fireRate;
     public float chargeRate;

     private float nextFire;
     private float currentSpeed;
     private float nextCharge;
     private Rigidbody rb;
     private AudioSource audioSource;

     private void Start()
     {
          currentSpeed = speed;
          audioSource = GetComponent<AudioSource>();
          rb = GetComponent<Rigidbody>();
     }

     void Update()
     {
          chargesText.text = "Charges: " + charges;
          
          if(Input.GetButton("Fire1") && Time.time > nextFire)
          {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

            audioSource.Play();
          }

          if(Input.GetKeyUp("x") && charges > 0 && Time.time > nextCharge)
          {
              nextCharge = Time.time + chargeRate; 
              charges--;
              
              timeMode();
          }
     }
     void FixedUpdate()
     {
          float moveHorizontal = Input.GetAxis("Horizontal");
          float moveVertical = Input.GetAxis("Vertical");

          Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
          rb.velocity = movement * speed;

          rb.position = new Vector3
          (
               Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
               0.0f,
               Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
          );

          rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
     }
   
     void timeMode()
     {
          
     }
}