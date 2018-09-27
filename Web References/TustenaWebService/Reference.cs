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

namespace Digita.Tustena.TustenaWebService {
    using System.Diagnostics;
    using System.Web.Services;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System;
    using System.Xml.Serialization;


    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceLeadInsertSoap", Namespace="http://tempuri.org/")]
    public partial class ServiceLeadInsert : System.Web.Services.Protocols.SoapHttpClientProtocol {

        private AuthHeader authHeaderValueField;

        private System.Threading.SendOrPostCallback StoreLeadOperationCompleted;

        private bool useDefaultCredentialsSetExplicitly;

        public ServiceLeadInsert() {
            this.Url = "http://ws.tustena.com/leadinsert.asmx";
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }

        public AuthHeader AuthHeaderValue {
            get {
                return this.authHeaderValueField;
            }
            set {
                this.authHeaderValueField = value;
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

        public event StoreLeadCompletedEventHandler StoreLeadCompleted;

        [System.Web.Services.Protocols.SoapHeaderAttribute("AuthHeaderValue")]
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/StoreLead", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool StoreLead(
                    string Name,
                    string Surname,
                    string CompanyName,
                    string Title,
                    string Address,
                    string City,
                    string Province,
                    string ZIPCode,
                    string State,
                    string VatID,
                    string EMail,
                    string Phone,
                    string Fax,
                    string MobilePhone,
                    string Notes,
                    string userName,
                    string UserPassword) {
            object[] results = this.Invoke("StoreLead", new object[] {
                        Name,
                        Surname,
                        CompanyName,
                        Title,
                        Address,
                        City,
                        Province,
                        ZIPCode,
                        State,
                        VatID,
                        EMail,
                        Phone,
                        Fax,
                        MobilePhone,
                        Notes,
                        userName,
                        UserPassword});
            return ((bool)(results[0]));
        }

        public System.IAsyncResult BeginStoreLead(
                    string Name,
                    string Surname,
                    string CompanyName,
                    string Title,
                    string Address,
                    string City,
                    string Province,
                    string ZIPCode,
                    string State,
                    string VatID,
                    string EMail,
                    string Phone,
                    string Fax,
                    string MobilePhone,
                    string Notes,
                    string userName,
                    string UserPassword,
                    System.AsyncCallback callback,
                    object asyncState) {
            return this.BeginInvoke("StoreLead", new object[] {
                        Name,
                        Surname,
                        CompanyName,
                        Title,
                        Address,
                        City,
                        Province,
                        ZIPCode,
                        State,
                        VatID,
                        EMail,
                        Phone,
                        Fax,
                        MobilePhone,
                        Notes,
                        userName,
                        UserPassword}, callback, asyncState);
        }

        public bool EndStoreLead(System.IAsyncResult asyncResult) {
            object[] results = this.EndInvoke(asyncResult);
            return ((bool)(results[0]));
        }

        public void StoreLeadAsync(
                    string Name,
                    string Surname,
                    string CompanyName,
                    string Title,
                    string Address,
                    string City,
                    string Province,
                    string ZIPCode,
                    string State,
                    string VatID,
                    string EMail,
                    string Phone,
                    string Fax,
                    string MobilePhone,
                    string Notes,
                    string userName,
                    string UserPassword) {
            this.StoreLeadAsync(Name, Surname, CompanyName, Title, Address, City, Province, ZIPCode, State, VatID, EMail, Phone, Fax, MobilePhone, Notes, userName, UserPassword, null);
        }

        public void StoreLeadAsync(
                    string Name,
                    string Surname,
                    string CompanyName,
                    string Title,
                    string Address,
                    string City,
                    string Province,
                    string ZIPCode,
                    string State,
                    string VatID,
                    string EMail,
                    string Phone,
                    string Fax,
                    string MobilePhone,
                    string Notes,
                    string userName,
                    string UserPassword,
                    object userState) {
            if ((this.StoreLeadOperationCompleted == null)) {
                this.StoreLeadOperationCompleted = new System.Threading.SendOrPostCallback(this.OnStoreLeadOperationCompleted);
            }
            this.InvokeAsync("StoreLead", new object[] {
                        Name,
                        Surname,
                        CompanyName,
                        Title,
                        Address,
                        City,
                        Province,
                        ZIPCode,
                        State,
                        VatID,
                        EMail,
                        Phone,
                        Fax,
                        MobilePhone,
                        Notes,
                        userName,
                        UserPassword}, this.StoreLeadOperationCompleted, userState);
        }

        private void OnStoreLeadOperationCompleted(object arg) {
            if ((this.StoreLeadCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.StoreLeadCompleted(this, new StoreLeadCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://tempuri.org/")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://tempuri.org/", IsNullable=false)]
    public partial class AuthHeader : System.Web.Services.Protocols.SoapHeader {

        private string userNameField;

        private string passwordField;

        private string companyCodeField;

        public string UserName {
            get {
                return this.userNameField;
            }
            set {
                this.userNameField = value;
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

        public string CompanyCode {
            get {
                return this.companyCodeField;
            }
            set {
                this.companyCodeField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    public delegate void StoreLeadCompletedEventHandler(object sender, StoreLeadCompletedEventArgs e);

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "2.0.50727.42")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class StoreLeadCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {

        private object[] results;

        internal StoreLeadCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) :
                base(exception, cancelled, userState) {
            this.results = results;
        }

        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591
