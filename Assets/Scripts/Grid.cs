using UnityEngine;
using System.Collections;
using System.IO;

public class Grid : Singleton<Grid> {

	public GameObject reflectPrefab;
	public GameObject passPrefab;
	public GameObject oneWayPrefab;
	public GameObject gridLinePrefab;

	public TextAsset level;
	public int tileScaleX, tileScaleY;

	Tile[,] tiles;
	int sizeX, sizeY;

	void fromFile() {
		string levelText = level.text;
		string[] lines = levelText.Split(new char[] {'\n'});
		string[] dimensions = lines[0].Split(new char[] {','});

		sizeX = int.Parse(dimensions[0].Trim());
		sizeY = int.Parse(dimensions[1].Trim());

		tiles = new Tile[sizeX, sizeY];

		for (int x = 0; x < sizeX; x++) {
			for (int y = 0; y < sizeY; y++) {
				tiles[x, y] = null;
			}
		}
		
		for (int i = 1; i < lines.Length; i++) {
			// VALUE STRING: X,Y,TYPE,ORIENTATION,COLOR
			string[] values = lines[i].Split(new char[] {','});

			// For some reason, split is giving empty elements
			if (values.Length != 5) {
				break;
			}

			int x = int.Parse(values[0].Trim());
			int y = int.Parse(values[1].Trim());
			if (tiles[x, y] != null) {
				Debug.Log("Trying to place duplicate tile at " + x + ", " + y);
			} else {
				// Make tile and make the grid its parent
				GameObject tile;
				switch (values[2].ToUpper()) {
					case "REFLECT":
						tile = Instantiate(reflectPrefab) as GameObject;
						break;

					case "PASS":
						tile = Instantiate(passPrefab) as GameObject;
						break;

					case "ONEWAY":
						tile = Instantiate(oneWayPrefab) as GameObject;
						break;

					default:
						tile = Instantiate(reflectPrefab) as GameObject;
						break;
				}

				Transform trans = tile.GetComponent<Transform>();
				trans.parent = transform;
				trans.localScale = new Vector3(tileScaleX, tileScaleY, 1);
				trans.position = new Vector3(x * tileScaleX, y * tileScaleY, 1);
				tiles[x, y] = tile.GetComponent<Tile>();
			}
		}
	}

	void drawGrid() {
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

	void Start() {
		// Initialize a grid of tiles
		this.fromFile();
		this.drawGrid();
	}

	void Update() {

	}
}
