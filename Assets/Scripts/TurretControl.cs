using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour {
	GameObject[] asteroids;
	GameObject asteroid;

	void Start () {
		asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
		asteroid = asteroids[0];
	}
	
	void Update () {
		Vector3 dir = asteroid.transform.position - gameObject.transform.position;
		//Matf.Atan2: return value is the angle between the x-axis 
		//and a 2D vector starting at zero and terminating at (x,y).
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		//AngleAxis: Creates a rotation which rotates angle degrees around axis.
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
