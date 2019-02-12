
using UnityEngine.EventSystems;

namespace m3
{
	public interface IInputController
	{
		ISelectableComponent LastItem { get; }
		ISelectableComponent DragItem { get; }

		void ItemPointerClick(ISelectableComponent obj, PointerEventData eventData);
		void ItemDragEnd(ISelectableComponent obj, PointerEventData eventData);
		void ItemDragBegin(ISelectableComponent obj, PointerEventData eventData);
		void ItemDrag(ISelectableComponent obj, PointerEventData eventData);
		void ItemDrop(ISelectableComponent obj, PointerEventData eventData);
	}
}

