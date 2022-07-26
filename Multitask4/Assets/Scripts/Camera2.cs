﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    Camera m_camera;
    float m_x, m_y, m_sizeX, m_sizeY;

    Coroutine m_rectChangeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GetComponent<Camera>();
        GameManager.instance.EventLevelUpHandler += LevelUp;
        m_x = 1f;
        m_y = 0f;
        m_sizeX = 0.5f;
        m_sizeY = 1f;
        m_camera.rect = new Rect(m_x, m_y, m_sizeX, m_sizeY);
    }

    //레벨오르면
    void LevelUp(int level)
    {
        switch (level)
        {
            case 2:
                m_rectChangeCoroutine = StartCoroutine(CoroutineLevel2());
                break;
            case 3:
                m_rectChangeCoroutine = StartCoroutine(CoroutineLevel3());
                //GameManager.instance.CameraReadyCounterUp();
                break;
            case 4:
                GameManager.instance.CameraReadyCounterUp();
                break;
            default:
                GameManager.instance.CameraReadyCounterUp();
                break;

        }
    }

    IEnumerator CoroutineLevel2()
    {
        while (true)
        {
            //카메라 화면비율 움직이기
            m_x -= Time.deltaTime / 10;
            m_camera.rect = new Rect(m_x, m_y, m_sizeX, m_sizeY);

            //멈추기
            if (m_x <= 0.5)
            {
                m_x = 0.5f;
                m_camera.rect = new Rect(m_x, m_y, m_sizeX, m_sizeY);
                m_rectChangeCoroutine = null;
                GameManager.instance.CameraReadyCounterUp();
                break;
            }
            yield return null;
        }
    }

    IEnumerator CoroutineLevel3()
    {
        while (true)
        {
            //카메라 화면비율 움직이기
            m_y += Time.deltaTime / 10;
            m_sizeY -= Time.deltaTime / 10;
            m_camera.rect = new Rect(m_x, m_y, m_sizeX, m_sizeY);

            //멈추기
            if (m_y >= 0.5)
            {
                m_y = 0.5f;
                m_sizeY = 0.5f;
                m_camera.rect = new Rect(m_x, m_y, m_sizeX, m_sizeY);
                m_rectChangeCoroutine = null;
                GameManager.instance.CameraReadyCounterUp();
                break;
            }
            yield return null;
        }
    }
}
