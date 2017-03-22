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

	void shoot(GameObject asteroid){
		Vector3 dir = CalculateMissileVelocity(asteroid.transform.position, asteroid.GetComponent<Rigidbody2D>().velocity, new Vector3(0,0,0));
		dir = new Vector3(dir.x, dir.y, 0).normalized;
		Vector3 pos = new Vector3(dir.x*0.1f, dir.y*0.1f, 0);
		GameObject ballInstance = Instantiate(cannonBall, pos, Quaternion.identity);

		ballInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(speed*dir.x, speed*dir.y, 0);
	}

	void FollowAsteroid(GameObject asteroid){
		Vector3 dir = asteroid.transform.position - transform.position;
		//Matf.Atan2: return value is the angle between the x-axis 
		//and a 2D vector starting at zero and terminating at (x,y).
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		//AngleAxis: Creates a rotation which rotates angle degrees around axis.
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	GameObject closest(List<GameObject> asteroids){
	    GameObject c = null;
	    float minDist = Mathf.Infinity;
	    Vector3 currentPos = transform.position;
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

	Vector3 CalculateMissileVelocity(Vector3 asteroidPosition, Vector3 asteroidVelocity, Vector3 turretPos){
		Vector3 targetDir = asteroidPosition - turretPos;
	    float iSpeed2 = speed * speed;
	    float tSpeed2 = asteroidVelocity.sqrMagnitude;
	    float fDot1 = Vector3.Dot(targetDir, asteroidVelocity);
	    float targetDist2 = targetDir.sqrMagnitude;
	    float d = (fDot1 * fDot1) - targetDist2 * (tSpeed2 - iSpeed2);
	    if (d < 0.1f)  // cannot intercept :(
	        return Vector3.zero;
	    float sqrt = Mathf.Sqrt(d);
	    float S1 = (-fDot1 - sqrt) / targetDist2;
	    float S2 = (-fDot1 + sqrt) / targetDist2;
	    if (S1 < 0.0001f)
	    {
	        if (S2 < 0.0001f)
	            return Vector3.zero;
	        else
	            return (S2) * targetDir + asteroidVelocity;
	    }
	    else if (S2 < 0.0001f)
	        return (S1) * targetDir + asteroidVelocity;
	    else if (S1 < S2)
	        return (S2) * targetDir + asteroidVelocity;
	    else
	        return (S1) * targetDir + asteroidVelocity;
	}
}
