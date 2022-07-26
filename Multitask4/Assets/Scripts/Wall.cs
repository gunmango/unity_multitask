using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    bool m_isPaused;
    public bool isPaused { set { m_isPaused = value; } }
    [SerializeField] float m_speed;
    [SerializeField] GameObject m_endPoint;
    int m_level;
    float m_endX;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        m_endX = -3.34f;
        m_isPaused = false;
    }
    public void Initialize()
    {
        m_level = 1;
        m_speed = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_isPaused)
        {
            transform.Translate(Vector3.left * m_speed * Time.deltaTime, Space.Self);


            //밖으로 나가면
            if (transform.position.x < m_endX)
            {
                gameObject.SetActive(false);
            }
        }
    }


    //충돌시
    private void OnTriggerEnter(Collider other)
    {
        //충돌한게 플레이어면
        if (other.tag == "Floater")
        {
            //게임오버
            //Debug.Log("게임오버");
            GameManager.instance.GameOver();
        }
    }


}
