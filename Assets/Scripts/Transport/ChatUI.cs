using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatUI : MonoBehaviour
{
    [Header("UI Ref")]
    public TMP_InputField inputField;
    public Button sendButton;
    public TMP_Text chatText;
    public ScrollRect scrollRect;

    [Header("Client Ref")]
    public ClientBehaviour client;

    [Header("Settings")]
    public int maxMessages = 50;
    public string playerName = "Player";

    void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClick);

        inputField.onSubmit.AddListener(OnInputSubmit);

        inputField.ActivateInputField();
    }

    void OnSendButtonClick()
    {
        SendMessage();
    }

    void OnInputSubmit(string text)
    {
        SendMessage();
        inputField.ActivateInputField();
    }

    void SendMessage()
    {
        string message = inputField.text.Trim();

        if (string.IsNullOrEmpty(message))
            return;

        string formattedMessage = $"{playerName}: {message}";

        if (client != null)
        {
            client.SendChatMessage(formattedMessage);
        }

        inputField.text = "";
    }

    public void AddMessage(string message)
    {
        Debug.Log($"[ChatUI] Adding message: {message}");

        chatText.text = chatText.text + message + "\n";
        chatText.ForceMeshUpdate();

        LimitMessages();

        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0f;
    }

    void LimitMessages()
    {
        string[] lines = chatText.text.Split('\n');

        if (lines.Length > maxMessages)
        {
            string[] newLines = new string[maxMessages];
            System.Array.Copy(lines, lines.Length - maxMessages, newLines, 0, maxMessages);
            chatText.text = string.Join("\n", newLines);
        }
    }
}