using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Animator animator; 
    [SerializeField] private GameObject sceneTransitionImage; 

    private void Awake()
    {
        if (animator == null)
        {
            Debug.LogWarning("Animator belum diatur pada LevelManager.");
        }

        
        if (sceneTransitionImage != null)
        {
            sceneTransitionImage.SetActive(false);
        }
    }

    public void TriggerGameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    private IEnumerator GameOverSequence()
    {
        
        if (sceneTransitionImage != null)
        {
            sceneTransitionImage.SetActive(true);
        }

        animator.SetTrigger("StartTransitionTrigger");

        
        yield return new WaitForSeconds(1f); 

        
        SceneManager.LoadScene("Main");
    }
}
