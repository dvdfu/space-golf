using UnityEngine;
using System.Collections;

public class MeteorScript : MonoBehaviour {

	void Start () {
		rigidbody2D.velocity = new Vector3 (Random.value/8, Random.value/8, 0);
		rigidbody2D.angularVelocity = (Random.value - 0.5f) * 32;
	}
}
