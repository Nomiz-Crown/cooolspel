using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtMusic : MonoBehaviour
{
    public GameObject objectToDisable1;
    public GameObject objectToDisable2;
    public GameObject musicObject;
    public GameObject objectToActivateAfterMusic;

    public bool hasTouched = false;
    private bool musicStarted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !musicStarted)
        {
            hasTouched = true;
            musicStarted = true;

            objectToDisable1.SetActive(false);
            objectToDisable2.SetActive(false);

            if (musicObject != null)
            {
                musicObject.SetActive(true);
                AudioSource audio = musicObject.GetComponent<AudioSource>();
                if (audio != null)
                {
                    StartCoroutine(WaitForMusicToEnd(audio));
                }
            }
        }
    }

    IEnumerator WaitForMusicToEnd(AudioSource audio)
    {
        yield return new WaitUntil(() => !audio.isPlaying);

        if (musicObject != null)
        {
            musicObject.SetActive(false);
        }

        if (objectToActivateAfterMusic != null)
        {
            objectToActivateAfterMusic.SetActive(true);
        }
    }
}
