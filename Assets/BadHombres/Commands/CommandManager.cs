using System;
using System.Collections.Generic;
using UnityEngine;

namespace BadHombres.Commands
{

    public class CommandManager : MonoBehaviour
    {

        public static CommandManager Instance;

        private static readonly Dictionary<string, Type> CommandTypeByString = new Dictionary<string, Type>();

        /// <summary>
        /// (ᵔᴥᵔ)
        /// Mr Seal says: Call me to execute a command!
        /// </summary>
        /// <param name="commandType">The class to be used, must inherit from Command</param>
        /// <param name="data">Data to be passed to the command</param>
        public void ExcuteCommand(Type commandType, object data)
        {

            if (Instance != null && commandType.IsSubclassOf(typeof(Command)))
            {
                var command = Instance.gameObject.AddComponent(commandType) as Command;

                if (command == null) return;

                command.Execute(data);
                Debug.LogFormat("Excuted command of type {0} with data: {1}", commandType.Name, data);
            }

        }


        /// <summary>
        /// Execute a registered command
        /// </summary>
        /// <param name="id">string used to registered the command</param>
        /// <param name="data">Data to be passed to the command</param>
        public void ExecuteCommand(string id, object data)
        {

            if (CommandTypeByString.ContainsKey(id))
                ExcuteCommand(CommandTypeByString[id], data);

        }

        /// <summary>
        /// (ᵔᴥᵔ)
        /// Mr Seal says: Call me to execute a command without data!
        /// </summary>
        /// <param name="commandType">The class to be used, must inherit from Command</param>
        public void ExcuteCommand(Type commandType)
        {

            ExcuteCommand(commandType, null);

        }


        /// <summary>
        /// Execute a registered command
        /// </summary>
        /// <param name="id">string used to register the command</param>
        public void ExecuteCommand(string id)
        {

            if (CommandTypeByString.ContainsKey(id))
                ExcuteCommand(CommandTypeByString[id]);

        }


        /// <summary>
        /// Register a command
        /// </summary>
        /// <param name="id">string: Unique id</param>
        /// <param name="commandType">command type</param>
        public void RegisterCommand(string id, Type commandType)
        {

            if (CommandTypeByString.ContainsKey(id))
                CommandTypeByString[id] = commandType;
            else
                CommandTypeByString.Add(id, commandType);

        }


        /// <summary>
        /// Remove a command
        /// </summary>
        /// <param name="id">unique id used to register the command (string)</param>
        public void RemoveCommand(string id)
        {
            if (CommandTypeByString.ContainsKey(id))
                CommandTypeByString.Remove(id);
        }


        /// <summary>
        /// Removes a registered command.
        /// </summary>
        /// <param name="commandType">command type</param>
        public void RemoveCommand(Type commandType)
        {
            if (CommandTypeByString.ContainsValue(commandType))
            {
                foreach (KeyValuePair<string, Type> kvp in CommandTypeByString)
                {
                    if (kvp.Value == commandType) CommandTypeByString.Remove(kvp.Key);
                }
            }

        }


        // ʕ´• ᴥ •`ʔ 
        // Mr Bear is awake!
        void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        // cleanup!
        void OnDestroy()
        {
            CommandTypeByString.Clear();
        }
    }
}
