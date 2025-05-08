using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleSelectorUI : MonoBehaviour
{
    public GameObject buttonPrefab;
    public Transform buttonContainer;
    public BuildingManager buildingManager;

    void Start()
    {
        if (buttonPrefab == null || buttonContainer == null || buildingManager == null)
        {
            Debug.LogError("Mancano riferimenti nello script ModuleSelectorUI!");
            return;
        }

        GenerateButtons();
    }

    void GenerateButtons()
    {
        for (int i = 0; i < buildingManager.modulePrefabs.Length; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonContainer);
            Button buttonComponent = newButton.GetComponent<Button>();

            if (buttonComponent == null)
            {
                Debug.LogError("Il prefab del pulsante non ha un componente Button!");
                continue;
            }

            int index = i; // Importante per evitare problemi di chiusura
            buttonComponent.onClick.AddListener(() => OnModuleSelected(index));

            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText != null)
            {
                buttonText.text = buildingManager.modulePrefabs[index].name;
            }
            else
            {
                Debug.LogError("Il prefab del pulsante non ha un componente TextMeshProUGUI!");
            }
        }
    }

    void OnModuleSelected(int index)
    {
        Debug.Log($"Selected module: {buildingManager.modulePrefabs[index].name}");
        buildingManager.SwitchModule(index);
    }
}