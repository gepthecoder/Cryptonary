using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class AuthController : MonoBehaviour
{
    [SerializeField] private Text emailInput, passwordInput;

    public void SignIn()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync
            (emailInput.text, passwordInput.text).ContinueWith((task => {

                if (task.IsCanceled)
                {
                    return;
                }
                if (task.IsFaulted)
                {// problem detected
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsCompleted)
                {

                }

            }));
    }

    public void SignOut()
    {

    }

    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();

        switch (msg)
        {
            default:
                break;
        }
        print(msg);
    }

    public void SignUp()
    {

    }
}
