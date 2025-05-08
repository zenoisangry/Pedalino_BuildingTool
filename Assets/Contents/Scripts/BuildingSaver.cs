using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SavedModule
{
    public Vector3 position;
    public int moduleID;
}

public class BuildingSaver : MonoBehaviour
{
    public List<GameObject> modulePrefabs;

    public void SaveBuilding(string path)
    {
        var modules = new List<SavedModule>();
        foreach (var mod in FindObjectsOfType<BuildingModule>())
        {
            int id = modulePrefabs.IndexOf(mod.gameObject);
            if (id >= 0)
                modules.Add(new SavedModule { position = mod.transform.position, moduleID = id });
        }

        File.WriteAllText(path, JsonUtility.ToJson(new Wrapper { modules = modules }));
    }

    public void LoadBuilding(string path)
    {
        var json = File.ReadAllText(path);
        var data = JsonUtility.FromJson<Wrapper>(json);

        foreach (var mod in FindObjectsOfType<BuildingModule>())
            Destroy(mod.gameObject);

        foreach (var entry in data.modules)
            Instantiate(modulePrefabs[entry.moduleID], entry.position, Quaternion.identity);
    }

    [System.Serializable]
    private class Wrapper
    {
        public List<SavedModule> modules;
    }
}