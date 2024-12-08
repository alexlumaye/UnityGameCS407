using UnityEngine;

public class TectonicPlate : MonoBehaviour {
    public GameObject box; // The box that will open
    public GameObject collectible; // The collectible inside the box

    public static bool plate1Activated = false;
    public static bool plate2Activated = false;
    private bool boxOpened = false;

    void Start() {
        collectible.SetActive(false); // Initially hide scuba mask
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            Debug.Log(gameObject.name + " Triggered by " + other.name);

            if (gameObject.name == "Plate1" && !plate1Activated) {
                plate1Activated = true;
                Debug.Log("Plate 1 activated");
            } else if (gameObject.name == "Plate2" && !plate2Activated) {
                plate2Activated = true;
                Debug.Log("Plate 2 activated");
            }

            //check if both plates are activated
            CheckBoxActivation();
        }
    }

    void Update() {
        CheckBoxActivation();
    }

    private void CheckBoxActivation() {
        if (plate1Activated && plate2Activated && !boxOpened) {
            Debug.Log("Both plates activated. Opening the box.");
            OpenBox();
        }
    }

    private void OpenBox() {
        // Detach the collectible from the box
        collectible.transform.parent = null;

        // Adjust the position of the collectible
        collectible.transform.position = new Vector3(box.transform.position.x, box.transform.position.y + 1, box.transform.position.z);

        // Ensure collectible is visible and in the right sorting layer
        SpriteRenderer sr = collectible.GetComponent<SpriteRenderer>();
        if (sr != null) {
            sr.sortingOrder = 10; 
        }

        // activate collectible
        collectible.SetActive(true);
        Debug.Log("Collectible is now visible at: " + collectible.transform.position);

        // deactivate box
        box.SetActive(false);
        boxOpened = true;
    }


}
