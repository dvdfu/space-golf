using UnityEngine;
using System.Collections;

public class MeteorScript : MonoBehaviour {

	void Start () {
		GetComponent<Rigidbody2D>().velocity = new Vector3 (Random.value/8, Random.value/8, 0);
		GetComponent<Rigidbody2D>().angularVelocity = (Random.value - 0.5f) * 32;
	}
}
