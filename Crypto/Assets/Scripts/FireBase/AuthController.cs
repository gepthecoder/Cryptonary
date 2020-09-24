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
    private InputField log_in_emailInput, log_in_passwordInput;

    public Text ErrorLoginMessage_Email;
    public Text ErrorLoginMessage_Password;


    // <summary>
    // Animators for transitions
    // </summary>
    public Animator RegistrationCompletedTemplateAnime;
    public Animator LoginCompletedTemplateAnime;
    public Animator LogOutCompletedTemplateAnime;
    public Text TEXT_CHILD_LogOutCompletedTemplateAnime;

    // <summary>
    // variables for successfull login
    // </summary>
    public Button BTN_BANNER_SIGN_IN;
    public Button BTN_BANNER_SIGN_OUT;
    public Button BTN_BANNER_CREATE_ACC;

    public Button BTN_MORE_FEATURES_SignUp_SignIn;

    private DataBridge bridge;

    void Awake()
    {
        bridge = GetComponent<DataBridge>();
    }

    void Start()
    {
        CleanUpLoginInputs();
        CleanUpRegisterInputs();

        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            USER_GUI();
        }
        else
        {
            DEFAULT_GUI();
        }
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
                    print("User Signed in Successfully: " + FirebaseAuth.DefaultInstance.CurrentUser.Email);
                    SIGN_IN_BTN.interactable = false;
                }

            }));

        LoginCompletedTemplateAnime.SetTrigger("success");
        CleanUpLoginInputs();
        Invoke("CloseSignInOpenBanner", 1f);
    }

    private void CloseSignInOpenBanner()
    {
        AppHandler.Instance.CloseSignIn();
        AppHandler.Instance.OpenLoginBanner();
        LOGIN_SUCCESSFULL();
    }

    public void LOGIN_SUCCESSFULL()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            // set default GUI
            print("User is Null...\nreturning...");
            return;
        }
        USER_GUI();
    }

    private void DEFAULT_GUI()
    {
        // BANNER
        BTN_BANNER_CREATE_ACC.interactable = true;
        BTN_BANNER_CREATE_ACC.GetComponentInChildren<Text>().text = "CREATE ACCOUNT";
        BTN_BANNER_SIGN_IN.gameObject.SetActive(true);
        BTN_BANNER_SIGN_OUT.gameObject.SetActive(false);

        SIGN_IN_BTN.interactable = true;
        // MORE FEATURES
        BTN_MORE_FEATURES_SignUp_SignIn.GetComponentInChildren<Text>().text = "Sign Up / Sign In";
    }

    private void USER_GUI()
    {
        string user_email = FirebaseAuth.DefaultInstance.CurrentUser.Email;/*bridge.LoadData_Email();*/
        print("USER-GUI email : " + user_email);
        // BANNER
        BTN_BANNER_CREATE_ACC.interactable = false;
        BTN_BANNER_CREATE_ACC.GetComponentInChildren<Text>().text = user_email;
        BTN_BANNER_SIGN_IN.gameObject.SetActive(false);
        BTN_BANNER_SIGN_OUT.gameObject.SetActive(true);

        // MORE FEATURES
        BTN_MORE_FEATURES_SignUp_SignIn.GetComponentInChildren<Text>().text = user_email;
    }


    public void SignOut()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            string user = FirebaseAuth.DefaultInstance.CurrentUser.Email;
            TEXT_CHILD_LogOutCompletedTemplateAnime.text = "Logging out user " + user;
            LogOutCompletedTemplateAnime.SetTrigger("success");
            Invoke("SIGN_OUT_OPTION", 1f);
        }
    }

    private void SIGN_OUT_OPTION()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        DEFAULT_GUI();
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

        bridge.SaveData();
        RegistrationCompletedTemplateAnime.SetTrigger("success");
        // clear text fields
        CleanUpRegisterInputs();
        // close register template; open banner Login/SignIn || Login template
        Invoke("CloseRegOpenBanner", 1f);
    }

    private void CloseRegOpenBanner()
    {
        AppHandler.Instance.CloseCreateAccount();
        AppHandler.Instance.OpenLoginBanner();
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
