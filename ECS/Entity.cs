using System;
using System.Linq;
using System.Collections.Generic;

namespace ECS {

    /// <summary>Entity interface which describes the functions an entity
    ///     should implement.</summary>
    public interface IEntity {

        /// <summary>Gets a component of type <c>T</c> from the entity.</summary>
        /// <exception cref="ComponentException">If a component of type <c>T</c>
        ///     is not found, <c>GetComponent</c> will throw an exception.</exception>
        Component<T> GetComponent<T>();

        /// <summary>Gets a component of name <paramref name="name">name</paramref> from the entity.</summary>
        /// <exception cref="ComponentException">If a component of name <paramref name="name">name</paramref>
        ///     is not found, <c>GetComponent</c> will throw an exception.</exception>
        /// <param name="name">The name of the component to get.</param>
        Component<T> GetComponentByName<T>(string name);

        /// <summary>Gets a component of type <c>T</c> from the entity. If one is not
        /// found, it will return a <c>Component</c> with a default value.</summary>
        Component<T> GetComponentOrDefault<T>();

        /// <summary>Gets a component of name <paramref name="name">name</paramref> from the entity.
        /// If one is not found, it will return a <c>Component</c> with a default value.</summary>
        /// <param name="name">The name of the component to get.</param>
        Component<T> GetComponentByNameOrDefault<T>(string name);

        /// <summary>Gets one or more component of type <c>T</c> from the entity.</summary>
        /// <exception cref="ComponentException">If a component of type <c>T</c>
        ///     is not found, <c>GetComponent</c> will throw an exception.</exception>
        Component<T>[] GetComponents<T>();

        /// <summary>Gets one or more component of name <paramref name="name">name</c> from the entity.</summary>
        /// <param name="name">The name of the components to get.</param>
        Component<T>[] GetComponentsByName<T>(string name);

        /// <summary>Adds a component of type <c>T</c> to the entity with a value 
        ///     of <paramref name="componentValue">componentValue</paramref>.</summary>
        /// <param name="componentValue">The value to initialize the component with.</param>
        void AddComponent<T>(T componentValue, string name = default);

        /// <summary>Adds a component of type <c>T</c> to the entity.</summary>
        /// <param name="component">The component to add.</param>
        void AddComponent(IComponent component);
    }

    /// <summary>Entity class where components may be stored and retrieved.</summary>
    public class Entity : IEntity {

        /// <summary>The list of <c>IComponent</c> objects in the entity.</summary>
        private readonly List<IComponent> components;

        /// <summary>Constructs a new <c>Entity</c> object.</summary>
        public Entity() {
            components = new();
        }

        public Component<T> GetComponent<T>() {
            var comp = components.Where(c => c as Component<T> is not null);

            return comp.Any() ?
                comp.First() as Component<T> :
                throw new ComponentException($"No component of type {typeof(T)} in entity.");
        }

        public Component<T> GetComponentByName<T>(string name) {
            var comp = components.Where(c => c as Component<T> is not null &&
                c.Name == name);

            return comp.Any() ?
                comp.First() as Component<T> :
                throw new ComponentException($"No component of type {typeof(T)} in entity.");
        }

        public Component<T> GetComponentOrDefault<T>() {
            var comp = components.Where(c => c as Component<T> is not null);

            return comp.Any() ?
                comp.First() as Component<T> :
                new Component<T>(); // default
        }

        public Component<T> GetComponentByNameOrDefault<T>(string name) {
            var comp = components.Where(c => c as Component<T> is not null &&
                c.Name == name);

            return comp.Any() ?
                comp.First() as Component<T> :
                new Component<T>(default, name); // default
        }

        public Component<T>[] GetComponents<T>() {
            var comp = components.Where(c => c as Component<T> is not null);

            return comp.Select(c => c as Component<T>).ToArray() ??
                throw new ComponentException($"No components of type {typeof(T)} in entity.");
        }

        public Component<T>[] GetComponentsByName<T>(string name) {
            var comp = components.Where(c => c as Component<T> is not null &&
                c.Name == name);

            return comp.Select(c => c as Component<T>).ToArray() ??
                throw new ComponentException($"No components of type {typeof(T)} in entity.");
        }

        public void AddComponent<T>(T componentValue, string name = null) =>
            components.Add(new Component<T>(componentValue, name));

        public void AddComponent(IComponent component) =>
            components.Add(component);
    }
}
