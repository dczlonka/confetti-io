using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Syncano.Data;
using Newtonsoft.Json;

public class Gameplay : MonoBehaviour
{
    private GameController controller;
    private PlayerData player;

    void Start()
    {
        controller = GameController.Instance;
        JoinRoom("tester1", Constants.ROOM_ID);
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

//    private void GetRoom()
//    {
//        SyncanoWrapper.Please().Get<RoomData>(Constants.ROOM_ID, OnRoomDownloadSuccess, OnRoomDownloadFali);
//    }
//
//    private void OnRoomDownloadSuccess(Response<RoomData> response)
//    {
//        Debug.Log("Downloaded room");
//    }
//
//    private void OnRoomDownloadFali(Response<RoomData> response)
//    {
//        Debug.Log("Failed to download room: " + response.syncanoError);
//    }

    //------------------------ Join ------------------------//

    private void JoinRoom(string nick, long roomId)
    {
        Dictionary<string, string> payload = new Dictionary<string, string>();
        payload.Add("nickname", nick);
        payload.Add("room_id", roomId.ToString());

        SyncanoWrapper.Please().CallScriptEndpoint(Constants.ENDPOINT_JOIN_ROOM_ID, Constants.ENDPOINT_JOIN_ROOM, OnRoomJoined, payload);
    }

    private void OnRoomJoined(ScriptEndpoint response)
    {
        if (response.IsSuccess)
        {
            Debug.Log(response.stdout);
            player = JsonConvert.DeserializeObject<PlayerData>(response.stdout);
            StartGame(player.id, Constants.ROOM_ID);
        }
        else
        {
            Debug.Log("Error: " + response.syncanoError);
        }
    }

    //------------------------ Start ------------------------//

    private void StartGame(long playerId, long roomId)
    {
        Dictionary<string, string> payload = new Dictionary<string, string>();
        payload.Add("player_id", playerId.ToString());
        payload.Add("room_id", roomId.ToString());

        SyncanoWrapper.Please().CallScriptEndpoint(Constants.ENDPOINT_START_GAME_ID, Constants.ENDPOINT_START_GAME, OnGameStarted, payload);
    }

    private void OnGameStarted(ScriptEndpoint response)
    {
        if (response.IsSuccess)
        {
            Debug.Log(response.stdout);
            controller.StartGame(Constants.ROOM_ID, player);
            GameInput.Instance.Enable();
        }
        else
        {
            Debug.Log("Error: " + response.syncanoError);
            StartGame(player.id, Constants.ROOM_ID);
        }
    }
}
