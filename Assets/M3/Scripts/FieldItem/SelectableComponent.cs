using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

namespace m3
{
	public partial class SelectableComponent : MonoBehaviour, ISelectableComponent
	{
		public static event Action<ISelectableComponent, PointerEventData> OnFieldItemPointerClick = delegate { };
		public static event Action<ISelectableComponent, PointerEventData> OnFieldItemBeginDrag = delegate { };
		public static event Action<ISelectableComponent, PointerEventData> OnFieldItemDrag = delegate { };
		public static event Action<ISelectableComponent, PointerEventData> OnFieldItemEndDrag = delegate { };
		public static event Action<ISelectableComponent, PointerEventData> OnFieldItemDrop = delegate { };

		public IFieldItem fieldItem { get; set; }

		public void Init(IFieldItem fieldItem)
		{
			this.fieldItem = fieldItem;
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			OnFieldItemPointerClick.Invoke(this, eventData);
		}
		public void OnBeginDrag(PointerEventData eventData)
		{
			OnFieldItemBeginDrag.Invoke(this, eventData);
		}

		public void OnDrag(PointerEventData eventData)
		{
			OnFieldItemDrag.Invoke(this, eventData);
		}

		public void OnEndDrag(PointerEventData eventData)
		{
			OnFieldItemEndDrag.Invoke(this, eventData);
		}

		public void Select()
		{
			(fieldItem as MonoBehaviour).transform.localScale = Vector3.one * 0.85f;
		}

		public void UnSelect()
		{
			(fieldItem as MonoBehaviour).transform.localScale = Vector3.one * 1;
		}

		public void OnDrop(PointerEventData eventData)
		{
			OnFieldItemDrop.Invoke(this, eventData);
		}
	}
}

