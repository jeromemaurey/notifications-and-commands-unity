using UnityEngine;

namespace BadHombres.Commands
{

    /// <summary>
    /// Base MonoBehaviours to create commands. 
    /// A command is meant to encapsulate complex actions into a single actor. 
    /// It inherits from MonoBehaviour to allow access to its convenience methods, like Coroutines.
    /// Note: The CommandManager will call execute automatically.
    /// </summary>
    public class Command : MonoBehaviour
    {

        /// <summary>
        /// Method to override to execute your own code. Make sure to call Remove() once done!
        /// </summary>
        /// <param name="data">Optional payload to be passed to the Command for handling</param>
        public virtual void Execute(object data = null)
        {
            Remove();
        }

        /// <summary>
        /// Make sure to cleanup before calling this.
        /// </summary>
        protected virtual void Remove()
        {
            Destroy(this);
        }

    }


}
