using UnityEngine;

public class DissolveFade : MonoBehaviour
{
    private float m_time = default;

    private Renderer m_renderer = default;
    private Material m_material = default;

    void Start()
    {
        m_renderer = gameObject.GetComponent<Renderer>();
        m_material = m_renderer.material;

        m_time = 0.0f;
    }

    void Update()
    {
        if (m_time >= 1.5f)
        {
            m_time = 0.0f;
        }

        m_material.SetFloat("_Threshold", m_time);
        m_time += 0.0015f;
    }
}
