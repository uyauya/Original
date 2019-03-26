using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchController : MonoBehaviour
{
    [SerializeField] Material m_material_safe;
    [SerializeField] Material m_material_warning;
    Renderer m_renderer;

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_renderer.material = m_material_warning;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            m_renderer.material = m_material_safe;
        }
    }
}
