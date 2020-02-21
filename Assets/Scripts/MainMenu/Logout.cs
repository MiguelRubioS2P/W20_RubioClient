using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Logout : MonoBehaviour
{
    public Button loginButton;
    public Button logoutButton;
    public Button playGameButton;
    public Text messageBoardText;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }


    public void OnLogoutButtonClicked()
    {
        //CerrarLogin();
        TryLogout();

    }

    

    private void TryLogout()
    {
        UnityWebRequest httpClient = new UnityWebRequest(player.HttpServerAddress + "api/Account/Logout", "POST");
        httpClient.SetRequestHeader("Authorization", "bearer " + player.Token);
        httpClient.SendWebRequest();
        while (!httpClient.isDone)
        {
            Task.Delay(1);
        }

        if (httpClient.isNetworkError || httpClient.isHttpError)
        {
            throw new Exception("Login > TryLogout: " + httpClient.error);
        }
        else
        {
            player.Token = string.Empty;
            player.Id = string.Empty;
            player.Email = string.Empty;
            player.Name = string.Empty;
            player.BirthDay = DateTime.MinValue;
            messageBoardText.text += $"\n{httpClient.responseCode} Bye bye {player.Id}.";
            loginButton.interactable = true;
            logoutButton.interactable = false;
            playGameButton.interactable = false;
        }
    }

    private void CerrarLogin()
    {

        LoginModel loginModel = new LoginModel();
        loginModel.Id = player.Id;
        loginModel.Name = player.name;

        using (UnityWebRequest httpClient = new UnityWebRequest(player.HttpServerAddress + "api/Login/CerrarLogin", "POST"))
        {
            string bodyJson = JsonUtility.ToJson(loginModel);
            byte[] bodyRaw = Encoding.UTF8.GetBytes(bodyJson);

            httpClient.uploadHandler = new UploadHandlerRaw(bodyRaw);
            httpClient.SetRequestHeader("Content-type", "application/json");
            httpClient.SetRequestHeader("Authorization", "bearer " + player.Token);

            httpClient.SendWebRequest();

            if (httpClient.isNetworkError || httpClient.isHttpError)
            {
                throw new Exception("CerrarLogin > Error: " + httpClient.error);
            }
            else
            {
                Debug.Log("CerrarLogin > Info: " + httpClient.responseCode);
            }

            
        }
    }

}
