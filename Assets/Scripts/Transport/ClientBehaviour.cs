using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;

public class ClientBehaviour : MonoBehaviour
{
    NetworkDriver m_Driver;
    NetworkConnection m_Connection;
    bool m_IsConnected = false;

    [Header("UI Ref")]
    public ChatUI chatUI;

    void Start()
    {
        m_Driver = NetworkDriver.Create();
        var endpoint = NetworkEndpoint.LoopbackIpv4.WithPort(7779);
        m_Connection = m_Driver.Connect(endpoint);

        Debug.Log("Attempting to connect to server...");
    }

    void OnDestroy()
    {
        if (m_Driver.IsCreated)
        {
            m_Driver.Dispose();
        }
    }

    void Update()
    {
        m_Driver.ScheduleUpdate().Complete();

        if (!m_Connection.IsCreated)
            return;

        DataStreamReader stream;
        NetworkEvent.Type cmd;

        while ((cmd = m_Connection.PopEvent(m_Driver, out stream)) != NetworkEvent.Type.Empty)
        {
            if (cmd == NetworkEvent.Type.Connect)
            {
                Debug.Log("Connected to server!");
                m_IsConnected = true;

 
                if (chatUI != null)
                    chatUI.AddMessage("=== Connected to server ===");
            }
            else if (cmd == NetworkEvent.Type.Data)
            {
                var message = stream.ReadFixedString128();
                Debug.Log($"Received: {message}");

                if (chatUI != null)
                    chatUI.AddMessage(message.ToString());
            }
            else if (cmd == NetworkEvent.Type.Disconnect)
            {
                Debug.Log("Disconnected from server");
                m_Connection = default;
                m_IsConnected = false;

                if (chatUI != null)
                    chatUI.AddMessage("=== Disconnected from server ===");
            }
        }
    }

    public void SendChatMessage(string message)
    {
        if (!m_IsConnected || !m_Connection.IsCreated)
        {
            Debug.LogWarning("Not connected to server!");
            if (chatUI != null)
                chatUI.AddMessage("[ERROR] Not connected to server!");
            return;
        }

        m_Driver.BeginSend(m_Connection, out var writer);
        writer.WriteFixedString128(message);
        m_Driver.EndSend(writer);
    }
}