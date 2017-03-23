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
		//always have 6 asteroids
		if(asteroidCount<6){
			float y = RandomPosY();
			float x = RandomPosX(y);
			GameObject asteroidInstance = Instantiate(asteroid, new Vector3(x,y,0), Quaternion.identity);
			asteroidInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(VelocityX(x), VelocityY(y), 0);
			asteroidInstance.GetComponent<AsteroidControl>().id=id;
			id++;
			asteroidCount++;
		}
	}

	//generate random positions and velocities for asteroids:

	//-5 to -2, +2 to +5
	float RandomPosY(){
		float posy = Random.Range(2f,8f);
		//map values > 5 to negative range
 		if (posy > 5)
     		posy = 3-posy;

     	return posy;
	}

	float RandomPosX(float y){
		float posx = 0;
		if(y>=-3.5f && y<=3.5f){
			int i = Random.Range(0,2);
			if(i==1) posx = 8f;
			else posx = -8f;
		}
		else
			posx = Random.Range(-6f, 6f);

     	return posx;
	}

	float VelocityX(float x){
		float vel = Random.Range(1.0f,4.0f);

     	if(x > 2)
     		vel*=-1;

     	return vel;
	}

	float VelocityY(float y){
		float vel = Random.Range(1.0f,4.0f);

     	if(y > 2)
     		vel*=-1;

     	return vel;
	}
}