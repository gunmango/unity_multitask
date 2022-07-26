using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creater : MonoBehaviour
{
    int m_cubeCount;
    float m_timer;  //
    float m_term; //큐브생성주기
    bool m_isPaused;
    int m_level;
    [SerializeField] GameObject m_minRange;
    [SerializeField] GameObject m_maxRange;

    GameObject[] m_prefabCubes = new GameObject[20];


    // Start is called before the first frame update
    void Start()
    {
        m_cubeCount = 20;
        GameObject prefabCube = Resources.Load("Cube") as GameObject;
        for (int i = 0; i < m_cubeCount; i++)
        {
            m_prefabCubes[i] = Instantiate(prefabCube);
        }
        
        //대리자
        GameManager.instance.EventLevelUpHandler += LevelUp;
        GameManager.instance.EventResumeHandler += Resume;

        Initialize();

    }

    void Initialize()
    {
        m_isPaused = true;
        m_timer = 0;
        m_term = 5f;
        m_level = 0;
        for (int i = 0; i < m_cubeCount; i++)
        {
            m_prefabCubes[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isPaused)
        {
            m_timer += Time.deltaTime;

            if(m_timer >= m_term)
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

        //레벨업관련
    }

    void Resume()
    {
        if(m_level >= 3)
        {
            m_isPaused = false;
        }
    }

    void Create()
    {
        float posX = Random.Range(m_minRange.transform.position.x, m_maxRange.transform.position.x);
        float posY = 3.3f;
        float posZ = Random.Range(m_minRange.transform.position.z, m_maxRange.transform.position.z);

        for (int i = 0; i < m_cubeCount; i++)
        {
            if (m_prefabCubes[i].gameObject.activeInHierarchy == false)
            {
                m_prefabCubes[i].transform.position = new Vector3(posX, posY, posZ);
                m_prefabCubes[i].SetActive(true);
                m_prefabCubes[i].GetComponent<Cube>().Initialize();
                break;
            }
        }
    }
}
