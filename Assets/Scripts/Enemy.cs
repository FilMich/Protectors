using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public float speed = 10f;

    private Transform target;
    private int indexOfWaypoint = 0;
    
    void Start()
    {
        target = WayPoints.waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = target.position - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World); //normalization kvoli relativnej rychlosti a vzdialenosti k cielu  

        if (Vector3.Distance(transform.position, target.position) <= 0.4f) {

            if (indexOfWaypoint >= WayPoints.waypoints.Length -1) { 
                Destroy(gameObject);
                return;
            }

			indexOfWaypoint++;
            target = WayPoints.waypoints[indexOfWaypoint];

		}
    }
}
