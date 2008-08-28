namespace Mono.Cecil
{
	using System;
	using System.Collections;
	using System.Text;
	using Mono.Cecil.Metadata;

	public class TypeDefExtendedTable : BaseMetadataTableVisitor
	{
		private IMetadataRowVisitor m_rowVisitor;
		private TypeDefExtendedRow [] m_rows;

		public override IMetadataRowVisitor GetRowVisitor () {
			return m_rowVisitor;
		}

		public TypeDefExtendedRow this [int i] {
			get {
				return m_rows [i];
			}
		}

		public override void VisitTableCollection (TableCollection coll) {
			IMetadataTable typesTable = coll.Heap [TypeDefTable.RId];
			m_rows = new TypeDefExtendedRow [typesTable == null ? 0 : typesTable.Rows.Count];
			m_rowVisitor = new TypeDefExtRowVistor (m_rows, coll);
		}
	}

	public class TypeDefExtRowVistor : ExtendedTableRowVisitor
	{
		TypeDefExtendedRow [] m_rows;
		private TableCollection m_tableCollection;

		public TypeDefExtRowVistor (TypeDefExtendedRow [] rows, TableCollection col) {
			m_rows = rows;
			m_tableCollection = col;
		}

		public override void VisitNestedClassRow (NestedClassRow row) {
			GetOrCreate ((int) row.NestedClass - 1).m_declaringType = (int) row.EnclosingClass - 1;
			Add (ref GetOrCreate ((int) row.EnclosingClass - 1).m_nestedTypes, (int) row.EnclosingClass - 1);
		}

		public override void VisitGenericParamRow (Mono.Cecil.Metadata.GenericParamRow row) {
			if (row.Owner.TokenType == TokenType.TypeDef)
				Add (ref GetOrCreate ((int) row.Owner.RID - 1).m_genericParameters, m_currentRowIndex);
			m_currentRowIndex++;
		}

		public override void VisitCustomAttributeRow (CustomAttributeRow row) {
			if (row.Parent.TokenType == TokenType.TypeDef)
				Add (ref GetOrCreate ((int) row.Parent.RID - 1).m_customAttributes, m_currentRowIndex);
			m_currentRowIndex++;
		}

		public override void VisitInterfaceImplRow (InterfaceImplRow row) {
			Add (ref GetOrCreate ((int) row.Class - 1).m_interfaces, row.Interface);
		}

		public override void VisitEventMapRow (EventMapRow row) {
			GetOrCreate ((int) row.Parent - 1).m_eventStart = (int) row.EventList - 1;
			EventMapRow evMapRow = (EventMapRow) NextRow (m_currentRowIndex);
			GetOrCreate ((int) row.Parent - 1).m_eventEnd = evMapRow != null ? (int) evMapRow.EventList - 1 :
				GetRowCount (m_tableCollection.Heap [EventTable.RId]);
			m_currentRowIndex++;
		}

		public override void VisitPropertyMapRow (PropertyMapRow row) {
			GetOrCreate ((int) row.Parent - 1).m_propertyStart = (int) row.PropertyList - 1;
			PropertyMapRow propMapRow = (PropertyMapRow) NextRow (m_currentRowIndex);
			GetOrCreate ((int) row.Parent - 1).m_propertyEnd = propMapRow != null ? (int) propMapRow.PropertyList - 1 :
				GetRowCount (m_tableCollection.Heap [PropertyTable.RId]);
			m_currentRowIndex++;
		}

		public override void VisitDeclSecurityRow (DeclSecurityRow row) {
			if (row.Parent.TokenType == TokenType.TypeDef)
				Add (ref GetOrCreate ((int) row.Parent.RID - 1).m_securityDeclerations, m_currentRowIndex);
			m_currentRowIndex++;
		}

		private TypeDefExtendedRow GetOrCreate (int i) {
			if (m_rows [i] == null)
				m_rows [i] = new TypeDefExtendedRow ();
			return m_rows [i];
		}
	}

	public class TypeDefExtendedRow
	{	
		public int m_declaringType = -1;
		public ArrayList m_genericParameters;
		public ArrayList m_customAttributes;
		public ArrayList m_interfaces;
		public ArrayList m_nestedTypes;
		public int m_eventStart;
		public int m_eventEnd;
		public int m_propertyStart;
		public int m_propertyEnd;
		public ArrayList m_securityDeclerations;
	}
}
