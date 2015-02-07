using UnityEngine;
using System.Collections;

public class ColorManager : Singleton<ColorManager> {

    Color GetColorForIndex(int index) {
        switch (index) {
            case 0: return Color.cyan;
            case 1: return Color.red;
            case 2: return Color.green;
            case 3: return Color.yellow;
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
