/// <license>TUSTENA PUBLIC LICENSE v1.0</license>
/// <copyright>
/// Portions Copyright (c) 2003-2006 Digita S.r.l. All Rights Reserved.
///
/// Tustena CRM is a trademark of:
/// Digita S.r.l.
/// Viale Enrico Fermi 14/z
/// 31011 Asolo (Italy)
/// Tel. +39-0423-951251
/// Mail. info@digita.it
///
/// This file contains Original Code and/or Modifications of Original Code
/// as defined in and that are subject to the Tustena Public Source License
/// Version 1.0 (the 'License'). You may not use this file except in
/// compliance with the License. Please obtain a copy of the License at
/// http://www.tustena.com/TPL/ and read it before using this
// file.
///
/// The Original Code and all software distributed under the License are
/// distributed on an 'AS IS' basis, WITHOUT WARRANTY OF ANY KIND, EITHER
/// EXPRESS OR IMPLIED, AND DIGITA S.R.L. HEREBY DISCLAIMS ALL SUCH WARRANTIES,
/// INCLUDING WITHOUT LIMITATION, ANY WARRANTIES OF MERCHANTABILITY,
/// FITNESS FOR A PARTICULAR PURPOSE, QUIET ENJOYMENT OR NON-INFRINGEMENT.
/// Please see the License for the specific language governing rights and
/// limitations under the License.
///
/// YOU MAY NOT REMOVE OR ALTER THIS COPYRIGHT NOTICE!
/// </copyright>

using System;
using System.Collections;

namespace Digita.Tustena.MailingList.webmail
{

	[Serializable]
	internal class MessageCache
	{
		public Message[] Messages
		{
			get { return messages; }
			set { messages = value; }
		}

		public string LastSerial
		{
			get { return lastserial; }
			set { lastserial = value; }
		}

		public long LastPosition
		{
			get { return lastposition; }
			set { lastposition = value; }
		}

		public long FilledTo
		{
			get { return filledTo; }
			set { filledTo = value; }
		}
		private Message[] messages;
		private string lastserial = string.Empty;
		private long lastposition = -1;
		private long filledTo = 0;

		public MessageCache(Message[] messages, string lastserial)
		{
			this.messages = messages;
			this.lastserial = lastserial;
		}
	}

	[Serializable]
	internal class MessageAttach
	{
		private string link = String.Empty;
		private string filename = String.Empty;

		public string Link
		{
			set { link = value; }
			get { return link; }
		}

		public string Filename
		{
			set { filename = value; }
			get { return filename; }
		}
	}

	[Serializable]
	internal class Message
	{
		private string from = String.Empty;
		private string to = String.Empty;
		private string subject = String.Empty;
		private string body = String.Empty;
		private string contenttype = String.Empty;
		private long size = -1;
		private long msgid = -1;
		private string messageid = String.Empty;
		private string msgserial = String.Empty;
		private DateTime msgdate;
		private MessageAttach[] attachment;

		public DateTime MsgDate
		{
			get { return msgdate; }
			set { msgdate = value; }
		}

		public long Size
		{
			get { return size; }
			set { size = value; }
		}

		public enum MsgType
		{
			Plain = 0,
			HTML
		}

		public MessageAttach[] Attachment
		{
			set { attachment = value; }
			get { return attachment; }
		}

		public string MessageID
		{
			set { messageid = value; }
			get { return messageid; }
		}

		public string From
		{
			set { from = value; }
			get { return from; }
		}

		public string To
		{
			set { to = value; }
			get { return to; }
		}

		public string Subject
		{
			set { subject = value; }
			get { return subject; }
		}

		public string Body
		{
			set { body = value; }
			get { return body; }
		}

		public string ContentType
		{
			set { contenttype = value; }
			get { return contenttype; }
		}

		public long MsgID
		{
			set { msgid = value; }
			get { return msgid; }
		}

		public string MsgSerial
		{
			set { msgserial = value; }
			get { return msgserial; }
		}

	}

	internal class Messages : IEnumerable, IEnumerator
	{
		private int _index = -1;
		private ArrayList messages = new ArrayList();

		IEnumerator IEnumerable.GetEnumerator()
		{
			return (this);
		}

		void IEnumerator.Reset()
		{
			this._index = -1;
		}

		public void Add(Message value)
		{
			messages.Add(value);
		}

		object IEnumerator.Current
		{
			get { return (Message) messages[this._index]; }
		}

		bool IEnumerator.MoveNext()
		{
			this._index++;
			try
			{
				return (this._index < messages.Count);
			}
			catch
			{
				return (false);
			}
		}

	}
}
