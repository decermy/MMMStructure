using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m3
{
	public class FieldItem : MonoBehaviour, IFieldItem
	{
		public int Row { get; set; }
		public int Column { get; set; }

		public void SetIndex(int i, int j)
		{
			this.Row = j;
			this.Column = i;
		}

		public ILevelAnimator LevelAnimator { get; set; }
		private IFieldItemComponent[] Components;

		public void Init(ILevelAnimator LevelAnimator, int i, int j)
		{
			this.LevelAnimator = LevelAnimator;
			SetIndex(i, j);

			InitComponents();
		}

		private void InitComponents()
		{
			SelectableComponent selectableComponent = gameObject.AddComponent<SelectableComponent>();
			selectableComponent.Init(this);

			Components = new IFieldItemComponent[] {
				selectableComponent as ISelectableComponent,
				new RemovableComponent(this) as IRemovableComponent,
				new MoveableComponent(this) as IMoveableComponent
			};
		}

		public T GetItemFieldComp<T>() where T : IFieldItemComponent
		{
			if (Components == null)
			{
				return default(T);
			}

			for (int i = 0; i < Components.Length; i++)
			{
				if (Components[i].GetType() == typeof(T))
				{
					return (T)Components[i];
				}
			}

			return default(T);
		}
	}

}
