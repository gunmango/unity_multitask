using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3 : MonoBehaviour
{
    Camera m_camera;
    float m_x, m_y, m_sizeX, m_sizeY;

    Coroutine m_rectChangeCoroutine;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        m_camera = GetComponent<Camera>();
        GameManager.instance.EventLevelUpHandler += LevelUp;
        m_x = 0f;
        m_y = 0f;
        m_sizeX = 1f;
        m_sizeY = 0f;
        m_camera.rect = new Rect(m_x, m_y, m_sizeX, m_sizeY);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //레벨오르면
    void LevelUp(int level)
    {
        switch (level)
        {
            case 2:
                break;
            case 3:
                m_rectChangeCoroutine = StartCoroutine(CoroutineLevel3());
                break;
            case 4:
                m_rectChangeCoroutine = StartCoroutine(CoroutineLevel4());
                break;
            case 5:
                break;
            default:
                break;

        }
    }


    IEnumerator CoroutineLevel3()
    {
        while (true)
        {
            //카메라 화면비율 움직이기
            m_sizeY += Time.deltaTime / 10;
            m_camera.rect = new Rect(m_x, m_y, m_sizeX, m_sizeY);

            //멈추기
            if (m_sizeY >= 0.5)
            {
                m_sizeY = 0.5f;
                m_camera.rect = new Rect(m_x, m_y, m_sizeX, m_sizeY);
                m_rectChangeCoroutine = null;
                GameManager.instance.CameraReadyCounterUp();
                break;
            }
            yield return null;
        }
    }

    IEnumerator CoroutineLevel4()
    {
        while (true)
        {
            //카메라 화면비율 움직이기
            m_sizeX -= Time.deltaTime / 10;
            m_camera.rect = new Rect(m_x, m_y, m_sizeX, m_sizeY);

            //멈추기
            if (m_sizeX < 0.5)
            {
                m_sizeX = 0.5f;
                m_camera.rect = new Rect(m_x, m_y, m_sizeX, m_sizeY);
                m_rectChangeCoroutine = null;
                GameManager.instance.CameraReadyCounterUp();
                break;
            }
            yield return null;
        }
    }
}
