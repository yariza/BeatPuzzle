using UnityEngine;
using System.Collections;

public class ReflectTile : Tile {

    public MeshRenderer mesh;

    public override void SetColor(int colorIndex) {
        base.SetColor(colorIndex);
        Color color = ColorManager.Instance.GetColorForIndex(colorIndex);
        mesh.material.color = color;
    }

    public override Direction ResultingDirection(Direction incoming) {
        Direction result;
        if (direction.x == 0) {
            // horizontal wall
            if (incoming.x == 0) {
                // incoming vertically
                result.y = -incoming.y;
                result.x = 0;
            }
            else {
                // something weird, so we pass through
                result = incoming;
            }
        }
        else if (direction.y == 0) {
            // vertical wall
            if (incoming.y == 0) {
                // incoming horizontally
                result.x = -incoming.x;
                result.y = 0;
            }
            else {
                result = incoming;
            }
        }
        else if (direction.x / direction.y > 0) {
            // top-right
            result.x = -incoming.y;
            result.y = -incoming.x;
        }
        else {
            // top-left
            result.x = incoming.y;
            result.y = incoming.x;
        }
        return result;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
