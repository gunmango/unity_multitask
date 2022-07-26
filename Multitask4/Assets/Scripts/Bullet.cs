using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float m_speed;
    [SerializeField] bool m_isPaused;

    public bool isPaused { set { m_isPaused = value; } }

    int m_level;

    // Start is called before the first frame update
    void Start()
    {
        m_speed = 3;
        m_level = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if(!m_isPaused)
        {
            transform.Translate(Vector3.up * m_speed * Time.deltaTime, Space.Self);
        }

        //밖으로나가면
        if(transform.position.x < -14 || transform.position.x > 2 )
        {
            gameObject.SetActive(false);


        }
    }


    public void Fire(Vector3 vector, Vector3 angle)
    {
       
    }

    void LevelUp(int level)
    {
        m_level = level;
        m_isPaused = true;
    }

    void Resume()
    {
        if(m_level >= 2)
        {
            m_isPaused = false;
        }
    }

}
