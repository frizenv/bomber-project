using UnityEngine;

namespace Gameplay.AI.Navigation
{
	public sealed class PointsGrid : MonoBehaviour, IDestinationProvider
	{
		[SerializeField] private int _rows = 5;
		[SerializeField] private int _columns = 5;
		[SerializeField] private float _distance = 5;

		private void OnDrawGizmos()
		{
			for (int i = 0; i < _rows; i++)
			{
				for (int  j = 0; j < _columns; j++)
				{
					Gizmos.DrawSphere(GetPointByIndex(i, j), 1f);
				}
			}
		}

		public Vector3 GetDestination()
		{
			int i = Random.Range(0, _rows);
			int j = Random.Range(0, _columns);
			return GetPointByIndex(i, j);
		}

		private Vector3 GetPointByIndex(int row, int column)
		{
			float xDistance = _rows - 1;
			float yDistance = _columns - 1;

			Vector3 localPosition = (xDistance / 2 - row) * _distance * transform.forward + (yDistance / 2 - column) * _distance * transform.right;
			return transform.position + localPosition;
		}
	}
}