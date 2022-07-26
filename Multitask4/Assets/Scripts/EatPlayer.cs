using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatPlayer : MonoBehaviour
{
    [SerializeField] GameObject m_minRange;
    [SerializeField] GameObject m_maxRange;

    float m_speed;
    bool m_isPaused;
    [SerializeField] int m_level;

    float m_minX, m_minZ, m_maxX, m_maxZ;

    // Start is called before the first frame update
    void Start()
    {
        //대리자
        GameManager.instance.EventLevelUpHandler += LevelUp;
        GameManager.instance.EventResumeHandler += Resume;


        //범위
        m_minX = m_minRange.transform.position.x;
        m_minZ = m_minRange.transform.position.z;
        m_maxX = m_maxRange.transform.position.x;
        m_maxZ = m_maxRange.transform.position.z;

        Initailize();
    }

    void Initailize()
    {
        m_speed = 4f;
        m_level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isPaused)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if(transform.position.z <= m_maxZ)
                {
                 transform.Translate(Vector3.forward * m_speed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (transform.position.z >= m_minZ)
                {
                    transform.Translate(Vector3.back * m_speed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (transform.position.x >= m_minX)
                {
                    transform.Translate(Vector3.left * m_speed * Time.deltaTime);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (transform.position.x <= m_maxX)
                {
                    transform.Translate(Vector3.right * m_speed * Time.deltaTime);
                }
            }
        }
    }

    void LevelUp(int level)
    {
        m_level = level;
        m_isPaused = true;
    }

    void Resume()
    {
        if(m_level >= 3)
        {
            m_isPaused = false;
        }
    }



}
