using System;
using System.Collections;
using System.Data;
using OutSystems.HubEdition.RuntimePlatform;
using OutSystems.RuntimePublic.Db;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OutSystems.NssAzureEventHubsSenderExtension2 {

	public class CssAzureEventHubsSenderExtension2: IssAzureEventHubsSenderExtension2 {

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ssBusinessEvent"></param>
		public void MssSendMultiple(RLEventsRecordList ssBusinessEvent, out string ssmsgOut) {
			// TODO: Write implementation for action
			ssmsgOut = "";

			List<ClassLibraryOSTest.MFEventData> events = new List<ClassLibraryOSTest.MFEventData>();

			foreach (RCEventsRecord e in ssBusinessEvent)
			{
				var MFe = new ClassLibraryOSTest.MFEventData();
				MFe.BusinessEvent= e.ssSTEvents.ssBusinessEvent;
				MFe.EventMessage = e.ssSTEvents.ssEventMessage;
				events.Add(MFe);
			}

			ClassLibraryOSTest.Sender.SendMultiple(events, out ssmsgOut);

		} // MssSendMultiple


		/// <summary>
		/// 
		/// </summary>
		/// <param name="ssBusinessEvent"></param>
		/// <param name="ssmsgIn1"></param>
		/// <param name="ssmsgOut"></param>
		public void MssSenderSend3(string ssBusinessEvent, string ssmsgIn1, out string ssmsgOut) {
			// TODO: Write implementation for action

			ssmsgOut = "";
			ClassLibraryOSTest.Sender.Send3(ssBusinessEvent,ssmsgIn1, out ssmsgOut);
		} // MssSenderSend3


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

			//string ssmsgIn = "Hello Event Hubs Async to Sync!";
			//Task<string> ssmsgOutTask = ClassLibraryOSTest.Sender.Send(ssmsgIn);
			//ssmsgOut = ssmsgOutTask.Result;

	} // MssSenderSend


	} // CssAzureEventHubsSenderExtension2

} // OutSystems.NssAzureEventHubsSenderExtension2

