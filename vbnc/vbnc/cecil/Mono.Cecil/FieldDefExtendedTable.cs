namespace Mono.Cecil
{
	using System;
	using System.Collections;
	using System.Text;
	using Mono.Cecil.Metadata;

	public class FieldDefExtendedTable : BaseMetadataTableVisitor
	{
		private IMetadataRowVisitor m_rowVisitor;
		private FieldExtRow [] m_rows;

		public override IMetadataRowVisitor GetRowVisitor () {
			return m_rowVisitor;
		}

		public FieldExtRow this [int i] {
			get {
				return m_rows [i];
			}
		}

		public override void VisitTableCollection (TableCollection coll) {
			IMetadataTable fieldsTable = coll.Heap [FieldTable.RId];
			m_rows = new FieldExtRow [fieldsTable == null ? 0 : fieldsTable.Rows.Count];
			m_rowVisitor = new FieldExtRowVistor (m_rows, coll);
		}
	}

	public class FieldExtRowVistor : ExtendedTableRowVisitor
	{
		FieldExtRow [] m_rows;
		TableCollection m_tables;

		public FieldExtRowVistor (FieldExtRow [] rows, TableCollection coll) {
			m_rows = rows;
			m_tables = coll;
		}

		public override void VisitFieldRVARow (FieldRVARow row) {
			GetOrCreate ((int) row.Field - 1).m_RVA = m_currentRowIndex;
			m_currentRowIndex++;
		}

		public override void VisitConstantRow (ConstantRow row) {
			if (row.Parent.TokenType == TokenType.Field)
				GetOrCreate ((int) row.Parent.RID - 1).m_constant = m_currentRowIndex;
			m_currentRowIndex++;
		}

		public override void VisitCustomAttributeRow (CustomAttributeRow row) {
			if (row.Parent.TokenType == TokenType.Field) {
				Add (ref GetOrCreate ((int) row.Parent.RID - 1).m_customAttributes, m_currentRowIndex);
			}
			m_currentRowIndex++;
		}

		public override void VisitTypeDefRow (TypeDefRow row) {
			int fieldStart = (int) row.FieldList - 1;
			TypeDefRow nextRow = (TypeDefRow) NextRow (m_currentRowIndex);
			int fieldEnd = nextRow != null ? (int) nextRow.FieldList - 1 : GetRowCount (m_tables.Heap [FieldTable.RId]);
			for (int i = fieldStart; i < fieldEnd; ++i)
				GetOrCreate (i).m_declaringType = m_currentRowIndex;
			m_currentRowIndex++;
		}

		private FieldExtRow GetOrCreate (int i) {
			if (m_rows [i] == null)
				m_rows [i] = new FieldExtRow ();
			return m_rows [i];
		}
	}

	public class FieldExtRow
	{
		public int m_RVA = -1;
		public int m_constant = -1;
		public ArrayList m_customAttributes;
		public int m_declaringType = -1;
	}
}
