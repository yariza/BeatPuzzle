using UnityEngine;
using System.Collections;

public class GameController : Singleton<GameController> {

    public Grid grid;
    public Sequencer sequencer;
    public Audiobank audiobank;

    public GameObject ballPrefab;

    private int currentFrame;
    private Ball[] balls;

    private void SetUpBalls() {

        int ballIndex = 0;
        for (int x = 0; x < grid.sizeX; x++) {
            for (int y=0; y < grid.sizeY; y++) {
                Tile t = grid.tiles[x,y];

                if (t != null && t.type == Tile.Type.EMITTER) {

                    Ball b = balls[ballIndex];
                    if (b == null) {
                        GameObject ballObj = Instantiate(ballPrefab) as GameObject;
                        ballObj.transform.parent = grid.transform;
                        b = ballObj.GetComponent<Ball>();
                    }

                    b.velocity = b.originDir = t.direction;
                    b.x = b.originX = x + b.velocity.x;
                    b.y = b.originY = y + b.velocity.y;

                    balls[ballIndex] = b;
                    ballIndex++;
                }
            }
        }
    }

    private void moveBalls() {

        for (int i=0; i<balls.Length; i++) {
            Ball b = balls[i];

            // set velocity animations, check for tile overlap

            iTween.MoveTo(b.gameObject,
                iTween.Hash(
                    "position", grid.GetGlobalPositionFromCoord(b.x+b.velocity.x, b.y+b.velocity.y, 1),
                    "easetype", "linear",
                    "time", audiobank.bgLoop.length / sequencer.measureLength));

            int nextX = b.x + b.velocity.x;
            int nextY = b.y + b.velocity.y;

            if (currentFrame == sequencer.measureLength-1) {
                nextX = b.originX;
                nextY = b.originY;
            }

            if (0 <= nextX && nextX < grid.sizeX && 0 <= nextY && nextY < grid.sizeY) {
                Tile t = grid.tiles[nextX,nextY];
                if (t != null && t.type != Tile.Type.EMITTER) {

                    int nextFrame = (currentFrame+1)%sequencer.measureLength;
                    int countIndex = sequencer.numHits(t.color, nextFrame);

                    audiobank.PlayHit(nextFrame, sequencer.measureLength,
                                      t.color, countIndex);
                }
            }
        }
    }

    private void tick() {
        for (int i=0; i<balls.Length; i++) {
            Ball b = balls[i];

            if (currentFrame == 0) {
                b.x = b.originX;
                b.y = b.originY;
                b.velocity = b.originDir;
                b.ResetTrail();
            }
            else {
                b.x = b.x + b.velocity.x;
                b.y = b.y + b.velocity.y;
            }

            if (0 <= b.x && b.x < grid.sizeX && 0 <= b.y && b.y < grid.sizeY) {
                Tile t = grid.tiles[b.x,b.y];
                if (t != null) {
                    b.velocity = t.ResultingDirection(b.velocity);
                }
            }
        }

        moveBalls();
    }

    private Tile selectedTile;
    private int originalX, originalY;

    private void unbindTile(int x, int y) {
        Tile t = grid.tiles[x,y];
        if (t != null && t.type != Tile.Type.EMITTER) {
            selectedTile = t;
            grid.tiles[x,y] = null;
        }
    }

    private void rebindTile(int x, int y) {
        selectedTile.transform.localPosition = new Vector3(x * grid.tileScaleX, y * grid.tileScaleY, 1);
        grid.tiles[x,y] = selectedTile;
        selectedTile = null;
    }

    // Use this for initialization
    void Start () {
        currentFrame = 0;
        selectedTile = null;

        int numEmitters = (FindObjectsOfType(typeof(EmitterTile)) as EmitterTile[]).Length;
        balls = new Ball[numEmitters];

        SetUpBalls();
    }

    // Update is called once per frame
    void Update () {
        int newFrame = audiobank.GetFrameIndex(sequencer.measureLength);
        if (newFrame != currentFrame) {
            currentFrame = newFrame;
            tick();
        }

        // handle mouse calls
        if (Input.GetMouseButton(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo)) {
                Vector3 pos = hitInfo.point;
                int xCoord = grid.GetXCoordFromGlobalPosition(pos);
                int yCoord = grid.GetYCoordFromGlobalPosition(pos);

                if (selectedTile == null) {
                    unbindTile(xCoord, yCoord);
                    originalX = xCoord;
                    originalY = yCoord;
                }

                if (selectedTile != null) {
                    float z = selectedTile.transform.position.z;
                    selectedTile.transform.position = new Vector3(pos.x, pos.y, z);
                }
            }
        }
        else if (selectedTile != null) {
            int xCoord = grid.GetXCoordFromGlobalPosition(selectedTile.transform.position);
            int yCoord = grid.GetYCoordFromGlobalPosition(selectedTile.transform.position);
            if (grid.tiles[xCoord, yCoord] == null) {
                rebindTile(xCoord, yCoord);
            } else {
                rebindTile(originalX, originalY);
            }
        }

    }
}
