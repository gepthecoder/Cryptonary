using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance { get; set; }
    public SaveState state;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        instance = this;
        Load();
        Debug.Log(Helper.Serialize<SaveState>(state));
    }

    // save the whole state of this saveState script to the player pref
    public void Save()
    {
        PlayerPrefs.SetString("save", Helper.Serialize<SaveState>(state));
    }

    // load the previious saved state from the player prefs
    public void Load()
    {
        // do we already have a save?
        if (PlayerPrefs.HasKey("save"))
        {
            state = Helper.Deserialize<SaveState>(PlayerPrefs.GetString("save"));
        }
        else
        {
            state = new SaveState();
            Save();
            Debug.Log("No save file found! Creating a new one..");
        }
    }
}
