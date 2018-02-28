using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] [Range(1.0f, 50.0f)] float m_speed = 1.0f;
    [SerializeField] [Range(1.0f, 100.0f)] float m_health = 100.0f;

    Gun m_gun;
    Rigidbody m_rigidBody;

    void Start()
    {
        m_gun = GetComponentInChildren<Gun>();
        m_rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;

        velocity.x = Input.GetAxis("Horizontal") * m_speed;
        velocity.z = Input.GetAxis("Vertical") * m_speed;

        transform.position += velocity * Time.deltaTime;

        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            if(Input.GetKey(KeyCode.Space))
            {
                Vector3 direction = targetPoint - transform.position;
                direction.y = 0;
                m_gun.Fire(direction);
            }

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5.0f * Time.deltaTime);
        }
    }
}
