using UnityEngine;
using Cinemachine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class GameController : MonoBehaviour {
    public static GameController instance;

    public GameScore Score = new GameScore();

    private Animator animator;

    #region private
    private void Awake() {
        if(instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);        

        PlayerPrefs.SetInt($"Minigame.{PlayerPrefs.GetInt("Current Level")}", 0);
    }

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private IEnumerator Shake(float strength, float freq, float time) {
        CinemachineBasicMultiChannelPerlin noise;
        noise = FindObjectOfType<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = strength;
        noise.m_FrequencyGain = freq;
        yield return new WaitForSeconds(time);
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
    }
    #endregion

    #region public
    public void ResetScore() {
        Score.Reset();
    }

    public void ShakeOnce() {
        StartCoroutine(Shake(1f, 1f, .2f));
    }

    public bool HaveMinigameAccess() {
        return PlayerPrefs.GetInt($"Minigame.{PlayerPrefs.GetInt("Current Level")}") == 0;
    }

    public void SetMinigameAccess(bool state) {
        PlayerPrefs.SetInt($"Minigame.{PlayerPrefs.GetInt("Current Level")}", state ? 0 : 1);        
    }
    #endregion
}
