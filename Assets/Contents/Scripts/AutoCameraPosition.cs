using UnityEngine;

public class AutoCameraPosition : MonoBehaviour
{
    public Transform placementPlane;
    public Canvas uiCanvas;
    public float distanceMultiplier = 2.5f;
    public float heightOffset = 5f;
    public float angle = 45f;

    void Start()
    {
        if (placementPlane == null || uiCanvas == null)
        {
            Debug.LogError("PlacementPlane o UICanvas non assegnato!");
            return;
        }

        // Calcola il centro del piano
        Bounds bounds = placementPlane.GetComponent<Renderer>().bounds;
        Vector3 center = bounds.center;
        float maxDimension = Mathf.Max(bounds.size.x, bounds.size.z);
        float distance = maxDimension * distanceMultiplier;

        // Calcola la posizione della camera
        Vector3 cameraPosition = center + new Vector3(0, distance * Mathf.Tan(Mathf.Deg2Rad * angle), -distance);
        transform.position = cameraPosition;
        transform.LookAt(center + Vector3.up * heightOffset);

        Debug.Log("Camera posizionata automaticamente.");
    }
}