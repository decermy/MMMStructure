using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace m3
{
	public class InputController : IInputController
	{
		private ISelectableComponent lastItem;
		public ISelectableComponent LastItem
		{
			get
			{
				return lastItem;
			}
		}

		private ISelectableComponent dragItem;
		public ISelectableComponent DragItem
		{
			get
			{
				return lastItem;
			}
		}

		private ILevelCommandsManager levelCommandsManager;

		private LevelController levelController;

		private Vector3 startPoint;

		public InputController(ILevelCommandsManager levelCommandsManager, LevelController levelController)
		{
			this.levelCommandsManager = levelCommandsManager;
			this.levelController = levelController;

			SelectableComponent.OnFieldItemPointerClick += ItemPointerClick;
			SelectableComponent.OnFieldItemBeginDrag += ItemDragBegin;
			SelectableComponent.OnFieldItemDrag += ItemDrag;
			SelectableComponent.OnFieldItemEndDrag += ItemDragEnd;
			SelectableComponent.OnFieldItemDrop += ItemDrop;
		}

		public void ItemDrag(ISelectableComponent obj, PointerEventData eventData)
		{
			if (levelController.LevelState == LevelState.inAction)
			{
				return;
			}

			if (IsHaveMoveableComponent(obj))
			{
				(obj.fieldItem as MonoBehaviour).transform.position = eventData.position;
			}
		}

		public void ItemDragBegin(ISelectableComponent obj, PointerEventData eventData)
		{
			if (levelController.LevelState == LevelState.inAction)
			{
				return;
			}

			if (IsHaveMoveableComponent(obj))
			{
				(obj.fieldItem as MonoBehaviour).transform.SetSiblingIndex(0);

				dragItem = obj;
				startPoint = (obj.fieldItem as MonoBehaviour).transform.position;

				if (lastItem != null)
				{
					lastItem.UnSelect();
				}

				levelController.LevelState = LevelState.selected;
			}
		}

		public void ItemDragEnd(ISelectableComponent obj, PointerEventData eventData)
		{
			if (levelController.LevelState == LevelState.inAction)
			{
				return;
			}

			if (IsHaveMoveableComponent(obj))
			{
				(obj.fieldItem as MonoBehaviour).transform.position = startPoint;

				lastItem = null;
				dragItem = null;
				levelController.LevelState = LevelState.idle;
			}
		}
		public void ItemDrop(ISelectableComponent obj, PointerEventData eventData)
		{
			if (levelController.LevelState == LevelState.inAction)
			{
				return;
			}

			if (IsHaveMoveableComponent(obj))
			{
				int rowDelta = Mathf.Abs(dragItem.fieldItem.Row - obj.fieldItem.Row);
				int coloumnDelta = Mathf.Abs(dragItem.fieldItem.Column - obj.fieldItem.Column);

				if ((rowDelta == 1 && coloumnDelta == 0) || (rowDelta == 0 && coloumnDelta == 1))
				{
					Swap(obj.fieldItem.Column, obj.fieldItem.Row, dragItem.fieldItem.Column, dragItem.fieldItem.Row);
					dragItem = null;
				}
				else
				{
					dragItem = null;
					levelController.LevelState = LevelState.idle;
				}
			}
		}

		public void ItemPointerClick(ISelectableComponent obj, PointerEventData eventData)
		{
			if (levelController.LevelState == LevelState.inAction)
			{
				return;
			}

			if (IsHaveMoveableComponent(obj))
			{
				if (obj == dragItem)
				{
					return;
				}

				if (lastItem == null)
				{
					lastItem = obj;
					lastItem.Select();
					levelController.LevelState = LevelState.selected;
					return;
				}
				else
				{
					if (lastItem == obj)
					{
						obj.UnSelect();
						levelController.LevelState = LevelState.idle;
						lastItem = null;
						return;
					}

					int rowDelta = Mathf.Abs(lastItem.fieldItem.Row - obj.fieldItem.Row);
					int coloumnDelta = Mathf.Abs(lastItem.fieldItem.Column - obj.fieldItem.Column);

					lastItem.UnSelect();

					if ((rowDelta == 1 && coloumnDelta == 0) || (rowDelta == 0 && coloumnDelta == 1))
					{
						Swap(lastItem.fieldItem.Column, lastItem.fieldItem.Row, obj.fieldItem.Column, obj.fieldItem.Row);
						lastItem = null;
						return;
					}
					else
					{
						lastItem = obj;
						lastItem.Select();
						levelController.LevelState = LevelState.selected;
						return;
					}
				}
			}
		}

		private void Swap(int column1, int row1, int column2, int row2)
		{
			levelCommandsManager.SwapItemsIfItPosible(column1, row1, column2, row2);
		}

		private bool IsHaveMoveableComponent(ISelectableComponent selectableComponent)
		{
			if ((selectableComponent.fieldItem as FieldItem).GetItemFieldComp<MoveableComponent>() != null)
			{
				return true;
			}

			return false;
		}
	}
}

