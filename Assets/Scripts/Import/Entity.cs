using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Base class for all interactive entities
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {
        /// <summary>
        ///
        /// </summary>
        [SerializeField]
        protected string _interactiveName;

        /// <summary>
        /// Gets name of an interactive entity
        /// </summary>
        public string InteractiveName => _interactiveName;
    }
}
