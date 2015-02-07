using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public GameObject reflectPrefab;
	public GameObject passPrefab;
	public GameObject oneWayPrefab;

	public int sizeX, sizeY;
	public int tileScaleX, tileScaleY;

	Tile[,] tiles;

	void Start() {
		// Initialize a grid of tiles
		tiles = new Tile[sizeX, sizeY];

		for (int x = 0; x < sizeX; x++) {
			for (int y = 0; y < sizeY; y++) {
				GameObject gObject = Instantiate(reflectPrefab) as GameObject;
				Transform trans = gObject.GetComponent<Transform>();
				trans.parent = transform;
				trans.localScale = new Vector3(tileScaleX, tileScaleY, 1);
				trans.position = new Vector3(x * tileScaleX, y * tileScaleY, 1);
				tiles[x, y] = gObject.GetComponent<Tile>();
			}
		}
	}

	void Update() {

	}
}
