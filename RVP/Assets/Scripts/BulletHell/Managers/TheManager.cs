using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheManager : MonoBehaviour
{
    public int units;
    public float globalTime;
    public bool start = false;
    public KeyCode resetKey;

    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public AudioClip audioClip;
    int index;

    // Start is called before the first frame update
    void Start()
    {
        units = 0;
        start = false;

        globalTime = 0f;

        audioSource = GetComponent<AudioSource>();

        index = Random.Range(0, audioClips.Length);
        audioClip = audioClips[index];
        audioSource.clip = audioClip;

    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            globalTime += Time.deltaTime;
        }

        if(Input.GetKeyDown(resetKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(audioSource)
        {
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}
