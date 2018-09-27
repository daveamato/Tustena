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

namespace Digita.Tustena.smscontentservice {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="SendContentServiceSoap", Namespace="com.esendex.ems.soapinterface")]
    public partial class SendContentService : System.Web.Services.Protocols.SoapHttpClientProtocol {

        private MessengerHeader messengerHeaderValueField;

        private System.Threading.SendOrPostCallback SendWAPPushOperationCompleted;

        private System.Threading.SendOrPostCallback SendWAPPushFullOperationCompleted;

        private System.Threading.SendOrPostCallback SendWAPPushMultipleRecipientsOperationCompleted;

        private System.Threading.SendOrPostCallback SendWAPPushMultipleRecipientsFullOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        public SendContentService() {
            this.Url = "https://www.esendex.com/secure/messenger/soap/SendContentService.asmx";
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

        public event SendWAPPushCompletedEventHandler SendWAPPushCompleted;

        public event SendWAPPushFullCompletedEventHandler SendWAPPushFullCompleted;

        public event SendWAPPushMultipleRecipientsCompletedEventHandler SendWAPPushMultipleRecipientsCompleted;

        public event SendWAPPushMultipleRecipientsFullCompletedEventHandler SendWAPPushMultipleRecipientsFullCompleted;

        [System.Web.Services.Protocols.SoapHeaderAttribute("MessengerHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("com.esendex.ems.soapinterface/SendWAPPush", RequestNamespace="com.esendex.ems.soapinterface", ResponseNamespace="com.esendex.ems.soapinterface", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SendWAPPush(string recipient, string href, string text) {
            object[] results = this.Invoke("SendWAPPush", new object[] {
                        recipient,
                        href,
                        text});
            return ((string)(results[0]));
        }

        public System.IAsyncResult BeginSendWAPPush(string recipient, string href, string text, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendWAPPush", new object[] {
                        recipient,
                        href,
                        text}, callback, asyncState);
        }

        public string EndSendWAPPush(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        public void SendWAPPushAsync(string recipient, string href, string text) {
            this.SendWAPPushAsync(recipient, href, text, null);
        }

        public void SendWAPPushAsync(string recipient, string href, string text, object userState) {
            if ((this.SendWAPPushOperationCompleted == null)) {
                this.SendWAPPushOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendWAPPushOperationCompleted);
            }
            this.InvokeAsync("SendWAPPush", new object[] {
                        recipient,
                        href,
                        text}, this.SendWAPPushOperationCompleted, userState);
        }

        private void OnSendWAPPushOperationCompleted(object arg) {
            if ((this.SendWAPPushCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendWAPPushCompleted(this, new SendWAPPushCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        [System.Web.Services.Protocols.SoapHeaderAttribute("MessengerHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("com.esendex.ems.soapinterface/SendWAPPushFull", RequestNamespace="com.esendex.ems.soapinterface", ResponseNamespace="com.esendex.ems.soapinterface", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string SendWAPPushFull(string originator, string recipient, string href, string text, int validityperiod) {
            object[] results = this.Invoke("SendWAPPushFull", new object[] {
                        originator,
                        recipient,
                        href,
                        text,
                        validityperiod});
            return ((string)(results[0]));
        }

        public System.IAsyncResult BeginSendWAPPushFull(string originator, string recipient, string href, string text, int validityperiod, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendWAPPushFull", new object[] {
                        originator,
                        recipient,
                        href,
                        text,
                        validityperiod}, callback, asyncState);
        }

        public string EndSendWAPPushFull(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string)(results[0]));
        }

        public void SendWAPPushFullAsync(string originator, string recipient, string href, string text, int validityperiod) {
            this.SendWAPPushFullAsync(originator, recipient, href, text, validityperiod, null);
        }

        public void SendWAPPushFullAsync(string originator, string recipient, string href, string text, int validityperiod, object userState) {
            if ((this.SendWAPPushFullOperationCompleted == null)) {
                this.SendWAPPushFullOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendWAPPushFullOperationCompleted);
            }
            this.InvokeAsync("SendWAPPushFull", new object[] {
                        originator,
                        recipient,
                        href,
                        text,
                        validityperiod}, this.SendWAPPushFullOperationCompleted, userState);
        }

        private void OnSendWAPPushFullOperationCompleted(object arg) {
            if ((this.SendWAPPushFullCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendWAPPushFullCompleted(this, new SendWAPPushFullCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        [System.Web.Services.Protocols.SoapHeaderAttribute("MessengerHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("com.esendex.ems.soapinterface/SendWAPPushMultipleRecipients", RequestNamespace="com.esendex.ems.soapinterface", ResponseNamespace="com.esendex.ems.soapinterface", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] SendWAPPushMultipleRecipients(string[] recipients, string href, string text) {
            object[] results = this.Invoke("SendWAPPushMultipleRecipients", new object[] {
                        recipients,
                        href,
                        text});
            return ((string[])(results[0]));
        }

        public System.IAsyncResult BeginSendWAPPushMultipleRecipients(string[] recipients, string href, string text, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendWAPPushMultipleRecipients", new object[] {
                        recipients,
                        href,
                        text}, callback, asyncState);
        }

        public string[] EndSendWAPPushMultipleRecipients(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        public void SendWAPPushMultipleRecipientsAsync(string[] recipients, string href, string text) {
            this.SendWAPPushMultipleRecipientsAsync(recipients, href, text, null);
        }

        public void SendWAPPushMultipleRecipientsAsync(string[] recipients, string href, string text, object userState) {
            if ((this.SendWAPPushMultipleRecipientsOperationCompleted == null)) {
                this.SendWAPPushMultipleRecipientsOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendWAPPushMultipleRecipientsOperationCompleted);
            }
            this.InvokeAsync("SendWAPPushMultipleRecipients", new object[] {
                        recipients,
                        href,
                        text}, this.SendWAPPushMultipleRecipientsOperationCompleted, userState);
        }

        private void OnSendWAPPushMultipleRecipientsOperationCompleted(object arg) {
            if ((this.SendWAPPushMultipleRecipientsCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendWAPPushMultipleRecipientsCompleted(this, new SendWAPPushMultipleRecipientsCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }

        [System.Web.Services.Protocols.SoapHeaderAttribute("MessengerHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("com.esendex.ems.soapinterface/SendWAPPushMultipleRecipientsFull", RequestNamespace="com.esendex.ems.soapinterface", ResponseNamespace="com.esendex.ems.soapinterface", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string[] SendWAPPushMultipleRecipientsFull(string originator, string[] recipients, string href, string text, int validityPeriod) {
            object[] results = this.Invoke("SendWAPPushMultipleRecipientsFull", new object[] {
                        originator,
                        recipients,
                        href,
                        text,
                        validityPeriod});
            return ((string[])(results[0]));
        }

        public System.IAsyncResult BeginSendWAPPushMultipleRecipientsFull(string originator, string[] recipients, string href, string text, int validityPeriod, System.AsyncCallback callback, object asyncState) {
            return this.BeginInvoke("SendWAPPushMultipleRecipientsFull", new object[] {
                        originator,
                        recipients,
                        href,
                        text,
                        validityPeriod}, callback, asyncState);
        }

        public string[] EndSendWAPPushMultipleRecipientsFull(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((string[])(results[0]));
        }

        public void SendWAPPushMultipleRecipientsFullAsync(string originator, string[] recipients, string href, string text, int validityPeriod) {
            this.SendWAPPushMultipleRecipientsFullAsync(originator, recipients, href, text, validityPeriod, null);
        }

        public void SendWAPPushMultipleRecipientsFullAsync(string originator, string[] recipients, string href, string text, int validityPeriod, object userState) {
            if ((this.SendWAPPushMultipleRecipientsFullOperationCompleted == null)) {
                this.SendWAPPushMultipleRecipientsFullOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSendWAPPushMultipleRecipientsFullOperationCompleted);
            }
            this.InvokeAsync("SendWAPPushMultipleRecipientsFull", new object[] {
                        originator,
                        recipients,
                        href,
                        text,
                        validityPeriod}, this.SendWAPPushMultipleRecipientsFullOperationCompleted, userState);
        }

        private void OnSendWAPPushMultipleRecipientsFullOperationCompleted(object arg) {
            if ((this.SendWAPPushMultipleRecipientsFullCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SendWAPPushMultipleRecipientsFullCompleted(this, new SendWAPPushMultipleRecipientsFullCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void SendWAPPushCompletedEventHandler(object sender, SendWAPPushCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendWAPPushCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal SendWAPPushCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
    public delegate void SendWAPPushFullCompletedEventHandler(object sender, SendWAPPushFullCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendWAPPushFullCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal SendWAPPushFullCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
    public delegate void SendWAPPushMultipleRecipientsCompletedEventHandler(object sender, SendWAPPushMultipleRecipientsCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendWAPPushMultipleRecipientsCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal SendWAPPushMultipleRecipientsCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
    public delegate void SendWAPPushMultipleRecipientsFullCompletedEventHandler(object sender, SendWAPPushMultipleRecipientsFullCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SendWAPPushMultipleRecipientsFullCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal SendWAPPushMultipleRecipientsFullCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
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
}

#pragma warning restore 1591
