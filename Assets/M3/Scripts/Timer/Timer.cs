using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace m3
{
	public class Timer : MonoBehaviour, ITimer
	{
		public void SetTimer(float time, Action callback)
		{
			StartCoroutine(MyTimer(time, callback));
		}

		IEnumerator MyTimer(float time, Action callback)
		{
			yield return new WaitForSeconds(time);
			callback.Invoke();
		}
	}
}
