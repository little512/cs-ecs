using System;

namespace ECS {
    [System.Serializable]
    public class ComponentException : System.Exception
    {
        public ComponentException() { }
        public ComponentException(string message) : base(message) { }
        public ComponentException(string message, System.Exception inner) : base(message, inner) { }
        protected ComponentException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}