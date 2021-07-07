using System;

using Xunit;

namespace ECS.Tests {
    public class EntityTests {

        [Fact]
        public void ECS_EntityTests_ShouldGetComponent() {
            int data = 1;
            Entity entity = new();

            entity.AddComponent(data);

            var component = entity.GetComponent<int>();

            Assert.Equal(data, component.Value);
        }

        [Fact]
        public void ECS_EntityTests_ShouldThrowComponentException() {
            int data = 1;
            Entity entity = new();

            entity.AddComponent(data);

            Assert.Throws<ComponentException>(() => entity.GetComponent<string>());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(100)]
        [InlineData(-1)]
        public void ECS_EntityTests_GetComponent(int data) {
            Entity entity = new();

            entity.AddComponent(data);

            var component = entity.GetComponent<int>();

            Assert.Equal(data, component.Value);
        }

        [Theory]
        [InlineData(1, "Value")]
        [InlineData(100, "Price")]
        [InlineData(-1, "Health")]
        [InlineData(10_000, "First Checkpoint")]
        public void ECS_EntityTests_GetComponentByName(int data, string name) {
            Entity entity = new();

            entity.AddComponent(data, name);

            var component = entity.GetComponentByName<int>(name);

            Assert.Equal(data, component.Value);
            Assert.Equal(name, component.Name);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        public void ECS_EntityTests_GetComponentOrDefault(int data) {
            Entity entity = new();

            if (data != 0)
                entity.AddComponent(data);

            var component = entity.GetComponentOrDefault<int>();

            if (data != 0)
                Assert.Equal(data, component.Value);
            else
                Assert.Equal(default, component.Value);
        }

        [Theory]
        [InlineData(1, "Component")]
        [InlineData(0, "Component")]
        public void ECS_EntityTests_GetComponentByNameOrDefault(int data, string name) {
            Entity entity = new();

            if (data != 0)
                entity.AddComponent(data, name);

            var component = entity.GetComponentByNameOrDefault<int>(name);

            if (data != 0) {
                Assert.Equal(data, component.Value);
                Assert.Equal(name, component.Name);
            } else {
                Assert.Equal(default, component.Value);
                Assert.Equal(name, component.Name);
            }
        }

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(100, 5, 10)]
        [InlineData(-1, 0, 0)]
        public void ECS_EntityTests_GetComponents(int data1, int data2, int data3) {
            Entity entity = new();

            entity.AddComponent(data1);
            entity.AddComponent(data2);
            entity.AddComponent(data3);

            var components = entity.GetComponents<int>();
            var expectedComponents = new Component<int>[] {
                new Component<int>(data1),
                new Component<int>(data2),
                new Component<int>(data3)
            };

            Assert.Equal(expectedComponents.Length, components.Length);
            Assert.Collection(components,
                c => Assert.Equal(data1, c.Value),
                c => Assert.Equal(data2, c.Value),
                c => Assert.Equal(data3, c.Value)
            );
        }

        [Theory]
        [InlineData(1, 2, 3, "Value")]
        [InlineData(100, 5, 10, "Price")]
        [InlineData(-1, 0, 0, "Vertex")]
        public void ECS_EntityTests_GetComponentsByName(int data1, int data2, int data3, string name) {
            Entity entity = new();

            entity.AddComponent(data1, name);
            entity.AddComponent(data2, name);
            entity.AddComponent(data3, name);

            var components = entity.GetComponentsByName<int>(name);
            var expectedComponents = new Component<int>[] {
                new Component<int>(data1, name),
                new Component<int>(data2, name),
                new Component<int>(data3, name)
            };

            Assert.Equal(expectedComponents.Length, components.Length);

            Assert.Collection(components,
                c => Assert.Equal(data1, c.Value),
                c => Assert.Equal(data2, c.Value),
                c => Assert.Equal(data3, c.Value)
            );

            Assert.Collection(components,
                c => Assert.Equal(name, c.Name),
                c => Assert.Equal(name, c.Name),
                c => Assert.Equal(name, c.Name)
            );
        }
    }
}
