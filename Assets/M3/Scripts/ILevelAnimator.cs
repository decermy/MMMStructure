using System;
using UnityEngine;

namespace m3
{
	public interface ILevelAnimator

	{
		int AnimationsCount { get; set; }

		event Action OnAnimationsFinished;
		event Action OnAnimationsStarted;

		void MovePositionTo(RectTransform transform, Vector2 position, float moveSpeed);

		void ScaleTo(RectTransform transform, Vector3 scale, float scaleSpeed);
	}

}
