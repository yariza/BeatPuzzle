using UnityEngine;
using System.Collections;

public class ColorManager : Singleton<ColorManager> {

    public int numColors = 5;

    public Color GetColorForIndex(int index) {
        switch (index) {
            case 0: return new Color(  0.0f/255, 21.0f/255, 255.0f/255);
            case 1: return new Color(212.0f/255, 34.0f/255, 232.0f/255);
            case 2: return new Color(255.0f/255, 90.0f/255, 55.0f/255);
            case 3: return new Color(61.0f/255,  255.0f/255,48.0f/255);
            default: return Color.white;
        }
    }

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
