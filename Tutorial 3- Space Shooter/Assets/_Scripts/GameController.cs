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

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
       gameOver = false;
       restart = false;

       restartText.text = "";
       gameOverText.text = "";
       winText.text = "";

       score = 0;
       UpdateScore();
       StartCoroutine (SpawnWaves());
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

       if(score >=100)
       {
           winText.text = "YOU WIN";
           
           gameOver= true;
           restart = true;

           gameOverText.text = "Game created by Jason Vargas";
       }

       if(score >= 170)
       {
           winText.text = "YOU WIN!";

            gameOver= true;
            restart = true;

           gameOverText.text= "You double Win!";
       }
    } 

    public void GameOver()
    {
       gameOverText.text = "GAME OVER!";
       gameOver = true;
    }
}
/*
    _|_

 */
