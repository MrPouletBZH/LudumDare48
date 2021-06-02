using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdapter : MonoBehaviour{

    private readonly float ratio = 1.7777f;
    private readonly float ratioTier = 1.3333f;
    private Camera currentCam;
    void Start()   {
        currentCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        print(currentCam==null);
        AdaptCamera();
    }

    public void AdaptCamera(){
        if (currentCam.aspect<ratio-0.01f){
            if (currentCam.aspect<ratioTier+0.01f)
                currentCam.orthographicSize = 7;
            else
                currentCam.orthographicSize = 6;
        }
        else
            currentCam.orthographicSize = 5;
    }
}
