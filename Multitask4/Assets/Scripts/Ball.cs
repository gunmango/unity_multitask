using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    bool m_isPaused;
    float m_forceY;
    float m_forceX;
    float m_term;
    float m_timer;
    Vector3 m_initialPosition;
    Rigidbody m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.EventLevelUpHandler += LevelUp;
        GameManager.instance.EventResumeHandler += Resume;
        m_rigidbody = GetComponent<Rigidbody>();
        Initialize();
    }

    void Initialize()
    {
        m_isPaused = true;
        m_term = 7;
        m_timer = 0;
        m_forceY = 2000;
        m_forceX = 200;
    }


    // Update is called once per frame
    void Update()
    {
        if (!m_isPaused)
        {
            m_timer += Time.deltaTime;
            if (m_timer >= m_term)
            {
                Jump();
                m_timer = 0;
            }
        }


        //공떨어졌나 확인
        if(transform.position.y < -2)
        {
            //게임오버
            GameManager.instance.GameOver();
        }
    }

    void Jump()
    {
        float x;
        if ( Random.Range(0,2)== 0)
        {
            x = -m_forceX;
        }
        else
        {
            x = m_forceX;
        }


        m_rigidbody.AddForce(x, m_forceY,0f);
    }

    void LevelUp(int level)
    {
        m_isPaused = true;
        //물리꺼주고
        m_rigidbody.Sleep();
        m_rigidbody.useGravity = false;
    }

    void Resume()
    {
        m_isPaused = false;

        //버그고치기용 물리켜주기
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.angularVelocity = Vector3.zero;
        m_rigidbody.useGravity = true;
        m_rigidbody.WakeUp();
    }

    
}
