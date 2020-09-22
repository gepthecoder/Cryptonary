using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;

public class AuthController : MonoBehaviour
{
    /// <summary>
    /// email & pass for register
    /// </summary>
    public InputField emailInput, passwordInput;

    public InputField confirm_passwordInput;

    public Toggle agreement1;
    public Toggle agreement2;

    public Text ErrorRegisterMessage_Email;
    public Text ErrorRegisterMessage_Password;
    public Text ErrorRegisterMessage_PasswordConfirmation;
    public Text ErrorRegisterMessage_ErrorMsgTerms;


    public Button SIGN_IN_BTN;
    public Button SIGN_UP_BTN;

    // <summary>
    // email & pass for logging
    // </summary>
    [SerializeField]
    private Text log_in_emailInput, log_in_passwordInput;

    public Text ErrorLoginMessage_Email;
    public Text ErrorLoginMessage_Password;


    public Animator RegistrationCompletedTemplateAnime;
    public Animator LoginCompletedTemplateAnime;


    private DataBridge bridge;


    void Awake()
    {
        bridge = GetComponent<DataBridge>();
    }

    void Start()
    {
        CleanUpLoginInputs();
        CleanUpRegisterInputs();
    }


    public void SignIn()
    {
        if ((log_in_emailInput.text == ""))
        {
            print("Please enter both email to continue..");
            ErrorLoginMessage_Email.text = Constants.ERROR_LOGIN_EMAIL;
            return;
        }
        if ((log_in_passwordInput.text == ""))
        {
            print("Please enter password to continue..");
            ErrorLoginMessage_Password.text = Constants.ERROR_LOGIN_PASSWORD;
            return;
        }

        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync
            (log_in_emailInput.text, log_in_passwordInput.text).ContinueWith((task => {

                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessageLog((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsFaulted)
                {// problem detected
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessageLog((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsCompleted)
                {
                    print("User Signed in Successfully");
                    SIGN_IN_BTN.interactable = false;
                }

            }));

        bridge.SaveData();
        CleanUpLoginInputs();
        LoginCompletedTemplateAnime.SetTrigger("success");
        AppHandler.Instance.CloseSignIn();
        AppHandler.Instance.OpenMoreFeatures();
    }

    public void SignOut()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
        }
    }

    void GetErrorMessageReg(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();

        switch (errorCode)
        {
            // existing user
            case AuthError.AccountExistsWithDifferentCredentials:
                ErrorRegisterMessage_Email.text = msg;
                break;
            // password
            case AuthError.MissingPassword:
                ErrorRegisterMessage_Password.text = msg;
                break;
            case AuthError.WeakPassword:
                ErrorRegisterMessage_Password.text = msg;
                break;
            // email
            case AuthError.InvalidEmail:
                ErrorRegisterMessage_Email.text = msg;
                break;
            case AuthError.MissingEmail:
                ErrorRegisterMessage_Email.text = msg;
                break;

            default:
                break;
        }
        print(msg);

    }

    void GetErrorMessageLog(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();

        switch (errorCode)
        {
            // password
            case AuthError.MissingPassword:
                ErrorLoginMessage_Password.text = msg;
                break;
            case AuthError.WrongPassword:
                ErrorLoginMessage_Password.text = msg;
                break;

            // email
            case AuthError.InvalidEmail:
                ErrorLoginMessage_Email.text = msg;
                break;
            case AuthError.MissingEmail:
                ErrorLoginMessage_Email.text = msg;
                break;

            default:
                break;
        }
        print(msg);
    }

    public void CheckForRegisterErrors()
    {
        if ((emailInput.text == ""))
        {
            print("Please enter both email to continue..");
            ErrorRegisterMessage_Email.text = Constants.ERROR_REGISTER_EMAIL;
            return;
        }
        if ((passwordInput.text == ""))
        {
            print("Please enter password to continue..");
            ErrorRegisterMessage_Password.text = Constants.ERROR_REGISTER_PASSWORD;
            return;
        }
        if (!(passwordInput.text.Equals(confirm_passwordInput.text)))
        {
            print("Password mismatch");
            ErrorRegisterMessage_PasswordConfirmation.text = Constants.ERROR_REGISTER_CONFIRM_PASSWORD;
            return;
        }
        if (!(agreement1.isOn))
        {
            print("User didnt agree with terms");
            ErrorRegisterMessage_ErrorMsgTerms.text = Constants.ERROR_REGISTER_TOGGLE;
            return;
        }
    }

    public void CheckForLoginErrors()
    {
        if ((log_in_emailInput.text == ""))
        {
            print("Please enter both email to continue..");
            ErrorLoginMessage_Email.text = Constants.ERROR_LOGIN_EMAIL;
            return;
        }
        if ((log_in_passwordInput.text == ""))
        {
            print("Please enter password to continue..");
            ErrorLoginMessage_Password.text = Constants.ERROR_LOGIN_PASSWORD;
            return;
        }
    }

    public void CleanUpRegisterInputs()
    {
        Debug.Log("Cleaning..");
        emailInput.text = "";
        passwordInput.text = "";
        confirm_passwordInput.text = "";


        ErrorRegisterMessage_Email.text = "";
        ErrorRegisterMessage_Password.text = "";
        ErrorRegisterMessage_PasswordConfirmation.text = "";
        ErrorRegisterMessage_ErrorMsgTerms.text = "";


        agreement1.isOn = false;
        agreement2.isOn = false;
        Debug.Log("Cleaned..");
    }

    public void CleanUpLoginInputs()
    {
        log_in_emailInput.text = "";
        log_in_passwordInput.text = "";

        ErrorLoginMessage_Email.text = "";
        ErrorLoginMessage_Password.text = "";
    }

    public void SignUp()
    {
        if ((emailInput.text == ""))
        {
            print("Please enter both email to continue..");
            ErrorRegisterMessage_Email.text = Constants.ERROR_REGISTER_EMAIL;
            return;
        }
        if ((passwordInput.text == ""))
        {
            print("Please enter password to continue..");
            ErrorRegisterMessage_Password.text = Constants.ERROR_REGISTER_PASSWORD;
            return;
        }
        if (!(passwordInput.text.Equals(confirm_passwordInput.text)))
        {
            print("Password mismatch");
            ErrorRegisterMessage_PasswordConfirmation.text = Constants.ERROR_REGISTER_CONFIRM_PASSWORD;
            return;
        }
        if (!(agreement1.isOn))
        {
            print("User didnt agree with terms");
            ErrorRegisterMessage_ErrorMsgTerms.text = Constants.ERROR_REGISTER_TOGGLE;
            return;
        }

        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text, passwordInput.text)
            .ContinueWith((task =>{

                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessageReg((AuthError)e.ErrorCode);
                    return;
                }

                if (task.IsFaulted)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessageReg((AuthError)e.ErrorCode);
                    return;
                }

                if (task.IsCompleted)
                {
                    print("Registration completed successfully! :)");
                    SIGN_UP_BTN.interactable = false;
                }

            }));

        RegistrationCompletedTemplateAnime.SetTrigger("success");
        // close register template; open banner Login/SignIn || Login template
        AppHandler.Instance.CloseCreateAccount();
        AppHandler.Instance.OpenLoginBanner();
        // clear text fields
        CleanUpRegisterInputs();
    }


    public void SignIn_Anonymous()
    {
        FirebaseAuth.DefaultInstance.SignInAnonymouslyAsync()
            .ContinueWith((task =>
            {

                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessageLog((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsFaulted)
                {// problem detected
                    Firebase.FirebaseException e = task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;
                    GetErrorMessageLog((AuthError)e.ErrorCode);
                    return;
                }
                if (task.IsCompleted)
                {
                    print("USER IS LOGGED IN");
                }
            }));
    }
}
