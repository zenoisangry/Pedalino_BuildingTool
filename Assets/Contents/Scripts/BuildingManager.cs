using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [Header("Module Settings")]
    public GameObject[] modulePrefabs;
    public Transform previewParent;
    public LayerMask placementLayer;
    public Material validMat;
    public Material invalidMat;

    private GameObject currentPreview;
    private int currentIndex = -1;
    private bool canPlace = false;
    private List<GameObject> placedModules = new List<GameObject>();

    void Update()
    {
        UpdatePreview();
        HandlePlacement();
    }

    public void SwitchModule(int index)
    {
        if (index < 0 || index >= modulePrefabs.Length) return;
        currentIndex = index;

        // Distruggi l'anteprima precedente se esiste
        if (currentPreview != null) Destroy(currentPreview);

        // Crea il nuovo modulo di anteprima
        currentPreview = Instantiate(modulePrefabs[currentIndex], previewParent);
        SetPreviewMaterial(validMat);
    }

    void UpdatePreview()
    {
        if (currentPreview == null || Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, placementLayer))
        {
            Vector3 snapPosition = SnapToGrid(hit.point);
            currentPreview.transform.position = snapPosition;

            canPlace = CheckPlacement(snapPosition);
            SetPreviewMaterial(canPlace ? validMat : invalidMat);
        }
    }

    void HandlePlacement()
    {
        if (currentPreview == null || !canPlace) return;

        if (Input.GetMouseButtonDown(0))
        {
            GameObject newModule = Instantiate(modulePrefabs[currentIndex], currentPreview.transform.position, currentPreview.transform.rotation);
            placedModules.Add(newModule);
            Debug.Log($"Placed module {modulePrefabs[currentIndex].name} at {newModule.transform.position}");
        }
    }

    Vector3 SnapToGrid(Vector3 position)
    {
        float gridSize = 1f;
        return new Vector3(
            Mathf.Round(position.x / gridSize) * gridSize,
            Mathf.Round(position.y / gridSize) * gridSize,
            Mathf.Round(position.z / gridSize) * gridSize
        );
    }

    bool CheckPlacement(Vector3 position)
    {
        return true;
    }

    void SetPreviewMaterial(Material mat)
    {
        MeshRenderer[] renderers = currentPreview.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer rend in renderers)
        {
            rend.material = mat;
        }
    }
}