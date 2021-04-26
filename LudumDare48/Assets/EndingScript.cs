using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScript : MonoBehaviour
{
    void Start() {
        StartCoroutine(Ending());
    }
    public void ReturnToMenu(){
        SceneManager.LoadScene("MainMenuScene"); 
    }

    private IEnumerator Ending(){
        CanvasGroup canvas = transform.GetChild(1).GetComponent<CanvasGroup>();
        for (int x = 0; x<1000; x++){
            canvas.alpha-=0.001f;
            yield return new WaitForSeconds(0.002f);
        }
    }
}
