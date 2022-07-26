using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidPlayer : MonoBehaviour
{
    enum State
    {
        Stop,
        MovingUp,
        MovingDown
    }

    //[SerializeField] GameObject Floor[];
    [SerializeField] GameObject[] m_Floors = new GameObject[5];
    
    float[] m_FloorY = new float[5];
    


    [SerializeField] bool m_isPaused;
    float m_speed;
    [SerializeField] int m_floorIndex;
    [SerializeField] int m_targetIndex;
    State m_state;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            m_FloorY[i] = m_Floors[i].transform.position.y;
        }

        //대리자
        GameManager.instance.EventLevelUpHandler += LevelUp;
        GameManager.instance.EventResumeHandler += Resume;


        Initialize();
    }

    void Initialize()
    {
        m_speed = 5f;
        m_floorIndex = 2;
        m_isPaused = true;
        m_state = State.Stop;
    }

    // Update is called once per frame
    void Update()
    {

        //일시정지 아니면
        if (!m_isPaused)
        {
            //아래방향키
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //안움직이고있으면
                if (m_state == State.Stop)
                {
                    //맨아래아니면
                    if (m_floorIndex < 4)
                    {
                        m_state = State.MovingDown;
                        m_targetIndex = m_floorIndex + 1;
                    }
                }
                //올라가고있으면
                else if (m_state == State.MovingUp)
                {
                    m_state = State.MovingDown;
                    m_targetIndex = m_floorIndex;
                }
            }


            //위방향키
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //안움직이고있으면
                if (m_state == State.Stop)
                {
                    //맨위아니면
                    if (m_floorIndex > 0)
                    {
                        m_state = State.MovingUp;
                        m_targetIndex = m_floorIndex - 1;
                    }
                }
                //내려가고있으면
                else if (m_state == State.MovingDown)
                {
                    m_state = State.MovingUp;
                    m_targetIndex = m_floorIndex;
                }
            }


            if(m_state == State.MovingDown)
            {
                //움직이기
                MoveDown();

                //도달했나 체크
                CheckArrival();
            }

            else if (m_state == State.MovingUp)
            {
                //움직이기
                MoveUp();

                //도달했나 체크
                CheckArrival();
            }


        }
    }


    void MoveUp()
    {
        transform.Translate(Vector3.up * m_speed * Time.deltaTime);

    }

    void MoveDown()
    {
        transform.Translate(Vector3.down * m_speed * Time.deltaTime);

    }

    void CheckArrival()
    {

        //아래로
        if(m_state == State.MovingDown)
        {
            if(transform.position.y <= m_FloorY[m_targetIndex])
            {
                //위치조정
                Vector3 vector = transform.position;
                vector.y = m_FloorY[m_targetIndex];
                transform.position = vector;

                m_state = State.Stop;
                m_floorIndex = m_targetIndex;
            }
        }


        //위로
        else if(m_state == State.MovingUp)
        {
            if (transform.position.y >= m_FloorY[m_targetIndex])
            {
                //위치조정
                Vector3 vector = transform.position;
                vector.y = m_FloorY[m_targetIndex];
                transform.position = vector;

                m_state = State.Stop;
                m_floorIndex = m_targetIndex;
            }
        }
    }

    void LevelUp(int level)
    {
        m_isPaused = true;
    }
   
    void Resume()
    {
        m_isPaused = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("gameover");
            GameManager.instance.GameOver();
        }
    }
}
