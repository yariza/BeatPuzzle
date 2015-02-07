using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour {

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
        if (Input.GetButton("Jump")) {
            int level = Application.loadedLevel;
            if (level == Application.levelCount-1) {
                Application.Quit();
            }
            else {
                Application.LoadLevel(level+1);
            }
        }
    }
}
