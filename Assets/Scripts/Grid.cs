using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public GameObject tilePrefab;
	public int sizeX, sizeY;
	public int tileScaleX, tileScaleY;

	Tile[,] tiles;

	void Start() {
		// Initialize a grid of tiles
		tiles = new Tile[sizeX, sizeY];

		for (int x = 0; x < sizeX; x++) {
			for (int y = 0; y < sizeY; y++) {
				GameObject gObject = Instantiate(tilePrefab) as GameObject;
				Transform trans = gObject.GetComponent<Transform>();
				trans.parent = transform;
				trans.scale.set(tileScaleX, tileScaleY);
				trans.position.set(x * tileScaleX, y * tileScaleY);
				tiles[x, y] = gObject.GetComponent<Tile>();
			}
		}
	}

	void Update() {

	}
}
