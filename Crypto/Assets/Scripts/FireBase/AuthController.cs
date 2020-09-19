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
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
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
                    print("User Signed in successfully");
                }

            }));
    }

    public void SignOut()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }

    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();

        switch (errorCode)
        {
            // existing user
            case AuthError.AccountExistsWithDifferentCredentials:
                break;

            // password
            case AuthError.MissingPassword:
                break;
            case AuthError.WrongPassword:
                break;
            case AuthError.WeakPassword:
                break;

            // email
            case AuthError.InvalidEmail:
                break;
            case AuthError.MissingEmail:
                break;

            default:
                break;
        }
        print(msg);
    }

    public void SignUp()
    {

        if(emailInput.text == "" || passwordInput.text == "")
        {
            print("Please enter both email & password to continue..");
            return;
        }
            FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text, passwordInput.text)
            .ContinueWith((task =>{

                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }

                if (task.IsFaulted)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
                    return;
                }

                if (task.IsCompleted)
                {
                    print("Registration completed successfully! :)");
                }

            }));
    }


    public void SignIn_Anonymous()
    {
        FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync()
            .ContinueWith((task =>
            {

                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessage((AuthError)e.ErrorCode);
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
                    print("USER IS LOGGED IN");
                }
            }));
    }
}
