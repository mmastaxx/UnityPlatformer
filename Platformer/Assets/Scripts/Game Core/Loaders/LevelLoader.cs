using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transition;
    [SerializeField] float transitionTime = 1;
    public void LoadNextLevel() 
    {
       StartCoroutine(loadLevel(gameObject.scene.buildIndex + 1));
    }
    public void LoadFirstLevel()
    {
        StartCoroutine(loadLevel(1));
    }
    public void LoadMenu()
    {
        StartCoroutine(loadLevel(0));
    }
    IEnumerator loadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

}
