Notifications & Commands
========================


A set of classes to create a simple notification system in Unity. It is intended to help decouple actors and reduce interdependence (aka spaghetti code.)

Loosely based on PureMVC's notifications and commands, the main focus of this implementation was reducing overhead and minimal boilerplate code.

Notifications and Commands can be used together or separately.

----------


What's the difference between a Notification and Command?
---------------------------------------------------------

A Notification is an event being dispatched to the registered actors for handling.

A Command is meant to encapsulate code, like complex actions, into a single actor. Command inherits from MonoBehaviour and can include things like Coroutine. They are a good place to create dependencies when needed (like getting information from various actors.)  

----------

Main Actors
-----------

 - **NotificationManager**
  Main event bus used to register for or send notifications.
 - **Notification**
 Payload sent with each notification.
 - **CommandManager**
 Used to execute commands.
 - **Command**
 Base class to be passed to the CommandManager for execution.
 - **NotificationToCommandRouter**
 Used to map a Notification to a Command. 