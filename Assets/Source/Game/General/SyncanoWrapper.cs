using UnityEngine;
using System.Collections;
using Syncano;
using Syncano.Request;

public class SyncanoWrapper
{
    private static SyncanoClient instance;

    public static SyncanoClient GetSyncano()
    {
        if (instance == null)
        {
            instance = SyncanoClient.Instance;
            instance.Init(Constants.API_KEY, Constants.INSTANCE_NAME);
        }

        return instance;
    }

    public static RequestBuilder Please()
    {
        return GetSyncano().Please();
    }
}
