using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;

    public int hazardCount;

    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text ScoreText;
    public Text restartText;
    public Text gameOverText;

    public Text winText;

    public AudioClip winMu;
    public AudioClip lossMu;

    private bool gameOver;
    private bool restart;
    private int score;

    private BG_Scroller background;

    private WepSwitch weapon;

    private StarSpeed stars;

    private AudioSource audioSource;

    void Start()
    {
       gameOver = false;
       restart = false;

       restartText.text = "";
       gameOverText.text = "";
       winText.text = "";

       audioSource = GetComponent<AudioSource>();

       score = 0;
       UpdateScore();
       StartCoroutine (SpawnWaves());

       GameObject backgroundObject = GameObject.FindWithTag("Background");

       GameObject weaponObject = GameObject.FindWithTag("Weapon");
       

        if(backgroundObject != null)
        {
            background = backgroundObject.GetComponent <BG_Scroller>();
        }
        
        if(weaponObject != null)
        {
            weapon = weaponObject.GetComponent <WepSwitch>();
        }
        
        if (background == null)
        {
            Debug.Log("Cannot find 'background' script!");
        }

        if (weapon == null)
        {
            Debug.Log("Cannot find 'WepSwitch' script!");
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds (startWait);
        
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];

                Vector3 spawnPosition = new Vector3 (Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);

                yield return new WaitForSeconds (spawnWait);
            }
            yield return new WaitForSeconds (waveWait);

            if(gameOver)
            {
                restartText.text = "Press 'Z' for restart";
                restart = true;

                break;
            }
        }
    }

    void Update()
    {
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {
                SceneManager.LoadScene("Main");
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape)) Application.Quit();    
    }

    public void AddScore(int newScoreValue)
    {
       score += newScoreValue;
       UpdateScore();
    }

    void UpdateScore()
    {
        ScoreText.text = "Points: " + score;

        if(score >= 150)
        {
            weapon.WeaponSwitcher();
        }

        if(score >=400)
        {
           winText.text = "YOU WIN";
           
           gameOver= true;
           restart = true;

           gameOverText.text = "Game created by Jason Vargas";

           background.speedUp();
           //stars.speedUp();

           audioSource.clip = winMu;
           audioSource.Play();
        }

       if(score >= 500)
       {
           winText.text = "YOU WIN!";
           gameOverText.text= "You double Win!";
       }


    } 
    
    public void GameOver()
    {
       gameOverText.text = "GAME OVER!";
       gameOver = true;

       audioSource.clip = lossMu;
       audioSource.Play();
    }
}
/*
    _|_

 */
