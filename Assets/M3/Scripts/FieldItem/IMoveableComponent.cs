
using UnityEngine;

namespace m3
{
	public interface IMoveableComponent: IFieldItemComponent
	{
		void Move(Vector2 destination);
	}
}