using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWP : MonoBehaviour
{

    public GameObject[] waypoints;
    private int m_currentWaypoint;

    public float speed;
    public float rotSpeed;
    public float distance_from_next_waypoint;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(this.transform.position, waypoints[m_currentWaypoint].transform.position) < distance_from_next_waypoint)
        {
            m_currentWaypoint = (m_currentWaypoint + 1) % waypoints.Length;
        }

        //this.transform.LookAt(waypoints[m_currentWaypoint].transform);

        Quaternion rotation = Quaternion.LookRotation(waypoints[m_currentWaypoint].transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
