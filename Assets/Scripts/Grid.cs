using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

	public GameObject reflectPrefab;
	public GameObject passPrefab;
	public GameObject oneWayPrefab;
	public GameObject gridLinePrefab;

	public int sizeX, sizeY;
	public int tileScaleX, tileScaleY;

	Tile[,] tiles;

	void Start() {
		// Initialize a grid of tiles
		tiles = new Tile[sizeX, sizeY];

		for (int x = 0; x < sizeX; x++) {
			for (int y = 0; y < sizeY; y++) {
				// Make tile and make the grid its parent
				GameObject tile = Instantiate(reflectPrefab) as GameObject;
				Transform trans = tile.GetComponent<Transform>();
				trans.parent = transform;
				trans.localScale = new Vector3(tileScaleX, tileScaleY, 1);
				trans.position = new Vector3(x * tileScaleX, y * tileScaleY, 1);
				tiles[x, y] = tile.GetComponent<Tile>();

			}
		}

		// Draw grid lines
		for (int x = 0; x <= sizeX; x++) {
				GameObject gridLine = Instantiate(gridLinePrefab) as GameObject;
				LineRenderer lr = gridLine.GetComponent<LineRenderer>();
				lr.SetPosition(0, new Vector3(x - 0.5f * tileScaleX, -0.5f * tileScaleY, 1));
				lr.SetPosition(1, new Vector3(x - 0.5f * tileScaleX, (sizeY - 0.5f) * tileScaleY, 1));
				Transform trans = gridLine.GetComponent<Transform>();
				trans.parent = transform;
		}

		for (int y = 0; y <= sizeY; y++) {
				GameObject gridLine = Instantiate(gridLinePrefab) as GameObject;
				LineRenderer lr = gridLine.GetComponent<LineRenderer>();
				lr.SetPosition(0, new Vector3(-0.5f * tileScaleX, y - 0.5f * tileScaleY, 1));
				lr.SetPosition(1, new Vector3((sizeX - 0.5f) * tileScaleX, y - 0.5f * tileScaleY));
				Transform trans = gridLine.GetComponent<Transform>();
				trans.parent = transform;
		}
	}

	void Update() {

	}
}
