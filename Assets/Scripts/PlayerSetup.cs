using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Player))]
public class PlayerSetup : NetworkBehaviour
{
    [SerializeField]
    Behaviour[] componentsToDisable;

    [SerializeField]
    string remoteLayerName = "RemotePlayer";

    [SerializeField]
    GameObject playerUIPrefab;
    private GameObject playerUIInstance;

    void Start()
    {        
        if(!isLocalPlayer)
        {
            AssignRemoteLayer();
            for(int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
        }
        else
        {
            playerUIInstance  = Instantiate(playerUIPrefab);
            playerUIInstance.name = playerUIPrefab.name;
        }
    }

  /*  public override void OnStartClient()
    {
        base.OnStartClient();
        string _ID = GetComponent<NetworkIdentity>().netId.ToString();
        Player _player = GetComponent<Player>();
        GameManager.registerPlayer(_ID, _player);
    }*/

    void AssignRemoteLayer()
    {
        gameObject.layer = LayerMask.NameToLayer(remoteLayerName);
    }

  /*  private void OnDisable()
    {
        GameManager.UnRegisterPlayer(transform.name);
        Destroy(playerUIInstance);
    }*/

}
