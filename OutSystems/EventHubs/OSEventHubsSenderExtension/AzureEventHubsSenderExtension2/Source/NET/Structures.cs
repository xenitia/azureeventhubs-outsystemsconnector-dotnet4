using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization;
using OutSystems.ObjectKeys;
using OutSystems.RuntimeCommon;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.HubEdition.RuntimePlatform.Db;
using OutSystems.Internal.Db;

namespace OutSystems.NssAzureEventHubsSenderExtension2 {

	/// <summary>
	/// Structure <code>STEventsStructure</code> that represents the Service Studio structure
	///  <code>Events</code> <p> Description: </p>
	/// </summary>
	[Serializable()]
	public partial struct STEventsStructure: ISerializable, ITypedRecord<STEventsStructure>, ISimpleRecord {
		internal static readonly GlobalObjectKey IdBusinessEvent = GlobalObjectKey.Parse("FUGxLW6vYkWjkZwBRQ9O_Q*QcC57whDREuir4_sGDrYrg");
		internal static readonly GlobalObjectKey IdEventMessage = GlobalObjectKey.Parse("FUGxLW6vYkWjkZwBRQ9O_Q*zRbUlzaWYUOmgaiMeXRb0w");

		public static void EnsureInitialized() {}
		[System.Xml.Serialization.XmlElement("BusinessEvent")]
		public string ssBusinessEvent;

		[System.Xml.Serialization.XmlElement("EventMessage")]
		public string ssEventMessage;


		public BitArray OptimizedAttributes;

		public STEventsStructure(params string[] dummy) {
			OptimizedAttributes = null;
			ssBusinessEvent = "";
			ssEventMessage = "";
		}

		public BitArray[] GetDefaultOptimizedValues() {
			BitArray[] all = new BitArray[0];
			return all;
		}

		public BitArray[] AllOptimizedAttributes {
			set {
				if (value == null) {
				} else {
				}
			}
			get {
				BitArray[] all = new BitArray[0];
				return all;
			}
		}

		/// <summary>
		/// Read a record from database
		/// </summary>
		/// <param name="r"> Data base reader</param>
		/// <param name="index"> index</param>
		public void Read(IDataReader r, ref int index) {
			ssBusinessEvent = r.ReadText(index++, "Events.BusinessEvent", "");
			ssEventMessage = r.ReadText(index++, "Events.EventMessage", "");
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
		public void ReadIM(STEventsStructure r) {
			this = r;
		}


		public static bool operator == (STEventsStructure a, STEventsStructure b) {
			if (a.ssBusinessEvent != b.ssBusinessEvent) return false;
			if (a.ssEventMessage != b.ssEventMessage) return false;
			return true;
		}

		public static bool operator != (STEventsStructure a, STEventsStructure b) {
			return !(a==b);
		}

		public override bool Equals(object o) {
			if (o.GetType() != typeof(STEventsStructure)) return false;
			return (this == (STEventsStructure) o);
		}

		public override int GetHashCode() {
			try {
				return base.GetHashCode()
				^ ssBusinessEvent.GetHashCode()
				^ ssEventMessage.GetHashCode()
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

		public STEventsStructure(SerializationInfo info, StreamingContext context) {
			OptimizedAttributes = null;
			ssBusinessEvent = "";
			ssEventMessage = "";
			Type objInfo = this.GetType();
			FieldInfo fieldInfo = null;
			fieldInfo = objInfo.GetField("ssBusinessEvent", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssBusinessEvent' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssBusinessEvent = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
			fieldInfo = objInfo.GetField("ssEventMessage", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
			if (fieldInfo == null) {
				throw new Exception("The field named 'ssEventMessage' was not found.");
			}
			if (fieldInfo.FieldType.IsSerializable) {
				ssEventMessage = (string) info.GetValue(fieldInfo.Name, fieldInfo.FieldType);
			}
		}

		public void RecursiveReset() {
		}

		public void InternalRecursiveSave() {
		}


		public STEventsStructure Duplicate() {
			STEventsStructure t;
			t.ssBusinessEvent = this.ssBusinessEvent;
			t.ssEventMessage = this.ssEventMessage;
			t.OptimizedAttributes = null;
			return t;
		}

		IRecord IRecord.Duplicate() {
			return Duplicate();
		}

		public void ToXml(Object parent, System.Xml.XmlElement baseElem, String fieldName, int detailLevel) {
			System.Xml.XmlElement recordElem = VarValue.AppendChild(baseElem, "Structure");
			if (fieldName != null) {
				VarValue.AppendAttribute(recordElem, "debug.field", fieldName);
				fieldName = fieldName.ToLowerInvariant();
			}
			if (detailLevel > 0) {
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".BusinessEvent")) VarValue.AppendAttribute(recordElem, "BusinessEvent", ssBusinessEvent, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "BusinessEvent");
				if (!VarValue.FieldIsOptimized(parent, fieldName + ".EventMessage")) VarValue.AppendAttribute(recordElem, "EventMessage", ssEventMessage, detailLevel, TypeKind.Text); else VarValue.AppendOptimizedAttribute(recordElem, "EventMessage");
			} else {
				VarValue.AppendDeferredEvaluationElement(recordElem);
			}
		}

		public void EvaluateFields(VarValue variable, Object parent, String baseName, String fields) {
			String head = VarValue.GetHead(fields);
			String tail = VarValue.GetTail(fields);
			variable.Found = false;
			if (head == "businessevent") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".BusinessEvent")) variable.Value = ssBusinessEvent; else variable.Optimized = true;
			} else if (head == "eventmessage") {
				if (!VarValue.FieldIsOptimized(parent, baseName + ".EventMessage")) variable.Value = ssEventMessage; else variable.Optimized = true;
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
			if (key == IdBusinessEvent) {
				return ssBusinessEvent;
			} else if (key == IdEventMessage) {
				return ssEventMessage;
			} else {
				throw new Exception("Invalid key");
			}
		}
		public void FillFromOther(IRecord other) {
			if (other == null) return;
			ssBusinessEvent = (string) other.AttributeGet(IdBusinessEvent);
			ssEventMessage = (string) other.AttributeGet(IdEventMessage);
		}
		public bool IsDefault() {
			STEventsStructure defaultStruct = new STEventsStructure(null);
			if (this.ssBusinessEvent != defaultStruct.ssBusinessEvent) return false;
			if (this.ssEventMessage != defaultStruct.ssEventMessage) return false;
			return true;
		}
	} // STEventsStructure

} // OutSystems.NssAzureEventHubsSenderExtension2
