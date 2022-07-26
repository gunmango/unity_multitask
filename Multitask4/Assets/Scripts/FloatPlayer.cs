using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatPlayer : MonoBehaviour
{
    bool m_isPaused;
    float m_speed;
    float m_jumpForce;
    int m_level;

    // Start is called before the first frame update
    void Start()
    {
        Initailize();
        GameManager.instance.EventLevelUpHandler += LevelUp;
        GameManager.instance.EventResumeHandler += Resume;
    }

    void Initailize()
    {
        m_isPaused = true;
        m_speed = 4;
        m_level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //if(!m_isPaused)
        //{
            if(Input.GetKey(KeyCode.Space))
            {
                GetComponent<Rigidbody>().AddForce(0f, m_speed, 0f);
            }

        //}
    }

    void LevelUp(int level)
    {

        m_level = level;
        if(level >= 4)
        {
            //m_isPaused = true;
        }
    }

    void Resume()
    {
        if (m_level >= 4)
        {
           // m_isPaused = false;
        }
    }
}
