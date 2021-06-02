using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnPos : MonoBehaviour{
    [SerializeField] private int numberOfDash;
    [SerializeField] private int numberOfJump;
    [SerializeField] private string scene;
    [SerializeField] private bool isFinalScene;
    void Awake() {
        GameObject player = init.player;
        PlayerMovements playerMovements = player.GetComponent<PlayerMovements>();
        ExitScript exitScript = player.GetComponent<ExitScript>();

        player.transform.position = transform.position;

        playerMovements.numberOfDash = numberOfDash;
        playerMovements.numberOfJump = numberOfJump;
        playerMovements.enteringScene = true;
        playerMovements.ResetVelocity();
        playerMovements.SetInGameMenu(false);
        playerMovements.SetMovingPlatforms();

        exitScript.scene = scene;
        exitScript.endingScene = isFinalScene;
    }
}
