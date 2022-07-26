using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCreater : MonoBehaviour
{
    int m_wallCount;
    float m_term;
    bool m_isPaused;
    float m_timer;
    int m_level;

    GameObject[] m_prefabWalls = new GameObject[7];


    // Start is called before the first frame update
    void Start()
    {
        m_wallCount = 7;
        GameObject prefabWall = Resources.Load("Wall") as GameObject;
        for (int i = 0; i < m_wallCount; i++)
        {
            m_prefabWalls[i] = Instantiate(prefabWall);
        }

        //대리자
        GameManager.instance.EventLevelUpHandler += LevelUp;
        GameManager.instance.EventResumeHandler += Resume;

        Initialize();

    }

    void Initialize()
    {
        m_term = 4;
        m_isPaused = true;
        m_timer = 0;
        m_level = 0;
        for (int i = 0; i < m_wallCount; i++)
        {
            m_prefabWalls[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isPaused)
        {
            m_timer += Time.deltaTime;

            if (m_timer >= m_term)
            {
                //만들어
                Create();
                m_timer = 0;

            }
        }
    }


    void LevelUp(int level)
    {
        m_level = level;

        m_isPaused = true;

        //벽도멈춰
        for(int i=0;i<m_wallCount; i++)
        {
            if (m_prefabWalls[i].gameObject.activeInHierarchy == true)
            {
                m_prefabWalls[i].GetComponent<Wall>().isPaused = true;
            }
        }

        //레벨업관련
    }

    void Resume()
    {
        if (m_level >= 4)
        {
            m_isPaused = false;

            //벽도 움직여
            for (int i = 0; i < m_wallCount; i++)
            {
                if (m_prefabWalls[i].gameObject.activeInHierarchy == true)
                {
                    m_prefabWalls[i].GetComponent<Wall>().isPaused = false;
                }
            }


        }
    }

    void Create()
    {
        float posX = transform.position.x;
        float posY = Random.Range(10,27) / 10 - 1;
        float posZ = transform.position.z;

        for (int i = 0; i < m_wallCount; i++)
        {
            if (m_prefabWalls[i].gameObject.activeInHierarchy == false)
            {
                m_prefabWalls[i].transform.position = new Vector3(posX, posY, posZ);
                m_prefabWalls[i].SetActive(true);

                break;
            }
        }
    }
}
