using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroid : MonoBehaviour {

	public GameObject asteroid;

	// Use this for initialization
	void Start () {
		Instantiate(asteroid, new Vector2(Random.Range(-5,5), Random.Range(-5,5)), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
