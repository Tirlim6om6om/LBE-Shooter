using System;
using System.Collections;
using Mirror;
using Zenject;

public class CustomNetworkManager : NetworkManager
{
    public event Action<NetworkConnectionToClient> OnPlayerJoin;
    
    // private DiContainer _container;
    //
    // [Inject]
    // public void Construct(DiContainer container)
    // {
    //     _container = container;
    // }
    //
    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        StartCoroutine(WaitReadyJoin(conn));
    }

    private IEnumerator WaitReadyJoin(NetworkConnectionToClient conn)
    {
        while (!conn.isReady)
        {
            yield return null;
        }
        //_container.InjectGameObject(conn.identity.gameObject);TODO ИСПРАВИТЬ INJECT
        OnPlayerJoin?.Invoke(conn);
    }
}
