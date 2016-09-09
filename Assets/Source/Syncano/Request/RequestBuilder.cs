﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Syncano.Client;
using Syncano.Data;

namespace Syncano.Request {
	
	/// <summary>
	/// This class is a gate for making call to syncano between client and the library.
	/// </summary>
	public class RequestBuilder {

		/// <summary>
		/// Sends a Get request for an object with specified id. Takes two callbacks, one when request is successful which is mandatory and second on failure which is optional for error handling.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="onSuccess">On success.</param>
		/// <param name="onFailure">On failure.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public Coroutine Get<T>(long id, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T : SyncanoObject, new() {
			return SyncanoHttpClient.Instance.PostAsync<T>(id, onSuccess, onFailure, UnityEngine.Networking.UnityWebRequest.kHttpVerbGET);
		}

		/// <summary>
		/// Get the specified id, onSuccess and onFailure.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="onSuccess">On success.</param>
		/// <param name="onFailure">On failure.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public Coroutine Get<T>(Action<ResponseGetList<T>> onSuccess, Action<ResponseGetList<T>> onFailure = null) where T : SyncanoObject, new() {
			return SyncanoHttpClient.Instance.PostAsync<T>(onSuccess, onFailure, UnityEngine.Networking.UnityWebRequest.kHttpVerbGET);
		}

		/// <summary>
		/// Get the specified getData, onSuccess and onFailure.
		/// </summary>
		/// <param name="getData">Get data.</param>
		/// <param name="onSuccess">On success.</param>
		/// <param name="onFailure">On failure.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public Coroutine Get<T>(string channelName, Dictionary<string, string> getData, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T : SyncanoObject, new() {
			return SyncanoHttpClient.Instance.GetAsync<T>(channelName, getData, onSuccess, onFailure);
		}

		/// <summary>
		/// Sends a POST request to Syncano with parameters.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="onSuccess">On success.</param>
		/// <param name="onFailure">On failure.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public Coroutine POST<T>(Dictionary<string, string> postData, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T : SyncanoObject, new() {
			return SyncanoHttpClient.Instance.PostAsync<T>(postData, onSuccess, onFailure, UnityEngine.Networking.UnityWebRequest.kHttpVerbPOST);
		}

		/// <summary>
		/// Creates the channel.
		/// </summary>
		/// <returns>The channel.</returns>
		/// <param name="channel">Channel.</param>
		/// <param name="onSuccess">On success.</param>
		/// <param name="onFailure">On failure.</param>
		public Coroutine CreateChannel(Channel channel, Action<Response<Channel>> onSuccess, Action<Response<Channel>> onFailure = null) {
			return SyncanoHttpClient.Instance.PostAsync<Channel>(channel, onSuccess, onFailure, UnityEngine.Networking.UnityWebRequest.kHttpVerbPOST);
		}

		/// <summary>
		/// Save the specified obj, onSuccess and onFailure. Sends a Get request for an object with specified id. Takes two callbacks, one when request is successful which is mandatory and second on failure which is optional for error handling.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="onSuccess">On success.</param>
		/// <param name="onFailure">On failure.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public Coroutine Save<T>(T obj, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T : SyncanoObject, new()  {
			return SyncanoHttpClient.Instance.PostAsync<T>(obj, onSuccess, onFailure);
		}

		/// <summary>
		/// Deletes the specified obj, onSuccess and onFailure. Sends a Get request for an object with specified id. Takes two callbacks, one when request is successful which is mandatory and second on failure which is optional for error handling.
		/// </summary>
		/// <param name="obj">Object.</param>
		/// <param name="onSuccess">On success.</param>
		/// <param name="onFailure">On failure.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public Coroutine Delete<T>(T obj, Action<Response<T>> onSuccess, Action<Response<T>> onFailure = null) where T : SyncanoObject, new()  {
			return SyncanoHttpClient.Instance.PostAsync<T>(obj, onSuccess, onFailure, UnityEngine.Networking.UnityWebRequest.kHttpVerbDELETE);
		}

		/// <summary>
		/// Calls the script endpoint.
		/// </summary>
		/// <returns>The script endpoint.</returns>
		/// <param name="endpointId">Endpoint identifier.</param>
		/// <param name="scriptName">Script name.</param>
		/// <param name="callback">Callback.</param>
		public Coroutine CallScriptEndpoint(string endpointId, string scriptName, System.Action<ScriptEndpoint> callback) {
			return SyncanoHttpClient.Instance.CallScriptEndpoint(endpointId, scriptName, callback);
		}
	}
}