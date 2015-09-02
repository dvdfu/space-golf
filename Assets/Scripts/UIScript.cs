using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIScript : MonoBehaviour {

	public Text strokes;
	private int strokesNum;

	void Start () {
		strokesNum = 0;
		strokes.text = "Strokes: " + strokesNum;
	}

	public void Stroke() {
		strokesNum += 1;
		strokes.text = "Strokes: " + strokesNum;
	}
}
