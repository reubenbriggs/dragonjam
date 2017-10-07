using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private static AudioManager instance;
    public static AudioManager Instance {
        get { return instance; }
    }

    private bool audioEnabled = true;
    public bool AudioEnabled {
        get { return audioEnabled; }
    }

    [SerializeField]
    private AudioSource audioObject;

	// Use this for initialization
	void Start () {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
	}
	
    public void PlayAudio(AudioClip clip, Vector3 pos) {
        if (audioEnabled) {
            audioObject.transform.position = pos;
            audioObject.PlayOneShot(clip);
        }
    }

    public void SetAudioEnabled(bool isEnabled) {
        audioEnabled = isEnabled;
    }
}
