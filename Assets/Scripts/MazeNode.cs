using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeNode : MonoBehaviour
{
    public Dictionary<string, MazeNode> nodes;
    public List<string> nodeDirs;
    public List<MazeNode> nodeRefs;

    private void Start() {
        nodes = new Dictionary<string, MazeNode>();
        for (int i = 0; i < nodeDirs.Count; i++) {
            nodes.Add(nodeDirs[i], nodeRefs[i]);
        }
    }

    public bool CheckDirection(string dir) {
        return nodes.ContainsKey(dir);
    }

    public MazeNode GetNextNode(string dir) {
        if (CheckDirection(dir)) {
            return nodes[dir];
        } else {
            return this;
        }
    }

    public Vector3 GetDestination(string dir) {
        if (CheckDirection(dir)) {
            return nodes[dir].transform.position;
        } else {
            return this.transform.position;
        }
    }

    public Vector3 SendDirection(string dir) {
        Vector3 destination = GetDestination(dir);
        Vector3 direction = (destination - transform.position).normalized;
        return direction;
    }
}
