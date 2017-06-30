using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReNomals : MonoBehaviour {

	// Use this for initialization
	void Start () {
    }
    private void OnTriggerEnter(Collider other) {
        if (other.isTrigger) { return; }
        if (other.gameObject.name == "DevPlayer") {
            ReversFace();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.isTrigger) { return; }
        if (other.gameObject.name == "DevPlayer") {
            ReversFace();
        }
    }

    public void ReversFace() {
        var mesh = GetComponent<MeshFilter>().mesh;
        var indices = mesh.triangles;
        var triangleCount = indices.Length / 3;
        for (var i = 0; i < triangleCount; i++) {
            var tmp = indices[i * 3];
            indices[i * 3] = indices[i * 3 + 1];
            indices[i * 3 + 1] = tmp;
        }
        mesh.triangles = indices;
        // additionally flip the vertex normals to get the correct lighting
        var normals = mesh.normals;
        for (var n = 0; n < normals.Length; n++) {
            normals[n] = -normals[n];
        }
        mesh.normals = normals;
    }
    
}
