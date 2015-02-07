using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    public enum Type {REFLECT, PASS, ONEWAY, EMITTER};
    public enum Orientation {N, NE, E, SE, S, SW, W, NW};

    public Type type;
    public Orientation orientation;
    public int color;

    public Direction direction;

    [System.Serializable]
    public struct Direction {
        public int x;
        public int y;
    }

    public virtual Direction ResultingDirection(Direction incoming) {
        Direction result;
        result.x = 0;
        result.y = 0;
        return result;
    }

    private float OrientationToAngle(Orientation orientation) {
        switch (orientation) {
            case Orientation.E: return 0;
            case Orientation.NE: return 45;
            case Orientation.N: return 90;
            case Orientation.NW: return 135;
            case Orientation.W: return 180;
            case Orientation.SW: return 225;
            case Orientation.S: return 270;
            case Orientation.SE: return 315;
            default: return 0; //should never happen
        }
    }

    public void SetDirection(Direction newDir) {
        direction = newDir;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(newDir.y, newDir.x));
    }

    public void SetOrientation(Orientation newOrientation) {
        orientation = newOrientation;
        transform.rotation = Quaternion.Euler(0, 0, OrientationToAngle(orientation));
    }

    public virtual void SetColor(int colorIndex) {
        color = colorIndex;
    }

    public GameObject lightObj;
    private float flashIntensity = 5.3f;

    public void Flash() {
        lightObj.light.intensity = flashIntensity;
        Color c = ColorManager.Instance.GetColorForIndex(color);
        lightObj.light.color = Color.Lerp(c, Color.white, 0.7f);
    }

    // Use this for initialization
    public virtual void Start () {
        lightObj = new GameObject();
        lightObj.AddComponent<Light>();
        lightObj.light.intensity = 0;
        lightObj.transform.parent = transform;
        lightObj.transform.localPosition = new Vector3(0, 0, -0.5f);
    }
    
    // Update is called once per frame
    public virtual void Update () {
        float intensity = lightObj.light.intensity;
        if (intensity > 0.2f) {
            lightObj.light.intensity = intensity - 0.4f;
        }
    }
}
