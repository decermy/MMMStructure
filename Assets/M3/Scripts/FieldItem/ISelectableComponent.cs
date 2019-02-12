
using UnityEngine.EventSystems;

namespace m3
{
	public interface ISelectableComponent : IFieldItemComponent, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler, IDropHandler
	{
		IFieldItem fieldItem { get; set; }

		void Select();
		void UnSelect();
	}
}
