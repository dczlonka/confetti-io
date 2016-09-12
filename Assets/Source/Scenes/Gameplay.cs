using UnityEngine;
using System.Collections;
using Syncano.Data;

public class Gameplay : MonoBehaviour
{
    private GameController controller;

    void Start()
    {
        controller = GameController.Instance;
        GetRoom();
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    private void GetRoom()
    {
        SyncanoWrapper.Please().Get<RoomData>(Constants.ROOM_ID, OnRoomDownloadSuccess, OnRoomDownloadFali);
    }

    private void OnRoomDownloadSuccess(Response<RoomData> response)
    {
        Debug.Log("Downloaded room");
        GameInput.Instance.Enable();
    }

    private void OnRoomDownloadFali(Response<RoomData> response)
    {
        Debug.Log("Failed to download room: " + response.syncanoError);
    }
}
