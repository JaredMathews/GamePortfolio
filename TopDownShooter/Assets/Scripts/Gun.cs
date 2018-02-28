using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 40.0f)] float m_speed = 5.0f;
    [SerializeField] [Range(0.0f, 2.0f)] float m_fireRate = 0.05f;
    [SerializeField] GameObject m_projectile = null;

    float m_fireRateTimer = 0.0f;

    public void Fire(Vector3 direction)
    {
        if (m_fireRateTimer <= 0.0f)
        {
            m_fireRateTimer = m_fireRate;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject projectile = Instantiate(m_projectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
            projectile.GetComponent<Rigidbody>().AddForce(direction * m_speed, ForceMode.Impulse);

            Destroy(projectile, 1.0f);
        }
    }

    void Update()
    {
        m_fireRateTimer -= Time.deltaTime;
    }
}
