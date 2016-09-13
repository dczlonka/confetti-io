using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Syncano;
using Syncano.Data;

[System.Serializable]
public class ResponseGetList<T> : Response<T>  where T : SyncanoObject, new() {

	public string prev;
	public string next;

	public List<T> objects;

	public override void SetData (string json)
	{
		Newtonsoft.Json.JsonConvert.PopulateObject(json, this);
	}
}