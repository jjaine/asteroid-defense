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
			asteroidInstance.GetComponent<Rigidbody2D>().velocity = new Vector3(RandomVelocityX(x), RandomVelocityY(y), 0);
			asteroidInstance.GetComponent<AsteroidControl>().id=id;
			id++;
			asteroidCount++;
		}
	}

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
		if(y>=-4.0f && y<=4.0f){
			int i = Random.Range(0,2);
			if(i==1) posx = 9f;
			else posx = -9f;
		}
		else
			posx = Random.Range(-9f, 9f);

     	return posx;
	}

	float RandomVelocityX(float x){
		float vel = Random.Range(2.0f,4.0f);

     	if(x > 2)
     		vel*=-1;

     	return vel;
	}

	float RandomVelocityY(float y){
		float vel = Random.Range(2.0f,4.0f);

     	if(y > 2)
     		vel*=-1;

     	return vel;
	}
}