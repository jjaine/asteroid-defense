using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour {

	public GameObject asteroid;
	public static int asteroidCount=0;
	int id=1;

	void Start () {

	}
	
	void Update () {
		//always have 5 asteroids
		if(asteroidCount<5){
			GameObject asteroidInstance = Instantiate(asteroid, new Vector3(RandomPosX(), RandomPosY(), 0), Quaternion.identity);
			asteroidInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(RandomVelocity(), RandomVelocity(), 0);
			asteroidInstance.GetComponent<AsteroidControl>().id=id;
			id++;
			asteroidCount++;
		}
	}

	//-5 to -2.5, +2.5 +5, not on the safe zone 
	float RandomPosY(){
		float posy = Random.Range(2.5f,7.5f);
		//map values > 5 to range -5 to -2.5
 		if (posy > 5)
     		posy = 2.5f-posy;

     	return posy;
	}

	//-9 to -2.5, +2.5 to 9, not on the safe zone
	float RandomPosX(){
		float posx = Random.Range(2.5f,16.5f);
 		if (posx > 9)
     		posx = 7.5f-posx;

     	return posx;
	}

	//-3 to -1, 1 to 3
	float RandomVelocity(){
		float vel = Random.Range(1.0f,5.0f);
 		if (vel > 3)
     		vel = 2-vel;

     	return vel;
	}
}