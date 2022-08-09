using System;
using System.Data;
using System.Collections;
using System.Runtime.Serialization;
using System.Reflection;
using System.Xml;
using OutSystems.ObjectKeys;
using OutSystems.RuntimeCommon;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.HubEdition.RuntimePlatform.Db;
using OutSystems.Internal.Db;
using OutSystems.HubEdition.RuntimePlatform.NewRuntime;

namespace OutSystems.NssAzureEventHubsSenderExtension2 {

	/// <summary>
	/// RecordList type <code>RLEventsRecordList</code> that represents a record list of
	///  <code>Events</code>
	/// </summary>
	[Serializable()]
	public partial class RLEventsRecordList: GenericRecordList<RCEventsRecord>, IEnumerable, IEnumerator, ISerializable {
		public static void EnsureInitialized() {}

		protected override RCEventsRecord GetElementDefaultValue() {
			return new RCEventsRecord("");
		}

		public T[] ToArray<T>(Func<RCEventsRecord, T> converter) {
			return ToArray(this, converter);
		}

		public static T[] ToArray<T>(RLEventsRecordList recordlist, Func<RCEventsRecord, T> converter) {
			return InnerToArray(recordlist, converter);
		}
		public static implicit operator RLEventsRecordList(RCEventsRecord[] array) {
			RLEventsRecordList result = new RLEventsRecordList();
			result.InnerFromArray(array);
			return result;
		}

		public static RLEventsRecordList ToList<T>(T[] array, Func <T, RCEventsRecord> converter) {
			RLEventsRecordList result = new RLEventsRecordList();
			result.InnerFromArray(array, converter);
			return result;
		}

		public static RLEventsRecordList FromRestList<T>(RestList<T> restList, Func <T, RCEventsRecord> converter) {
			RLEventsRecordList result = new RLEventsRecordList();
			result.InnerFromRestList(restList, converter);
			return result;
		}
		/// <summary>
		/// Default Constructor
		/// </summary>
		public RLEventsRecordList(): base() {
		}

		/// <summary>
		/// Constructor with transaction parameter
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLEventsRecordList(IDbTransaction trans): base(trans) {
		}

		/// <summary>
		/// Constructor with transaction parameter and alternate read method
		/// </summary>
		/// <param name="trans"> IDbTransaction Parameter</param>
		/// <param name="alternateReadDBMethod"> Alternate Read Method</param>
		[Obsolete("Use the Default Constructor and set the Transaction afterwards.")]
		public RLEventsRecordList(IDbTransaction trans, ReadDBMethodDelegate alternateReadDBMethod): this(trans) {
			this.alternateReadDBMethod = alternateReadDBMethod;
		}

		/// <summary>
		/// Constructor declaration for serialization
		/// </summary>
		/// <param name="info"> SerializationInfo</param>
		/// <param name="context"> StreamingContext</param>
		public RLEventsRecordList(SerializationInfo info, StreamingContext context): base(info, context) {
		}

		public override BitArray[] GetDefaultOptimizedValues() {
			BitArray[] def = new BitArray[1];
			def[0] = null;
			return def;
		}
		/// <summary>
		/// Create as new list
		/// </summary>
		/// <returns>The new record list</returns>
		protected override OSList<RCEventsRecord> NewList() {
			return new RLEventsRecordList();
		}


	} // RLEventsRecordList
}
