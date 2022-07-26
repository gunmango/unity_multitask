using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancer : MonoBehaviour
{
    float m_length;
    [SerializeField] float m_angle;
    [SerializeField] float m_rotateSpeed;
    GameManager m_gameManager;
    Coroutine m_pauseCoroutine;

    GameObject m_ball;

    bool m_isPaused;

    Rigidbody m_ballRigidbody;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        m_ballRigidbody = m_ball.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if(!m_isPaused)
        {
            //돌리기
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                transform.Rotate(0, 0, m_rotateSpeed * Time.deltaTime, Space.Self);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                transform.Rotate(0, 0, -m_rotateSpeed * Time.deltaTime, Space.Self);
            }
        }

        //m_angle = transform.localRotation;
    }

    void Initialize()
    {
        m_length = 4;
        transform.localScale = new Vector3(m_length, 0.5f, 0.5f);
        m_angle = 0;
        transform.Rotate(0f, 0f, m_angle);
        m_rotateSpeed = 70f;

        //공
        m_ball = GameObject.Find("Ball");
        m_lastPosition = m_ball.GetComponent<Transform>().position;


        //대리자
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_gameManager.EventLevelUpHandler += LevelUp;
        m_gameManager.EventResumeHandler += Resume;
    }

    void LevelUp(int level)
    {
        //멈추고
        Pause();

        //크기조절
    }

    Vector3 m_lastVelocity;
    Vector3 m_lastPosition;
    void Pause()
    {

        m_isPaused = true;
       // m_lastVelocity = m_ball.GetComponent<Rigidbody>().velocity;
       // m_lastPosition = m_ball.GetComponent<Transform>().position;
       // m_ballRigidbody.Sleep();
       // m_ballRigidbody.useGravity = false;
    }
    void Resume()
    {
        m_isPaused = false;
        //m_ballRigidbody.velocity = Vector3.zero;
        //m_ballRigidbody.angularVelocity = Vector3.zero;
        //m_ball.GetComponent<Transform>().position = m_lastPosition;
        //m_ballRigidbody.useGravity = true;
        //m_ballRigidbody.WakeUp();
    }
}
