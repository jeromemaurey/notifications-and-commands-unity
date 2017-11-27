using System.Collections;
using System.Collections.Generic;
using BadHombres.Notifications;
using UnityEngine;

public class DispatchSpawnOnClick : MonoBehaviour
{

    public string NotificationName = "SpawnObject";

    public void Dispatch()
    {

        if (NotificationManager.Instance != null) NotificationManager.Instance.DispatchNotification(NotificationName);

    }
}
