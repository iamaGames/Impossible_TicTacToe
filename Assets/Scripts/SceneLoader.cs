using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    [SerializeField] Animator fadeAnim;
    public void LoadGameScene(){
        
        StartCoroutine(LoadTransition(1));
    }
    public void LoadMenu(){
        StartCoroutine(LoadTransition(0));
    }

    public void LoadStats(){
        StartCoroutine(LoadTransition(2));
    }

    private IEnumerator LoadTransition(int sceneIndex){
        fadeAnim.SetTrigger("fade");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }
}
