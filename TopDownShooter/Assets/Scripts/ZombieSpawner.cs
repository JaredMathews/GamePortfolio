using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] [Range(0.0f, 10.0f)] float m_time = 0.0f;
    [SerializeField] GameObject m_zombie = null;
    [SerializeField] GameObject m_player = null;

    float m_timer = 1.0f;

    void Start()
    {
        m_timer = m_time;
    }

    void Update()
    {
        m_timer -= Time.deltaTime;
        if (m_timer <= 0.0f)
        {
            m_timer = m_time;

            Vector3 position = Vector3.zero;

            position.x = Random.Range(-5.0f, 5.0f);
            position.z = Random.Range(-5.0f, 5.0f);

            GameObject zombie = Instantiate(m_zombie, position, Quaternion.LookRotation(Vector3.forward), transform);
            zombie.GetComponent<Zombie>().GetPlayer(m_player);
        }
    }
}
