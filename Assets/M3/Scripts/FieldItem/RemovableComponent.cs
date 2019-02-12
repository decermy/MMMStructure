using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m3
{
	public class RemovableComponent : IRemovableComponent
	{
		private IFieldItem fieldItem;

		private float scaleSpeed = 5f;

		public RemovableComponent(IFieldItem fieldItem)
		{
			this.fieldItem = fieldItem;
		}

		public void Remove()
		{
			if ((fieldItem as FieldItem).LevelAnimator == null)
			{
				return;
			}
			(fieldItem as FieldItem).LevelAnimator.ScaleTo((fieldItem as MonoBehaviour).transform as RectTransform, Vector3.zero, scaleSpeed);
		}
	}
}

