using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 100.0f)] float m_health = 1.0f;
    [SerializeField] [Range(0.01f, 50.0f)] float m_speed = 1.0f;

    GameObject[] m_gameObjects;
    GameObject m_player = null;

    public void GetPlayer(GameObject player)
    {
        m_player = player;
    }

    void Update()
    {
        if(m_health <= 0.0f)
        {
            Destroy(gameObject);
        }

        Vector3 direction = m_player.transform.position - transform.position;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime);

        Vector3 velocity = transform.rotation * Vector3.forward * m_speed;
        transform.position = transform.position + (velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            m_health -= 20.0f;
            Destroy(other.gameObject);
        }
    }
}
