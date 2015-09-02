using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public Transform Sky;

	void Start () {
		for (float x = -5.12f; x < 5.12f; x += 1.28f) {
			for (float y = -5.12f; y < 5.12f; y += 1.28f) {
				Transform s = Instantiate(Sky) as Transform;
				s.parent = transform;
				s.localPosition = new Vector3(x, y, 0);
			}
		}
	}
}
