using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	enum Type {REFLECT, PASS, ONE_WAY}
	enum Orientation {N, NE, E, SE, S, SW, W, NW};

	Type type;
	Orientation orientation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
