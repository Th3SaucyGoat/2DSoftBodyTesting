using SoftBody2D;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.XR;


// This script maps the positions of the joints to points in an edge collider to create a boundary.
// Idk if this script is relevant to the wall clipping issue.
public class AdjustEdgePoints : MonoBehaviour
{
    public GameObject edgeColliderObject;
    [HideInInspector]
    public EdgeCollider2D boundary;

    private List<Transform> softBodyVertices;
    private List<Vector2> verticesPositions = new();

    private void Awake()
    {
        if (edgeColliderObject == null)
        {
            Debug.LogError("Edge Collider not assigned!");
            Destroy(this);
            return;
        }
        if (edgeColliderObject.TryGetComponent(out EdgeCollider2D edge))
        {
            boundary = edge;
            boundary.edgeRadius = GetComponent<SoftObject>().CollidersRadius/2;
        }

        softBodyVertices = new List<Transform>();
        foreach (Transform t in transform)
        {
            if (t.gameObject.name.Contains("Joint") || t.gameObject.name.Contains("bone"))
            {
                softBodyVertices.Add(t);
                CircleCollider2D collider = t.gameObject.GetComponent<CircleCollider2D>();

                if (boundary != null)
                    Physics2D.IgnoreCollision(collider, boundary);
            }
        }
        verticesPositions.AddRange(new Vector2[softBodyVertices.Count + 1]);
        AdjustEdgeVertices();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        AdjustEdgeVertices();
    }

    void AdjustEdgeVertices()
    {

        for(int i = 0; i < softBodyVertices.Count+1; i++)
        {
            if (i == softBodyVertices.Count)
            {
                verticesPositions[i] = new Vector2(softBodyVertices[0].localPosition.x, softBodyVertices[0].localPosition.y);
            }
            else
                verticesPositions[i] = new Vector2(softBodyVertices[i].localPosition.x, softBodyVertices[i].localPosition.y);
        }

        if (boundary != null)
            boundary.points = verticesPositions.ToArray();
    }

}

