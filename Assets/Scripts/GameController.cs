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
                    "position", grid.GetGlobalPositionFromCoord(b.x+b.velocity.x, b.y+b.velocity.y),
                    "easetype", "linear",
                    "time", audiobank.bgLoop.length / sequencer.measureLength));

            int nextX = b.x + b.velocity.x;
            int nextY = b.y + b.velocity.y;
            Tile.Direction dir = b.velocity;
            if (currentFrame == sequencer.measureLength-1) {
                nextX = b.originX;
                nextY = b.originY;
                dir = b.originDir;
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

    // Use this for initialization
    void Start () {
        currentFrame = 0;

        int numEmitters = (FindObjectsOfType(typeof(EmitterTile)) as EmitterTile[]).Length;
        balls = new Ball[numEmitters];
        Debug.Log(numEmitters + " emitters found");

        SetUpBalls();
    }
    
    // Update is called once per frame
    void Update () {
        int newFrame = audiobank.GetFrameIndex(sequencer.measureLength);
        if (newFrame != currentFrame) {
            currentFrame = newFrame;
            tick();
        }
    }
}
