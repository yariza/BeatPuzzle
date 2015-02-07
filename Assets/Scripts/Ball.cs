using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    public TrailRenderer trail;

    private int xPos, yPos;
    public int x {
        get {
            return xPos;
        }
        set {
            xPos = value;
            SetPositions();
        }
    }
    public int y {
        get {
            return yPos;
        }
        set {
            yPos = value;
            SetPositions();
        }
    }
    public int originX, originY;
    public Tile.Direction originDir;

    public Tile.Direction velocity;

    public void ResetTrail() {
        StartCoroutine(ResetTrailCo(trail));
    }

    static IEnumerator ResetTrailCo(TrailRenderer trail)
    {
        var trailTime = trail.time;
        trail.time = 0;
        yield return 0;
        trail.time = trailTime;
    }

    private void SetPositions() {
        transform.localPosition = Grid.Instance.GetLocalPositionFromCoord(xPos, yPos, 1);
    }

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
