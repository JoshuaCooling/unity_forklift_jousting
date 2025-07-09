using UnityEngine;

public class ForkliftHorn : MonoBehaviour
{
    public AudioClip hornClip; // Assign your horn sound in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = hornClip;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // When Space is pressed and not already playing
        if (Input.GetKeyDown(KeyCode.Space) && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
