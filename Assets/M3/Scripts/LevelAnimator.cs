using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m3
{
	public class LevelAnimator : MonoBehaviour, ILevelAnimator
	{
		[SerializeField]
		private int animationsCount = 0;
		public int AnimationsCount
		{
			get
			{
				return animationsCount;
			}
			set
			{
				if (animationsCount == 0 && value > 0)
				{
					OnAnimationsStarted.Invoke();
				}

				animationsCount = value;

				if (animationsCount == 0)
					OnAnimationsFinished.Invoke();
			}
		}

		public event Action OnAnimationsFinished = delegate { };
		public event Action OnAnimationsStarted = delegate { };

		public void MovePositionTo(RectTransform transform, Vector2 position, float moveSpeed)
		{
			AnimationsCount++;
			StartCoroutine(MoveAnchoredPositionToCoroutine(transform, position, moveSpeed));
		}

		private IEnumerator MoveAnchoredPositionToCoroutine(RectTransform transform, Vector2 position, float moveSpeed)
		{
			while (transform != null && transform.anchoredPosition != position)
			{
				transform.anchoredPosition = Vector2.MoveTowards(transform.anchoredPosition, position, Time.deltaTime * moveSpeed);
				yield return new WaitForEndOfFrame();
			}
			AnimationsCount--;
		}

		public void ScaleTo(RectTransform transform, Vector3 scale, float scaleSpeed)
		{
			AnimationsCount++;
			StartCoroutine(ScaleToCoroutine(transform, scale, scaleSpeed));
		}

		private IEnumerator ScaleToCoroutine(Transform transform, Vector3 scale, float scaleSpeed)
		{
			while (transform != null && transform.localScale != scale)
			{
				transform.localScale = Vector2.MoveTowards(transform.localScale, scale, Time.deltaTime * scaleSpeed);
				yield return new WaitForEndOfFrame();
			}
			AnimationsCount--;
		}
	}

}
