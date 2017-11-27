using System;
using System.Collections.Generic;
using UnityEngine;

namespace BadHombres.Notifications
{

    /// <summary>
    /// The NotificationManager is a singleton that behaves like an event bus. Actors can register for or dispatch a specific notification.
    /// Actors can register as broadcasters and receive all notifications dispatched.  
    /// </summary>
    public class NotificationManager : MonoBehaviour
    {

        #region Variables

        public static NotificationManager Instance;

        // Holds all registered handlers per notification types
        private readonly Dictionary<string, List<Action<Notification>>> _registeredHandlersByNoteType = new Dictionary<string, List<Action<Notification>>>();

        // hanlers that want to recieve all the notifications
        private readonly List<Action<Notification>> _registeredBroadcastHandlers = new List<Action<Notification>>();

        #endregion



        #region MonoBehaviour Methods

        // ʕ´• ᴥ •`ʔ 
        // Mr Bear is awake!
        void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        // cleans up!
        void OnDestroy()
        {
            _registeredBroadcastHandlers.Clear();
            _registeredBroadcastHandlers.Clear();
        }

        #endregion


        /// <summary>
        /// Register interest for a specific notification
        /// </summary>
        /// <param name="noteType">Notification type</param>
        /// <param name="handler">Function to handle notifications</param>
        public void RegisterNotificationInterest(string noteType, Action<Notification> handler)
        {

            if (_registeredHandlersByNoteType.ContainsKey(noteType))
            {

                // the note type exist check if we already registered this handler or add it;
                if (!_registeredHandlersByNoteType[noteType].Contains(handler))
                    _registeredHandlersByNoteType[noteType].Add(handler);

            }
            else
            {
                // the note type doesn't exist: add it and add handler
                _registeredHandlersByNoteType.Add(noteType, new List<Action<Notification>>());
                _registeredHandlersByNoteType[noteType].Add(handler);
            }

        }


        /// <summary>
        /// Register interest for multiple notification types
        /// </summary>
        /// <param name="noteTypes">Array of notification types</param>
        /// <param name="handler">Function to handle notifications</param>
        public void RegisterNotificationInterest(string[] noteTypes, Action<Notification> handler)
        {
            foreach (string t in noteTypes)
                RegisterNotificationInterest(t, handler);
        }


        /// <summary>
        /// Removes notification interest
        /// </summary>
        /// <param name="noteType">Notification type</param>
        /// <param name="handler">Handler function</param>
        public void RemoveNotificationInterest(string noteType, Action<Notification> handler)
        {

            if (_registeredHandlersByNoteType.ContainsKey(noteType) && _registeredHandlersByNoteType[noteType].Contains(handler))
            {
                _registeredHandlersByNoteType[noteType].Remove(handler);
                if (_registeredHandlersByNoteType[noteType].Count == 0) _registeredHandlersByNoteType.Remove(noteType);
            }
        }


        /// <summary>
        /// Removes notification interest for multiple notification types
        /// </summary>
        /// <param name="noteTypes">Array of notification types</param>
        /// <param name="handler">Handler function</param>
        public void RemoveNotificationInterest(string[] noteTypes, Action<Notification> handler)
        {
            foreach (string t in noteTypes)
                RemoveNotificationInterest(t, handler);
        }


        /// <summary>
        /// Register interest for broadcast: will receive ** all ** notifications
        /// </summary>
        /// <param name="handler">Handler function</param>
        public void RegisterBroadcastInterest(Action<Notification> handler)
        {

            if (!_registeredBroadcastHandlers.Contains(handler))
            {
                _registeredBroadcastHandlers.Add(handler);
            }

        }


        /// <summary>
        /// Remove broadcast interest
        /// </summary>
        /// <param name="handler">Handler function</param>
        public void RemoveBroadcastInterest(Action<Notification> handler)
        {

            if (_registeredBroadcastHandlers.Contains(handler))
                _registeredBroadcastHandlers.Remove(handler);
        }



        /// <summary>
        /// Dispatch notifications
        /// </summary>
        /// <param name="note">Notification</param>
        public void DispatchNotification(Notification note)
        {

            // dispatch by note type
            if (_registeredHandlersByNoteType.ContainsKey(note.notificationType))
            {
                var handler = _registeredHandlersByNoteType[note.notificationType];

                foreach (Action<Notification> t in handler)
                    t.Invoke(note);
            }


            // broadcast
            foreach (Action<Notification> t in _registeredBroadcastHandlers)
                t.Invoke(note);
        }


        /// <summary>
        /// Dispatch notification
        /// </summary>
        /// <param name="noteType">Short</param>
        public void DispatchNotification(string noteType)
        {
            DispatchNotification(new Notification(noteType));
        }


        /// <summary>
        /// Dispatch notification
        /// </summary>
        /// <param name="noteType">Notification type</param>
        /// <param name="payload">Data</param>
        public void DispatchNotification(string noteType, object payload)
        {
            DispatchNotification(new Notification(noteType, payload));
        }



    }


    [Serializable]
    public class Notification
    {

        public string uid;
        public string notificationType;
        public object payload;

        public Notification(string noteType, object notePayload)
        {
            uid = Guid.NewGuid().ToString();
            notificationType = noteType;
            payload = notePayload;
        }


        public Notification(string noteType)
        {
            uid = Guid.NewGuid().ToString();
            notificationType = noteType;
        }

    }


}
