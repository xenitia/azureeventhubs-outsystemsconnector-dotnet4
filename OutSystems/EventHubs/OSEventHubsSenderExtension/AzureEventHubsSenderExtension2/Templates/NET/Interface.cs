using System;
using System.Collections;
using System.Data;
using OutSystems.HubEdition.RuntimePlatform;

namespace OutSystems.NssAzureEventHubsSenderExtension2 {

	public interface IssAzureEventHubsSenderExtension2 {

		/// <summary>
		/// Import Details:
		/// 
		///  - Action SenderSend:
		///         Target: Method Send(string, out string)
		///         Declaring Type: ClassLibraryOSTest.Sender
		///         Parameters:
		///             msgIn(Text &lt;- string)
		///             msgOut(Text &lt;- string)
		/// </summary>
		/// <param name="ssmsgIn"></param>
		/// <param name="ssmsgOut"></param>
		void MssSenderSend(string ssmsgIn, out string ssmsgOut);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ssBusinessEvent"></param>
		/// <param name="ssmsgIn1"></param>
		/// <param name="ssmsgOut"></param>
		void MssSenderSend3(string ssBusinessEvent, string ssmsgIn1, out string ssmsgOut);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ssBusinessEvent"></param>
		/// <param name="ssmsgOut"></param>
		void MssSendMultiple(RLEventsRecordList ssBusinessEvent, out string ssmsgOut);

	} // IssAzureEventHubsSenderExtension2

} // OutSystems.NssAzureEventHubsSenderExtension2
