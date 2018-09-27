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


#pragma warning disable 1591

namespace Digita.Tustena.smsmessenger {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SendServiceSoap", Namespace="com.esendex.ems.soapinterface")]
    public partial class SendService : System.Web.Services.Protocols.SoapHttpClientProtocol {

        private MessengerHeader messengerHeaderValueField;

        private System.Threading.SendOrPostCallback SendMessageOperationCompleted;

        private System.Threading.SendOrPostCallback SendMessageFullOperationCompleted;

        private System.Threading.SendOrPostCallback SendMessageMultipleRecipientsOperationCompleted;

        private System.Threading.SendOrPostCallback SendMessageMultipleRecipientsFullOperationCompleted;

        private System.Threading.SendOrPostCallback SendMessageBatchOperationCompleted;

        private System.Threading.SendOrPostCallback GetMessageStatusOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        public SendService() {
            this.Url = "https://www.esendex.com/secure/messenger/soap/sendservice.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public MessengerHeader MessengerHeaderValue {
            get {
                return this.messengerHeaderValueField;
            }
            set {
                this.messengerHeaderValueField = value;
            }
        }

        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true)
                            && (this.useDefaultCredentialsSetExplicitly == false))
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }

        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public event SendMessageCompletedEventHandler SendMessageCompleted;

        public event SendMessageFullCompletedEventHandler SendMessageFullCompleted;

        public event SendMessageMultipleRecipientsCompletedEventHandler SendMessageMultipleRecipientsCompleted;

        public event SendMessageMultipleRecipientsFullCompletedEventHandler SendMessageMultipleRecipientsFullCompleted;

        public event SendMessageBatchCompletedEventHandler SendMessageBatchCompleted;

        public event GetMessageStatusCompletedEventHandler GetMessageStatusCompleted;

        [System.Web.Services.Protocols.SoapHeaderAttribute("MessengerHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("com.esendex.ems.soapinterface/SendMessage", RequestNamespace="com.esendex.ems.soapinterface", ResponseNamespace="com.esendex.ems.soapinterface", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SendMessage(string recipient, string body, MessageType type) {
            object[] results = this.Invoke("SendMessage", new object[] {
                        recipient,
                        body,
                        type});
            return ((string)(results[0]));
        }

        public System.IAsyncResult BeginSendMessage(string recipient, string body, MessageType type, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendMessage", new object[] {
                        recipient,
                        body,
                        type}, callback, asyncState);
        }

        public string EndSendMessage(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        public void SendMessageAsync(string recipient, string body, MessageType type) {
            this.SendMessageAsync(recipient, body, type, null);
        }

        public void SendMessageAsync(string recipient, string body, MessageType type, object userState) {
            if ((this.SendMessageOperationCompleted == null)) {
                this.SendMessageOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMessageOperationCompleted);
            }
            this.InvokeAsync("SendMessage", new object[] {
                        recipient,
                        body,
                        type}, this.SendMessageOperationCompleted, userState);
        }

        private void OnSendMessageOperationCompleted(object arg) {
            if ((this.SendMessageCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMessageCompleted(this, new SendMessageCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        [System.Web.Services.Protocols.SoapHeaderAttribute("MessengerHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("com.esendex.ems.soapinterface/SendMessageFull", RequestNamespace="com.esendex.ems.soapinterface", ResponseNamespace="com.esendex.ems.soapinterface", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SendMessageFull(string originator, string recipient, string body, MessageType type, int validityperiod) {
            object[] results = this.Invoke("SendMessageFull", new object[] {
                        originator,
                        recipient,
                        body,
                        type,
                        validityperiod});
            return ((string)(results[0]));
        }

        public System.IAsyncResult BeginSendMessageFull(string originator, string recipient, string body, MessageType type, int validityperiod, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendMessageFull", new object[] {
                        originator,
                        recipient,
                        body,
                        type,
                        validityperiod}, callback, asyncState);
        }

        public string EndSendMessageFull(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        public void SendMessageFullAsync(string originator, string recipient, string body, MessageType type, int validityperiod) {
            this.SendMessageFullAsync(originator, recipient, body, type, validityperiod, null);
        }

        public void SendMessageFullAsync(string originator, string recipient, string body, MessageType type, int validityperiod, object userState) {
            if ((this.SendMessageFullOperationCompleted == null)) {
                this.SendMessageFullOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMessageFullOperationCompleted);
            }
            this.InvokeAsync("SendMessageFull", new object[] {
                        originator,
                        recipient,
                        body,
                        type,
                        validityperiod}, this.SendMessageFullOperationCompleted, userState);
        }

        private void OnSendMessageFullOperationCompleted(object arg) {
            if ((this.SendMessageFullCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMessageFullCompleted(this, new SendMessageFullCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        [System.Web.Services.Protocols.SoapHeaderAttribute("MessengerHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("com.esendex.ems.soapinterface/SendMessageMultipleRecipients", RequestNamespace="com.esendex.ems.soapinterface", ResponseNamespace="com.esendex.ems.soapinterface", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] SendMessageMultipleRecipients(string[] recipients, string body, MessageType type) {
            object[] results = this.Invoke("SendMessageMultipleRecipients", new object[] {
                        recipients,
                        body,
                        type});
            return ((string[])(results[0]));
        }

        public System.IAsyncResult BeginSendMessageMultipleRecipients(string[] recipients, string body, MessageType type, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendMessageMultipleRecipients", new object[] {
                        recipients,
                        body,
                        type}, callback, asyncState);
        }

        public string[] EndSendMessageMultipleRecipients(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        public void SendMessageMultipleRecipientsAsync(string[] recipients, string body, MessageType type) {
            this.SendMessageMultipleRecipientsAsync(recipients, body, type, null);
        }

        public void SendMessageMultipleRecipientsAsync(string[] recipients, string body, MessageType type, object userState) {
            if ((this.SendMessageMultipleRecipientsOperationCompleted == null)) {
                this.SendMessageMultipleRecipientsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMessageMultipleRecipientsOperationCompleted);
            }
            this.InvokeAsync("SendMessageMultipleRecipients", new object[] {
                        recipients,
                        body,
                        type}, this.SendMessageMultipleRecipientsOperationCompleted, userState);
        }

        private void OnSendMessageMultipleRecipientsOperationCompleted(object arg) {
            if ((this.SendMessageMultipleRecipientsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMessageMultipleRecipientsCompleted(this, new SendMessageMultipleRecipientsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        [System.Web.Services.Protocols.SoapHeaderAttribute("MessengerHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("com.esendex.ems.soapinterface/SendMessageMultipleRecipientsFull", RequestNamespace="com.esendex.ems.soapinterface", ResponseNamespace="com.esendex.ems.soapinterface", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] SendMessageMultipleRecipientsFull(string originator, string[] recipients, string body, MessageType type, int validityperiod) {
            object[] results = this.Invoke("SendMessageMultipleRecipientsFull", new object[] {
                        originator,
                        recipients,
                        body,
                        type,
                        validityperiod});
            return ((string[])(results[0]));
        }

        public System.IAsyncResult BeginSendMessageMultipleRecipientsFull(string originator, string[] recipients, string body, MessageType type, int validityperiod, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendMessageMultipleRecipientsFull", new object[] {
                        originator,
                        recipients,
                        body,
                        type,
                        validityperiod}, callback, asyncState);
        }

        public string[] EndSendMessageMultipleRecipientsFull(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        public void SendMessageMultipleRecipientsFullAsync(string originator, string[] recipients, string body, MessageType type, int validityperiod) {
            this.SendMessageMultipleRecipientsFullAsync(originator, recipients, body, type, validityperiod, null);
        }

        public void SendMessageMultipleRecipientsFullAsync(string originator, string[] recipients, string body, MessageType type, int validityperiod, object userState) {
            if ((this.SendMessageMultipleRecipientsFullOperationCompleted == null)) {
                this.SendMessageMultipleRecipientsFullOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMessageMultipleRecipientsFullOperationCompleted);
            }
            this.InvokeAsync("SendMessageMultipleRecipientsFull", new object[] {
                        originator,
                        recipients,
                        body,
                        type,
                        validityperiod}, this.SendMessageMultipleRecipientsFullOperationCompleted, userState);
        }

        private void OnSendMessageMultipleRecipientsFullOperationCompleted(object arg) {
            if ((this.SendMessageMultipleRecipientsFullCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMessageMultipleRecipientsFullCompleted(this, new SendMessageMultipleRecipientsFullCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        [System.Web.Services.Protocols.SoapHeaderAttribute("MessengerHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("com.esendex.ems.soapinterface/SendMessageBatch", RequestNamespace="com.esendex.ems.soapinterface", ResponseNamespace="com.esendex.ems.soapinterface", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] SendMessageBatch(messagesubmission[] messages) {
            object[] results = this.Invoke("SendMessageBatch", new object[] {
                        messages});
            return ((string[])(results[0]));
        }

        public System.IAsyncResult BeginSendMessageBatch(messagesubmission[] messages, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendMessageBatch", new object[] {
                        messages}, callback, asyncState);
        }

        public string[] EndSendMessageBatch(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        public void SendMessageBatchAsync(messagesubmission[] messages) {
            this.SendMessageBatchAsync(messages, null);
        }

        public void SendMessageBatchAsync(messagesubmission[] messages, object userState) {
            if ((this.SendMessageBatchOperationCompleted == null)) {
                this.SendMessageBatchOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendMessageBatchOperationCompleted);
            }
            this.InvokeAsync("SendMessageBatch", new object[] {
                        messages}, this.SendMessageBatchOperationCompleted, userState);
        }

        private void OnSendMessageBatchOperationCompleted(object arg) {
            if ((this.SendMessageBatchCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendMessageBatchCompleted(this, new SendMessageBatchCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        [System.Web.Services.Protocols.SoapHeaderAttribute("MessengerHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("com.esendex.ems.soapinterface/GetMessageStatus", RequestNamespace="com.esendex.ems.soapinterface", ResponseNamespace="com.esendex.ems.soapinterface", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public MessageStatusCode GetMessageStatus(string id) {
            object[] results = this.Invoke("GetMessageStatus", new object[] {
                        id});
            return ((MessageStatusCode)(results[0]));
        }

        public System.IAsyncResult BeginGetMessageStatus(string id, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("GetMessageStatus", new object[] {
                        id}, callback, asyncState);
        }

        public MessageStatusCode EndGetMessageStatus(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((MessageStatusCode)(results[0]));
        }

        public void GetMessageStatusAsync(string id) {
            this.GetMessageStatusAsync(id, null);
        }

        public void GetMessageStatusAsync(string id, object userState) {
            if ((this.GetMessageStatusOperationCompleted == null)) {
                this.GetMessageStatusOperationCompleted = new System.Threading.SendOrPostCallback(this.OnGetMessageStatusOperationCompleted);
            }
            this.InvokeAsync("GetMessageStatus", new object[] {
                        id}, this.GetMessageStatusOperationCompleted, userState);
        }

        private void OnGetMessageStatusOperationCompleted(object arg) {
            if ((this.GetMessageStatusCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.GetMessageStatusCompleted(this, new GetMessageStatusCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }

        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null)
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024)
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="com.esendex.ems.soapinterface")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="com.esendex.ems.soapinterface", IsNullable=false)]
    public partial class MessengerHeader : System.Web.Services.Protocols.SoapHeader {

        private string usernameField;

        private string passwordField;

        private string accountField;

        public string Username {
            get {
                return this.usernameField;
            }
            set {
                this.usernameField = value;
            }
        }

        public string Password {
            get {
                return this.passwordField;
            }
            set {
                this.passwordField = value;
            }
        }

        public string Account {
            get {
                return this.accountField;
            }
            set {
                this.accountField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="com.esendex.ems.soapinterface")]
    public partial class messagesubmission {

        private string originatorField;

        private string recipientField;

        private string bodyField;

        private MessageType typeField;

        private int validityperiodField;

        public string originator {
            get {
                return this.originatorField;
            }
            set {
                this.originatorField = value;
            }
        }

        public string recipient {
            get {
                return this.recipientField;
            }
            set {
                this.recipientField = value;
            }
        }

        public string body {
            get {
                return this.bodyField;
            }
            set {
                this.bodyField = value;
            }
        }

        public MessageType type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }

        public int validityperiod {
            get {
                return this.validityperiodField;
            }
            set {
                this.validityperiodField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="com.esendex.ems.soapinterface")]
    public enum MessageType {

        Text,

        Binary,

        SmartMessage,

        Unicode,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="com.esendex.ems.soapinterface")]
    public enum MessageStatusCode {

        Queued,

        Sent,

        Delivered,

        Failed,
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void SendMessageCompletedEventHandler(object sender, SendMessageCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendMessageCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal SendMessageCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState) {
            this.results = results;
        }

        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void SendMessageFullCompletedEventHandler(object sender, SendMessageFullCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendMessageFullCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal SendMessageFullCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState) {
            this.results = results;
        }

        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void SendMessageMultipleRecipientsCompletedEventHandler(object sender, SendMessageMultipleRecipientsCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendMessageMultipleRecipientsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal SendMessageMultipleRecipientsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState) {
            this.results = results;
        }

        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void SendMessageMultipleRecipientsFullCompletedEventHandler(object sender, SendMessageMultipleRecipientsFullCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendMessageMultipleRecipientsFullCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal SendMessageMultipleRecipientsFullCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState) {
            this.results = results;
        }

        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void SendMessageBatchCompletedEventHandler(object sender, SendMessageBatchCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendMessageBatchCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal SendMessageBatchCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState) {
            this.results = results;
        }

        public string[] Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string[])(this.results[0]));
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void GetMessageStatusCompletedEventHandler(object sender, GetMessageStatusCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class GetMessageStatusCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal GetMessageStatusCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState) {
            this.results = results;
        }

        public MessageStatusCode Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((MessageStatusCode)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591
