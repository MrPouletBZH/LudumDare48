using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScript : MonoBehaviour{
    public string scene;
    public bool endingScene;
    private PlayerMovements playerMovements;
    void Start() {
        playerMovements = transform.GetComponent<PlayerMovements>();
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Exit"){
            if(playerMovements.GetItemTaken())
                PlayerPrefs.SetString(playerMovements.GetSuccess(), "taken");

            if (endingScene)
                StartCoroutine(Ending());
            else{
                transform.SetParent(null);
                DontDestroyOnLoad(gameObject);
                SceneManager.LoadScene(scene);
            }
        }
    }

    private IEnumerator Ending(){
        CanvasGroup canvas = GameObject.FindGameObjectWithTag("CanvasEnd").GetComponent<CanvasGroup>();
        for (int x = 0; x<100; x++){
            canvas.alpha+=0.01f;
            yield return new WaitForSeconds(0.02f);
        }
        SceneManager.LoadScene(scene);
    }
}
