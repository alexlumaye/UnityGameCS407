using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood : MonoBehaviour
{
    private int clicksUntilBreak = 3;

    void OnMouseDown() {
        clicksUntilBreak--;

        if (clicksUntilBreak == 0) {
            PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
            playerInventory.AddToInventory(gameObject.name, 1);
            Destroy(gameObject);
        } else transform.localScale = transform.localScale / 1.05f;
    }
}
