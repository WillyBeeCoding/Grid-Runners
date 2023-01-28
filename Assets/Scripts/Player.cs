using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Player : MonoBehaviour
{
    private Movement movement;
    public string wantedDirection;

    private void Awake() {
        movement = GetComponent<Movement>();
    }

    private void Update() {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            wantedDirection = "up";
            if (movement.CurrentDirection() == "down" && movement.isMoving) {
                TurnAround("up");
            }
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            wantedDirection = "down";
            if (movement.CurrentDirection() == "up" && movement.isMoving) {
                TurnAround("down");
            }
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            wantedDirection = "left";
            if (movement.CurrentDirection() == "right" && movement.isMoving) {
                TurnAround("left");
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            wantedDirection = "right";
            if (movement.CurrentDirection() == "left" && movement.isMoving) {
                TurnAround("right");
            }
        }
    }

    private void FixedUpdate() {
        if (!movement.isMoving) {
            DecideDirection(wantedDirection);
        }
    }

    private void DecideDirection(string newDir) {
        if (movement.nextNode.CheckDirection(newDir)) {
            Debug.LogWarning("NEW PATH");
            movement.SetNodes(movement.nextNode, movement.nextNode.GetNextNode(newDir));
            movement.SetDestination(movement.nextNode.transform.position, newDir);
            movement.isMoving = true;
            //MazeNode temp = nextNode.GetNextNode(newDir); prevNode = nextNode; nextNode = temp;
        } else if (movement.nextNode.CheckDirection(movement.direction)) {
            Debug.LogWarning("KEEP ON BABY");
            movement.SetNodes(movement.nextNode, movement.nextNode.GetNextNode(movement.direction));
            movement.SetDestination(movement.nextNode.transform.position, movement.direction);
            movement.isMoving = true;
            //MazeNode temp = nextNode.GetNextNode(movement.currentDirString); prevNode = nextNode; nextNode = temp;
        } else {
            //Debug.LogWarning("WAITING");
        }
    }

    private void TurnAround(string newDir) {
        movement.SwapNodes();
        movement.SetDestination(movement.nextNode.transform.position, newDir);
        movement.isMoving = true;
    }
}
