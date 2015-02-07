using UnityEngine;
using System.Collections;

public class Instrument : MonoBehaviour {

    public AudioClip[] sounds;

    private int count;
    private AudioSource[] sources;

    public void PlayHit(double time, int index) {
        AudioSource source = sources[index];
        source.PlayScheduled(time);
    }

    // Use this for initialization
    void Start () {
        sources = new AudioSource[sounds.Length];
        for(int i=0; i<sounds.Length; i++) {
            AudioClip clip = sounds[i];
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            sources[i] = source;
        }
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
