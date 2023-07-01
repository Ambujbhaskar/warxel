using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayIntro : MonoBehaviour {
    public float wait_time = 10.850f;

    private string movie = "LPH_Intro.mp4";

    void Start () 
    {
        StartCoroutine(streamVideo(movie));
    }

    private IEnumerator streamVideo(string video)
    {
        // Handheld.PlayFullScreenMovie(video, Color.black, FullScreenMovieControlMode.Hidden, FullScreenMovieScalingMode.Fill);
        yield return new WaitForSeconds(wait_time);
        SceneManager.LoadScene ("home");
    }
}
