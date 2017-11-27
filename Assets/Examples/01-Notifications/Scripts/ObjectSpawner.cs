using System.Collections;
using System.Collections.Generic;
using BadHombres.Notifications;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    public GameObject Prefab;

	// Use this for initialization
	void Start () {
		if(NotificationManager.Instance != null) NotificationManager.Instance.RegisterNotificationInterest("SpawnObject", HandleSpawnNotification);
	}

    private void HandleSpawnNotification(Notification note)
    {
        Instantiate(Prefab);
    }

    // Update is called once per frame
	void Update () {
		
	}
}
