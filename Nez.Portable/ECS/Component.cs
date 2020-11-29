using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;


namespace Nez
{
	/// <summary>
	/// Execution order:
	/// - OnAddedToEntity
	/// - OnEnabled
	///
	/// Removal:
	/// - OnRemovedFromEntity
	///
	/// </summary>
	public class Component : IComparable<Component>
	{
		/// <summary>
		/// the Entity this Component is attached to
		/// </summary>
		public Entity Entity;

		#region Entity's Transform

		/// <summary>
		/// shortcut to entity.transform
		/// </summary>
		/// <value>The transform.</value>
		public Transform Transform
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Entity.Transform;
		}

		#region Global

		[NotInspectable]
		public Vector2 Position
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Entity.Transform.Position;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => Entity.Transform.Position = value;
		}

		[NotInspectable]
		public Vector2 Scale
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Entity.Transform.Scale;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => Entity.Transform.Scale = value;
		}

		[NotInspectable]
		public float Rotation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Entity.Transform.Rotation;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => Entity.Transform.Rotation = value;
		}

		[NotInspectable]
		public float RotationDegrees
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Entity.Transform.RotationDegrees;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => Entity.Transform.RotationDegrees = value;
		}

		#endregion

		#region Local

		[NotInspectable]
		public Vector2 LocalPosition
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Entity.Transform.LocalPosition;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => Entity.Transform.LocalPosition = value;
		}

		[NotInspectable]
		public Vector2 LocalScale
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Entity.Transform.LocalScale;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => Entity.Transform.LocalScale = value;
		}

		[NotInspectable]
		public float LocalRotation
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Entity.Transform.LocalRotation;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => Entity.Transform.LocalRotation = value;
		}

		[NotInspectable]
		public float LocalRotationDegrees
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Entity.Transform.LocalRotationDegrees;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => Entity.Transform.LocalRotationDegrees = value;
		}

		#endregion

		/// <summary>
		/// Entity's parent accesor
		/// </summary>
		/// <value>The parent entity.</value>
		public Entity Parent
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => Entity.GetParent<Entity>();
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => Entity.SetParent(value);
		}

		#endregion

		/// <summary>
		/// Gets the entity's component of the given type
		/// </summary>
		/// <typeparam name="T">Component type</typeparam>
		/// <returns>The component</returns>
		public T GetComponent<T>() where T : Component
		{
			return Entity.GetComponent<T>();
		}

		/// <summary>
		/// Gets the first Component of type T and returns it. If no Component is found the Component will be created.
		/// </summary>
		/// <returns>The component.</returns>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public T GetOrCreateComponent<T>() where T : Component, new()
		{
			var comp = Entity.GetComponent<T>(true);
			if (comp == null)
				comp = Entity.AddComponent<T>();

			return comp;
		}

		/// <summary>
		/// Gets the component of the given type only if it's initialized
		/// </summary>
		/// <typeparam name="T">The component type</typeparam>
		/// <param name="onlyReturnInitializedComponents">Whether it should be initialized</param>
		/// <returns>The component.</returns>
		public T GetComponent<T>(bool onlyReturnInitializedComponents) where T : Component
		{
			return GetComponent<T>(onlyReturnInitializedComponents);
		}

		/// <summary>
		/// Creates a list of components of the given type
		/// </summary>
		/// <typeparam name="T">The components type</typeparam>
		/// <returns>A list of components</returns>
		public List<T> GetComponents<T>() where T : Component
		{
			return Entity.GetComponents<T>();
		}

		/// <summary>
		/// Fills the given list with the components of the given type
		/// </summary>
		/// <typeparam name="T">The components type</typeparam>
		/// <param name="componentsList">The list to be filled</param>
		public void GetComponents<T>(List<T> componentsList) where T : Component
		{
			Entity.GetComponents(componentsList);
		}

		/// <summary>
		/// Returns the parent
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetParent<T>() where T : Entity
		{
			return Entity.GetParent<T>();
		}

		/// <summary>
		/// true if the Component is enabled and the Entity is enabled. When enabled this Components lifecycle methods will be called.
		/// Changes in state result in onEnabled/onDisable being called.
		/// </summary>
		/// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
		public bool Enabled
		{
			get => Entity != null ? Entity.Enabled && _enabled : _enabled;
			set => SetEnabled(value);
		}

		/// <summary>
		/// update order of the Components on this Entity
		/// </summary>
		/// <value>The order.</value>
		public int UpdateOrder
		{
			get => _updateOrder;
			set => SetUpdateOrder(value);
		}

		bool _enabled = true;
		internal int _updateOrder = 0;


		#region Component Lifecycle

		/// <summary>
		/// called when this Component has had its Entity assigned but it is NOT yet added to the live Components list of the Entity yet. Useful
		/// for things like physics Components that need to access the Transform to modify collision body properties.
		/// </summary>
		public virtual void Initialize()
		{ }

		/// <summary>
		/// Called when this component is added to a scene after all pending component changes are committed. At this point, the Entity field
		/// is set and the Entity.Scene is also set.
		/// </summary>
		public virtual void OnAddedToEntity()
		{ }

		/// <summary>
		/// Called when this component is removed from its entity. Do all cleanup here.
		/// </summary>
		public virtual void OnRemovedFromEntity()
		{ }

		/// <summary>
		/// called when the entity's position changes. This allows components to be aware that they have moved due to the parent
		/// entity moving.
		/// </summary>
		public virtual void OnEntityTransformChanged(Transform.Component comp)
		{ }

		public virtual void DebugRender(Batcher batcher)
		{ }

		/// <summary>
		/// called when the parent Entity or this Component is enabled
		/// </summary>
		public virtual void OnEnabled()
		{ }

		/// <summary>
		/// called when the parent Entity or this Component is disabled
		/// </summary>
		public virtual void OnDisabled()
		{ }

		#endregion

		#region Fluent setters

		public Component SetEnabled(bool isEnabled)
		{
			if (_enabled != isEnabled)
			{
				_enabled = isEnabled;

				if (_enabled)
					OnEnabled();
				else
					OnDisabled();
			}

			return this;
		}

		public Component SetUpdateOrder(int updateOrder)
		{
			if (_updateOrder != updateOrder)
			{
				_updateOrder = updateOrder;
				if (Entity != null)
					Entity.Components.MarkEntityListUnsorted();
			}

			return this;
		}

		#endregion

		/// <summary>
		/// creates a clone of this Component. The default implementation is just a MemberwiseClone so if a Component has object references
		/// that need to be cloned this method should be overriden.
		/// </summary>
		public virtual Component Clone()
		{
			var component = MemberwiseClone() as Component;
			component.Entity = null;

			return component;
		}

		public int CompareTo(Component other) => _updateOrder.CompareTo(other._updateOrder);


		public override string ToString() => $"[Component: type: {GetType()}, updateOrder: {UpdateOrder}]";
	}
}