using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace m3
{
	public class Field : IField
	{
		private int width;
		public int Width
		{
			get
			{
				return width;
			}
		}

		private int height;
		public int Height
		{
			get
			{
				return height;
			}
		}

		private int maxItemValue;
		public int MaxItemValue
		{
			get
			{
				return maxItemValue;
			}
		}

		private IFieldItem[,] visualField;
		public IFieldItem[,] VisualField
		{
			get
			{
				return visualField;
			}

			set
			{
				visualField = value;
			}
		}

		private int[,] logicalField;
		public int[,] LogicalField
		{
			get
			{
				return logicalField;
			}

			set
			{
				logicalField = value;
			}
		}

		public Field(int width, int height, int maxItemValue)
		{
			this.width = width;
			this.height = height;
			this.maxItemValue = maxItemValue;
		}

		#region CollectionsHelper
		public IEnumerable<IFieldItem> GetColumnMatches(int columnIndex, IFieldItem[,] field)
		{
			var column = GetColumn(columnIndex, field).ToList();
			return GetArrayMatches(column);
		}

		public IEnumerable<T> GetColumn<T>(int columnIndex, T[,] field)
		{
			if (columnIndex < 0 || columnIndex >= Width)
				return null;
			var column = Enumerable.Range(0, Height)
				.Select(y => field[columnIndex, y]);
			return column;
		}

		public IEnumerable<IFieldItem> GetArrayMatches(List<IFieldItem> line)
		{
			var result = new List<IFieldItem>();
			for (var i = 1; i < line.Count - 1; i++)
			{
				var thisValue = logicalField[line[i].Column, line[i].Row];
				var leftValue = logicalField[line[i - 1].Column, line[i - 1].Row];
				var rightValue = logicalField[line[i + 1].Column, line[i + 1].Row];
				if (thisValue != -1 && thisValue == leftValue && thisValue == rightValue)
					result.AddRange(line.GetRange(i - 1, 3));
			}
			return result.Distinct();
		}

		public IEnumerable<IFieldItem> GetRowMatches(int rowIndex, IFieldItem[,] field)
		{
			var row = GetRow(rowIndex, field).ToList();
			return GetArrayMatches(row);
		}

		public IEnumerable<T> GetRow<T>(int rowIndex, T[,] field)
		{
			if (rowIndex < 0 || rowIndex >= Height)
				return null;
			var row = Enumerable.Range(0, Width)
				.Select(x => field[x, rowIndex]);
			return row;
		}

		public IEnumerable<IFieldItem> FindMatches(IFieldItem[,] field)
		{
			var columnMatches = Enumerable.Range(0, Width)
				.SelectMany(column => GetColumnMatches(column, field));
			var rowMatches = Enumerable.Range(0, Height)
				.SelectMany(row => GetRowMatches(row, field));
			return columnMatches.Concat(rowMatches).Distinct();
		}
		#endregion
	}
}


