using UnityEngine;

public class SceneTransitionController : MonoBehaviour
{
    [SerializeField] private GameObject sceneTransitionImage; 
    private void Awake()
    {
        if (sceneTransitionImage != null)
        {
            sceneTransitionImage.SetActive(false); 
        }
    }
    
    public void EnableTransitionImage()
    {
        if (sceneTransitionImage != null)
        {
            sceneTransitionImage.SetActive(true);
        }
    }

    
    public void DisableTransitionImage()
    {
        if (sceneTransitionImage != null)
        {
            sceneTransitionImage.SetActive(false);
        }
    }
}
