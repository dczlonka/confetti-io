using UnityEngine;
using System.Collections;
using Syncano.Request;

namespace Syncano.Data {
	
	/// <summary>
	/// Wrapper class for getting response from Syncano.
	/// </summary>
	public class Response<T> : SyncanoWebRequest where T : SyncanoObject, new() {
	
	/// <summary>
	/// Deserialized data.
	/// </summary>
	public T Data { set; get; }

		public virtual void SetData(string json)
		{
			Data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
		}
}
}