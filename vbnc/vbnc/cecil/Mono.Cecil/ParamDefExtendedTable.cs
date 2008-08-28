namespace Mono.Cecil
{
	using System;
	using System.Collections;
	using System.Text;
	using Mono.Cecil.Metadata;

	public class ParamDefExtendedTable : BaseMetadataTableVisitor
	{
		private IMetadataRowVisitor m_rowVisitor;
		private ParamDefExtendedRow [] m_rows;

		public override IMetadataRowVisitor GetRowVisitor () {
			return m_rowVisitor;
		}

		public ParamDefExtendedRow this [int i] {
			get {
				return m_rows [i];
			}
		}

		public override void VisitTableCollection (TableCollection coll) {
			IMetadataTable paramsTable = coll.Heap [ParamTable.RId];
			m_rows = new ParamDefExtendedRow [paramsTable == null ? 0 : paramsTable.Rows.Count];
			m_rowVisitor = new ParamDefExtRowVistor (m_rows, coll);
		}
	}

	public class ParamDefExtRowVistor : ExtendedTableRowVisitor
	{
		ParamDefExtendedRow [] m_rows;
		private TableCollection m_tableCollection;

		public ParamDefExtRowVistor (ParamDefExtendedRow [] rows, TableCollection col) {
			m_rows = rows;
			m_tableCollection = col;
		}

		public override void VisitMethodRow (MethodRow row) {
			int startIndex = (int) row.ParamList - 1;
			MethodRow next = (MethodRow) NextRow (m_currentRowIndex);
			int endIndex = next != null ? (int) next.ParamList - 1 : GetRowCount (m_tableCollection.Heap [ParamTable.RId]);
			for (int i = startIndex; i < endIndex; ++i)
				GetOrCreate (i).m_methodOwner = m_currentRowIndex;
			m_currentRowIndex++;
		}

		public override void VisitCustomAttributeRow (CustomAttributeRow row) {
			if (row.Parent.TokenType == TokenType.Param) {
				Add (ref GetOrCreate ((int) row.Parent.RID - 1).m_customAttributes, m_currentRowIndex);
			}
			m_currentRowIndex++;
		}

		public override void VisitConstantRow (ConstantRow row) {
			if (row.Parent.TokenType == TokenType.Param) {
				GetOrCreate ((int) row.Parent.RID - 1).m_constant = m_currentRowIndex;
			}
			m_currentRowIndex++;
		}

		private ParamDefExtendedRow GetOrCreate (int i) {
			if (m_rows [i] == null)
				m_rows [i] = new ParamDefExtendedRow ();
			return m_rows [i];
		}
	}

	public class ParamDefExtendedRow
	{
		public int m_methodOwner;
		public ArrayList m_customAttributes;
		public int m_constant = -1;
	}
}
