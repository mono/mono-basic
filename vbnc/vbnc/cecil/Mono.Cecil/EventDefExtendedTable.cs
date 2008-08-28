namespace Mono.Cecil
{
	using System;
	using System.Collections;
	using System.Text;
	using Mono.Cecil.Metadata;

	class EventDefExtendedTable : BaseMetadataTableVisitor
	{
		private IMetadataRowVisitor m_rowVisitor;
		private EventExtRow [] m_rows;

		public override IMetadataRowVisitor GetRowVisitor () {
			return m_rowVisitor;
		}

		public EventExtRow this [int i] {
			get {
				return m_rows [i];
			}
		}

		public override void VisitTableCollection (TableCollection coll) {
			IMetadataTable eventTable = coll.Heap [EventTable.RId];
			m_rows = new EventExtRow [eventTable == null ? 0 : eventTable.Rows.Count];
			m_rowVisitor = new EventExtRowVistor (m_rows, coll);
		}
	}

	public class EventExtRowVistor : ExtendedTableRowVisitor
	{
		EventExtRow [] m_rows;
		private TableCollection m_tables;

		public EventExtRowVistor (EventExtRow [] rows, TableCollection col) {
			m_rows = rows;
			m_tables = col;
		}

		public override void VisitCustomAttributeRow (CustomAttributeRow row) {
			if (row.Parent.TokenType == TokenType.Event)
				Add (ref GetOrCreate ((int) row.Parent.RID - 1).m_customAttributes, m_currentRowIndex);
			m_currentRowIndex++;
		}

		public override void VisitEventMapRow (EventMapRow row) {
			int startIndex = (int) row.EventList - 1;
			int declaringTypeIndex = (int) row.Parent - 1;
			EventMapRow next = (EventMapRow) NextRow (m_currentRowIndex);
			int endIndex = next != null ? (int) next.EventList - 1 : GetRowCount (m_tables.Heap [EventTable.RId]);
			for (int i = startIndex; i < endIndex; ++i)
				GetOrCreate (i).m_declaringType = declaringTypeIndex;
			m_currentRowIndex++;
		}

		public override void VisitMethodSemanticsRow (MethodSemanticsRow row) {
			if (row.Semantics == MethodSemanticsAttributes.AddOn)
				GetOrCreate ((int) row.Association.RID - 1).m_addMethod = (int) row.Method - 1;
			else if (row.Semantics == MethodSemanticsAttributes.RemoveOn)
				GetOrCreate ((int) row.Association.RID - 1).m_removeMethod = (int) row.Method - 1;
		}

		private EventExtRow GetOrCreate (int i) {
			if (m_rows [i] == null)
				m_rows [i] = new EventExtRow ();
			return m_rows [i];
		}
	}

	public class EventExtRow
	{
		public int m_constant = -1;
		public ArrayList m_customAttributes;
		public int m_declaringType;
		public int m_addMethod = -1;
		public int m_removeMethod = -1;
	}
}