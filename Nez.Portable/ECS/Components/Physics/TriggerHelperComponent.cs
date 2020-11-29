namespace Nez
{
	/// <summary>
	/// A simple component that will automatically update a collider trigger helper.
	/// </summary>
	public class TriggerHelperComponent : Component, IUpdatable
	{
		ColliderTriggerHelper _helper;

		public override void OnAddedToEntity()
		{
			_helper = new ColliderTriggerHelper(Entity);
		}

		public override void OnRemovedFromEntity()
		{
			_helper = null;
		}

		public void Update()
		{
			_helper.Update();
		}
	}
}
