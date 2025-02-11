using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject spawnPoint;
    public GameObject[] livesImage;
    private int currentLives=3;
    private GameObject player;
    private PlayerController playerController;
    public TMP_Text scoreText;
    public Canvas winCanvas;
    public Canvas loseCanvas;
    public GameObject hudBottomLine;
    [NonSerialized]public int score = 100000;
    private string ScoreTablePath = Path.GetFullPath(@"ScoreTable.txt");
    public TMP_Text playerNick;
    public TMP_Text timeDelayText;
    private int timeDelay =3;

    private void Awake()
    {
        player = Instantiate(playerPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
    void Start()
    {
        Time.timeScale = 1.0f;
        playerController = player.GetComponent<PlayerController>();
        foreach (GameObject live in livesImage)
        {
            live.SetActive(true);
        }
        scoreText.text = Convert.ToString(score);
        winCanvas.gameObject.SetActive(false);
        loseCanvas.gameObject.SetActive(false);
        hudBottomLine.SetActive(true);
        playerController.enabled = false;
        StartCoroutine(StartDelay());
    }
    public IEnumerator Death()
    {
        Time.timeScale = 0f;
        currentLives--;
        livesImage[currentLives].SetActive(false);
        score -= 20000;
        scoreText.text = score.ToString();
        yield return StartCoroutine(Wait());
        if (currentLives == 0)
        {
            Lost();
        }
        if (currentLives > 0)
            Respawn();
    }
    public IEnumerator UpdateScore()
    {
        while (true)
        {
            yield return StartCoroutine(Wait());
            if (Time.timeScale == 1.0f)
            {
                if (score > 0)
                {
                    score -= 100;
                }
                if (score < 0)
                    score = 0;
                scoreText.text = score.ToString();
            }
        }
    }
    public IEnumerator StartDelay()
    {
        for(int i = timeDelay; i >= 0; i--)
        {
            timeDelayText.text = i.ToString();
            if(i == 0)
            {
                timeDelayText.gameObject.SetActive(false);
                playerController.enabled = true;
                StartCoroutine(UpdateScore());
            }
            yield return StartCoroutine(Wait());
        }

    }
    public IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1f);
    }
    public void Respawn()
    {
        Time.timeScale = 1.0f;
        player.transform.position = spawnPoint.transform.position;
        player.transform.rotation = Quaternion.identity;

    }

    public void Win()
    {
        Time.timeScale = 0f;
        player.GetComponent<PlayerController>().gameObject.SetActive(false);
        hudBottomLine.gameObject.SetActive(false);
        winCanvas.gameObject.SetActive(true);
    }
    public void Lost()
    {
        player.GetComponent<PlayerController>().gameObject.SetActive(false);
        Time.timeScale = 0f;
        loseCanvas.gameObject.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    public void ToMenu()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    public void SaveScore()
    {
        if (!File.Exists(ScoreTablePath))
        {
            File.Create(ScoreTablePath);
        }
        using (StreamWriter sw = new StreamWriter(ScoreTablePath, true))
        {
            sw.WriteLine($"{playerNick.text}|{score}");
        }
        SceneManager.LoadSceneAsync("Score");
    }
}
