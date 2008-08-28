namespace Mono.Cecil
{
	using System;
	using System.Collections;
	using System.Text;
	using Mono.Cecil.Metadata;

	class PropertyDefExtendedTable : BaseMetadataTableVisitor
	{
		private IMetadataRowVisitor m_rowVisitor;
		private PropertyExtRow [] m_rows;

		public override IMetadataRowVisitor GetRowVisitor () {
			return m_rowVisitor;
		}

		public PropertyExtRow this [int i] {
			get {
				return m_rows [i];
			}
		}

		public override void VisitTableCollection (TableCollection coll) {
			IMetadataTable propTable = coll.Heap [PropertyTable.RId];
			m_rows = new PropertyExtRow [propTable == null ? 0 : propTable.Rows.Count];
			m_rowVisitor = new PropertyExtRowVistor (m_rows, coll);
		}
	}

	public class PropertyExtRowVistor : ExtendedTableRowVisitor
	{
		PropertyExtRow [] m_rows;
		private TableCollection m_tables;

		public PropertyExtRowVistor (PropertyExtRow [] rows, TableCollection col) {
			m_rows = rows;
			m_tables = col;
		}

		public override void VisitConstantRow (ConstantRow row) {
			if (row.Parent.TokenType == TokenType.Property)
				GetOrCreate ((int) row.Parent.RID - 1).m_constant = (int) m_currentRowIndex;
			m_currentRowIndex++;
		}

		public override void VisitCustomAttributeRow (CustomAttributeRow row) {
			if (row.Parent.TokenType == TokenType.Property)
				Add (ref GetOrCreate ((int) row.Parent.RID - 1).m_customAttributes, m_currentRowIndex);
			m_currentRowIndex++;
		}

		public override void VisitPropertyMapRow (PropertyMapRow row) {
			int startIndex = (int) row.PropertyList - 1;
			int declaringTypeIndex = (int) row.Parent - 1;
			PropertyMapRow next = (PropertyMapRow) NextRow (m_currentRowIndex);
			int endIndex = next != null ? (int) next.PropertyList - 1 : GetRowCount (m_tables.Heap [PropertyTable.RId]);
			for (int i = startIndex; i < endIndex; ++i)
				GetOrCreate (i).m_declaringType = declaringTypeIndex;
			m_currentRowIndex++;
		}

		public override void VisitMethodSemanticsRow (MethodSemanticsRow row) {
			if (row.Semantics == MethodSemanticsAttributes.Getter)
				GetOrCreate ((int) row.Association.RID - 1).m_getMethod = (int) row.Method - 1;
			else if (row.Semantics == MethodSemanticsAttributes.Setter)
				GetOrCreate ((int) row.Association.RID - 1).m_setMethod = (int) row.Method - 1;
		}

		private PropertyExtRow GetOrCreate (int i) {
			if (m_rows [i] == null)
				m_rows [i] = new PropertyExtRow ();
			return m_rows [i];
		}
	}

	public class PropertyExtRow
	{
		public int m_constant = -1;
		public ArrayList m_customAttributes;
		public int m_declaringType;
		public int m_getMethod = -1;
		public int m_setMethod = -1;
	}
}
