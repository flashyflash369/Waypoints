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
    public float distance_from_tracker;

    private GameObject tracker;
    public float tracker_speed_factor = 1;

    private void InitTracker()
    {
        tracker = GameObject.CreatePrimitive(PrimitiveType.Cube);
        
        tracker.transform.position = this.transform.position;
        tracker.transform.rotation = this.transform.rotation;

    }

    private void Tracker()
    {

        if(Vector3.Distance(tracker.transform.position, this.transform.position) > distance_from_tracker)
        {
            return;
        }

        if(Vector3.Distance(tracker.transform.position, waypoints[m_currentWaypoint].transform.position) < distance_from_next_waypoint)
        {
            m_currentWaypoint = (m_currentWaypoint + 1) % waypoints.Length;
        }   

        Destroy(tracker.GetComponent<Collider>());
        tracker.GetComponent<MeshRenderer>().enabled = false;

        tracker.transform.LookAt(waypoints[m_currentWaypoint].transform);
        tracker.transform.Translate(0, 0, (speed + tracker_speed_factor) * Time.deltaTime); 
    }

    private void Start()
    {
        InitTracker();
    }

    // Update is called once per frame
    void Update()
    {
        Tracker();


        Quaternion rotation = Quaternion.LookRotation(tracker.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
