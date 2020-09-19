using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;
using Firebase.Unity.Editor;
using Firebase;

public class DataBridge : MonoBehaviour
{
    [SerializeField] private Text usernameInput, passwordInput;

    private User data;
    private string DATA_URL = "https://cryptonary-57f21.firebaseio.com/";

    private DatabaseReference databaseReference;

    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(DATA_URL);
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveData()
    {
        if(usernameInput.text.Equals("") || passwordInput.text.Equals(""))
        {
            print("NO DATA");
            return;
        }

        data = new User(usernameInput.text, passwordInput.text);
        string jsonData = JsonUtility.ToJson(data);
        databaseReference.Child("Users" + Random.Range(0, 1000000))
            .SetRawJsonValueAsync(jsonData);
    }

    public void LoadData()
    {
        FirebaseDatabase.DefaultInstance.GetReferenceFromUrl(DATA_URL).GetValueAsync()
            .ContinueWith((task =>
            {
                if (task.IsCanceled) { }
                if (task.IsFaulted) { }
                if (task.IsCompleted)
                { // we got the data
                    DataSnapshot snapShot = task.Result;
                    string user_data = snapShot.GetRawJsonValue();
                    print("Data is: " + user_data);
                    // extract users
                    //User m = JsonUtility.FromJson<User>(user_data); => for one data
                    foreach (var child in snapShot.Children)
                    {
                        string t = child.GetRawJsonValue();
                        User extractedData = JsonUtility.FromJson<User>(t);

                        print("Username is: " + extractedData.username);
                        print("Password is: " + extractedData.password);
                    }
                }

            }));
    }

}
