using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallControl : MonoBehaviour {

	void Start () {
		
	}

	void Update () {
		Physics2D.IgnoreLayerCollision(10,10,true);
	}
}
