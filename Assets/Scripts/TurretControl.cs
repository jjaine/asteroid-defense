using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour {
	GameObject[] objects;
	List<GameObject> asteroids;
	List<int> shotAsteroids;
	GameObject asteroid;

	public GameObject cannonBall;
	float speed = 6.0f;

	void Start () {
		shotAsteroids = new List<int>();
	}
	
	void Update () {
		objects = GameObject.FindGameObjectsWithTag("Asteroid");

		//get hitting asteroids
		asteroids = hits(objects);

		if(asteroids.Count!=0){
			foreach (GameObject asteroid in asteroids)
	    	{
				FollowAsteroid(asteroid);
				if(!shotAsteroids.Contains(asteroid.GetComponent<AsteroidControl>().id)){
					shoot(asteroid);
					shotAsteroids.Add(asteroid.GetComponent<AsteroidControl>().id);
				}
			}
		}

	}

	//get asteroids that are marked as willhit
	List<GameObject> hits(GameObject[] asteroids){
	    List<GameObject> hitAsteroids = new List<GameObject>();

	    foreach (GameObject a in asteroids)
	    {
	        if(a.GetComponent<AsteroidControl>().willHit){
	        	hitAsteroids.Add(a);
	        }
	    }
	    return hitAsteroids;
	}

	//shoot the asteroids
	void shoot(GameObject asteroid){
		Vector3 dir = CalculateMissileVelocity(asteroid.transform.position, asteroid.GetComponent<Rigidbody2D>().velocity, new Vector3(0,0,0));
		dir = new Vector3(dir.x, dir.y, 0).normalized;
		GameObject ballInstance = Instantiate(cannonBall, dir, Quaternion.identity);

		ballInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(speed*dir.x, speed*dir.y, 0);
	}

	//look towards the asteroid
	void FollowAsteroid(GameObject asteroid){
		Vector3 dir = asteroid.transform.position - transform.position;
		//Matf.Atan2: return value is the angle between the x-axis 
		//and a 2D vector starting at zero and terminating at (x,y).
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		//AngleAxis: Creates a rotation which rotates angle degrees around axis.
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	//calculate at which spot the collision will happen and shoot there
	Vector3 CalculateMissileVelocity(Vector3 asteroidPosition, Vector3 asteroidVelocity, Vector3 turretPos){
		/* solved from t = (|a+t*v-w|)/s, where
		 * t = constant, time
		 * a = vector to asteroid position from origo
		 * v = asteroid's velocity
		 * w = vector to world's position from origo
		 and the |a+t*v-w| comes from the impact point from the world's point of view:
		 * go to origo -w
		 * go to asteroid +a
		 * go to impact point +t*v
		 */

		Vector3 dir = asteroidPosition - transform.position;
		float A = asteroidVelocity.sqrMagnitude - speed * speed;
		float B = 2 * Vector3.Dot (dir, asteroidVelocity);
		float C = dir.sqrMagnitude;
		if (A >= 0) {
			float dt = (-C)/(2*B);
			return asteroidPosition + asteroidVelocity * dt;
		} else {
			float rt = Mathf.Sqrt (B*B - 4*A*C);
			float dt1 = (-B + rt) / (2 * A);
			float dt2 = (-B - rt) / (2 * A);
			float dt = (dt1 < 0 ? dt2 : dt1);
			return asteroidPosition + asteroidVelocity * dt;
 		}
	}
}
