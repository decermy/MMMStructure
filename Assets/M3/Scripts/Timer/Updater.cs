using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace m3
{
	public class Updater : MonoBehaviour, IUpdater
	{
		private float updateTime = 0.1f;
		public event Action TickTac = delegate { };

		float IUpdater.UpdateTime
		{
			get
			{
				return updateTime;
			}

			set
			{
				updateTime = value;
			}
		}

		public void Init()
		{
			StartCoroutine(UpdateCorutine());
		}

		IEnumerator UpdateCorutine()
		{
			while (true)
			{
				yield return new WaitForSeconds(updateTime);
				TickTac.Invoke();
			}
		}
	}

}