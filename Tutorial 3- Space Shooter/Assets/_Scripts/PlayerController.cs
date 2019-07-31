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

     public float chargeUpTime;
     public int charges;
     public Boundary boundary;
     public GameObject shot;
     public Transform[] shotSpawns;
     public Text chargesText;
     public float fireRate;
     public float chargeRate;

     private float nextFire;
     private float currentSpeed;
     private float nextCharge;
     private Rigidbody rb;
     private AudioSource audioSource;
     private GameController gameController;

     private void Start()
     {
          currentSpeed = speed;
          audioSource = GetComponent<AudioSource>();
          rb = GetComponent<Rigidbody>();

          GameObject gameControllerObject = GameObject.FindWithTag("GameController");

          if(gameControllerObject != null)
          {
               gameController = gameControllerObject.GetComponent <GameController>();
          }
        
          if (gameController == null)
          {
               Debug.Log("Cannot find 'GameController' script!");
          }
          
     }

     void Update()
     {    
          if(Input.GetButton("Fire1") && Time.time > nextFire)
          {
            nextFire = Time.time + fireRate;
            
               foreach(var shotSpawn in shotSpawns)
               {
                    
                    
                    Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
                    

                    audioSource.Play();
               }
          }

          if(Input.GetKeyUp("x") && charges > 0 && Time.time > nextCharge)
          {
              nextCharge = Time.time + chargeRate; 
              charges--;

              StartCoroutine (HyperMode());
          }

          chargesText.text = "Charges: " + charges;


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
     IEnumerator HyperMode()
     {
          float cFire = fireRate;
          float cSpeed = speed;

          while (true)
          {
               chargesText.text = "ACTIVE";

               fireRate = .125F;
               speed = 10;
               yield return new WaitForSeconds(chargeUpTime);
               break;
          }

          fireRate = cFire;
          speed = cSpeed;
     }
}