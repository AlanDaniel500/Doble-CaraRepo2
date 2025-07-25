using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        public string text;
        public VideoClip videoClip;
    }

    [Header("Referencias")]
    [SerializeField] private List<TutorialStep> tutorialSteps;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private VideoPlayer tutorialVideoPlayer;
    [SerializeField] private GameObject videoPanel;
    [SerializeField] private Button nextButton;

    private int currentStep = 0;
    private bool tutorialEnded = false;

    private void Start()
    {
        nextButton.interactable = false; // Lo desactivo al inicio
        ShowStep(currentStep);
    }

    private void Update()
    {
        if (!tutorialEnded && Input.GetKeyDown(KeyCode.Space))
        {
            SkipTutorial();
        }
    }

    public void ShowStep(int index)
    {
        if (index < 0 || index >= tutorialSteps.Count) return;

        TutorialStep step = tutorialSteps[index];

        tutorialText.text = step.text;

        if (step.videoClip != null)
        {
            videoPanel.SetActive(true);
            tutorialVideoPlayer.clip = step.videoClip;
            tutorialVideoPlayer.Play();
            tutorialVideoPlayer.loopPointReached += OnVideoFinished;
            nextButton.interactable = false; // No activo botón hasta que termine video
        }
        else
        {
            videoPanel.SetActive(false);
            nextButton.interactable = true; // Si no hay video, activo botón inmediatamente
        }
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        vp.loopPointReached -= OnVideoFinished;
        nextButton.interactable = true; // Activo botón cuando video termina
    }

    public void NextStep()
    {
        currentStep++;
        if (currentStep < tutorialSteps.Count)
        {
            ShowStep(currentStep);
        }
        else
        {
            EndTutorial();
        }
    }

    private void SkipTutorial()
    {
        Debug.Log("Tutorial saltado con espacio");
        EndTutorial();
    }

    private void EndTutorial()
    {
        tutorialEnded = true;
        nextButton.interactable = false;
        videoPanel.SetActive(false);

        SceneManager.LoadScene("GAME");
    }
}
