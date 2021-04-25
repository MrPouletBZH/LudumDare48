using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParentPlatform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other) {
        other.collider.transform.SetParent(transform);
    }
    private void OnCollisionExit2D(Collision2D other) {
        other.collider.transform.SetParent(null);
    }
}
