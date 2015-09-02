using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public Transform sky;

	void Start () {
		for (float x = -5; x < 5; x += 1.28f) {
			for (float y = -5; y < 5; y += 1.28f) {
				Transform s = Instantiate(sky, new Vector3 (x, y, 10), Quaternion.identity) as Transform;
				s.parent = transform;
			}
		}
	}
}
