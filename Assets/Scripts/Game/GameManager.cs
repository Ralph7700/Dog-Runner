using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    #region other Variables
    public float MaxSpeed;
    public float MaxtimeSpeed;
    [SerializeField] float realSpeed;
    public UnityEvent GameOverEvent;
    public float speedMultiplier;
    public bool GameOver;
    private float ScaleSaver;
    private PlayerBehaviour player;
    PlayFabManager playfab;
    #endregion
    #region Score Variables
    public int Score;
    private float scoreTimer = 0f;
    #endregion
    [Header("UI:")]
    [Space(5)]
    #region UI
    private TMP_Text ScoreDisplay;
    public GameObject PausePanel;
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
        playfab = FindObjectOfType<PlayFabManager>().GetComponent<PlayFabManager>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 45;
    }

    private void Start()
    {
        ScoreDisplay = GameObject.Find("Score Display").GetComponent<TMP_Text>();
        player = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        Score = 0;
        speedMultiplier = 1f;
        Time.timeScale = 1;
        ScoreDisplay.text = "0";
        GameOver = false;
    }
    private void OnEnable()
    {

    }

    private void Update()
    {
        if (!GameOver)
        {
            if (speedMultiplier < MaxSpeed) speedMultiplier += 0.01f * Time.deltaTime;
            else if (Time.timeScale < MaxtimeSpeed) Time.timeScale += 0.01f * Time.unscaledDeltaTime;

            if (scoreTimer < 0.5f) scoreTimer += Time.deltaTime;
            else
            {
                AddScore(1);
                scoreTimer = 0f;
            }
            if (Input.GetKeyDown(KeyCode.Escape)) Pause();
            realSpeed = Time.timeScale;
        }
    }
    public void AddScore(int score)
    {
        Score += score;
        ScoreDisplay.text = $"Score : {Score.ToString()}";
    }
    public void GetLeaderboardDone() => playfab.GetLeaderboard();
    public void OnGameOver()
    {
        GameOver = true;
        StartCoroutine(StopGame());
        player.OnGameOver();
        if (Score > PlayFabManager.Instance.HighScore)
        {
            PlayFabManager.Instance.UploadHighScore(Score);
        }
    }
    public void PlayClick() => FindObjectOfType<AudioManager>().PlaySound("Click");

    public void Pause()
    {
        ScaleSaver = Time.timeScale;
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        FindObjectOfType<AudioManager>().StopSound("Run");

    }
    public void UnPause() { Time.timeScale = ScaleSaver; PausePanel.SetActive(false); }
    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void GoTMenu() => SceneManager.LoadScene(0);

    IEnumerator StopGame()
    {
        yield return new WaitForSeconds(3);
        Time.timeScale = 0;
    }
}

