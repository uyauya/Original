using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class QuickWarpMovementController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed = 1.0f;
    [SerializeField] float m_warpDistance = 0.5f;
    /// <summary>この秒数以上方向キーを押すと、ワープはできなくなる</summary>
    [SerializeField] float m_moveInterval = 0.1f;
    /// <summary>この秒数未満の間隔で同じ方向に２回入力すると、ワープする</summary>
    [SerializeField] float m_quickWarpKeystrokeInterval = 0.3f;
    /// <summary>一度ワープすると、この時間はワープできなくする</summary>
    [SerializeField] float m_quickWarpIdleTime = 3.0f;

    float m_quickWarpTimer;
    float m_moveTimer;
    float m_quickWarpIdleTimer;
    Vector3 m_lastMoveDirection;
    Rigidbody m_rb;
    Renderer m_renderer;

    [SerializeField] Color m_warpAvailable = Color.green;
    [SerializeField] Color m_warpUnavailable = Color.red;

    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        m_renderer = GetComponent<Renderer>();
        m_quickWarpIdleTimer = m_quickWarpIdleTime;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        m_rb.velocity = m_moveSpeed * dir.normalized;

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            if (dir.Equals(m_lastMoveDirection))
            {
                if (m_quickWarpTimer < m_quickWarpKeystrokeInterval && m_quickWarpIdleTimer > m_quickWarpIdleTime)
                {
                    transform.Translate(h * m_warpDistance, 0, v * m_warpDistance, Space.World);
                    m_quickWarpIdleTimer = 0;
                }
            }
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            if (m_moveTimer < m_moveInterval)
            {
                m_moveTimer += Time.deltaTime;
            }
        }

        if (h != 0 || v != 0)
        {
            m_lastMoveDirection = dir;
        }

        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
        {
            if (h == 0 && v == 0)
            {
                if (m_moveTimer < m_moveInterval)
                {
                    m_quickWarpTimer = 0f;
                }
                m_moveTimer = 0f;
            }
        }

        if (m_quickWarpTimer < m_quickWarpKeystrokeInterval)
        { 
            m_quickWarpTimer += Time.deltaTime;
        }

        if (m_quickWarpIdleTimer > m_quickWarpIdleTime)
        {
            if (m_renderer.material.color != m_warpAvailable)
            {
                m_renderer.material.color = m_warpAvailable;
            }
        }
        else
        {
            if (m_renderer.material.color != m_warpUnavailable)
            {
                m_renderer.material.color = m_warpUnavailable;
            }
            m_quickWarpIdleTimer += Time.deltaTime;
        }
    }
}
