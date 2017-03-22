using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour {
	public int id;
	public bool willHit = false;
	public GameObject explosion;
	
	void Update () {

		Physics2D.IgnoreLayerCollision(8,8,true);
		Physics2D.IgnoreLayerCollision(8,9,true);

		if(gameObject.transform.position.x > 10 || gameObject.transform.position.x < -10 
			|| gameObject.transform.position.y > 6 || gameObject.transform.position.y < -6){
			Destroy(gameObject);
			SpawnAsteroid.asteroidCount--;
		}

		willHit = TrajectoryWithinSafetyZone(gameObject.transform.position, gameObject.GetComponent<Rigidbody2D>().velocity);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		Collider2D collider = col.collider;

		if (collider.tag == "CannonBall") { 
			GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
			Destroy(gameObject);
			Destroy(collider.gameObject);
			Destroy(exp, 2);
			SpawnAsteroid.asteroidCount--;
		}
	}

	bool TrajectoryWithinSafetyZone(Vector3 asteroidPosition, Vector3 asteroidVelocity){
		return Physics2D.Raycast(asteroidPosition, (asteroidVelocity).normalized, 20, 1<<9);
	}
}
