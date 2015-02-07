using UnityEngine;
using System.Collections;

public class Sequencer : MonoBehaviour {

    public TextAsset sequence;

    int measureLength;
    bool[,] measure;

    void fromFile() {
		string sequenceText = sequence.text;
		string[] lines = sequenceText.Split(new char[] {'\n'});
        measureLength = int.Parse(lines[0]);

        measure = new bool[8, measureLength];

        for (int i = 1; i < lines.Length; i++) {
            char[] instrument = lines[i].ToCharArray();

            if (instrument.Length != measureLength) {
                Debug.Log("Instrument " + i + " in sequence doesn't match measure length!");
            }

            for (int j = 0; j < measureLength; j++) {
                switch (instrument[j]) {
                    case '0':
                        measure[i-1, j] = false;
                        break;

                    case '1':
                        measure[i-1, j] = true;
                        break;

                    default:
                        measure[i-1, j] = false;
                        Debug.Log("Frame at " + i + ", " + j + " unknown!");
                        break;
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
