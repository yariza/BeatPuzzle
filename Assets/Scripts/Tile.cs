using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public static GameObject reflectPrefab;
	public static GameObject passPrefab;
	public static GameObject oneWayPrefab;

	enum Type {REFLECT, PASS, ONEWAY}
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
