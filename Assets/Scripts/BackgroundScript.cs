using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	public Transform Sky;

	void Start () {
		int n = 10;
		float size = 1.28f;
		for (int x = -n; x < n; x++) {
			for (int y = -n; y < n; y++) {
				Transform s = Instantiate(Sky) as Transform;
				s.parent = transform;
				s.localPosition = new Vector3(x*size, y*size, 0);
			}
		}
	}
}
