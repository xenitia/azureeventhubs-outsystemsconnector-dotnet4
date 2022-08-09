using System;
using System.Collections;
using System.Data;
using System.Runtime.Serialization;
using System.Reflection;
using System.Xml;
using OutSystems.ObjectKeys;
using OutSystems.RuntimeCommon;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.HubEdition.RuntimePlatform.Db;
using OutSystems.Internal.Db;

namespace OutSystems.NssAzureEventHubsSenderExtension2 {

	/// <summary>
	/// Structure <code>RCEventsRecord</code>
	/// </summary>
	[Serializable()]
	public partial struct RCEventsRecord: ISerializable, ITypedRecord<RCEventsRecord> {
		internal static readonly GlobalObjectKey IdEvents = GlobalObjectKey.Parse("2UmDmepsh0WSfJ_D1JexCA*30PnaoG+WJ8ZF6QDjMTgoA");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("Events")]
		public STEventsStructure ssSTEvents;


		public static implicit operator STEventsStructure(RCEventsRecord r) {
			return r.ssSTEvents;
		}

		public static implicit operator RCEventsRecord(STEventsStructure r) {
			RCEventsRecord res = new RCEventsRecord(null);
			res.ssSTEvents = r;
			return res;
		}

		public BitArray OptimizedAttributes;

		public RCEventsRecord(params string[] dummy) {
			OptimizedAttributes = null;
			ssSTEvents = new STEventsStructure(null);
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[1];
			all[0] = null;
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
					ssSTEvents.OptimizedAttributes = value[0];
				}
			}
			get {
				BitArray[] all = new BitArray[1];
				all[0] = null;
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssSTEvents.Read(r, ref index);
		}
		/// <summary>
		/// Read from database
		/// </summary>
		/// <param name="r"> Data reader</param>
		public void ReadDB(IDataReader r) {
			int index = 0;
			Read(r, ref index);
		}

		/// <summary>
		/// Read from record
		/// </summary>
		/// <param name="r"> Record</param>
		public void ReadIM(RCEventsRecord r) {
			this = r;
		}


		public static bool operator == (RCEventsRecord a, RCEventsRecord b) {
			if (a.ssSTEvents != b.ssSTEvents) return false;
			return true;
		}

		public static bool operator != (RCEventsRecord a, RCEventsRecord b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(RCEventsRecord)) return false;
			return (this == (RCEventsRecord) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssSTEvents.GetHashCode()
				;
			} catch {
				return base.GetHashCode();
			}
		}

		public void GetObjectData(SerializationInfo info, StreamingContext context) {
			Type objInfo = this.GetType();
			FieldInfo[] fields;
			fields = objInfo.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			for (int i = 0; i < fields.Length; i++)
			if (fields[i] .FieldType.IsSerializable)
			info.AddValue(fields[i] .Name, fields[i] .GetValue(this));
		}

		public RCEventsRecord(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssSTEvents = new STEventsStructure(null);
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssSTEvents", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssSTEvents' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssSTEvents = (STEventsStructure) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
			ssSTEvents.RecursiveReset();
		}

		public void InternalRecursiveSave() {
			ssSTEvents.InternalRecursiveSave();
		}


		public RCEventsRecord Duplicate() {
			RCEventsRecord t;
			t.ssSTEvents = (STEventsStructure) this.ssSTEvents.Duplicate();
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Record");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
			}
			if (detailLevel > 0) {
				ssSTEvents.ToXml(this, recordElem, "Events", detailLevel - 1);
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "events") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".Events")) variable.Value = ssSTEvents; else variable.Optimized = true;
				variable.SetFieldName("events");
			}
			if (variable.Found && tail != null) variable.EvaluateFields(this, head, tail);
		}

		public bool ChangedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public bool OptimizedAttributeGet(GlobalObjectKey key) {
			throw new Exception("Method not Supported");
		}

		public object AttributeGet(GlobalObjectKey key) {
			if (key == IdEvents) {
				return ssSTEvents;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssSTEvents.FillFromOther((IRecord) other.AttributeGet(IdEvents));
		}
		public bool IsDefault() {
			RCEventsRecord defaultStruct = new RCEventsRecord(null);
			if (this.ssSTEvents != defaultStruct.ssSTEvents) return false;
			return true;
		}
	} // RCEventsRecord
}
