using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public GameObject titleScreen;
    private int score = 0;
    private float spawnRate = 1.0f;
    public bool isGameActive = true;
    private int lifes = 3;

    public GameObject pauseScreen;
    private bool paused;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
    }

    void ChangePaused()
    {
        if (!paused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        paused = !paused;
        pauseScreen.SetActive(paused);
    }


    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        titleScreen.gameObject.SetActive(false);
        UpdateScore(0);
        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        if(lifes > 0 )
        {
            lifes--;
            lifeText.text = "Lifes: " + lifes.ToString();
        }
        if(lifes == 0)
        {
            isGameActive = false;
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
