using UnityEngine;
using System.Collections;
using BadHombres.Commands;
using BadHombres.Notifications;

public class UpdateCameraTargetAndSpawnObjectCommand : Command
{
    public override void Execute(object data = null)
    {

        // get all targets in the scene
        var targets = GameObject.FindGameObjectsWithTag("CameraTarget");

        // if there are targets pick one at random
        if(targets.Length > 0)
            Camera.main.transform.LookAt(targets[Random.Range(0, targets.Length)].transform);

        // dispatch spawn notification
        NotificationManager.Instance.DispatchNotification("SpawnObject");

        base.Remove();
    }

}
