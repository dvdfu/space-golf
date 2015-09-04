using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {

	public Text strokes;
	public Text distance;
	public Transform flag;
	public Camera cam;

	private int strokesNum;
	private Transform flagImage;

	void Start () {
		strokesNum = 0;
		strokes.text = "Strokes: " + strokesNum;
		flagImage = transform.FindChild ("Flag");
	}

	void Update () {
		if (flag != null) {
			if (flag.GetComponent<FlagScript>().Visible()) {
				flagImage.GetComponent<Image> ().enabled = false;
			} else {
//				flagImage.GetComponent<Image> ().enabled = true;
				if (cam != null) {
					Vector3 pos = cam.WorldToViewportPoint(flag.position - cam.ViewportToWorldPoint(Vector3.zero));
					pos.z = 0;
					flagImage.GetComponent<RectTransform>().localPosition = pos.normalized * 200;
				}
			}
		}
	}

	public void Stroke() {
		strokesNum += 1;
		strokes.text = "Strokes: " + strokesNum;
	}

	public void SetDistance(float d) {
		distance.text = "Distance: " + d.ToString("0.00") + "m";
	}
}
