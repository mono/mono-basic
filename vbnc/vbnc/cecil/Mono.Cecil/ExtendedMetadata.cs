namespace Mono.Cecil
{
	using System;
	using System.Collections;
	using System.Text;
	using Mono.Cecil.Metadata;

	public abstract class ExtendedTableRowVisitor : BaseMetadataRowVisitor
	{
		private RowCollection m_currentCollection;
		protected int m_currentRowIndex;

		protected RowCollection CurrentCollection {
			get {
				return m_currentCollection;
			}
		}
		protected IMetadataRow NextRow (int current) {
			return current < CurrentCollection.Count - 1 ? CurrentCollection [current + 1] : null;
		}
		public override void VisitRowCollection (RowCollection coll) {
			m_currentCollection = coll;
			m_currentRowIndex = 0;
		}

		protected void Add (ref ArrayList list, object item) {
			if (list == null)
				list = new ArrayList ();
			list.Add (item);
		}

		protected int GetRowCount (IMetadataTable table) {
			return table == null ? 0 : table.Rows.Count;
		}
	}

	public class MetadataExtendedTables
	{
		TypeDefExtendedTable m_typesExtTable = new TypeDefExtendedTable ();
		MethodDefExtendedTable m_methodsExtTable = new MethodDefExtendedTable ();
		FieldDefExtendedTable m_fieldsExtTable = new FieldDefExtendedTable ();
		PropertyDefExtendedTable m_propertyExtTable = new PropertyDefExtendedTable ();
		ParamDefExtendedTable m_paramExtTable = new ParamDefExtendedTable ();
		EventDefExtendedTable m_eventsTable = new EventDefExtendedTable ();

		public MetadataExtendedTables (TableCollection tables) {
			BuildTables (tables);
		}

		private void BuildTables (TableCollection tables) {
			tables.Accept (m_typesExtTable);
			tables.Accept (m_methodsExtTable);
			tables.Accept (m_fieldsExtTable);
			tables.Accept (m_propertyExtTable);
			tables.Accept (m_paramExtTable);
			tables.Accept (m_eventsTable);
		}

		internal TypeDefExtendedTable Types {
			get {
				return m_typesExtTable;
			}
		}

		internal MethodDefExtendedTable Methods {
			get {
				return m_methodsExtTable;
			}
		}

		internal FieldDefExtendedTable Fields {
			get {
				return m_fieldsExtTable;
			}
		}

		internal PropertyDefExtendedTable Properties {
			get {
				return m_propertyExtTable;
			}
		}

		internal ParamDefExtendedTable Parameters {
			get {
				return m_paramExtTable;
			}
		}

		internal EventDefExtendedTable Events {
			get {
				return m_eventsTable;
			}
		}
	}
}
