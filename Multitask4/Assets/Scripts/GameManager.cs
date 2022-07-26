using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonobehavourSingleton<GameManager>
{
    Coroutine m_scoreCoroutine;
    Coroutine m_pauseCoroutine;

    public delegate void EventResume();
    public event EventResume EventResumeHandler;

    public delegate void EventLevelUp(int level);
    public event EventLevelUp EventLevelUpHandler;

    enum GameState
    {
        Play,
        Pause,
        Over
    }

    [SerializeField] GameState m_gameState;
    [SerializeField] int m_level;
    //점수
    [SerializeField] int m_score;
    float m_timer;
    [SerializeField] Text m_scoreText;
    [SerializeField] Text m_pauseText;
    [SerializeField] Image m_pauseImage;

    [SerializeField] int m_cameraReadyCounter;


    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        //m_pauseCoroutine = StartCoroutine(CoroutinePause());
    }

    void Initialize()
    {
        m_score = 0;
        m_level = 1;
        m_timer = 0;
        m_gameState = GameState.Pause;
        m_cameraReadyCounter = 1;
        SetPauseText(1);
        Time.timeScale = 1;
    }


    // Update is called once per frame
    void Update()
    {
        //점수늘리기
        if (m_gameState == GameState.Play)
        {
            //시간늘리기
            m_timer += Time.deltaTime;
            //점수올리기
            if (m_timer > 2)
            {
                m_timer = 0;
                m_score++;
                m_scoreText.text = "Score: " + m_score;
                //특정점수되면 난이도올려
                if (m_score == 5)
                {
                    m_level++;
                    m_cameraReadyCounter = 0;
                    Pause(m_level);
                    EventLevelUpHandler(m_level);
                }
                else if (m_score == 10)
                {
                    m_level++;
                    m_cameraReadyCounter = 0;
                    Pause(m_level);
                    EventLevelUpHandler(m_level);
                }
                else if(m_score == 20)
                {
                    m_level++;
                    m_cameraReadyCounter = 0;
                    Pause(m_level);
                    EventLevelUpHandler(m_level);
                }
            }
        }


        if(m_gameState == GameState.Pause)
        {
            if(m_pauseText.gameObject.activeInHierarchy == false)
            {
                if (m_cameraReadyCounter == 4 || m_cameraReadyCounter == m_level)
                {
                    m_pauseImage.gameObject.SetActive(true);
                    m_pauseText.gameObject.SetActive(true);
                }
            }
        }


        //재시작
        if (Input.anyKeyDown)
        {
            if(m_cameraReadyCounter == 4 || m_cameraReadyCounter == m_level)
            {
                if (m_gameState == GameState.Pause)
                {
                    m_gameState = GameState.Play;
                    m_pauseText.gameObject.SetActive(false);
                    m_pauseImage.gameObject.SetActive(false);
                    EventResumeHandler();
                }
            }

            //게임오버일때
            if(m_gameState == GameState.Over)
            {
                //재시작
                SceneManager.LoadScene(0);
            }
        }
    }

    IEnumerator CoroutineScoreRaise()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator CoroutinePause()
    {
        yield return new WaitForSeconds(1f);
        Pause(1);
    }


    void SetPauseText(int level)
    {
        switch (level)
        {
            case 0:
                m_pauseText.text = "GAME OVER \nScore: " + m_score;
                break;
            case 1:
                m_pauseText.text = "Left Right Arrows to Balance";
                break;
            case 2:
                m_pauseText.text = "Up Down Arrows to Avoid";
                break;
            case 3:
                m_pauseText.text = "W A S D to Move";
                break;
            case 4:
                m_pauseText.text = "SPACE to Jump";
                break;
            case 5:
                m_pauseText.text = "난이도 상승";
                break;
            default:
                break;
        }
    }

    public void Pause(int level)
    {
        SetPauseText(level);

        if (m_gameState == GameState.Play)
        {
            m_gameState = GameState.Pause;

        }
        return;
    }

    public void CameraReadyCounterUp()
    {
        m_cameraReadyCounter++;
    }


    public void GameOver()
    {
        //Debug.Log("게임오버");
        Time.timeScale = 0;
        m_gameState = GameState.Over;
        SetPauseText(0);
        m_pauseImage.gameObject.SetActive(true);
        m_pauseText.gameObject.SetActive(true);
    }
}
