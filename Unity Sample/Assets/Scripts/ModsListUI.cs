using ModEnabler;
using UnityEngine;

/// <summary>
/// Simple script to show the available mods and to show their info
/// </summary>
public class ModsListUI : MonoBehaviour
{
    private void OnGUI()
    {
        GUI.Window(0, new Rect(0, 0, Screen.width / 3, Screen.height), TestWindow, "Mods Control Panel");
    }

    private void TestWindow(int windowID)
    {
        if (GUILayout.Button("Reload all mods"))
            ModsManager.ReloadAllMods();

        // Just in case
        if (ModsManager.modsList == null)
            return;

        // Loop through all the mods and display their info
        foreach (var item in ModsManager.modsList)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name: " + item.properties.DisplayName, GUILayout.Width(200));
            GUILayout.Label("Version: " + item.properties.Version);

            // Toggle the mod's active state
            bool active = item.active;
            active = GUILayout.Toggle(active, " Active");
            // The check it so that we dont flood the system, otherwise it would set the state every frame
            if (item.active != active)
            {
                if (active)
                    ModsManager.ActivateMod(item);
                else
                    ModsManager.DeactivateMod(item);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Author: " + item.properties.Author);
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Description: " + item.properties.Details);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("________________________________________");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}