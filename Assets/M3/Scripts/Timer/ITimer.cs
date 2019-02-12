using System;

namespace m3
{
	public interface ITimer
	{

		void SetTimer(float time, Action callback);
	}

}
