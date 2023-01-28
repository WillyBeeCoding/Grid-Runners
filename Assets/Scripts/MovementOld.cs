using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementOld : MonoBehaviour
{
    public float speed;
    public float speedMultiplier;
    public LayerMask obstacleLayer;
    public bool bonked;

    public Vector3 startingPosition { get; private set; }
    public Vector3 initialDir;
    public Vector3 currentDir { get; private set; }
    public Vector3 nextDir { get; private set; }

    private void Awake() {
        this.startingPosition = this.transform.position;
    }

    private void Start() {
        ResetState();
    }

    public void ResetState() {
        currentDir = initialDir;
        nextDir = Vector3.zero;
        transform.position = startingPosition;
        this.enabled = true;
        bonked = false;
        Debug.Log("CURRENT: " + currentDir + " NEXT: " + nextDir + " INITIAL DIR: " + initialDir + " INITIAL POS: " + startingPosition);
    }

    private void Update() {
        if (nextDir != Vector3.zero) {
            SetDirection(nextDir);
        }
    }

    private void FixedUpdate() {
        if (!bonked) {
            transform.Translate(currentDir * speed * speedMultiplier* Time.fixedDeltaTime);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            bonked = false;
        }
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("BONKED AT " + this.transform.position);
        //bonked = true;
    }

    public void SetDirection(Vector3 newDirection, bool forced = false) {
        if (forced || !Occupied(newDirection)) {
            Debug.Log("NOT OCCUPIED");
            currentDir = newDirection;
            nextDir = Vector3.zero;
        } else {
            Debug.Log("OCCUPIED");
            nextDir = newDirection;
        }
    }

    public bool Occupied(Vector3 newDirection) {
        RaycastHit hit;
        bool isHit = Physics.BoxCast(this.transform.position, Vector3.one * 0.375f, newDirection, out hit, Quaternion.identity, 1.5f, obstacleLayer);
        Debug.Log("NEW DIR: " + newDirection + " " + isHit);
        return isHit;
    }
}
