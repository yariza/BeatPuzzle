using UnityEngine;
using System.Collections;

public class Audiobank : Singleton<Audiobank> {

    public Instrument[] instruments;
    public AudioClip bgLoop;

    public int GetFrameIndex(int numFrames) {
        return (int)((1.0 * audio.timeSamples) * numFrames / audio.clip.samples);
    }

    public void PlayHit(int frameIndex, int numFrames, int instrIndex, int soundIndex) {
        // sound index is which sound within Instrument to play (count hits played so far)
        double time = GetNextDSPTime(frameIndex, numFrames);
        instruments[instrIndex].PlayHit(time, soundIndex);
    }

    private double GetNextDSPTime(int frameIndex, int numFrames) {
        double audioLength = (1.0 * audio.clip.samples) / audio.clip.frequency;
        double currentPosition = (1.0 * audio.timeSamples) * audioLength / audio.clip.samples;
        double framePosition = (1.0 * frameIndex) * audioLength / numFrames;

        if (currentPosition < framePosition) {
            // it's still in the future
            return AudioSettings.dspTime + (framePosition - currentPosition);
        }
        else {
            // loop back to start
            return AudioSettings.dspTime + (audioLength - currentPosition) + framePosition;
        }
    }

    void Start () {
        if (audio == null) {
            gameObject.AddComponent<AudioSource>();
        }
        audio.clip = bgLoop;
        audio.loop = true;

        audio.Play();
    }

    // Update is called once per frame
    void Update () {

    }
}
