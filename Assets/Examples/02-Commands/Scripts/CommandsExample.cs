using System.Collections;
using System.Collections.Generic;
using BadHombres.Commands;
using BadHombres.Notifications;
using UnityEngine;

public class CommandsExample : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(NotificationManager.Instance != null) NotificationManager.Instance.RegisterNotificationInterest("UpdateCameraTarget", HandleUpdateCameraTarget);
	}

    private void HandleUpdateCameraTarget(Notification note)
    {
        if( CommandManager.Instance != null) CommandManager.Instance.ExcuteCommand(typeof(UpdateCameraTargetAndSpawnObjectCommand));
    }

    // Update is called once per frame
	//void Update () {
		
	//}
}
