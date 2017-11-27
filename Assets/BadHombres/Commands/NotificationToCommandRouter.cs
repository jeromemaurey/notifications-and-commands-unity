using System;
using System.Collections.Generic;
using BadHombres.Notifications;
using UnityEngine;

namespace BadHombres.Commands
{
    public class NotificationToCommandRouter : MonoBehaviour
    {

        public static NotificationToCommandRouter Instance;

        private static readonly List<string> Notifications = new List<string>();


        #region MonoBehaviour Methods


        // ʕ´• ᴥ •`ʔ 
        // Mr Bear is awake!
        void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        void Start()
        {
            if (NotificationManager.Instance != null) NotificationManager.Instance.RegisterBroadcastInterest(HandleNotifications);
        }

        // cleanup
        void OnDestroy()
        {

            if (NotificationManager.Instance != null) NotificationManager.Instance.RemoveBroadcastInterest(HandleNotifications);

            if (CommandManager.Instance != null)
            {
                foreach (var notification in Notifications)
                {
                    CommandManager.Instance.RemoveCommand(notification);
                }
            }

            Notifications.Clear();
        }

        #endregion


        private void HandleNotifications(Notification note)
        {
            if (Notifications.Contains(note.notificationType)) CommandManager.Instance.ExecuteCommand(note.notificationType, note.payload);
        }

        public void MapNotificationToCommand(string notificationType, Type commandType)
        {
            if (Notifications.Contains(notificationType)) return;

            Notifications.Add(notificationType);
            CommandManager.Instance.RegisterCommand(notificationType, commandType);
        }
    }
}
