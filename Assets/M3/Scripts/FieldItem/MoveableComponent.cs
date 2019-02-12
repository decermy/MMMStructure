using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m3
{
	public class MoveableComponent : IMoveableComponent
	{
		private float moveSpeed = 500f;

		private IFieldItem fieldItem;

		public MoveableComponent(IFieldItem fieldItem)
		{
			this.fieldItem = fieldItem;
		}

		public void Move(Vector2 destination)
		{
			(fieldItem as FieldItem).LevelAnimator.MovePositionTo((fieldItem as MonoBehaviour).transform as RectTransform, destination, moveSpeed);
		}
	}
}

