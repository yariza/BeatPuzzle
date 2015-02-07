using UnityEngine;
using System.Collections;

public class PassthroughTile : Tile {

    public SpriteRenderer sprite;

    public override void SetColor(int colorIndex) {
        base.SetColor(colorIndex);
        Color color = ColorManager.Instance.GetColorForIndex(colorIndex);
        sprite.color = color;
    }

    public override Direction ResultingDirection(Direction incoming) {
        return incoming;
    }

	// Use this for initialization
	public override void Start () {
	   base.Start();
	}
	
	// Update is called once per frame
    public override void Update () {
       base.Update();
    }
}
