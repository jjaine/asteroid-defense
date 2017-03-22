using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour {
	GameObject[] asteroids;
	GameObject asteroid;

	void Start () {
		
	}
	
	void Update () {
		asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

		//find the closest TODO: update to next hit
		asteroid = closest(asteroids);

		Vector3 dir = asteroid.transform.position - gameObject.transform.position;
		//Matf.Atan2: return value is the angle between the x-axis 
		//and a 2D vector starting at zero and terminating at (x,y).
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		//AngleAxis: Creates a rotation which rotates angle degrees around axis.
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	GameObject closest(GameObject[] asteroids){
	    GameObject c = null;
	    float minDist = Mathf.Infinity;
	    Vector3 currentPos = gameObject.transform.position;
	    foreach (GameObject a in asteroids)
	    {
	        float dist = Vector3.Distance(a.transform.position, currentPos);
	        if (dist < minDist){
	            c = a;
	            minDist = dist;
	        }
	    }
	    return c;
	}
}
