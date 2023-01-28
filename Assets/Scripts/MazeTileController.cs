using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTileController : MonoBehaviour
{
    private bool steppedOn;
    public GameObject tilePanel;
    public Material panelLitMaterial;

    public List<bool> directions; // Booleans dictating which directions the player can travel in
    
    void Start()
    {
        steppedOn = false;
        directions = new List<bool>() {false, false, false, false};
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            //Debug.Log("Collision");
            tilePanel.GetComponent<MeshRenderer>().material = panelLitMaterial;
            steppedOn = true;
        }
    }

    public void assignDirections(bool up, bool down, bool left, bool right) {
        directions = new List<bool>() {up, down, left, right};
    }
}
