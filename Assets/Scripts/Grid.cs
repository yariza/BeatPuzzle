using UnityEngine;
using System.Collections;
using System.IO;

public class Grid : Singleton<Grid> {

	public GameObject reflectPrefab;
	public GameObject passPrefab;
	public GameObject oneWayPrefab;
	public GameObject emitterPrefab;
	public GameObject gridLinePrefab;

	public Transform quad;

	public TextAsset level;
	public float tileScaleX, tileScaleY;

	public Tile[,] tiles;
	public int sizeX, sizeY;

	public Vector3 GetLocalPositionFromCoord(int x, int y) {
		return new Vector3(x*tileScaleX, y*tileScaleY, 2);
	}

	public Vector3 GetGlobalPositionFromCoord(int x, int y) {
		return transform.TransformPoint(GetLocalPositionFromCoord(x, y));
	}

	public Vector3 GetLocalGridCenter() {
		return new Vector3((sizeX-1)*tileScaleX/2, (sizeY-1)*tileScaleY/2, 2);
	}

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
			if (values.Length != 6) {
				continue;
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

					case "EMITTER":
						tile = Instantiate(emitterPrefab) as GameObject;
						break;

					default:
                        Debug.Log("Tile type not recognized at line " + i);
                        tile = null;
						break;
				}

				Transform trans = tile.GetComponent<Transform>();
				trans.parent = transform;
				trans.localScale = new Vector3(tileScaleX, tileScaleY, 1);
				trans.localPosition = new Vector3(x * tileScaleX, y * tileScaleY, 1);

				Tile tileComp = tile.GetComponent<Tile>();

				Tile.Direction dir;
				dir.x = int.Parse(values[3].Trim());
				dir.y = int.Parse(values[4].Trim());
				tileComp.SetDirection(dir);

				// switch (values[3].ToUpper()) {
				// 	case "N":
				// 		tileComp.SetOrientation(Tile.Orientation.N);
				// 		break;
				// 	case "NW":
				// 		tileComp.SetOrientation(Tile.Orientation.NW);
				// 		break;
				// 	case "W":
				// 		tileComp.SetOrientation(Tile.Orientation.W);
				// 		break;
				// 	case "SW":
				// 		tileComp.SetOrientation(Tile.Orientation.SW);
				// 		break;
				// 	case "S":
				// 		tileComp.SetOrientation(Tile.Orientation.S);
				// 		break;
				// 	case "SE":
				// 		tileComp.SetOrientation(Tile.Orientation.SE);
				// 		break;
				// 	case "E":
				// 		tileComp.SetOrientation(Tile.Orientation.E);
				// 		break;
				// 	case "NE":
				// 		tileComp.SetOrientation(Tile.Orientation.NE);
				// 		break;
				// 	default:
				// 		Debug.Log("Tile direction not recognized at line " + i);
				// 		break;
				// }

				int color = int.Parse(values[5].Trim());
				tileComp.SetColor(color);

				tiles[x, y] = tileComp;
			}
		}
	}

	void drawGrid() {
		for (int x = 0; x <= sizeX; x++) {
				GameObject gridLine = Instantiate(gridLinePrefab) as GameObject;
				LineRenderer lr = gridLine.GetComponent<LineRenderer>();
				lr.SetPosition(0, new Vector3((x - 0.5f) * tileScaleX, -0.5f * tileScaleY, 0));
				lr.SetPosition(1, new Vector3((x - 0.5f) * tileScaleX, (sizeY - 0.5f) * tileScaleY, 0));
				Transform trans = gridLine.GetComponent<Transform>();
				trans.parent = transform;
				trans.localPosition = Vector3.zero;
		}

		for (int y = 0; y <= sizeY; y++) {
				GameObject gridLine = Instantiate(gridLinePrefab) as GameObject;
				LineRenderer lr = gridLine.GetComponent<LineRenderer>();
				lr.SetPosition(0, new Vector3(-0.5f * tileScaleX, (y - 0.5f) * tileScaleY, 0));
				lr.SetPosition(1, new Vector3((sizeX - 0.5f) * tileScaleX, (y - 0.5f) * tileScaleY, 0));
				Transform trans = gridLine.GetComponent<Transform>();
				trans.parent = transform;
				trans.localPosition = Vector3.zero;
		}
		quad.localPosition = GetLocalGridCenter();
		quad.localScale = new Vector3(sizeX*tileScaleX+0.2f, sizeY*tileScaleY+0.2f, 1);
	}

	void Start() {
		// Initialize a grid of tiles
		this.fromFile();
		this.drawGrid();
	}

	void Update() {

	}
}
