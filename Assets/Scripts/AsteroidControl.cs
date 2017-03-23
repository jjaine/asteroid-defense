using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour {
	public int id;
	public bool willHit = false;
	public GameObject explosion;
	float x,y;

	void Start(){
	}

	void Update () {

		Physics2D.IgnoreLayerCollision(8,8,true);
		Physics2D.IgnoreLayerCollision(8,9,true);
		Physics2D.IgnoreLayerCollision(11,8,true);
		Physics2D.IgnoreLayerCollision(11,9,true);
		Physics2D.IgnoreLayerCollision(11,10,true);
		Physics2D.IgnoreLayerCollision(11,11,true);

		if(transform.position.x > 10 || transform.position.x < -10 
			|| transform.position.y > 6 || transform.position.y < -6){
			Destroy(gameObject);
			SpawnAsteroid.asteroidCount--;
		}

		willHit = TrajectoryWithinSafetyZone(transform.position, GetComponent<Rigidbody2D>().velocity);
		if(!willHit)
			gameObject.layer = 11;
		else
			gameObject.layer = 8;

		x = Input.mousePosition.x;
    	y = Input.mousePosition.y;
	}
	void OnMouseDrag(){
    	transform.position = Camera.main.ScreenToWorldPoint(new Vector3(x,y,10.0f));
	}

	void OnMouseUp() {
		Vector3 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 dir = MousePos - transform.position;
		dir.Normalize ();
		GetComponent<Rigidbody2D>().velocity = dir * 4f;
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
