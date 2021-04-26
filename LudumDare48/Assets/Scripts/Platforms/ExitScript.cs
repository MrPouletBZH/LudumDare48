using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour{
    public string scene;
    public bool endingScene;
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Exit"){
            if(transform.GetComponent<PlayerMovements>().GetItemTaken())
                PlayerPrefs.SetString(transform.GetComponent<PlayerMovements>().GetSuccess(), "taken");

            if (endingScene)
                StartCoroutine(Ending());
            else
                SceneManager.LoadScene(scene);
        }
    }

    private IEnumerator Ending(){
        CanvasGroup canvas = GameObject.FindGameObjectWithTag("CanvasEnd").GetComponent<CanvasGroup>();
        for (int x = 0; x<1000; x++){
            canvas.alpha+=0.001f;
            yield return new WaitForSeconds(0.002f);
        }
        SceneManager.LoadScene(scene);
    }
}
