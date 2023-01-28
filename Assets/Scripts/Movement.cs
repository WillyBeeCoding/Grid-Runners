using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public bool isMoving;

    public MazeNode prevNode;
    public MazeNode nextNode;

    public string direction;
    public Vector3 destination;

    public GameObject character;

    private void Awake() {
        
    }

    private void Start() {
        isMoving = true;
        character = GetComponentInChildren<Rigidbody>().gameObject;
    }

    private void FixedUpdate() {
        Vector3 destination = nextNode.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, destination, speed * 10f * Time.fixedDeltaTime);
        if (Vector3.Distance(transform.position, destination) < 0.0001f)
        {
            isMoving = false;
        } else {
            RotateCharacter();
        }
    }

    public void SetDestination(Vector3 newDest, string newDir) {
        destination = newDest;
        direction = newDir;
    }

    public string CurrentDirection() {
        return direction;
    }

    public void SetNodes(MazeNode newPrev, MazeNode newNext) {
        prevNode = newPrev;
        nextNode = newNext;
    }

    public void SwapNodes() {
        MazeNode temp = prevNode; prevNode = nextNode; nextNode = temp;
    }

    private void RotateCharacter() {
        Quaternion q = Quaternion.LookRotation(destination - transform.position);
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, q, Time.fixedDeltaTime * 20f);
    }
}
