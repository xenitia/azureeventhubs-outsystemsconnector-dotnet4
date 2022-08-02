using System;
using System.Collections;
using System.Data;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.RuntimePublic.Db;

namespace OutSystems.NssAzureEventHubsSenderExtension2 {

	public class CssAzureEventHubsSenderExtension2: IssAzureEventHubsSenderExtension2 {

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
		public void MssSenderSend(string ssmsgIn, out string ssmsgOut) {
			ssmsgOut = "";
			// Implementation for action
			ClassLibraryOSTest.Sender.Send(ssmsgIn, out ssmsgOut);
		} // MssSenderSend

	} // CssAzureEventHubsSenderExtension2

} // OutSystems.NssAzureEventHubsSenderExtension2

