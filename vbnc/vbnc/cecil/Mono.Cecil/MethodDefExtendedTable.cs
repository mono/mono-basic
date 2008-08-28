namespace Mono.Cecil
{
	using System;
	using System.Collections;
	using System.Text;
	using Mono.Cecil.Metadata;

	public class MethodDefExtendedTable : BaseMetadataTableVisitor
	{
		private IMetadataRowVisitor m_rowVisitor;
		private MethodExtRow [] m_rows;

		public override IMetadataRowVisitor GetRowVisitor () {
			return m_rowVisitor;
		}

		public MethodExtRow this [int i] {
			get {
				return m_rows [i];
			}
		}

		public override void VisitTableCollection (TableCollection coll) {
			IMetadataTable methodTable = coll.Heap [MethodTable.RId];
			m_rows = new MethodExtRow [methodTable == null ? 0 : methodTable.Rows.Count];
			m_rowVisitor = new MethodExtRowVistor (m_rows, coll);
		}
	}

	public class MethodExtRowVistor : ExtendedTableRowVisitor
	{
		MethodExtRow [] m_rows;
		private TableCollection m_tables;

		public MethodExtRowVistor (MethodExtRow [] rows, TableCollection col) {
			m_rows = rows;
			m_tables = col;
		}

		public override void VisitTypeDefRow (TypeDefRow row) {
			int methodStart = (int) row.MethodList - 1;
			TypeDefRow nextRow = (TypeDefRow) NextRow (m_currentRowIndex);
			int methodEnd = nextRow != null ? (int) nextRow.MethodList - 1 :
				GetRowCount (m_tables.Heap [MethodTable.RId]);
			for (int i = methodStart; i < methodEnd; ++i)
				GetOrCreate (i).m_declaringType = m_currentRowIndex;
			m_currentRowIndex++;
		}

		public override void VisitMethodRow (MethodRow row) {
			GetOrCreate (m_currentRowIndex).m_parameterStart = (int) row.ParamList - 1;
			MethodRow nextRow = (MethodRow) NextRow (m_currentRowIndex);
			GetOrCreate (m_currentRowIndex).m_parameterEnd = nextRow != null ? (int) nextRow.ParamList - 1 :
				GetRowCount (m_tables.Heap [ParamTable.RId]);
			m_currentRowIndex++;
		}

		public override void VisitGenericParamRow (GenericParamRow row) {
			if (row.Owner.TokenType == TokenType.Method)
				Add (ref GetOrCreate ((int) row.Owner.RID - 1).m_genericParameters, m_currentRowIndex);
			m_currentRowIndex++;
		}

		public override void VisitDeclSecurityRow (DeclSecurityRow row) {
			if (row.Parent.TokenType == TokenType.Method)
				Add (ref GetOrCreate ((int) row.Parent.RID - 1).m_securityDeclerations, m_currentRowIndex);
			m_currentRowIndex++;
		}

		public override void VisitCustomAttributeRow (CustomAttributeRow row) {
			if (row.Parent.TokenType == TokenType.Method)
				Add (ref GetOrCreate ((int) row.Parent.RID - 1).m_customAttributes, m_currentRowIndex);
			m_currentRowIndex++;
		}

		public override void VisitImplMapRow (ImplMapRow row) {
			if (row.MemberForwarded.TokenType == TokenType.Method) { // should always be true
				GetOrCreate ((int) row.MemberForwarded.RID - 1).m_pInvoke = m_currentRowIndex;
			}
			m_currentRowIndex++;
		}

		public override void VisitMethodImplRow (MethodImplRow row) {
			if (row.MethodBody.TokenType != TokenType.Method)
				return;

			if (row.MethodDeclaration.TokenType == TokenType.Method) {
				Add (ref GetOrCreate ((int) row.MethodBody.RID - 1).m_methodsOverrides, (int)row.MethodDeclaration.RID-1);
			}
			else {
				Add (ref GetOrCreate ((int) row.MethodBody.RID - 1).m_memberOverrides, (int) row.MethodDeclaration.RID - 1);
			}
			m_currentRowIndex++;
		}

		private MethodExtRow GetOrCreate (int i) {
			if (m_rows [i] == null)
				m_rows [i] = new MethodExtRow ();
			return m_rows [i];
		}
	}

	public class MethodExtRow
	{
		public int m_declaringType;
		public int m_parameterStart;
		public int m_parameterEnd;
		public ArrayList m_genericParameters;
		public ArrayList m_securityDeclerations;
		public ArrayList m_customAttributes;
		public int m_pInvoke = -1;
		public ArrayList m_memberOverrides;
		public ArrayList m_methodsOverrides;
	}
}
