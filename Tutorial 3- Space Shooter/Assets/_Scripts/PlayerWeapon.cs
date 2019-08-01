using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{

    public GameObject shot;
    public Transform[] shotSpawns;
    public float fireRate;
    public float chargeUpTime;
    public float chargeRate;
    private int charges;
    public AudioSource audioSource;
    public Text chargesText;
    
    private GameObject playerGameObject;
    private float nextFire;
    private float nextCharge;
    // Start is called before the first frame update
    void Start()
    {
        charges = 3;
        audioSource = GetComponent<AudioSource>();
        GameObject playerGameObject = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
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

    IEnumerator HyperMode()
     {
          float cFire = fireRate;
          
          while (true)
          {
               chargesText.text = "ACTIVE";

               fireRate = .125F;
               
               yield return new WaitForSeconds(chargeUpTime);
               break;
          }
          
          fireRate = cFire;
          
     }
}
