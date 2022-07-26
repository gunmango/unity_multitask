using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{


    [SerializeField] bool m_isPaused;
    float m_timer;
    float m_r;
    float m_g;
    float m_b;
    float m_term;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        GameManager.instance.EventLevelUpHandler += LevelUp;
        GameManager.instance.EventResumeHandler += Resume;
    }

    public void Initialize()
    {
        m_r = 255;
        m_g = 255;
        m_b = 255;
        m_isPaused = false;
        m_timer = 0;
        m_term = 14;
        gameObject.GetComponent<Renderer>().material.color = new Color(m_r / 255f, m_g / 255f, m_b / 255f);

    }


    // Update is called once per frame
    void Update()
    {
        if(!m_isPaused)
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(m_r / 255f, m_g / 255f, m_b / 255f);

            if(m_g != 0)
            {
                m_g -= Time.deltaTime * m_term;
                m_b -= Time.deltaTime * m_term;
                if(m_g < 0)
                {
                    m_g = 0;
                    m_b = 0;

                    //게임오버
                    GameManager.instance.GameOver();
                }
            }
        }
    }

    void Explode()
    {

    }


    //충돌시
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("충돌");
        //충돌한게 플레이어면
        if(other.tag == "Eater")
        {
            gameObject.SetActive(false);
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
}
