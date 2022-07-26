using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{   
    [SerializeField] GameObject[] m_Floors = new GameObject[5];
    float[] m_FloorY = new float[5];
    float[] m_FloorX = new float[2];

    bool m_isPaused;

    float m_timer;
    int m_level;
    int m_maxBulletNum; //화면에 보일 총알 최대갯수
    int m_fireAtSameNum;    //같이발사할 총알 최대개수
    int m_floorIndex;
    bool m_side;    //true: 오른쪽, false: 왼쪽
    float m_term;
    
    
    GameObject [] m_prefabBullets = new GameObject[20];


    // Start is called before the first frame update
    void Start()
    {
        GameObject prefabBullet = Resources.Load("Bullet") as GameObject;
        for (int i=0; i<20; i++)
        {
            m_prefabBullets[i] = Instantiate(prefabBullet);
        }

        for (int i = 0; i < 5; i++)
        {
            m_FloorY[i] = m_Floors[i].transform.position.y;
        }

        Initialize();
        m_FloorX[0] = 1f;
        m_FloorX[1] = -12.8f;
        //대리자
        GameManager.instance.EventLevelUpHandler += LevelUp;
        GameManager.instance.EventResumeHandler += Resume;
    }

    void Initialize()
    {
        m_isPaused = true;
        m_maxBulletNum = 4;
        m_fireAtSameNum = 2;
        m_timer = 0;
        m_level = 0;
        m_term = 5;
        
        for (int i = 0; i < 20; i++)
        {
            m_prefabBullets[i].SetActive(false);
        }

    }


    // Update is called once per frame
    void Update()
    {
        if(!m_isPaused)
        {
            //발사
            m_timer += Time.deltaTime;

            if(m_timer > m_term)
            {
                m_timer = 0;
                int rand = Random.Range(0, 5);
                int rand2 = Random.Range(0, 2);
                Fire(rand, rand2);
       
            }
        }
        else
        {

        }
    }
    void Fire(int floor, int side)
    {
        for(int i=0; i<20; i++)
        {
            if(m_prefabBullets[i].gameObject.activeInHierarchy == false)
            {
                //위치조정
                Vector3 vector;
                vector.z = -4.7f;
                vector.y = m_FloorY[floor];
                vector.x = m_FloorX[side];
                m_prefabBullets[i].transform.position = vector;

                //활성화
                m_prefabBullets[i].SetActive(true);

                //각도
                if(side == 0)
                {
                    m_prefabBullets[i].transform.localEulerAngles = new Vector3(0, 0, 90);

                }
                else
                {
                    m_prefabBullets[i].transform.localEulerAngles = new Vector3(0, 0, 270);
                }

                break;
            }
        }
    }


    void LevelUp(int level)
    {
        m_level = level;
        m_isPaused = true;

        //총알도 멈춰
        for(int i=0;i<20;i++)
        {
            if (m_prefabBullets[i].gameObject.activeInHierarchy == true)
            {
                m_prefabBullets[i].GetComponent<Bullet>().isPaused = true;
            }
        }

        if(level >= 3)
        {
            m_term--;
        }


    }

    void Resume()
    {
        if (m_level >= 2)
        {
            m_isPaused = false;

            //총알도 재시작
            for (int i = 0; i < 20; i++)
            {
                if (m_prefabBullets[i].gameObject.activeInHierarchy == true)
                {
                    m_prefabBullets[i].GetComponent<Bullet>().isPaused = false;
                }
            }
        }
    }

}
