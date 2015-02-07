using UnityEngine;
using System.Collections;

public class Sequencer : MonoBehaviour {

    public TextAsset sequence;

    public int measureLength;
    public bool[,] measure;
    ColorManager cs;

    void fromFile() {
		string sequenceText = sequence.text;
		string[] lines = sequenceText.Split(new char[] {'\n'});
        measureLength = int.Parse(lines[0]);
        if (lines.Length != cs.numColors + 1) {
            Debug.Log("Wrong number of instruments!" + measureLength);
        }

        measure = new bool[cs.numColors, measureLength];

        for (int i = 1; i < lines.Length; i++) {
            char[] instrument = lines[i].Trim().ToCharArray();

            // Seriously I don't get why there's an extra line
            if (instrument.Length != measureLength) {
                Debug.Log("Instrument " + i + " in sequence doesn't match measure length!");
                continue;
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

    /* Returns the 0-indexed number of a hit
     * color: index in ColorManager
     * frame: frame index
     */
    public int numHits(int color, int frame) {
        int hits = 0;
        if (measure[color, frame] == false) {
            return -1;
        } else {
            for (int i = 0; i <= frame; i++) {
                if (measure[color, i] == true) {
                    hits += 1;
                }
            }
            return hits - 1;
        }
    }

    // Use this for initialization
    void Start () {
        cs = ColorManager.Instance;
        fromFile();
        Debug.Log(numHits(0, 4));
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
