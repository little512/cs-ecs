using System;

namespace ECS {

    /// <summary>Component interface which describes the functions a component
    ///     should implement.</summary>
    public interface IComponent {

        /// <summary>Gets the type of the component value.</summary>
        Type GetComponentType();

        /// <summary>The name of the component, which can be used with <see cref="IEntity.GetComponentByName{T}(string)"/>
        /// or other similar functions.</summary>
        string Name { get; set; }
    }

    /// <summary>Component class where type-safe objects may be stored and
    ///     retrieved.</summary>
    public class Component<T> : IComponent {

        /// <summary>The value of the component, or the object which the component holds.</summary>
        public T Value { get; set; }

        public string Name { get; set; }

        /// <summary>Constructs a new <c>Component</c> object.</summary>
        /// <param name="value">The value to initialize the component with.</param>
        /// <param name="name">The name to initialize the component with.</param>
        public Component(T value = default, string name = default) {
            Value = value;
            Name = name;
        }

        public Type GetComponentType() => typeof(T);
    }
}
