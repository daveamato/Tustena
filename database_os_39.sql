SET LANGUAGE us_english
go

begin transaction
go

CREATE TABLE [dbo].[ACCOUNT] (
	[UID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[USERACCOUNT] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,
	[PASSWORD] [nvarchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,
	[ACCESSLEVEL] [int] NOT NULL ,
	[ACTIVE] [int] NOT NULL ,
	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[SURNAME] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[ISMANAGER] [bit] NOT NULL ,
	[ISEMPLOYEE] [bit] NOT NULL ,
	[OFFICEID] [int] NOT NULL ,
	[MANAGERID] [int] NOT NULL ,
	[WORKSTART_1] [smalldatetime] NULL ,
	[WORKEND_1] [smalldatetime] NULL ,
	[WORKSTART_2] [smalldatetime] NULL ,
	[WORKEND_2] [smalldatetime] NULL ,
	[WORKDAYS] [tinyint] NOT NULL ,
	[GROUPID] [smallint] NULL ,
	[SELFCONTACTID] [int] NOT NULL ,
	[DIARYACCOUNT] [varchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[STRETCHMENU] [bit] NOT NULL ,
	[OFFICEACCOUNT] [varchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TIMEZONE] [char] (4) COLLATE Latin1_General_CI_AS NOT NULL ,
	[ENABLEPERSCONTACT] [bit] NOT NULL ,
	[PERSISTLOGIN] [bit] NOT NULL ,
	[PERSISTCODE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[FLAGNOTIFYAPPOINTMENT] [bit] NOT NULL ,
	[PAGING] [tinyint] NOT NULL ,
	[FULLSCREEN] [bit] NOT NULL ,
	[VIEWBIRTHDATE] [bit] NOT NULL ,
	[CULTURE] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[STATE] [bit] NOT NULL ,
	[LASTLOGIN] [datetime] NOT NULL ,
	[SESSIONTIMEOUT] [int] NOT NULL ,
	[INSERTGROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[MAILSERVER] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[MAILUSER] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[MAILPASSWORD] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[FIRSTDAYOFWEEK] [bit] NOT NULL ,
	[NOTIFYEMAIL] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[OTHERGROUPS] [varchar] (500) COLLATE Latin1_General_CI_AS NULL ,
	[TIMEZONEINDEX] [int] NOT NULL ,
	[ZONES] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[LASTNEWS] [smalldatetime] NOT NULL ,
	[LISTPRICE] [bigint] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ACTIVITYMOVELOG] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ACID] [bigint] NOT NULL ,
	[ACTIONTYPE] [tinyint] NOT NULL ,
	[PREVVALUE] [nvarchar] (500) COLLATE Latin1_General_CI_AS NULL ,
	[NEXTVALUE] [nvarchar] (500) COLLATE Latin1_General_CI_AS NULL ,
	[MOVEDATE] [datetime] NOT NULL ,
	[OWNERID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ADDEDFIELDS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TABLENAME] [tinyint] NOT NULL ,
	[NAME] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TYPE] [tinyint] NOT NULL ,
	[ITEMS] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[PARENTFIELD] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[PARENTFIELDVALUE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[VIEWORDER] [int] NOT NULL ,
	[PARENTQUERY] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ADDEDFIELDS_CROSS] (
	[PKEY] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ID] [bigint] NOT NULL ,
	[IDRIF] [bigint] NOT NULL ,
	[FIELDVAL] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ADDEDFIELD_DROPDOWN] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[REFERENCETABLE] [tinyint] NOT NULL ,
	[RMVALUE] [int] NOT NULL ,
	[REFERENCEFIELD] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[SONTABLE] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[SONFIELDTXT] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[SONFIELDVALUE] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[SONFIELDPARAM] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[SONFIELDPARAMVALUE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[QUERYTYPE] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[BASE_CALENDAR] (
	[CONFIRMATION] [smallint] NOT NULL ,
	[STARTDATE] [datetime] NULL ,
	[PLACE] [bit] NOT NULL ,
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[NOTE] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[ENDDATE] [datetime] NULL ,
	[CONTACT] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[COMPANY] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[UID] [int] NULL ,
	[UIDINS] [int] NULL ,
	[IDCRMTASK] [int] NULL ,
	[ROOM] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[ADDRESS] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CITY] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CAP] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[RECURRID] [int] NOT NULL ,
	[REMINDER] [datetime] NULL ,
	[SECONDUID] [int] NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[PHONE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[COMPANYID] [bigint] NULL ,
	[CONTACTID] [bigint] NULL ,
	[SENCONDIDOWNER] [bigint] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[BASE_COMPANIES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[COMPANYNAME] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[COMPANYCODE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CONTACTTYPEID] [int] NULL ,
	[COMPANYTYPEID] [int] NULL ,
	[BILLED] [money] NOT NULL ,
	[ESTIMATE] [int] NULL ,
	[PHONE] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[FAX] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[EMAIL] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[WEBSITE] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[EMPLOYEES] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[INVOICINGADDRESS] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[INVOICINGCITY] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[INVOICINGSTATEPROVINCE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[INVOICINGSTATE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[INVOICINGZIPCODE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[SHIPMENTADDRESS] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[SHIPMENTCITY] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[SHIPMENTSTATEPROVINCE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[SHIPMENTSTATE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[SHIPMENTZIPCODE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[SHIPMENTPHONE] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[SHIPMENTFAX] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[SHIPMENTEMAIL] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[WAREHOUSEADDRESS] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[WAREHOUSECITY] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[WAREHOUSESTATEPROVINCE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[WAREHOUSESTATE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[WAREHOUSEZIPCODE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[WAREHOUSEPHONE] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[WAREHOUSEFAX] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[WAREHOUSEEMAIL] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[DESCRIPTION] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[REFERRING] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[OWNERID] [int] NOT NULL ,
	[FLAGGLOBALORPERSONAL] [tinyint] NOT NULL ,
	[LASTACTIVITY] [tinyint] NOT NULL ,
	[LIMBO] [bit] NOT NULL ,
	[INSERTFROMCRM] [bit] NOT NULL ,
	[EVALUATION] [int] NOT NULL ,
	[CATEGORIES] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[COMPETITOR] [bit] NOT NULL ,
	[MLEMAIL] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[MLFLAG] [bit] NOT NULL ,
	[FROMLEAD] [bigint] NULL ,
	[TAXID] [varchar] (25) COLLATE Latin1_General_CI_AS NULL ,
	[VATID] [char] (20) COLLATE Latin1_General_CI_AS NULL ,
	[LASTCONTACT] [smalldatetime] NULL ,
	[NOCONTACT] [bit] NOT NULL ,
	[COMPANYNAMEFILTERED] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[COMMERCIALZONE] [bigint] NOT NULL ,
	[SALESPERSONID] [bigint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[BASE_CONTACTS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[COMPANYID] [int] NULL ,
	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[SURNAME] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[TITLE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[ADDRESS_1] [nvarchar] (150) COLLATE Latin1_General_CI_AS NULL ,
	[ADDRESS_2] [nvarchar] (150) COLLATE Latin1_General_CI_AS NULL ,
	[CITY_1] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CITY_2] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE_1] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE_2] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[ZIPCODE_1] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[ZIPCODE_2] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[STATE_1] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[STATE_2] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[VATID] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[TAXIDENTIFICATIONNUMBER] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[EMAIL] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[PHONE_1] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[PHONE_2] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[FAX] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[MOBILEPHONE_1] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[MOBILEPHONE_2] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[BUSINESSROLE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[SKYPE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[NOTES] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[ACTIVE] [bit] NOT NULL ,
	[OWNERID] [int] NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[OTHERCOMPANYID] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[FLAGGLOBALORPERSONAL] [tinyint] NOT NULL ,
	[BIRTHDAY] [smalldatetime] NULL ,
	[LASTACTIVITY] [tinyint] NOT NULL ,
	[LIMBO] [bit] NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[BIRTHPLACE] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[CATEGORIES] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[MLEMAIL] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[MLFLAG] [bit] NOT NULL ,
	[FROMLEAD] [bigint] NULL ,
	[SEX] [bit] NULL ,
	[LASTCONTACT] [smalldatetime] NULL ,
	[NOCONTACT] [bit] NOT NULL ,
	[COMMERCIALZONE] [bigint] NOT NULL ,
	[SALESPERSONID] [bigint] NULL ,
	[CODE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[BASE_CONTACTS_LINKS] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[LINKNAME] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LINKURL] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[COMPANYID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[BASE_EVENTS] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[STARTDATE] [datetime] NOT NULL ,
	[TITLE] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[NOTE] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[UID] [int] NOT NULL ,
	[GROUPS] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[GLOBALEVENT] [bit] NOT NULL ,
	[RECURRID] [int] NOT NULL ,
	[REMINDER] [datetime] NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[BASE_MESSAGES] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[FILE0] [varchar] (250) COLLATE Latin1_General_CI_AS NULL ,
	[FILE1] [varchar] (250) COLLATE Latin1_General_CI_AS NULL ,
	[FILE2] [varchar] (250) COLLATE Latin1_General_CI_AS NULL ,
	[FROMACCOUNT] [int] NOT NULL ,
	[SUBJECT] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
	[BODY] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[TOACCOUNT] [int] NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[DELETESENDER] [bit] NOT NULL ,
	[DELETERECEIVING] [bit] NOT NULL ,
	[READED] [bit] NOT NULL ,
	[INOUT] [bit] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[BASE_NOTES] (
	[OWNERID] [int] NOT NULL ,
	[FLAGGLOBAL] [bit] NOT NULL ,
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[BODY] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[SUBJECT] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CATALOGCATEGORIES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[PARENTID] [bigint] NOT NULL ,
	[DESCRIPTION] [nvarchar] (2000) COLLATE Latin1_General_CI_AS NULL ,
	[EMAILOWNER] [varchar] (200) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CATALOGPRICELISTDESCRIPTION] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[PERCENTAGE] [tinyint] NOT NULL ,
	[INCREASE] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CATALOGPRODUCTPRICE] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[PRODUCTID] [bigint] NOT NULL ,
	[LISTID] [bigint] NOT NULL ,
	[UNITPRICE] [decimal](18, 4) NOT NULL ,
	[COST] [decimal](18, 4) NOT NULL ,
	[VAT] [decimal](18, 4) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CATALOGPRODUCTS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[CATEGORY] [bigint] NOT NULL ,
	[CODE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[SHORTDESCRIPTION] [nvarchar] (2000) COLLATE Latin1_General_CI_AS NULL ,
	[LONGDESCRIPTION] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[UNIT] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[QTA] [decimal](10, 4) NOT NULL ,
	[QTABLISTER] [decimal](10, 4) NOT NULL ,
	[UNITPRICE] [decimal](19, 4) NOT NULL ,
	[VAT] [decimal](19, 4) NULL ,
	[ACTIVE] [bit] NOT NULL ,
	[IMAGE] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[DOCUMENT] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[COST] [decimal](18, 4) NOT NULL ,
	[PUBLISH] [bit] NOT NULL ,
	[PRICEEXPIRE] [datetime] NULL ,
	[OWNERID] [bigint] NULL ,
	[PRINTDESCRIPTION] [bit] NOT NULL ,
	[STOCK] [decimal](18, 4) NOT NULL ,
	[EXCLUDELIST] [varchar] (500) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CELEBRATION] (
	[DAYNAME] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CELDATE] [char] (5) COLLATE Latin1_General_CI_AS NOT NULL ,
	[NATION] [char] (2) COLLATE Latin1_General_CI_AS NOT NULL ,
	[YEARS] [int] NOT NULL ,
	[ID] [int] IDENTITY (1, 1) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[COMPANYMENU] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[MENUID] [int] NOT NULL ,
	[ACCESSGROUP] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[COMPANYTYPE] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[K_ID] [int] NOT NULL ,
	[DESCRIPTION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TYPE] [smallint] NOT NULL ,
	[LANG] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CONTACTESTIMATE] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[ESTIMATE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[FIELDORDER] [smallint] NOT NULL ,
	[K_ID] [int] NOT NULL ,
	[LANG] [varchar] (2) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CONTACTTYPE] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[K_ID] [int] NULL ,
	[CONTACTTYPE] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LANG] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[COUNTRY] (
	[CODE2] [char] (2) COLLATE Latin1_General_CI_AS NULL ,
	[CODE3] [char] (3) COLLATE Latin1_General_CI_AS NULL ,
	[NUM] [char] (3) COLLATE Latin1_General_CI_AS NULL ,
	[NAME_EN] [varchar] (255) COLLATE Latin1_General_CI_AS NULL ,
	[NAME_IT] [varchar] (255) COLLATE Latin1_General_CI_AS NULL ,
	[NAME_ES] [varchar] (255) COLLATE Latin1_General_CI_AS NULL ,
	[NAME_FR] [varchar] (255) COLLATE Latin1_General_CI_AS NULL ,
	[NAME_DE] [varchar] (255) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRMWORKINGCLASSIFICATION] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[TYPE] [int] NOT NULL ,
	[DESCRIPTION] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[DROPPOSITION] [smallint] NOT NULL ,
	[LANG] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_ACTIVITYTYPE] (
	[ID] [smallint] NOT NULL ,
	[K_ID] [smallint] NOT NULL ,
	[DESCRIPTION] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LANG] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_ADDRESSES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ADDRESS] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[CITY] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[STATE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[ZIPCODE] [nvarchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[TYPE] [tinyint] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_BILL] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[COMPANYID] [bigint] NOT NULL ,
	[BILLINGDATE] [smalldatetime] NOT NULL ,
	[BILLNUMBER] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TOTALPRICE] [money] NOT NULL ,
	[PAYMENT] [bit] NOT NULL ,
	[PAYMENTDATE] [smalldatetime] NULL ,
	[NROWS] [smallint] NOT NULL ,
	[NOTE] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[EXPIRATIONDATE] [smalldatetime] NULL ,
	[OWNERID] [bigint] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_BILLROWS] (
	[ID] [bigint] IDENTITY (1, 1) NOT FOR REPLICATION  NOT NULL ,
	[BILLID] [bigint] NOT NULL ,
	[DESCRIPTION] [nvarchar] (1500) COLLATE Latin1_General_CI_AS NOT NULL ,
	[UNITPRICE] [money] NULL ,
	[LISTPRICE] [money] NULL ,
	[TAX] [float] NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[PRODUCTID] [bigint] NOT NULL ,
	[UM] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[QTA] [int] NULL ,
	[FINALPRICE] [money] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_COMPETITORPRODUCTS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[COMPETITORID] [bigint] NOT NULL ,
	[PRODUCTNAME] [nvarchar] (150) COLLATE Latin1_General_CI_AS NOT NULL ,
	[PRODUCTDESCRIPTION] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[PRICE] [decimal](19, 4) NOT NULL ,
	[UNITPRICE] [decimal](19, 4) NULL ,
	[PACKAGE] [int] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_CONTACTCATEGORIES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[FLAGPERSONAL] [bit] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_CROSSCOMPANYPRODUCT] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[PRODUCTID] [bigint] NOT NULL ,
	[COMPANYID] [bigint] NOT NULL ,
	[RELATION] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_CROSSCONTACTCOMPETITOR] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[COMPETITORID] [bigint] NOT NULL ,
	[CONTACTID] [bigint] NOT NULL ,
	[RELATION] [nvarchar] (4000) COLLATE Latin1_General_CI_AS NULL ,
	[CONTACTTYPE] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_CROSSLEAD] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[LEADID] [bigint] NOT NULL ,
	[ASSOCIATEDCOMPANY] [bigint] NULL ,
	[ASSOCIATEDCONTACT] [bigint] NULL ,
	[ASSOCIATEDOPPORTUNITY] [bigint] NULL ,
	[LEADOWNER] [bigint] NULL ,
	[STATUS] [int] NULL ,
	[RATING] [int] NULL ,
	[PRODUCTINTEREST] [int] NULL ,
	[POTENTIALREVENUE] [money] NULL ,
	[ESTIMATEDCLOSEDATE] [smalldatetime] NULL ,
	[LEADCURRENCY] [int] NULL ,
	[SOURCE] [int] NULL ,
	[CAMPAIGN] [int] NULL ,
	[INDUSTRY] [int] NULL ,
	[SALESPERSON] [bigint] NULL ,
	[OTHEROPPORTUNIES] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_CROSSOPPORTUNITY] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[OPPORTUNITYID] [bigint] NOT NULL ,
	[CONTACTID] [bigint] NOT NULL ,
	[TABLETYPEID] [int] NOT NULL ,
	[TYPE] [int] NOT NULL ,
	[CONTACTTYPE] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_CROSSOPPORTUNITYREFERRING] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[REFERRERID] [bigint] NOT NULL ,
	[COMPANYID] [bigint] NOT NULL ,
	[OPPORTUNITYID] [bigint] NOT NULL ,
	[ROLE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[PERCDECISIONAL] [smallint] NULL ,
	[CHARACTERTEXT] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[NOTE] [ntext] COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_LEADDESCRIPTION] (
	[ID] [int] NOT NULL ,
	[K_ID] [int] NOT NULL ,
	[DESCRIPTION] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LANG] [varchar] (2) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TYPE] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_LEADS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[COMPANYID] [int] NULL ,
	[COMPANYNAME] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[NAME] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[SURNAME] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[TITLE] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[ADDRESS] [nvarchar] (150) COLLATE Latin1_General_CI_AS NULL ,
	[CITY] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[ZIPCODE] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[STATE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[VATID] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[TAXIDENTIFICATIONNUMBER] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[EMAIL] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[PHONE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[FAX] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[MOBILEPHONE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[BUSINESSROLE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[NOTES] [text] COLLATE Latin1_General_CI_AS NULL ,
	[OWNERID] [int] NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LASTACTIVITY] [tinyint] NOT NULL ,
	[LIMBO] [bit] NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[BIRTHPLACE] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[CATEGORIES] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[BIRTHDAY] [smalldatetime] NULL ,
	[ACTIVE] [bit] NOT NULL ,
	[LASTCONTACT] [smalldatetime] NULL ,
	[NOCONTACT] [bit] NOT NULL ,
	[WEBSITE] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[COMMERCIALZONE] [bigint] NOT NULL ,
	[CODE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_OPPLOSTREASONS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_OPPORTUNITY] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TITLE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[OWNERID] [bigint] NOT NULL ,
	[DESCRIPTION] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[SOURCE] [varchar] (30) COLLATE Latin1_General_CI_AS NULL ,
	[ADMINACCOUNT] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[BASICACCOUNT] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[EXPECTEDREVENUE] [float] NOT NULL ,
	[INCOMEPROBABILITY] [float] NOT NULL ,
	[AMOUNTCLOSED] [float] NOT NULL ,
	[CURRENCY] [int] NOT NULL ,
	[CURRENCYCHANGE] [float] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_OPPORTUNITYCOMPETITOR] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[OPPORTUNITYID] [bigint] NOT NULL ,
	[COMPETITORID] [bigint] NOT NULL ,
	[EVALUATION] [smallint] NOT NULL ,
	[DESCPRODUCT] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[STRENGTHS] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[WEAKNESSES] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[NOTE] [ntext] COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_OPPORTUNITYCOMPETITORPRODUCTS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[OPPORTUNITYID] [bigint] NOT NULL ,
	[COMPETITORID] [bigint] NOT NULL ,
	[PRODUCTID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_OPPORTUNITYCONTACT] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[OPPORTUNITYID] [bigint] NOT NULL ,
	[CONTACTID] [bigint] NOT NULL ,
	[NOTE] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[EXPECTEDREVENUE] [float] NOT NULL ,
	[INCOMEPROBABILITY] [float] NOT NULL ,
	[AMOUNTCLOSED] [float] NOT NULL ,
	[CONTACTTYPE] [tinyint] NOT NULL ,
	[STARTDATE] [smalldatetime] NOT NULL ,
	[ESTIMATEDCLOSEDATE] [smalldatetime] NOT NULL ,
	[ENDDATE] [smalldatetime] NULL ,
	[SALESPERSON] [bigint] NOT NULL ,
	[LOSTREASON] [bigint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_OPPORTUNITYCUSTOMTABLETYPE] (
	[K_ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ID] [int] NOT NULL ,
	[DESCRIPTION] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TYPE] [int] NOT NULL ,
	[LANG] [varchar] (5) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_OPPORTUNITYPARTNERS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[COMPANYID] [bigint] NOT NULL ,
	[OPPORTUNITYID] [bigint] NOT NULL ,
	[NOTE] [text] COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_OPPORTUNITYTABLETYPE] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[K_ID] [int] NOT NULL ,
	[DESCRIPTION] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TYPE] [int] NOT NULL ,
	[PERCENTAGE] [tinyint] NULL ,
	[LANG] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[OPTIONS] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_OPPPRODUCTROWS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[OPPORTUNITYID] [bigint] NOT NULL ,
	[CATALOGID] [bigint] NOT NULL ,
	[QTA] [int] NOT NULL ,
	[UPRICE] [money] NOT NULL ,
	[NEWUPRICE] [money] NOT NULL ,
	[DESCRIPTION] [nvarchar] (3900) COLLATE Latin1_General_CI_AS NULL ,
	[LEADORCOMPANYID] [bigint] NOT NULL ,
	[TYPE] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_PHASE] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[PHASE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_PRODUCTSCAT_MATRIX] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (500) COLLATE Latin1_General_CI_AS NOT NULL ,
	[MEASUREUNIT] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_PRODUCTSGROUPS] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[FATHERID] [int] NOT NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_REFERRERCATEGORIES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[FLAGPERSONAL] [bit] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_REMINDER] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[REMINDERDATE] [smalldatetime] NOT NULL ,
	[OWNERID] [bigint] NOT NULL ,
	[K_ID] [bigint] NOT NULL ,
	[TABLENAME] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[OPPORTUNITYID] [bigint] NOT NULL ,
	[NOTE] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[ADVANCEREMIND] [tinyint] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_TODOLIST] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[OWNERID] [bigint] NOT NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TASK] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[EXPIRATIONDATE] [datetime] NOT NULL ,
	[OUTCOME] [nvarchar] (3900) COLLATE Latin1_General_CI_AS NULL ,
	[OPPORTUNITYID] [bigint] NULL ,
	[COMPANYID] [bigint] NULL ,
	[FLAGEXECUTED] [bit] NOT NULL ,
	[CREATEDDATE] [smalldatetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CRM_WORKACTIVITY] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[OWNERID] [int] NOT NULL ,
	[TYPE] [int] NOT NULL ,
	[REFERRERID] [int] NULL ,
	[REFERRERTXT] [nvarchar] (150) COLLATE Latin1_General_CI_AS NULL ,
	[SUBJECT] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[DESCRIPTION] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[PROPOSALID] [int] NULL ,
	[COMPANYID] [int] NULL ,
	[CALENDARID] [int] NULL ,
	[OPPORTUNITYID] [bigint] NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[INOUT] [bit] NOT NULL ,
	[ACTIVITYDATE] [smalldatetime] NOT NULL ,
	[TRACEID] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CLASSIFICATION] [int] NULL ,
	[PARENTID] [int] NOT NULL ,
	[STATE] [int] NULL ,
	[PRIORITY] [int] NULL ,
	[TOBILL] [bit] NOT NULL ,
	[COMMERCIAL] [bit] NOT NULL ,
	[TECHNICAL] [bit] NOT NULL ,
	[ALLARM] [smalldatetime] NULL ,
	[DAYSALLARM] [smallint] NULL ,
	[DESCRIPTION2] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[DURATION] [int] NOT NULL ,
	[TODO] [tinyint] NOT NULL ,
	[VISITID] [bigint] NULL ,
	[LEADID] [bigint] NULL ,
	[DOCID] [bigint] NULL ,
	[ORDERDATE] [datetime] NOT NULL ,
	[RECALLENDHOUR] [smalldatetime] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CURRENCYTABLE] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[CURRENCY] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CHANGETOEURO] [float] NOT NULL ,
	[CHANGEFROMEURO] [float] NOT NULL ,
	[CURRENCYSYMBOL] [varchar] (10) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DEFAULTVALUES] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[TABLENAME] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[DESCRIPTION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[REFID] [int] NOT NULL ,
	[LANG] [varchar] (2) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ESTIMATEDROWS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ESTIMATEID] [bigint] NOT NULL ,
	[CATALOGID] [bigint] NOT NULL ,
	[QTA] [float] NOT NULL ,
	[UPRICE] [money] NOT NULL ,
	[NEWUPRICE] [money] NOT NULL ,
	[DESCRIPTION] [nvarchar] (500) COLLATE Latin1_General_CI_AS NULL ,
	[DESCRIPTION2] [nvarchar] (2000) COLLATE Latin1_General_CI_AS NULL ,
	[REDUCTION] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ESTIMATELANGUAGES] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[LANG] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ESTIMATES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[CURRENCY] [bigint] NOT NULL ,
	[CHANGE] [float] NOT NULL ,
	[LASTMODIFIEDDATE] [smalldatetime] NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[EXPIRATIONDATE] [smalldatetime] NULL ,
	[ACTIVITYID] [bigint] NULL ,
	[REDUCTION] [tinyint] NOT NULL ,
	[STAGE] [tinyint] NOT NULL ,
	[NUMBER] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[EVENTSCHEDULER] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[EVENTYPE] [int] NULL ,
	[LASTEVENT] [smalldatetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FILECROSSTABLES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TABLENAME] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[IDRIF] [bigint] NOT NULL ,
	[IDFILE] [bigint] NOT NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FILEMANAGER] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[GUID] [uniqueidentifier] NOT NULL ,
	[FILENAME] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[SIZE] [bigint] NOT NULL ,
	[HASH] [binary] (16) NULL ,
	[REMOTE] [bit] NOT NULL ,
	[TYPE] [int] NOT NULL ,
	[DESCRIPTION] [nvarchar] (3000) COLLATE Latin1_General_CI_AS NULL ,
	[ISREVIEW] [bigint] NOT NULL ,
	[REVIEWNUMBER] [smallint] NOT NULL ,
	[OWNERID] [int] NOT NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[HAVEREVISION] [bit] NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FILESCATEGORIES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[PARENTID] [bigint] NOT NULL ,
	[DESCRIPTION] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FOLDERSIZE] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[FOLDERSIZE] [decimal](18, 0) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GROUPS] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[DEPENDENCY] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[DESCRIPTION] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[HELPMENU] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[MENUID] [int] NOT NULL ,
	[HELPFILE] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[INVOICES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ORDERID] [bigint] NOT NULL ,
	[INVOICENUMBER] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[INVOICEDATE] [datetime] NOT NULL ,
	[EXPIRATIONDATE] [datetime] NULL ,
	[PAYMENTDATE] [datetime] NULL ,
	[PAID] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBANSWER] (
	[IDANSWER] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDEXQUESTION] [bigint] NULL ,
	[DATECREATION] [datetime] NULL ,
	[IDEXCREATOR] [bigint] NULL ,
	[DATELASTMODIFICATION] [datetime] NULL ,
	[IDEXMODIFIER] [bigint] NULL ,
	[NVISIT] [int] NULL ,
	[RATING] [int] NULL ,
	[TEXT] [nvarchar] (3900) COLLATE Latin1_General_CI_AS NULL ,
	[TOTALVALUE] [int] NULL ,
	[TOTALVALUTATION] [int] NULL ,
	[INSERITO] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBCATEGORY] (
	[IDCATEGORY] [bigint] IDENTITY (1, 1) NOT NULL ,
	[CATEGORY] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[PARENT] [bigint] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBCOMMENT] (
	[IDCOMMENT] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDEXANSWER] [bigint] NULL ,
	[TEXT] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[IDEXCOMMENTATOR] [bigint] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBCORRELATEARGUMENT] (
	[IDEXANSWER] [bigint] NOT NULL ,
	[IDEXQUESTION] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBDOCUMENT] (
	[IDDOCUMENT] [bigint] IDENTITY (1, 1) NOT NULL ,
	[NAMEDOCUMENT] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[DATECREATION] [datetime] NULL ,
	[DATELASTMODIFICATION] [datetime] NULL ,
	[GUID] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[IDEXPERSON] [bigint] NULL ,
	[SIZEDOCUMENT] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBENCLOSEDOCUMENT] (
	[IDEXANSWER] [bigint] NOT NULL ,
	[IDEXDOCUMENT] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBEXTERNALLINK] (
	[IDEXANSWER] [bigint] NOT NULL ,
	[IDEXLINK] [bigint] NOT NULL ,
	[REASON] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBGROUP] (
	[IDGROUP] [bigint] IDENTITY (1, 1) NOT NULL ,
	[NAMEGROUP] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[MASTER] [varchar] (10) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBKEYWORD] (
	[IDKEY] [bigint] IDENTITY (1, 1) NOT NULL ,
	[KEYWORD] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[IDREFERENCE] [bigint] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBLINK] (
	[IDLINK] [bigint] IDENTITY (1, 1) NOT NULL ,
	[LINK] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBPERSON] (
	[IDPERSON] [bigint] IDENTITY (1, 1) NOT NULL ,
	[NAMEPERSON] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[IDEXGROUP] [bigint] NULL ,
	[PASSWORD] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[EMAIL] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBPERSONCATEGORY] (
	[IDEXPERSON] [bigint] NOT NULL ,
	[IDEXCATEGORY] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBQUESTION] (
	[IDQUESTION] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TEXT] [nvarchar] (4000) COLLATE Latin1_General_CI_AS NULL ,
	[IDEXPROMPTER] [bigint] NULL ,
	[IDFATHER] [bigint] NULL ,
	[DATEINSERT] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBQUESTIONCATEGORY] (
	[IDEXCATEGORY] [bigint] NOT NULL ,
	[IDEXQUESTION] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[KBSTORY] (
	[IDEXANSWER] [bigint] NOT NULL ,
	[IDEXPERSON] [bigint] NOT NULL ,
	[NVISITE] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LASTCONTACT] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ACCOUNT] [bigint] NOT NULL ,
	[ACTIVITYID] [bigint] NOT NULL ,
	[CROSSID] [bigint] NOT NULL ,
	[TABLEID] [bigint] NOT NULL ,
	[ACTIVITYDATE] [smalldatetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LINKS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDREF] [bigint] NOT NULL ,
	[NAME] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[URL] [varchar] (300) COLLATE Latin1_General_CI_AS NOT NULL ,
	[DYNAMIC] [bit] NOT NULL ,
	[COUNTRY] [char] (2) COLLATE Latin1_General_CI_AS NOT NULL ,
	[VIEWORDER] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LOCALUSER] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[USERNAME] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[ACCOUNTID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LOGINLOG] (
	[ID]  uniqueidentifier ROWGUIDCOL  NOT NULL ,
	[USERID] [bigint] NOT NULL ,
	[LOGINDATE] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MAILEVENTS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[EVENTTYPE] [tinyint] NOT NULL ,
	[RECURRENCEID] [bigint] NOT NULL ,
	[REFID] [bigint] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MENUMAP] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[USERID] [bigint] NOT NULL ,
	[MENUID] [int] NOT NULL ,
	[FIRSTTIME] [bit] NOT NULL ,
	[NEWHOMEPAGE] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[NEWHOMEPAGEID] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_ATTACHMENT] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[MLID] [bigint] NOT NULL ,
	[FILEID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_AUTH] (
	[ID] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TABLEID] [tinyint] NOT NULL ,
	[FIELDID] [bigint] NOT NULL ,
	[DATEAUTH] [smalldatetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_AUTHLOG] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TABLEID] [tinyint] NOT NULL ,
	[RIFID] [bigint] NOT NULL ,
	[AUTHDATE] [smalldatetime] NOT NULL ,
	[AUTHTYPE] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_CATEGORIES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[CATDESCRIPTION] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_COMPANIES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDMAILINGLIST] [bigint] NOT NULL ,
	[COMPANYNAMES] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[ADDRESS] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[CITY] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[NATION] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[ZIPCODE] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[PHONE] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[FAX] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[EMAIL] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[WEBSITE] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[CODE] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[COMPANYTYPE] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[CONTACTTYPE] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[BILLED] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[EMPLOYEES] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[ESTIMATE] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[CATEGORIES] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[OPPORTUNITY] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[OWNERID] [varchar] (10) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_CONTACTS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDMAILINGLIST] [bigint] NOT NULL ,
	[ADDRESS] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[CITY] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[NATION] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[ZIPCODE] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[CATEGORIES] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[EMAIL] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_DESCRIPTION] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[GROUPS] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[QUERY] [varchar] (3000) COLLATE Latin1_General_CI_AS NULL ,
	[SUBJECT] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[SMS] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_FIXEDPARAMS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDMAILINGLIST] [bigint] NOT NULL ,
	[COMPANY] [varchar] (2000) COLLATE Latin1_General_CI_AS NULL ,
	[CONTACT] [varchar] (2000) COLLATE Latin1_General_CI_AS NULL ,
	[LEAD] [varchar] (2000) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_LEAD] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDMAILINGLIST] [bigint] NOT NULL ,
	[ADDRESS] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[CITY] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[NATION] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[ZIPCODE] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[CATEGORIES] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[EMAIL] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[OPPORTUNITY] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[OWNERID] [varchar] (10) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_LOG] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[LISTID] [bigint] NOT NULL ,
	[MAILID] [bigint] NOT NULL ,
	[MAILNUMBER] [int] NOT NULL ,
	[SENDDATE] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_MAIL] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TITLE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[DESCRIPTION] [nvarchar] (150) COLLATE Latin1_General_CI_AS NULL ,
	[SUBJECT] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[BODY] [text] COLLATE Latin1_General_CI_AS NOT NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[CATEGORYID] [int] NOT NULL ,
	[WELCOME] [bit] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[ML_REMOVEDFROM] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDML] [bigint] NOT NULL ,
	[IDREF] [bigint] NOT NULL ,
	[TYPE] [tinyint] NOT NULL ,
	[ABUSE] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[NEWMENU] (
	[ID] [int] NOT NULL ,
	[RMVALUE] [int] NOT NULL ,
	[VOICE] [varchar] (25) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LINK] [varchar] (150) COLLATE Latin1_General_CI_AS NULL ,
	[LASTMENU] [bit] NOT NULL ,
	[PARENTMENU] [int] NOT NULL ,
	[SORTORDER] [int] NOT NULL ,
	[ACCESSGROUP] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[MENUTITLE] [bit] NOT NULL ,
	[ACTIVE] [bit] NOT NULL ,
	[FOLDER] [varchar] (20) COLLATE Latin1_General_CI_AS NULL ,
	[MODE] [bit] NOT NULL ,
	[MODULES] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OFFICES] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[OFFICE] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ORDERDOCUMENT] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ORDERID] [bigint] NOT NULL ,
	[DOCUMENTID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ORDERROWS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ORDERID] [bigint] NOT NULL ,
	[CATALOGID] [bigint] NOT NULL ,
	[QTA] [float] NOT NULL ,
	[UPRICE] [decimal](19, 4) NOT NULL ,
	[NEWUPRICE] [decimal](19, 4) NOT NULL ,
	[DESCRIPTION] [nvarchar] (500) COLLATE Latin1_General_CI_AS NULL ,
	[DESCRIPTION2] [nvarchar] (2000) COLLATE Latin1_General_CI_AS NULL ,
	[REDUCTION] [int] NULL ,
	[TAX] [decimal](19, 4) NULL ,
	[COST] [decimal](18, 4) NULL ,
	[UNITMEASURE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[LISTPRICE] [decimal](18, 4) NULL ,
	[PRODUCTCODE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[REALLISTPRICE] [decimal](18, 0) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ORDERS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [text] COLLATE Latin1_General_CI_AS NULL ,
	[CURRENCY] [bigint] NOT NULL ,
	[CHANGE] [float] NOT NULL ,
	[LASTMODIFIEDDATE] [smalldatetime] NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[EXPIRATIONDATE] [smalldatetime] NULL ,
	[ACTIVITYID] [bigint] NULL ,
	[REDUCTION] [tinyint] NOT NULL ,
	[STAGE] [tinyint] NOT NULL ,
	[NUMBER] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[PAYMENTID] [bigint] NOT NULL ,
	[OWNERID] [bigint] NOT NULL ,
	[CROSSID] [bigint] NOT NULL ,
	[CROSSTYPE] [tinyint] NULL ,
	[ADDRESS] [nvarchar] (1200) COLLATE Latin1_General_CI_AS NULL ,
	[SHIPADDRESS] [nvarchar] (1200) COLLATE Latin1_General_CI_AS NULL ,
	[CITY] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[NATION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[ZIPCODE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[GRANDTOTAL] [decimal](18, 4) NOT NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[SUBJECT] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[SUBTOTAL] [decimal](18, 4) NULL ,
	[TAXTOTAL] [decimal](18, 4) NULL ,
	[SHIP] [decimal](18, 4) NULL ,
	[MANAGERID] [bigint] NULL ,
	[SIGNALER] [bigint] NULL ,
	[SHIPVAT] [decimal](18, 4) NULL ,
	[INCLUDEPRODPDF] [bit] NOT NULL ,
	[QUOTEDATE] [datetime] NOT NULL ,
	[SHIPDATE] [datetime] NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[ORIGINALQUOTE] [bigint] NOT NULL ,
	[SHIPID] [bigint] NULL ,
	[LIST] [bigint] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[PAYMENTLIST] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[VIEWORDER] [int] NOT NULL ,
	[LANG] [varchar] (3) COLLATE Latin1_General_CI_AS NOT NULL ,
	[K_ID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PRODUCTCHARACTERISTICS] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[PRODUCTID] [int] NOT NULL ,
	[CHARACTERISTICSID] [int] NOT NULL ,
	[VALUE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PRODUCTS] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[PRODUCTCODE] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[DESCRIPTION] [nvarchar] (3900) COLLATE Latin1_General_CI_AS NULL ,
	[PRODUCTGROUPSID] [int] NOT NULL ,
	[SUPPLIERID] [int] NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_ALL_FIELDS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[RMVALUE] [int] NOT NULL ,
	[FIELD] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[FIELDTYPE] [tinyint] NOT NULL ,
	[FLAGPARAM] [bit] NOT NULL ,
	[TABLEID] [int] NOT NULL ,
	[MATCHINGVALUE] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[JOINID] [int] NULL ,
	[PARENTFIELD] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[AGGREGATEFIELDS] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[FIELDCAT_RMVALUE] [int] NOT NULL ,
	[VIEWORDER] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_ALL_TABLES] (
	[ID] [int] NOT NULL ,
	[RMVALUE] [smallint] NULL ,
	[TABLENAME] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[SELECTABLE] [bit] NOT NULL ,
	[FIXEDQUERY] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[PARENT] [int] NOT NULL ,
	[MODULES] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_CATEGORIES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_CUSTOMERQUERY] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[GROUPS] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[QUERYTYPE] [tinyint] NOT NULL ,
	[EXTENDEDWHERE] [varchar] (300) COLLATE Latin1_General_CI_AS NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[GROUPBY] [varchar] (500) COLLATE Latin1_General_CI_AS NULL ,
	[TITLE] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[FROMWAP] [int] NULL ,
	[CATEGORY] [bigint] NOT NULL ,
	[RM] [varchar] (10) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_CUSTOMERQUERYFIELDS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDQUERY] [bigint] NOT NULL ,
	[IDTABLE] [int] NOT NULL ,
	[IDFIELD] [int] NOT NULL ,
	[FIELDVISIBLE] [bit] NOT NULL ,
	[COLUMNNAME] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[OPTIONS] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_CUSTOMERQUERYFREEFIELDS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDQUERY] [bigint] NOT NULL ,
	[IDTABLE] [int] NOT NULL ,
	[IDFREEFIELD] [bigint] NOT NULL ,
	[COLUMNAME] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_CUSTOMERQUERYPARAMFIELDS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDQUERY] [bigint] NOT NULL ,
	[IDTABLE] [int] NOT NULL ,
	[IDFIELD] [int] NOT NULL ,
	[FIXEDVALUE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_CUSTOMERQUERYTABLES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDQUERY] [bigint] NOT NULL ,
	[IDTABLE] [int] NOT NULL ,
	[MAINTABLE] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_DROPDOWNPARAMS] (
	[IDRIF] [int] NOT NULL ,
	[REFTABLE] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[VALUEFIELD] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TEXTFIELD] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LANGFIELD] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[P1] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[P2] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[P3] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[P4] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[P5] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[P6] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[P7] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[P8] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_FIXEDDROPDOWNPARAMS] (
	[IDRIF] [bigint] NOT NULL ,
	[DROPVALUE] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[RMVALUE] [varchar] (20) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_FIXEDQUERY] (
	[id] [int] IDENTITY (1, 1) NOT NULL ,
	[FlagStored] [bit] NOT NULL ,
	[QueryString] [varchar] (5000) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_JOIN] (
	[ID] [int] NOT NULL ,
	[FIRSTTABLEID] [int] NOT NULL ,
	[SECONDTABLEID] [int] NOT NULL ,
	[FIRSTFIELD] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,
	[SECONDFIELD] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TYPE] [tinyint] NOT NULL ,
	[ASTABLE] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QB_LOCKARRAY] (
	[IDTABLE] [bigint] NOT NULL ,
	[LOCKTABLES] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QUICKLOG] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TABLEID] [tinyint] NOT NULL ,
	[CONTACTID] [bigint] NOT NULL ,
	[OWNERID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QUOTEDOCUMENT] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[QUOTEID] [bigint] NOT NULL ,
	[DOCUMENTID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QUOTENUMBERS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[NPROG] [int] NOT NULL ,
	[CHECKDAY] [bit] NOT NULL ,
	[CHECKMONTH] [bit] NOT NULL ,
	[CHECKYEAR] [bit] NOT NULL ,
	[TWODIGITYEAR] [bit] NOT NULL ,
	[CHECKCUSTOMERCODE] [bit] NOT NULL ,
	[NPROGSTART] [int] NOT NULL ,
	[NPROGRESTART] [tinyint] NOT NULL ,
	[DISABLED] [bit] NOT NULL ,
	[LASTRESET] [smalldatetime] NOT NULL ,
	[TYPE] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QUOTEROWS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[ESTIMATEID] [bigint] NOT NULL ,
	[CATALOGID] [bigint] NOT NULL ,
	[QTA] [float] NOT NULL ,
	[UPRICE] [decimal](19, 4) NOT NULL ,
	[NEWUPRICE] [decimal](19, 4) NOT NULL ,
	[DESCRIPTION] [nvarchar] (500) COLLATE Latin1_General_CI_AS NULL ,
	[DESCRIPTION2] [nvarchar] (2000) COLLATE Latin1_General_CI_AS NULL ,
	[REDUCTION] [int] NULL ,
	[TAX] [decimal](19, 4) NULL ,
	[COST] [decimal](18, 4) NULL ,
	[UNITMEASURE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[LISTPRICE] [decimal](18, 4) NULL ,
	[PRODUCTCODE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[REALLISTPRICE] [decimal](18, 0) NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[QUOTES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [text] COLLATE Latin1_General_CI_AS NULL ,
	[CURRENCY] [bigint] NOT NULL ,
	[CHANGE] [float] NOT NULL ,
	[LASTMODIFIEDDATE] [smalldatetime] NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL ,
	[EXPIRATIONDATE] [smalldatetime] NULL ,
	[ACTIVITYID] [bigint] NULL ,
	[REDUCTION] [tinyint] NOT NULL ,
	[STAGE] [tinyint] NOT NULL ,
	[NUMBER] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[PAYMENTID] [bigint] NOT NULL ,
	[OWNERID] [bigint] NOT NULL ,
	[CROSSID] [bigint] NOT NULL ,
	[CROSSTYPE] [tinyint] NULL ,
	[ADDRESS] [nvarchar] (1200) COLLATE Latin1_General_CI_AS NULL ,
	[SHIPADDRESS] [nvarchar] (1200) COLLATE Latin1_General_CI_AS NULL ,
	[CITY] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[NATION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[ZIPCODE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[GRANDTOTAL] [decimal](18, 4) NOT NULL ,
	[GROUPS] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[SUBJECT] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[SUBTOTAL] [decimal](18, 4) NULL ,
	[TAXTOTAL] [decimal](18, 4) NULL ,
	[SHIP] [decimal](18, 4) NULL ,
	[MANAGERID] [bigint] NULL ,
	[SIGNALER] [bigint] NULL ,
	[SHIPVAT] [decimal](18, 4) NULL ,
	[INCLUDEPRODPDF] [bit] NOT NULL ,
	[QUOTEDATE] [datetime] NOT NULL ,
	[SHIPDATE] [datetime] NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[SHIPID] [bigint] NULL ,
	[LIST] [bigint] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[QUOTESHIPMENT] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (500) COLLATE Latin1_General_CI_AS NOT NULL ,
	[REQUIREDDATE] [bit] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RECENT] (
	[ID] [numeric](18, 0) IDENTITY (1, 1) NOT NULL ,
	[USERID] [bigint] NOT NULL ,
	[RECID] [bigint] NOT NULL ,
	[TYPE] [tinyint] NOT NULL ,
	[LASTDATE] [datetime] NOT NULL ,
	[TEXT] [nvarchar] (500) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RECURRENCE] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[STARTDATE] [datetime] NOT NULL ,
	[ENDDATE] [datetime] NOT NULL ,
	[TYPE] [tinyint] NOT NULL ,
	[VAR1] [tinyint] NOT NULL ,
	[VAR2] [tinyint] NOT NULL ,
	[VAR3] [tinyint] NOT NULL ,
	[STANDARD] [smallint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[REPORTREFERENCE] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[TABLENAME] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[FIELDNAME] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LABEL_IT] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[LABEL_EN] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[LABEL_FR] [varchar] (100) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SCOREDESCRIPTION] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[WEIGHT] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SCORELOG] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDVALUE] [bigint] NOT NULL ,
	[VOTEDATE] [datetime] NOT NULL ,
	[OWNERID] [bigint] NOT NULL ,
	[VOTE] [smallint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SCOREVALUES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[IDDESCRIPTION] [bigint] NOT NULL ,
	[IDCROSS] [bigint] NOT NULL ,
	[TYPE] [tinyint] NOT NULL ,
	[SCOREVALUE] [int] NOT NULL ,
	[VOTENUMBER] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SURVEYDB] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[XML] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[COMPANYID] [bigint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[SURVEYRESPONSES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[COMPANYID] [char] (10) COLLATE Latin1_General_CI_AS NULL ,
	[LEADREF] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CONTACTREF] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[COMPANYREF] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[TUSTENAREF] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[RESPONSE] [varchar] (1000) COLLATE Latin1_General_CI_AS NULL ,
	[SURVEYID] [char] (10) COLLATE Latin1_General_CI_AS NULL ,
	[LASTACCESS] [datetime] NULL ,
	[SENT] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TAXVALUES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TAXVALUE] [decimal](19, 4) NOT NULL ,
	[TAXDESCRIPTION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[VIEWORDER] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TEMPLATES] (
	[TEMPLATENAME] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LANG] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[BODY] [text] COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKETNOTRESOLVEDREASON] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKET_AREA] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[AREA] [nvarchar] (500) COLLATE Latin1_General_CI_AS NULL ,
	[OWNERID] [bigint] NOT NULL ,
	[SLAID] [bigint] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKET_MAIN] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[Owner] [bigint] NOT NULL ,
	[STATUS] [tinyint] NOT NULL ,
	[OPENDATE] [datetime] NOT NULL ,
	[TIMETOFIX] [int] NOT NULL ,
	[PRIORITY] [tinyint] NOT NULL ,
	[DESCRIPTION] [text] COLLATE Latin1_General_CI_AS NULL ,
	[COMPANYID] [bigint] NULL ,
	[CONTACTID] [bigint] NULL ,
	[LEADID] [bigint] NULL ,
	[TYPE] [bigint] NULL ,
	[SHORTDESCRIPTION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[AREA] [bigint] NOT NULL ,
	[TICKETID] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[NOTRESOLVED] [bigint] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKET_MOVELOG] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TICKETID] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[ACTIONTYPE] [tinyint] NOT NULL ,
	[PREVVALUE] [nvarchar] (500) COLLATE Latin1_General_CI_AS NULL ,
	[NEXTVALUE] [nvarchar] (500) COLLATE Latin1_General_CI_AS NULL ,
	[CHANGEDATE] [datetime] NOT NULL ,
	[OWNERID] [bigint] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKET_PROGRESS] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TICKETYEAR] [int] NOT NULL ,
	[TICKETIDPROG] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKET_SCHEDULE] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[SLAID] [bigint] NOT NULL ,
	[DAYOFWEEK] [tinyint] NOT NULL ,
	[STARTMINUTE] [int] NOT NULL ,
	[ENDMINUTE] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKET_SLA] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[TITLE] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TIMETOTAKE] [int] NOT NULL ,
	[TIMETORESOLVE] [int] NOT NULL ,
	[TICKETSTATUS] [tinyint] NOT NULL ,
	[TICKETPRIORITY] [tinyint] NOT NULL ,
	[TYPE] [tinyint] NOT NULL ,
	[DEFAULTOWNER] [bigint] NULL ,
	[KEYWORDS] [nvarchar] (1000) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKET_SLA_CUSTOMER] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[SLAID] [bigint] NOT NULL ,
	[CONTACTTYPE] [tinyint] NOT NULL ,
	[CONTACTID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKET_SUPPORTLOG] (
	[TICKETID] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[DESCRIPTION] [text] COLLATE Latin1_General_CI_AS NULL ,
	[UPDATEDBY] [bigint] NULL ,
	[CONTACTID] [bigint] NULL ,
	[COMPANYID] [bigint] NULL ,
	[LEADID] [bigint] NULL ,
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[UPDATEDDATE] [datetime] NULL ,
	[TYPE] [tinyint] NULL ,
	[SUBJECT] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[EMAILTO] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[MAILSENT] [bit] NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKET_TYPE] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (200) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TICKET_USER] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[UID] [bigint] NOT NULL ,
	[TICKETAREA] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[TYPE] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[PERCREDIRECT] [tinyint] NULL ,
	[REDIRECTID] [bigint] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TIMEZONES] (
	[NAME] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[SHORTNAME] [char] (4) COLLATE Latin1_General_CI_AS NOT NULL ,
	[MTIMEZONE] [int] NOT NULL ,
	[HTIMEZONE] [float] NOT NULL ,
	[CITY] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[DTZ] [float] NOT NULL ,
	[DAYLIGHTSAVINGSTART] [datetime] NULL ,
	[DAYLIGHTSAVINGEND] [datetime] NULL ,
	[MDAYLIGHT] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TODOLIST] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[OWNERID] [int] NOT NULL ,
	[DESCRIPTION] [ntext] COLLATE Latin1_General_CI_AS NULL ,
	[PRIORITY] [smallint] NOT NULL ,
	[FLAGEXECUTED] [bit] NOT NULL ,
	[STARTDATE] [datetime] NOT NULL ,
	[EXPIRATIONDATE] [datetime] NULL ,
	[REMINDERDATE] [datetime] NULL ,
	[FLAGRECURRENCE] [int] NOT NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[CREATEDBYID] [bigint] NOT NULL ,
	[LASTMODIFIEDDATE] [datetime] NOT NULL ,
	[LASTMODIFIEDBYID] [bigint] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[TOKENS] (
	[TOKEN] [uniqueidentifier] NOT NULL ,
	[USERNAME] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,
	[PASS] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,
	[EXPIRE] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TUSTENADB] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[DBNAME] [varchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[GROUPS] [varchar] (1000) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TUSTENA_DATA] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[COMPANYNAME] [nvarchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LICENCE] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[PHONE] [varchar] (30) COLLATE Latin1_General_CI_AS NULL ,
	[FAX] [varchar] (30) COLLATE Latin1_General_CI_AS NULL ,
	[EMAIL] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[WEBSITE] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[ADDRESS] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[CITY] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[PROVINCE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[REGION] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[STATE] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[ZIPCODE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CREATEDDATE] [datetime] NOT NULL ,
	[ACTIVE] [bit] NOT NULL ,
	[TESTING] [bit] NOT NULL ,
	[MAXUSER] [int] NOT NULL ,
	[ADMINGROUPID] [bigint] NOT NULL ,
	[PHONENORMALIZE] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[CUSTOMTYPES] [bit] NOT NULL ,
	[LASTACCESS] [datetime] NOT NULL ,
	[TESTINGDAYS] [int] NOT NULL ,
	[TAXIDENTIFICATIONNUMBER] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[VATID] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[ESTIMATEDDATEDAYS] [int] NOT NULL ,
	[DEBUGMODE] [bit] NOT NULL ,
	[EXPIRATIONDATE] [datetime] NULL ,
	[FROMWEB] [bigint] NULL ,
	[GUID] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[PIN] [int] NULL ,
	[DEFAULTWEBUSER] [bigint] NULL ,
	[IDAGENDA] [bigint] NULL ,
	[DATASTORAGECAPACITY] [int] NOT NULL ,
	[SMSCREDIT] [int] NOT NULL ,
	[SMSORIGIN] [varchar] (12) COLLATE Latin1_General_CI_AS NULL ,
	[LINKFORVOIP] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[INTERNATIONALPREFIX] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[DISKSPACE] [int] NOT NULL ,
	[WIZARD] [bit] NOT NULL ,
	[LOGO] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[MODULES] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[USAGELOG] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DATELOG] [datetime] NOT NULL ,
	[DLLINFO] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[DLLHASH] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[IP] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[VERSION] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[DBVERSION] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[SWVERSION] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[MODIFIED] [bit] NOT NULL ,
	[MODIFYDATE] [datetime] NOT NULL ,
	[DLLHASH] [varchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[VIEWSTATEMANAGER] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[PAGE] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[USERID] [int] NOT NULL ,
	[SESSIONID] [varchar] (55) COLLATE Latin1_General_CI_AS NOT NULL ,
	[LASTACCESS] [datetime] NOT NULL ,
	[VIEWSTATE] [text] COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[WEBGATEPARAMS] (
	[ID] [int] IDENTITY (1, 1) NOT NULL ,
	[WEBSITE] [varchar] (250) COLLATE Latin1_General_CI_AS NOT NULL ,
	[OWNERID] [bigint] NOT NULL ,
	[GROUPS] [bigint] NOT NULL ,
	[NOTIFYID] [bigint] NOT NULL ,
	[NOTIFYBY] [bit] NOT NULL ,
	[CATEGORYID] [int] NOT NULL ,
	[PERSONALCATEGORYID] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ZONES] (
	[ID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[DESCRIPTION] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[VIEWORDER] [int] NOT NULL 
) ON [PRIMARY]
GO

/* Generated on 20060809 19:05:46 */

/* Data for table ACCOUNT */
SET identity_insert [ACCOUNT] on

INSERT [ACCOUNT] ([UID], [USERACCOUNT], [PASSWORD], [ACCESSLEVEL], [ACTIVE], [Name], [SURNAME], [ISMANAGER], [ISEMPLOYEE], [OFFICEID], [MANAGERID], [WORKSTART_1], [WORKEND_1], [WORKSTART_2], [WORKEND_2], [WORKDAYS], [GROUPID], [SELFCONTACTID], [DIARYACCOUNT], [STRETCHMENU], [OFFICEACCOUNT], [TIMEZONE], [ENABLEPERSCONTACT], [PERSISTLOGIN], [PERSISTCODE], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID], [FLAGNOTIFYAPPOINTMENT], [PAGING], [FULLSCREEN], [VIEWBIRTHDATE], [CULTURE], [STATE], [LASTLOGIN], [SESSIONTIMEOUT], [INSERTGROUPS], [MAILSERVER], [MAILUSER], [MAILPASSWORD], [FIRSTDAYOFWEEK], [NOTIFYEMAIL], [OTHERGROUPS], [TIMEZONEINDEX], [ZONES], [LASTNEWS], [LISTPRICE]) VALUES ('1', 'admin@admin.com', 'admin', 0, 1, 'Admin Name', 'Admin Surname', 1, 1, 1, 0, '20060809 9:00:00', '20060809 13:00:00', '20060809 14:00:00', '20060809 18:00:00', '0', 1, 0, '', 0, '|1|', 'GMT ', 0, 0, NULL, '20051206 13:06:58', '0', '20051206 13:06:58', '0', 1, '20', 0, 1, 'es-ES', 1, '20060809 18:58:08', 20, '|1|0|', '', '', NULL, 0, '', NULL, 110, NULL, '20060404 14:51:00', NULL)
INSERT [ACCOUNT] ([UID], [USERACCOUNT], [PASSWORD], [ACCESSLEVEL], [ACTIVE], [Name], [SURNAME], [ISMANAGER], [ISEMPLOYEE], [OFFICEID], [MANAGERID], [WORKSTART_1], [WORKEND_1], [WORKSTART_2], [WORKEND_2], [WORKDAYS], [GROUPID], [SELFCONTACTID], [DIARYACCOUNT], [STRETCHMENU], [OFFICEACCOUNT], [TIMEZONE], [ENABLEPERSCONTACT], [PERSISTLOGIN], [PERSISTCODE], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID], [FLAGNOTIFYAPPOINTMENT], [PAGING], [FULLSCREEN], [VIEWBIRTHDATE], [CULTURE], [STATE], [LASTLOGIN], [SESSIONTIMEOUT], [INSERTGROUPS], [MAILSERVER], [MAILUSER], [MAILPASSWORD], [FIRSTDAYOFWEEK], [NOTIFYEMAIL], [OTHERGROUPS], [TIMEZONEINDEX], [ZONES], [LASTNEWS], [LISTPRICE]) VALUES ('2', 'Nuevo@Usuario.com', 'nuevo', 0, 1, 'nuevo', 'usuario', 0, 0, 1, 0, '20060809 9:00:00', '20060809 13:00:00', '20060809 14:00:00', '20060809 18:00:00', '127', 1, 3, '', 0, '|', 'CET ', 0, 0, NULL, '20060809 17:32:32', '0', '20060809 17:32:32', '0', 0, '20', 0, 0, 'it-IT', 0, '20060809 17:32:32', 20, '', '', '', NULL, 0, 'Nuevo@Usuario.com', '|1|', 110, NULL, '20060809 17:33:00', NULL)
INSERT [ACCOUNT] ([UID], [USERACCOUNT], [PASSWORD], [ACCESSLEVEL], [ACTIVE], [Name], [SURNAME], [ISMANAGER], [ISEMPLOYEE], [OFFICEID], [MANAGERID], [WORKSTART_1], [WORKEND_1], [WORKSTART_2], [WORKEND_2], [WORKDAYS], [GROUPID], [SELFCONTACTID], [DIARYACCOUNT], [STRETCHMENU], [OFFICEACCOUNT], [TIMEZONE], [ENABLEPERSCONTACT], [PERSISTLOGIN], [PERSISTCODE], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID], [FLAGNOTIFYAPPOINTMENT], [PAGING], [FULLSCREEN], [VIEWBIRTHDATE], [CULTURE], [STATE], [LASTLOGIN], [SESSIONTIMEOUT], [INSERTGROUPS], [MAILSERVER], [MAILUSER], [MAILPASSWORD], [FIRSTDAYOFWEEK], [NOTIFYEMAIL], [OTHERGROUPS], [TIMEZONEINDEX], [ZONES], [LASTNEWS], [LISTPRICE]) VALUES ('3', 'a@a.com', 'aaaa', 0, 1, 'a', 'a', 0, 0, 0, 0, '20060809 9:00:00', '20060809 13:00:00', '20060809 14:00:00', '20060809 18:00:00', '127', 1, 4, '', 0, '|', 'CET ', 0, 0, NULL, '20060809 17:33:32', '0', '20060809 17:33:32', '0', 0, '20', 0, 0, 'it-IT', 1, '20060809 17:38:26', 20, '|1|0|', '', '', NULL, 0, '', NULL, 110, NULL, '20060809 17:34:00', NULL)
SET identity_insert [ACCOUNT] off
GO
/* Data for table CELEBRATION */
SET identity_insert [CELEBRATION] on

INSERT [CELEBRATION] ([DAYNAME], [CELDATE], [NATION], [YEARS], [ID]) VALUES ('Capodanno', '1/1  ', 'IT', 0, 1)
INSERT [CELEBRATION] ([DAYNAME], [CELDATE], [NATION], [YEARS], [ID]) VALUES ('Epifania', '6/1  ', 'IT', 0, 2)
INSERT [CELEBRATION] ([DAYNAME], [CELDATE], [NATION], [YEARS], [ID]) VALUES ('Anniversario della Liberazione', '25/4 ', 'IT', 0, 3)
INSERT [CELEBRATION] ([DAYNAME], [CELDATE], [NATION], [YEARS], [ID]) VALUES ('Festa dei Lavoratori', '1/5  ', 'IT', 0, 4)
INSERT [CELEBRATION] ([DAYNAME], [CELDATE], [NATION], [YEARS], [ID]) VALUES ('Festa della Repubblica', '2/6  ', 'IT', 0, 5)
INSERT [CELEBRATION] ([DAYNAME], [CELDATE], [NATION], [YEARS], [ID]) VALUES ('Ferragosto', '15/8 ', 'IT', 0, 6)
INSERT [CELEBRATION] ([DAYNAME], [CELDATE], [NATION], [YEARS], [ID]) VALUES ('Ognissanti', '1/11 ', 'IT', 0, 7)
INSERT [CELEBRATION] ([DAYNAME], [CELDATE], [NATION], [YEARS], [ID]) VALUES ('Immacolata Concezione', '8/12 ', 'IT', 0, 8)
INSERT [CELEBRATION] ([DAYNAME], [CELDATE], [NATION], [YEARS], [ID]) VALUES ('Año nuevo', '1/1  ', 'ES', 0, 9)
INSERT [CELEBRATION] ([DAYNAME], [CELDATE], [NATION], [YEARS], [ID]) VALUES ('Verano', '15/08', 'ES', 0, 10)
SET identity_insert [CELEBRATION] off
GO
/* Data for table COMPANYMENU */
SET identity_insert [COMPANYMENU] on

INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('1', 1, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('2', 2, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('3', 3, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('4', 5, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('5', 6, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('6', 7, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('7', 8, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('8', 10, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('9', 11, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('10', 12, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('11', 13, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('12', 14, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('13', 15, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('14', 16, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('15', 17, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('16', 18, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('17', 19, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('18', 20, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('19', 21, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('20', 22, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('21', 23, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('22', 24, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('23', 25, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('24', 26, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('25', 27, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('26', 28, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('27', 29, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('28', 31, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('29', 32, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('30', 33, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('31', 34, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('32', 35, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('33', 36, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('34', 37, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('35', 38, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('36', 39, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('37', 40, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('38', 41, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('39', 42, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('40', 44, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('41', 45, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('42', 46, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('43', 47, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('44', 48, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('45', 49, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('46', 51, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('47', 52, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('48', 53, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('49', 55, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('50', 56, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('51', 57, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('52', 58, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('53', 59, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('54', 61, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('55', 62, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('56', 60, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('57', 63, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('58', 54, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('59', 43, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('60', 50, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('61', 66, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('62', 67, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('63', 64, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('64', 65, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('65', 68, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('66', 69, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('67', 70, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('68', 71, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('69', 72, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('70', 74, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('71', 80, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('72', 73, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('73', 79, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('74', 77, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('75', 78, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('76', 75, '|1|')
INSERT [COMPANYMENU] ([ID], [MENUID], [ACCESSGROUP]) VALUES ('77', 76, '|1|')
SET identity_insert [COMPANYMENU] off
GO
/* Data for table COMPANYTYPE */
SET identity_insert [COMPANYTYPE] on

INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (1, 1, 'Agricoltura', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (2, 2, 'Abbigliamento', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (3, 4, 'Settore bancario', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (4, 5, 'Biotecnologia', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (5, 6, 'Chimica', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (6, 7, 'Comunicazioni', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (7, 8, 'Edilizia', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (8, 9, 'Consulenza', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (9, 10, 'Istruzione', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (10, 11, 'Elettronica', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (11, 12, 'Energia', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (12, 13, 'Ingegneria', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (13, 14, 'Intrattenimento', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (14, 15, 'Ambiente', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (15, 16, 'Finanza', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (16, 17, 'Cibi e bevande', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (17, 18, 'Enti governativi', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (18, 19, 'Sanità', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (19, 20, 'Ospitalità', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (20, 21, 'Assicurazioni', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (21, 22, 'Meccanica', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (22, 26, 'Settore manifatturiero', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (23, 27, 'Media', 0, 'it', '20051206 13:07:08', '0', '20051206 13:07:08', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (24, 28, 'Not for profit', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (25, 29, 'Attività ricreative', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (26, 30, 'Vendite al dettaglio', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (27, 31, 'Spedizioni', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (28, 32, 'Tecnologia', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (29, 33, 'Telecomunicazioni', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (30, 34, 'Trasporti', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (31, 35, 'Servizi pubblici', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (32, 37, 'Tessile', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (33, 38, 'Impianti elettrici', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (34, 39, 'Impianti idraulici', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (35, 1, 'Agriculture', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (36, 41, 'Associazioni o Club', 0, 'it', '20051206 13:07:09', '1', '20051206 13:07:09', '1')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (37, 103, 'Artigianato', 0, 'it', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (38, 103, 'Artisan', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (39, 21, 'Insurance', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (40, 41, 'Club or Association', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (41, 29, 'Recreational Activities', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (42, 5, 'Biotechnology', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (43, 6, 'Chemistry', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (44, 17, 'Food and Drink', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (45, 7, 'Communications', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (46, 9, 'Consultancy', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (47, 8, 'Building/Construction', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (48, 11, 'Electronics', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (49, 12, 'Energy', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (50, 18, 'Govermental Organizations', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (51, 16, 'Finance', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (52, 38, 'Electrician', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (53, 39, 'Plumbing', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (54, 13, 'Engineering', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (55, 14, 'Entertainment', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (56, 15, 'Environment', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (57, 10, 'Education', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (58, 22, 'Mechanics', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (59, 27, 'Media', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (60, 28, 'Not-For-Profit', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (61, 20, 'Hospitality', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (62, 19, 'Health', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (63, 35, 'Public Services', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (64, 4, 'Banking', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (65, 26, 'Manufacturing', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (66, 31, 'Shipping', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (67, 32, 'Technology', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (68, 33, 'Telecommunications', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (69, 37, 'Textile', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (70, 34, 'Transportation', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (71, 30, 'Retail', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (72, 2, 'Clothing', 0, 'en', '20051206 13:07:09', '0', '20051206 13:07:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (73, 1, 'Agricultura', 0, 'es', '20060809 18:33:15', '0', '20060809 18:33:15', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (74, 2, 'Textil', 0, 'es', '20060809 18:33:31', '0', '20060809 18:33:31', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (75, 4, 'Banco', 0, 'es', '20060809 18:33:40', '0', '20060809 18:33:40', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (76, 5, 'Biotecnologia', 0, 'es', '20060809 18:33:58', '0', '20060809 18:33:58', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (77, 6, 'Quimica', 0, 'es', '20060809 18:34:01', '0', '20060809 18:34:01', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (78, 7, 'Comunicaciones', 0, 'es', '20060809 18:34:16', '0', '20060809 18:34:16', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (79, 8, 'Construcciones', 0, 'es', '20060809 18:34:24', '0', '20060809 18:34:24', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (80, 9, 'Consultoria', 0, 'es', '20060809 18:34:32', '0', '20060809 18:34:32', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (81, 10, 'Educacion', 0, 'es', '20060809 18:34:46', '0', '20060809 18:34:46', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (82, 11, 'Electronica', 0, 'es', '20060809 18:35:11', '0', '20060809 18:35:11', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (83, 12, 'Energia', 0, 'es', '20060809 18:35:20', '0', '20060809 18:35:20', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (84, 13, 'Ingenieria', 0, 'es', '20060809 18:35:29', '0', '20060809 18:35:29', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (85, 14, 'Entretenimiento', 0, 'es', '20060809 18:35:38', '0', '20060809 18:35:38', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (86, 15, 'Ambiente', 0, 'es', '20060809 18:35:46', '0', '20060809 18:35:46', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (87, 16, 'Finanzas', 0, 'es', '20060809 18:35:56', '0', '20060809 18:35:56', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (88, 17, 'Alimentos y bebidas', 0, 'es', '20060809 18:36:05', '0', '20060809 18:36:05', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (89, 18, 'Gobierno', 0, 'es', '20060809 18:36:13', '0', '20060809 18:36:13', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (90, 19, 'Salud', 0, 'es', '20060809 18:36:19', '0', '20060809 18:36:19', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (91, 20, 'Hospitalidad', 0, 'es', '20060809 18:36:31', '0', '20060809 18:36:31', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (92, 21, 'Aseguraciones', 0, 'es', '20060809 18:36:47', '0', '20060809 18:36:47', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (93, 22, 'Mecanica', 0, 'es', '20060809 18:36:56', '0', '20060809 18:36:56', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (94, 26, 'Manufactura', 0, 'es', '20060809 18:37:03', '0', '20060809 18:37:03', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (95, 27, 'Media', 0, 'es', '20060809 18:37:15', '0', '20060809 18:37:15', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (96, 28, 'No lucrativa', 0, 'es', '20060809 18:37:27', '0', '20060809 18:37:27', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (97, 29, 'Actividades recreativas', 0, 'es', '20060809 18:37:35', '0', '20060809 18:37:35', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (98, 30, 'Venta al detalle', 0, 'es', '20060809 18:37:43', '0', '20060809 18:37:43', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (99, 31, 'Mensajeria', 0, 'es', '20060809 18:37:50', '0', '20060809 18:37:50', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (100, 32, 'Tecnologia', 0, 'es', '20060809 18:37:57', '0', '20060809 18:37:57', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (101, 33, 'Telecomunicaciones', 0, 'es', '20060809 18:38:05', '0', '20060809 18:38:05', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (102, 34, 'Transporte', 0, 'es', '20060809 18:38:10', '0', '20060809 18:38:10', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (103, 35, 'Servicios publicos', 0, 'es', '20060809 18:38:46', '0', '20060809 18:38:46', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (104, 37, 'Textil', 0, 'es', '20060809 18:38:54', '0', '20060809 18:38:54', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (105, 38, 'Instalaciones electricas', 0, 'es', '20060809 18:39:02', '0', '20060809 18:39:02', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (106, 39, 'Instalaciones hidraulicas', 0, 'es', '20060809 18:39:09', '0', '20060809 18:39:09', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (107, 41, 'Asociaciones o club', 0, 'es', '20060809 18:39:16', '0', '20060809 18:39:16', '0')
INSERT [COMPANYTYPE] ([ID], [K_ID], [DESCRIPTION], [TYPE], [LANG], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (108, 103, 'Artesanias', 0, 'es', '20060809 18:39:18', '0', '20060809 18:39:18', '0')
SET identity_insert [COMPANYTYPE] off
GO
/* Data for table CONTACTESTIMATE */
SET identity_insert [CONTACTESTIMATE] on

INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (1, 'OTTIMO', 3, 1, 'it')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (2, 'BUONO', 2, 2, 'it')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (3, 'SUFFICIENTE', 1, 3, 'it')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (4, 'PESSIMO', 0, 16, 'it')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (5, 'EXCELENT', 3, 1, 'en')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (6, 'GOOD', 2, 2, 'en')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (7, 'MORE LESS', 1, 3, 'en')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (8, 'BAD', 0, 16, 'en')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (9, 'OPTIMO', 3, 1, 'es')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (10, 'BUENO', 2, 2, 'es')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (11, 'SUFICIENTE', 1, 3, 'es')
INSERT [CONTACTESTIMATE] ([ID], [ESTIMATE], [FIELDORDER], [K_ID], [LANG]) VALUES (12, 'PESIMO', 0, 16, 'es')
SET identity_insert [CONTACTESTIMATE] off
GO
/* Data for table CONTACTTYPE */
SET identity_insert [CONTACTTYPE] on

INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (1, 1, 'ANALISTA', 'it')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (3, 3, 'CLIENTE', 'it')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (4, 4, 'COMPETITOR', 'it')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (5, 5, 'INTEGRATORE', 'it')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (6, 6, 'PARTNER', 'it')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (7, 7, 'STAMPA', 'it')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (8, 8, 'POTENZIALE CLIENTE', 'it')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (9, 9, 'RIVENDITORE', 'it')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (10, 10, 'LEAD', 'it')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (11, 19, 'FORNITORE', 'it')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (12, 1, 'Analyst', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (13, 3, 'Client', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (14, 11, 'Competitor', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (15, 2, 'Competetitor', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (16, 19, 'Supplier', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (17, 4, 'System Integrator', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (18, 5, 'Investor', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (19, 10, 'Lead', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (20, 6, 'Partner', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (21, 8, 'Prospect', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (22, 9, 'Reseller', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (23, 7, 'Press', 'en')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (24, 1, 'Analista', 'es')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (25, 2, 'Cliente', 'es')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (26, 3, 'Competidor', 'es')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (27, 4, 'Integrador de sistemas', 'es')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (28, 5, 'Inversionista', 'es')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (29, 6, 'Socio', 'es')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (30, 7, 'Revendedor', 'es')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (31, 8, 'Prospecto', 'es')
INSERT [CONTACTTYPE] ([ID], [K_ID], [CONTACTTYPE], [LANG]) VALUES (32, 9, 'Proveedor', 'es')
SET identity_insert [CONTACTTYPE] off
GO
/* Data for table COUNTRY */
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AF', 'AFG', '004', 'Afghanistan', 'Afghanistan', 'Afganistán', 'Afghanistan', 'Afghanistan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AL', 'ALB', '008', 'Albania', 'Albania', 'Albania', 'Albanie', 'Albanien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('DZ', 'DZA', '012', 'Algeria', 'Algeria', 'Argelia', 'Algérie', 'Algerien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AS', 'ASM', '016', 'American Samoa', 'Samoa (USA)', 'Samoa americana', 'Samoa, partie américaine (Manua, Tutuila avec Pago Pago)', 'Samoa (amerikanischer Teil)')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AD', 'AND', '020', 'Andorra', 'Andorra', 'Andorra', 'Andorre', 'Andorra')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AO', 'AGO', '024', 'Angola', 'Angola', 'Angola', 'Angola', 'Angola')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AI', 'AIA', '660', 'Anguilla', 'Anguilla', 'Anguilla', 'Anguilla', 'Anguilla')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AG', 'ATG', '028', 'Antigua and Barbuda', 'Antigua/Barbuda', 'Antigua y Barbuda', 'Antigua et Barbuda', 'Antigua und Barbuda')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AR', 'ARG', '032', 'Argentine', 'Argentina', 'Argentina', 'Argentine', 'Argentinien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AM', 'ARM', '051', 'Armenia', 'Armenia', 'Armenia', 'Arménie', 'Armenien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AW', 'ABW', '533', 'Aruba', 'Aruba', 'Aruba', 'Aruba', 'Aruba')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SH', 'SHN', '654', 'Ascension', 'Ascension', 'Santa Helena', 'Ascension, Sainte Hélène et Tristan da Cunha', 'Ascension')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AU', 'AUS', '036', 'Australia', 'Australia', 'Australia', 'Australie', 'Australien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AT', 'AUT', '040', 'Austria', 'Austria', 'Austria', 'Autriche', 'Österreich')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AZ', 'AZE', '031', 'Azerbaijan', 'Azerbaigian', 'Azerbaiyán', 'Azerbaïdjan', 'Aserbaidschan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BS', 'BHS', '044', 'Bahamas', 'Bahama', 'Bahamas', 'Bahamas', 'Bahamas')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BH', 'BHR', '048', 'Bahrain', 'Bahrein', 'Bahrain', 'Bahrain', 'Bahrain')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BD', 'BGD', '050', 'Bangladesh', 'Bangladesh', 'Bangladesh', 'Bangladesh', 'Bangladesh')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BB', 'BRB', '052', 'Barbados', 'Barbados', 'Barbados', 'Barbade', 'Barbados')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BY', 'BLR', '112', 'Belarus', 'Bielorussia', 'Belarus', 'Belarus', 'Belarus')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BE', 'BEL', '056', 'Belgium', 'Belgio', 'Bélgica', 'Belgique', 'Belgien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BZ', 'BLZ', '084', 'Belize', 'Belize', 'Belize', 'Belize', 'Belize')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BJ', 'BEN', '204', 'Benin', 'Benin', 'Benin', 'Bénin', 'Benin')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BM', 'BMU', '060', 'Bermuda', 'Bermude', 'Bermuda', 'Bermudes', 'Bermuda')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BT', 'BTN', '064', 'Bhutan', 'Bhutan', 'Bhutan', 'Bhoutan', 'Bhutan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BO', 'BOL', '068', 'Bolivia', 'Bolivia', 'Bolivia', 'Bolivie', 'Bolivien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BA', 'BIH', '070', 'Bosnia and Herzegovina', 'Bosnia-Erzegovina', 'Bosnia y Herzegovina', 'Bosnie-Herzégovine', 'Bosnien-Herzegowina')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BW', 'BWA', '072', 'Botswana', 'Botswana', 'Botswana', 'Botswana', 'Botswana')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BR', 'BRA', '076', 'Brazil', 'Brasile', 'Brasil', 'Brésil', 'Brasilien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BN', 'BRN', '096', 'Brunei Darussalam', 'Brunei', 'Brunei Darussalam', 'Brunéi', 'Brunei Darussalam')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BG', 'BGR', '100', 'Bulgaria', 'Bulgaria', 'Bulgaria', 'Bulgarie', 'Bulgarien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BF', 'BFA', '854', 'Burkina Faso', 'Burkina Faso', 'Burkina Faso', 'Burkina Faso', 'Burkina Faso')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('BI', 'BDI', '108', 'Burundi', 'Burundi', 'Burundi', 'Burundi', 'Burundi')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KH', 'KHM', '116', 'Cambodia', 'Cambogia', 'Camboya', 'Cambodge', 'Kambodscha')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CM', 'CMR', '120', 'Cameroon', 'Camerun', 'Camerún', 'Cameroun', 'Kamerun')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CA', 'CAN', '124', 'Canada', 'Canada', 'Canadá', 'Canada', 'Kanada')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CV', 'CPV', '132', 'Cape Verde', 'Capo Verde', 'Cabo Verde', 'Cap-Vert, Iles du', 'Kapverdische Inseln')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KY', 'CYM', '136', 'Cayman Islands', 'Cayman', 'Islas Caimán', 'Cayman', 'Cayman')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CF', 'CAF', '140', 'Central African Republic', 'Centrafrica', 'República Centroafricana', 'Centrafrique', 'Zentralafrika')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TD', 'TCD', '148', 'Chad', 'Ciad', 'Chad', 'Tchad', 'Tschad')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CL', 'CHL', '152', 'Chile', 'Cile', 'Chile', 'Chili', 'Chile')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CN', 'CHN', '156', 'China, People''s Republic', 'Cina, Rep. pop.', 'China continental', 'Chine, République populaire', 'China, Volksrepublik')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TW', 'TWN', '158', 'China, Taiwan', 'Cina, Taiwan', 'Taiwán', 'Chine, Taïwan', 'China, Taiwan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CX', 'CXR', '162', 'Christmas Island', 'Christmas, isole', 'Isla Navidad', 'Christmas, Ile', 'Weihnachtsinsel')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CC', 'CCK', '166', 'Cocos (keeling) Islands', 'Cocos, Isole', 'Islas Cocos (Keeling)', 'Cocos (Keeling), Iles des', 'Kokos-Inseln')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CO', 'COL', '170', 'Colombia', 'Colombia', 'Colombia', 'Colombie', 'Kolumbien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KM', 'COM', '174', 'Comoros', 'Comore', 'Comoros', 'Comores', 'Komoren')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CD', 'COD', '180', 'Congo, Democratic Republic', 'Congo, Rep. Democratica (ex Zaire  )', 'Congo, República Democrática del', 'Congo (ex Zaïre), République démocratique', 'Kongo, Demokratische Republik (ex-Zaire)')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CG', 'COG', '178', 'Congo, Republic', 'Congo, Repubblica', 'Congo, República del', 'Congo, République', 'Kongo, Republik')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CK', 'COK', '184', 'Cook Islands', 'Cook, Arcipelago di', 'Islas Cook', 'Cook, Iles', 'Cookinseln')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CR', 'CRI', '188', 'Costa Rica', 'Costa Rica', 'Costa Rica', 'Costa Rica', 'Costa Rica')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CI', 'CIV', '384', 'Côte d''Ivoire', 'Côte d''Ivoire', 'Costa de Marfil', 'Côte d Ivoire', 'Côte d''Ivoire')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('HR', 'HRV', '191', 'Croatia', 'Croazia', 'Croacia', 'Croatie', 'Kroatien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CU', 'CUB', '192', 'Cuba', 'Cuba', 'Cuba', 'Cuba', 'Kuba')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CY', 'CYP', '196', 'Cyprus', 'Cipro', 'Chipre', 'Chypre', 'Zypern')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CZ', 'CZE', '203', 'Czech Republic', 'Ceca, Repubblica', 'Chequia', 'Tchèque, République', 'Tschechische Republik')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('DK', 'DNK', '208', 'Denmark', 'Danimarca', 'Dinamarca', 'Danemark', 'Dänemark')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('DJ', 'DJI', '262', 'Djibouti', 'Gibuti', 'Djibouti', 'Djibouti', 'Djibouti')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('DM', 'DMA', '212', 'Dominica', 'Dominica', 'Dominica', 'Dominique, Ile', 'Dominica')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('DO', 'DOM', '214', 'Dominican Republic', 'Dominicana, Repubblica', 'República Dominicana', 'Dominicaine, République', 'Dominikanische Republik')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TP', 'TMP', '626', 'East Timor', 'Timor Orientale', 'Timor Este', 'Timor Oriental', 'Osttimor')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('EC', 'ECU', '218', 'Ecuador', 'Equatore', 'Ecuador', 'Equateur', 'Ekuador')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('EG', 'EGY', '818', 'Egypt', 'Egitto', 'Egipto', 'Egypte', 'Ägypten')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SV', 'SLV', '222', 'El Salvador', 'Salvador', 'El Salvador', 'El Salvador', 'El Salvador')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GQ', 'GNQ', '226', 'Equatorial Guinea', 'Guinea Equatoriale', 'Guinea Ecuatorial', 'Guinée Equatoriale', 'Äquatorial-Guinea')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('ER', 'ERI', '232', 'Eritrea', 'Eritrea', 'Eritrea', 'Erythrée', 'Eritrea')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('EE', 'EST', '233', 'Estonia', 'Estonia', 'Estonia', 'Estonie', 'Estland')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('ET', 'ETH', '231', 'Ethiopia', 'Etiopia', 'Etiopía', 'Ethiopie', 'Äthiopien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('FK', 'FLK', '238', 'Falkland Islands', 'Falkland, isole', 'Islas Malvinas', 'Falkland', 'Falkland')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('FO', 'FRO', '234', 'Faroe Islands', 'Faeröer, isole', 'Islas Faroe', 'Féroé, Iles', 'Färöer')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('FJ', 'FJI', '242', 'Fiji', 'Figi, Isole', 'Fiji', 'Fidji', 'Fidschi')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('FI', 'FIN', '246', 'Finland', 'Finlandia', 'Finlandia', 'Finlande', 'Finnland')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('FR', 'FRA', '250', 'France', 'Francia', 'Francia', 'France', 'Frankreich')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GF', 'GUF', '254', 'French Guiana', 'Guiana francese', 'Guinea Francesa', 'Guyane française', 'Französisch-Guayana')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PF', 'PYF', '258', 'French Polynesia', 'Polinesia Francese', 'Polinesia Francesa', 'Polynésie française', 'Französisch-Polynesien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TF', 'ATF', '260', 'French Southern Territories', 'Antartide francese', 'Territorios Sureños de Francia', 'Terres Australes françaises', 'Französische Südgebiete')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GA', 'GAB', '266', 'Gabon', 'Gabon', 'Gabón', 'Gabon', 'Gabun')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GM', 'GMB', '270', 'Gambia', 'Gambia', 'Gambia', 'Gambie', 'Gambia')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GE', 'GEO', '268', 'Georgia', 'Georgia', 'Georgia', 'Géorgie', 'Georgien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('DE', 'DEU', '276', 'Germany', 'Germania', 'Alemania', 'Allemagne', 'Deutschland')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GH', 'GHA', '288', 'Ghana', 'Ghana', 'Ghana', 'Ghana', 'Ghana')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GI', 'GIB', '292', 'Gibraltar', 'Gibilterra', 'Gibraltar', 'Gibraltar', 'Gibraltar')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GB', 'GBR', '826', 'Great Britain', 'Gran Bretagna', 'Reino Unido', 'Grande-Bretagne', 'Grossbritannien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GR', 'GRC', '300', 'Greece', 'Grecia', 'Grecia', 'Grèce', 'Griechenland')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GL', 'GRL', '304', 'Greenland', 'Groenlandia', 'Groenlandia', 'Groenland', 'Grönland')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GD', 'GRD', '308', 'Grenada', 'Grenada', 'Granada', 'Grenade', 'Grenada')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GP', 'GLP', '312', 'Guadeloupe', 'Guadalupa', 'Guadalupe', 'Guadeloupe', 'Guadeloupe')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GU', 'GUM', '316', 'Guam', 'Guam', 'Guam', 'Guam', 'Guam')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GT', 'GTM', '320', 'Guatemala', 'Guatemala', 'Guatemala', 'Guatémala', 'Guatemala')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GB', 'GBR', '826', 'Guernsey', 'Gran Bretagna', 'Reino Unido', 'Grande-Bretagne', 'Grossbritannien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GN', 'GIN', '324', 'Guinea', 'Guinea, Repubblica', 'Guinea', 'Guinée, République', 'Guinea, Republik')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GW', 'GNB', '624', 'Guinea-Bissau', 'Guinea-Bissau', 'Guinea-Bissau', 'Guinée-Bissau', 'Guinea-Bissau')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GY', 'GUY', '328', 'Guyana', 'Guiana', 'Guayana', 'Guyane', 'Guyana')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('HT', 'HTI', '332', 'Haiti', 'Haiti', 'Haití', 'Haïti', 'Haiti')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('HN', 'HND', '340', 'Honduras', 'Honduras', 'Honduras', 'Honduras', 'Honduras')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('HK', 'HKG', '344', 'Hong Kong', 'Hongkong', 'Hong Kong', 'Hong-Kong', 'Hongkong')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('HU', 'HUN', '348', 'Hungary', 'Ungheria', 'Hungría', 'Hongrie', 'Ungarn')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('IS', 'ISL', '352', 'Iceland', 'Islanda', 'Islandia', 'Islande', 'Island')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('IN', 'IND', '356', 'India', 'India', 'India', 'Inde', 'Indien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('ID', 'IDN', '360', 'Indonesia', 'Indonesia', 'Indonesia', 'Indonésie', 'Indonesien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('IR', 'IRN', '364', 'Iran', 'Iran', 'Irán', 'Iran', 'Iran')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('IQ', 'IRQ', '368', 'Iraq', 'Iraq', 'Irak', 'Iraq', 'Irak')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('IE', 'IRL', '372', 'Ireland', 'Irlanda', 'Irlanda, República de', 'Irlande', 'Irland')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('IL', 'ISR', '376', 'Israel', 'Israele', 'Israel', 'Israël', 'Israel')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('IT', 'ITA', '380', 'Italy', 'Italia', 'Italia', 'Italie', 'Italien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('JM', 'JAM', '388', 'Jamaica', 'Giamaica', 'Jamaica', 'Jamaïque', 'Jamaika')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GB', 'GBR', '826', 'Jersey', 'Gran Bretagna', 'Reino Unido', 'Grande-Bretagne', 'Grossbritannien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('JP', 'JPN', '392', 'Japan', 'Giappone', 'Japón', 'Japon', 'Japan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('JO', 'JOR', '400', 'Jordan', 'Giordania', 'Jordania', 'Jordanie', 'Jordanien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KZ', 'KAZ', '398', 'Kazakhstan', 'Kazakistan', 'Kazajstán', 'Kazakhstan', 'Kasachstan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KE', 'KEN', '404', 'Kenya', 'Kenya', 'Kenia', 'Kenya', 'Kenia')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KI', 'KIR', '296', 'Kiribati', 'Kiribati', 'Kiribati', 'Kiribati', 'Kiribati')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KP', 'PRK', '408', 'Korea, Democratic People''s Republic of', 'Corea, Rep. pop. dem. (Corea del Nord)', 'Corea, República Democrática Popular de', 'Corée (Corée du Nord), République populaire démocratique', 'Korea, Demo. Volksrepublik (Nordkorea)')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KR', 'KOR', '410', 'Korea, Republic of', 'Corea, Rep. (Corea del Sud)', 'Corea, República de', 'Corée (Corée du Sud), République', 'Korea, Republik (Südkorea)')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('XZ', 'XZA', NULL, 'Kosovo (UN Interim Administration)', 'Kosovo (amministrato ad interim dall''ONU)', 'Kosovo', 'Kosovo (Administration intérimaire des Nations Unies)', 'Kosovo (Interim. Verw. der UNO)')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KW', 'KWT', '414', 'Kuwait', 'Kuwait', 'Kuwait', 'Kuwait', 'Kuwait')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KG', 'KGZ', '417', 'Kyrgyzstan', 'Kirghizistan', 'Kirgistán', 'Kirghizistan', 'Kirgisistan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LA', 'LAO', '418', 'Laos People''s Democratic Republic', 'Laos', 'Laos, República Democrática Popular de', 'Laos', 'Laos')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LV', 'LVA', '428', 'Latvia', 'Lettonia', 'Letonia', 'Lettonie', 'Lettland')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LB', 'LBN', '422', 'Lebanon', 'Libano', 'Líbano', 'Liban', 'Libanon')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LS', 'LSO', '426', 'Lesotho', 'Lesotho', 'Lesoto', 'Lesotho', 'Lesotho')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LR', 'LBR', '430', 'Liberia', 'Liberia', 'Liberia', 'Libéria', 'Liberia')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LY', 'LBY', '434', 'Libyan Arab Jamahiriya', 'Libia', 'Libia', 'Libye', 'Libyen')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LI', 'LIE', '438', 'Liechtenstein', 'Liechtenstein', 'Liechtenstein', 'Liechtenstein', 'Liechtenstein')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LT', 'LTU', '440', 'Lithuania', 'Lituania', 'Lituania', 'Lituanie', 'Litauen')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LU', 'LUX', '442', 'Luxembourg', 'Lussemburgo', 'Luxemburgo', 'Luxembourg', 'Luxemburg')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MO', 'MAC', '446', 'Macao', 'Macao', 'Macao', 'Macao', 'Macao')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MK', 'MKD', '807', 'Macedonia, the Former Yugoslav Republic', 'Macedonia', 'Macedonia', 'Macédoine', 'Mazedonien (ehemalige jugoslawische Republik)')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MG', 'MDG', '450', 'Madagascar', 'Madagascar', 'Madagascar', 'Madagascar', 'Madagaskar')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MW', 'MWI', '454', 'Malawi', 'Malawi', 'Malawi', 'Malawi', 'Malawi')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MY', 'MYS', '458', 'Malaysia', 'Malaisia', 'Malasia', 'Malaisie', 'Malaysia')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MV', 'MDV', '462', 'Maldives', 'Maldive', 'Maldivas', 'Maldives', 'Malediven')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('ML', 'MLI', '466', 'Mali', 'Mali', 'Mali', 'Mali', 'Mali')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MT', 'MLT', '470', 'Malta', 'Malta', 'Malta', 'Malte', 'Malta')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GB', 'GBR', '826', 'Man, Isle of', 'Gran Bretagna', 'Reino Unido', 'Grande-Bretagne', 'Grossbritannien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MP', 'MNP', '580', 'Mariana Islands', 'Marianne, Isole', 'Islas de Norte-Mariana', 'Mariannes du Nord, Iles', 'Marianen, nördliche')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MH', 'MHL', '584', 'Marshall Islands', 'Marshall, Isole', 'Islas Marshall', 'Marshall, Iles', 'Marshallinseln')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MQ', 'MTQ', '474', 'Martinique', 'Martinica', 'Martinica', 'Martinique', 'Martinique')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MR', 'MRT', '478', 'Mauritania', 'Mauritania', 'Mauritania', 'Mauritanie', 'Mauretanien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MU', 'MUS', '480', 'Mauritius', 'Maurizio', 'Mauricio', 'Maurice, Ile', 'Mauritius')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('YT', 'MYT', '175', 'Mayotte', 'Mayotte', 'Mayote', 'Mayotte', 'Mayotte')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MX', 'MEX', '484', 'Mexico', 'Messico', 'México', 'Mexique', 'Mexiko')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MD', 'MDA', '498', 'Moldova', 'Moldavia', 'Moldavia', 'Moldova', 'Moldova')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MC', 'MCO', '492', 'Monaco', 'Monaco', 'Mónaco', 'Monaco', 'Monaco')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MN', 'MNG', '496', 'Mongolia', 'Mongolia', 'Mongolia', 'Mongolie', 'Mongolei')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MS', 'MSR', '500', 'Montserrat', 'Montserrat', 'Montserrat', 'Montserrat', 'Montserrat')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MA', 'MAR', '504', 'Morocco', 'Marocco', 'Marruecos', 'Maroc', 'Marokko')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MZ', 'MOZ', '508', 'Mozambique', 'Mozambico', 'Mozambique', 'Mozambique', 'Mosambik')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('MM', 'MMR', '104', 'Myanmar (Union)', 'Myanmar (Unione)', 'Mianmar', 'Myanmar', 'Myanmar (Union)')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NA', 'NAM', '516', 'Namibia', 'Namibia', 'Namibia', 'Namibie', 'Namibia')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NR', 'NRU', '520', 'Nauru', 'Nauru', 'Nauru', 'Nauru', 'Nauru')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NP', 'NPL', '524', 'Nepal', 'Nepal', 'Nepal', 'Népal', 'Nepal')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NL', 'NLD', '528', 'Netherlands', 'Paesi Bassi', 'Holanda', 'Pays-Bas', 'Niederlande')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AN', 'ANT', '530', 'Netherlands Antilles', 'Antille olandesi', 'Antillas Holandesas', 'Antilles néerlandaises', 'Niederländische Antillen')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NC', 'NCL', '540', 'New Caledonia', 'Nuova Caledonia', 'Nueva Caledonia', 'Nouvelle-Calédonie', 'Neukaledonien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NZ', 'NZL', '554', 'New Zealand', 'Nuova Zelanda', 'Nueva Zelanda', 'Nouvelle-Zélande', 'Neuseeland')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NI', 'NIC', '558', 'Nicaragua', 'Nicaragua', 'Nicaragua', 'Nicaragua', 'Nikaragua')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NE', 'NER', '562', 'Niger', 'Niger', 'Níger', 'Niger', 'Niger')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NG', 'NGA', '566', 'Nigeria', 'Nigeria', 'Nigeria', 'Nigéria', 'Nigeria')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NF', 'NFK', '574', 'Norfolk Island', 'Norfolk, isola', 'Islas Norfolk', 'Norfolk, Ile', 'Norfolk')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('NO', 'NOR', '578', 'Norway', 'Norvegia', 'Noruega', 'Norvège', 'Norwegen')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('OM', 'OMN', '512', 'Oman', 'Oman', 'Omán', 'Oman', 'Oman')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PK', 'PAK', '586', 'Pakistan', 'Pakistan', 'Pakistán', 'Pakistan', 'Pakistan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PW', 'PLW', '585', 'Palau', 'Palau, Isole', 'Palau', 'Palau', 'Palau')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PA', 'PAN', '591', 'Panama', 'Panama', 'Panamá', 'Panama', 'Panama')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PG', 'PNG', '598', 'Papua New Guinea', 'Papua-Nuova Guinea', 'Papúa Nueva Guinea', 'Papouasie-Nouvelle-Guinée', 'Papua-Neuguinea')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PY', 'PRY', '600', 'Paraguay', 'Paraguay', 'Paraguay', 'Paraguay', 'Paraguay')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PE', 'PER', '604', 'Peru', 'Perù', 'Perú', 'Pérou', 'Peru')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PH', 'PHL', '608', 'Philippines', 'Filippine', 'Filipinas', 'Philippines', 'Philippinen')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PN', 'PCN', '612', 'Pitcairn', 'Pitcairn', 'Pitcairn', 'Pitcairn', 'Pitcairn')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PL', 'POL', '616', 'Poland', 'Polonia', 'Polonia', 'Pologne', 'Polen')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PT', 'PRT', '620', 'Portugal', 'Portogallo', 'Portugal', 'Portugal', 'Portugal')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PR', 'PRI', '630', 'Puerto Rico', 'Portorico', 'Puerto Rico', 'Porto Rico', 'Puerto Rico')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('QA', 'QAT', '634', 'Qatar', 'Qatar', 'Qatar', 'Qatar', 'Qatar')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('RE', 'REU', '638', 'Réunion', 'Riunione', 'Reunión', 'Réunion', 'Réunion')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('RO', 'ROM', '642', 'Romania', 'Romania', 'Rumanía', 'Roumanie', 'Rumänien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('RU', 'RUS', '643', 'Russian Federation', 'Russia, Federazione', 'Rusia', 'Russie, Fédération de', 'Russische Föderation')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('RW', 'RWA', '646', 'Rwanda', 'Ruanda', 'Ruanda', 'Rwanda', 'Rwanda')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('KN', 'KNA', '659', 'Saint Christopher and Nevis', 'San Cristoforo e Nevis', 'Santa Kitts y Nevis', 'St-Christophe (St.-Kitts) et Nevis', 'St. Christoph und Nevis')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SH', 'SHN', '654', 'Saint Helena', 'Ascension', 'Santa Helena', 'Ascension, Sainte Hélène et Tristan da Cunha', 'Ascension')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LC', 'LCA', '662', 'Saint Lucia', 'Santa Lucia', 'Santa Lucía', 'Sainte Lucie', 'St. Lucia')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('PM', 'SPM', '666', 'Saint Pierre and Miquelon', 'Saint-Pierre et Miquelon', 'San Pedro y Miquelon', 'St-Pierre et Miquelon', 'St. Pierre und Miquelon')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('VC', 'VCT', '670', 'Saint Vincent and the Grenadines', 'Saint Vincent e Grenadine', 'San Vincente y Las Granadinas', 'St-Vincent et Grenadines', 'St. Vincent und die Grenadinen')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SM', 'SMR', '674', 'San Marino', 'San Marino', 'San Marino', 'St-Marin', 'San Marino')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('ST', 'STP', '678', 'São Tome and Principe', 'Sao Tomé e Principe', 'Santo Tomé y Príncipe', 'St-Thomas et Prince', 'St. Thomas und Principe')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SA', 'SAU', '682', 'Saudi Arabia', 'Arabia Saudita', 'Arabia Saudí', 'Arabie Saoudite', 'Saudi-Arabien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SN', 'SEN', '686', 'Senegal', 'Senegal', 'Senegal', 'Sénégal', 'Senegal')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CS', 'SCG', '891', 'Serbia and Montenegro', 'Serbia e Montenegro', 'Serbia y Montenegro', 'Serbie et Monténégro', 'Serbien und Montenegro')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SC', 'SYC', '690', 'Seychelles', 'Seicelle', 'Seychelles', 'Seychelles', 'Seychellen')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SL', 'SLE', '694', 'Sierra Leone', 'Sierra Leone', 'Sierra Leona', 'Sierra Leone', 'Sierra Leone')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SG', 'SGP', '702', 'Singapore', 'Singapore', 'Singapur', 'Singapour', 'Singapur')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SK', 'SVK', '703', 'Slovakia , Republic of', 'Slovacca, Repubblica', 'Eslovaquia', 'Slovaque, République', 'Slowakische Republik')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SI', 'SVN', '705', 'Slovenia', 'Slovenia', 'Eslovenia', 'Slovénie', 'Slowenien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SB', 'SLB', '090', 'Solomon Islands', 'Salomone, isole', 'Islas Salomón', 'Salomon, Iles', 'Salomon-Inseln')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SO', 'SOM', '706', 'Somalia', 'Somalia', 'Somalia', 'Somalie', 'Somalia')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('ZA', 'ZAF', '710', 'South Africa', 'Sudafrica', 'Sudáfrica', 'Afrique du Sud', 'Südafrika')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('GS', 'SGS', '239', 'South Georgia and the south Sandwich Islands', 'Georgia del Sud e Sandwich del Sud, Isole', 'Georgia del Sur e Islas Sandwich del Sur', 'Géorgie du Sud et les Iles Sandwich du Sud', 'Südgeorgien und die südlichen Sandwichinseln')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('ES', 'ESP', '724', 'Spain', 'Spagna', 'España', 'Espagne', 'Spanien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('LK', 'LKA', '144', 'Sri Lanka', 'Sri Lanka', 'Sri Lanka', 'Sri Lanka', 'Sri Lanka')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SD', 'SDN', '736', 'Sudan', 'Sudan', 'Sudán', 'Soudan', 'Sudan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SR', 'SUR', '740', 'Suriname', 'Suriname', 'Surinám', 'Suriname', 'Suriname')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SZ', 'SWZ', '748', 'Swaziland', 'Swaziland', 'Suazilandia', 'Swaziland', 'Swasiland')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SE', 'SWE', '752', 'Sweden', 'Svezia', 'Suecia', 'Suéde', 'Schweden')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('CH', 'CHE', '756', 'Switzerland', 'Svizzera', 'Suiza', 'Suisse', 'Schweiz')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SY', 'SYR', '760', 'Syria', 'Siria', 'Siria', 'Syrie', 'Syrien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TJ', 'TJK', '762', 'Tajikistan', 'Tagiskistan', 'Tajikistán', 'Tadjikistan', 'Tadschikistan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TZ', 'TZA', '834', 'Tanzania', 'Tanzania', 'Tanzania', 'Tanzanie', 'Tansania')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TH', 'THA', '764', 'Thailand', 'Tailandia', 'Tailandia', 'Thaïlande', 'Thailand')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TG', 'TGO', '768', 'Togo', 'Togo', 'Togo', 'Togo', 'Togo')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TK', 'TKL', '772', 'Tokelau Islands', 'Tokelau, Isole', 'Tokelau', 'Tokélaou', 'Tokelau')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TO', 'TON', '776', 'Tonga', 'Tonga', 'Tongo', 'Tonga', 'Tonga')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TT', 'TTO', '780', 'Trinidad and Tobago', 'Trinidad e Tobago', 'Trinidad y Tobago', 'Trinité et Tobago', 'Trinidad und Tobago')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('SH', 'SHN', '654', 'Tristan da Cunha', 'Ascension', 'Santa Helena', 'Ascension, Sainte Hélène et Tristan da Cunha', 'Ascension')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TN', 'TUN', '788', 'Tunisia', 'Tunisia', 'Túnez', 'Tunisie', 'Tunesien')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TR', 'TUR', '792', 'Turkey', 'Turchia', 'Turquía', 'Turquie', 'Türkei')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TM', 'TKM', '795', 'Turkmenistan', 'Turkmenistan', 'Turmenistán', 'Turkménistan', 'Turkmenistan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TC', 'TCA', '796', 'Turks and Caicos Islands', 'Turks e Caicos', 'Islas Turks y Caicos', 'Turks et Caïques', 'Turks und Caicos')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('TV', 'TUV', '798', 'Tuvalu', 'Tuvalu', 'Tuvalu', 'Tuvalu', 'Tuvalu')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('UG', 'UGA', '800', 'Uganda', 'Uganda', 'Uganda', 'Ouganda', 'Uganda')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('UA', 'UKR', '804', 'Ukraine', 'Ucraina', 'Ucrania', 'Ukraine', 'Ukraine')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('AE', 'ARE', '784', 'United Arab Emirates', 'Emirati Arabi Uniti', 'Emiratos Árabes Unidos', 'Emirats Arabes Unis', 'Vereinigte Arabische Emirate')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('US', 'USA', '840', 'United States of America', 'Stati Uniti d''America', 'Estados Unidos', 'Etats-Unis d Amérique', 'Vereinigte Staaten von Amerika')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('UM', 'UMI', '581', 'United States Minor Outlying Islands', 'Isole Minori (USA)', 'Islas Ultramarinas de Estados Unidos', 'Iles Mineures Éloignées des États-Unis', 'Amerikanische Überseeinseln, kleinere')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('UY', 'URY', '858', 'Uruguay', 'Uruguay', 'Uruguay', 'Uruguay', 'Uruguay')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('UZ', 'UZB', '860', 'Uzbekistan', 'Uzbekistan', 'Uzbekistán', 'Ouzbékistan', 'Usbekistan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('VU', 'VUT', '548', 'Vanuatu', 'Vanuatu', 'Vanuatu', 'Vanuatu', 'Vanuatu')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('VA', 'VAT', '336', 'Vatican City State (Holy See)', 'Vaticano', 'Estado Vaticano', 'Vatican', 'Vatikan')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('VE', 'VEN', '862', 'Venezuela', 'Venezuela', 'Venezuela', 'Venezuela', 'Venezuela')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('VN', 'VNM', '704', 'Viet Nam', 'Vietnam', 'Vietnam', 'Viet Nam', 'Vietnam')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('VG', 'VGB', '092', 'Virgin Islands', 'Vergini, Isole (britanniche)', 'Islas Vírgenes Británicas', 'Vierges, Iles britanniques (Tortola)', 'Virginische Inseln (brit. Teil)')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('VI', 'VIR', '850', 'Virgin Islands (USA)', 'Vergini, Isole (USA)', 'Islas Vírgenes Estadounidenses', 'Vierges (Etats-Unis), Iles', 'Amerikanische Jungferninseln')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('WF', 'WLF', '876', 'Wallis and Futuna Islands', 'Wallis e Futuna', 'Wallis y Futuna', 'Wallis et Futuna', 'Wallis und Futuna')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('WS', 'WSM', '882', 'Western Samoa', 'Samoa Occidentali', 'Samoa', 'Samoa Occidental (Savaii, Upola)', 'West Samoa (Savaii, Upola)')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('YE', 'YEM', '887', 'Yemen', 'Yemen', 'Yemen', 'Yémen', 'Jemen')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('ZM', 'ZMB', '894', 'Zambia', 'Zambia', 'Zambia', 'Zambie', 'Sambia')
INSERT [COUNTRY] ([CODE2], [CODE3], [NUM], [NAME_EN], [NAME_IT], [NAME_ES], [NAME_FR], [NAME_DE]) VALUES ('ZW', 'ZWE', '716', 'Zimbabwe', 'Zimbabwe', 'Zimbabue', 'Zimbabwe', 'Zimbabwe')
/* Data for table CRM_ACTIVITYTYPE */
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (1, 1, 'Telefonata', 'it')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (2, 2, 'Lettera', 'it')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (3, 3, 'Fax', 'it')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (4, 4, 'Memo', 'it')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (5, 5, 'E-mail', 'it')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (6, 6, 'Visita', 'it')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (7, 7, 'Attività generica', 'it')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (8, 8, 'Caso/soluzione', 'it')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (9, 1, 'Phone Call', 'en')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (10, 2, 'Letter', 'en')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (11, 3, 'Fax', 'en')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (12, 4, 'Memo', 'en')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (13, 5, 'E-mail', 'en')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (14, 6, 'Visit', 'en')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (15, 7, 'Generic activity', 'en')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (16, 8, 'Case/Solution', 'en')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (17, 1, 'Llamada', 'es')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (18, 2, 'Carta', 'es')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (19, 3, 'Fax', 'es')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (20, 4, 'Memo', 'es')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (21, 5, 'Correo electrónico', 'es')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (22, 6, 'Visita', 'es')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (23, 7, 'Actividad genérica', 'es')
INSERT [CRM_ACTIVITYTYPE] ([ID], [K_ID], [DESCRIPTION], [LANG]) VALUES (24, 8, 'Caso/Solución', 'es')

/* Data for table CRM_LeadDescription */
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (1, 1, 'Primo Contatto', 'it', '1')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (2, 2, 'Valutazione', 'it', '1')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (3, 3, 'Chiuso', 'it', '1')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (4, 4, 'Pessimo', 'it', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (5, 5, 'Sufficiente', 'it', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (6, 6, 'Buono', 'it', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (7, 7, 'Ottimo', 'it', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (8, 8, 'Alto', 'it', '3')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (9, 9, 'Medio', 'it', '3')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (10, 10, 'Basso', 'it', '3')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (11, 11, 'Pubblicità', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (12, 12, 'Posta', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (13, 13, 'E-mail', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (14, 14, 'Seminario', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (15, 15, 'Fiera', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (16, 16, 'Evento', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (17, 17, 'Riferimento comprato', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (18, 18, 'Riferimento affittato', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (19, 19, 'Riferimento interno', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (20, 20, 'Riferimento esterno', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (21, 21, 'Sito Web', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (22, 22, 'Altro', 'it', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (23, 23, 'Euro', 'it', '5')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (24, 24, 'USD', 'it', '5')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (25, 1, 'First contact', 'en', '1')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (26, 2, 'Evaluation', 'en', '1')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (27, 3, 'Closed', 'en', '1')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (28, 4, 'Bad', 'en', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (29, 5, 'Sufficient', 'en', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (30, 6, 'Good', 'en', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (31, 7, 'Excellent', 'en', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (32, 8, 'High', 'en', '3')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (33, 9, 'Medium', 'en', '3')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (34, 10, 'Low', 'en', '3')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (35, 11, 'Advertizing', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (36, 12, 'Mail', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (37, 13, 'E-mail', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (38, 14, 'Seminar', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (39, 15, 'Fair', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (40, 16, 'Event', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (41, 17, 'Purchased referral', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (42, 18, 'Rented referral', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (43, 19, 'Internal referral', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (44, 20, 'External referral', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (45, 21, 'Web site', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (46, 22, 'Other', 'en', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (47, 23, 'Euro', 'en', '5')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (48, 24, 'USD', 'en', '5')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (49, 1, 'Primer Contacto', 'es', '1')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (50, 2, 'Evaluación', 'es', '1')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (51, 3, 'Cerrado', 'es', '1')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (52, 4, 'Pésimo', 'es', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (53, 5, 'Suficiente', 'es', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (54, 6, 'Bueno', 'es', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (55, 7, 'Óptimo', 'es', '2')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (56, 8, 'Alto', 'es', '3')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (57, 9, 'Medio', 'es', '3')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (58, 10, 'Bajo', 'es', '3')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (59, 11, 'Publicidad', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (60, 12, 'Correo', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (61, 13, 'Correo electrónico', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (62, 14, 'Seminario', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (63, 15, 'Feria', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (64, 16, 'Evento', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (65, 17, 'Referencia comprada', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (66, 18, 'Referencia alquilada', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (67, 19, 'Referencia interna', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (68, 20, 'Referencia externa', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (69, 21, 'Sitio Web', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (70, 22, 'Otro', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (71, 23, 'Euro', 'es', '5')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (72, 24, 'USD', 'es', '5')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (73, 25, '01-800', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (74, 26, 'DemoWeb', 'es', '4')
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (75, 27, 'Soporte tecnico', 'es', '4')          
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (76, 25, '01-800', 'en', '4')                   
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (77, 26, 'DemoWeb', 'en', '4')                  
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (78, 27, 'Technical support', 'en', '4')        
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (79, 25, 'Numero Verde', 'it', '4')             
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (80, 26, 'Demo Web', 'it', '4')                 
INSERT [CRM_LEADDESCRIPTION] ([ID], [K_ID], [DESCRIPTION], [LANG], [TYPE]) VALUES (81, 27, 'Supporto tecnico', 'it', '4')         

/* Data for table CRMWORKINGCLASSIFICATION */
SET identity_insert [CRMWORKINGCLASSIFICATION] on

INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (1, 1, 'Primo Contatto', 1, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (2, 1, 'Trattativa in corso', 2, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (3, 1, 'Segnalazione', 3, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (4, 1, 'Chiusa', 4, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (5, 1, 'Chiusa / Non interessante', 5, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (6, 6, 'Primo Contatto', 1, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (7, 6, 'Trattativa in corso', 2, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (8, 6, 'Chiusa', 3, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (9, 6, 'Chiusa / Non interessante', 4, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (10, 1, 'Nessuna Classificazione', 0, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (11, 2, 'Nessuna Classificazione', 0, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (12, 3, 'Nessuna Classificazione', 0, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (13, 4, 'Nessuna Classificazione', 0, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (14, 5, 'Nessuna Classificazione', 0, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (15, 6, 'Nessuna Classificazione', 0, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (16, 7, 'Nessuna Classificazione', 0, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (17, 8, 'Nessuna Classificazione', 0, 'it')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (18, 1, 'Initial Contact', 1, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (19, 1, 'Negotiations Underway', 2, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (20, 1, 'Flagged', 3, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (21, 1, 'Closed', 4, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (22, 1, 'Closed/ Not interesting', 5, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (23, 6, 'Initial Contact', 1, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (24, 6, 'Negotiations Underway', 2, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (25, 6, 'Closed', 3, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (26, 6, 'Closed/ Not interesting', 4, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (27, 1, 'No Classification', 0, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (28, 6, 'No Classification', 0, 'en')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (29, 1, 'Primer Contacto', 1, 'es')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (30, 1, 'Negociación en curso', 2, 'es')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (31, 1, 'Señalación', 3, 'es')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (32, 1, 'Cerrada', 4, 'es')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (33, 1, 'Cerrada/ No interesante', 5, 'es')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (34, 6, 'Primer Contacto', 1, 'es')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (35, 6, 'Negociación en curso', 2, 'es')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (36, 6, 'Cerrada', 3, 'es')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (37, 6, 'Cerrada/ No interesante', 4, 'es')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (38, 1, 'Ninguna Clasificación', 0, 'es')
INSERT [CRMWORKINGCLASSIFICATION] ([ID], [TYPE], [DESCRIPTION], [DROPPOSITION], [LANG]) VALUES (39, 6, 'Ninguna Clasificación', 0, 'es')
SET identity_insert [CRMWORKINGCLASSIFICATION] off
GO
/* Data for table CURRENCYTABLE */
SET identity_insert [CURRENCYTABLE] on

INSERT [CURRENCYTABLE] ([ID], [CURRENCY], [CHANGETOEURO], [CHANGEFROMEURO], [CURRENCYSYMBOL]) VALUES (1, 'EUR', '1', '1', '€')
INSERT [CURRENCYTABLE] ([ID], [CURRENCY], [CHANGETOEURO], [CHANGEFROMEURO], [CURRENCYSYMBOL]) VALUES (2, 'PESO', '13.97', '0.71', '$')
INSERT [CURRENCYTABLE] ([ID], [CURRENCY], [CHANGETOEURO], [CHANGEFROMEURO], [CURRENCYSYMBOL]) VALUES (3, 'USD', '10.84', '0.92', '$')
SET identity_insert [CURRENCYTABLE] off
GO
/* Data for table GROUPS */
SET identity_insert [GROUPS] on

INSERT [GROUPS] ([ID], [DEPENDENCY], [DESCRIPTION], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (1, '|0|', 'System Administrators', '20051206 13:06:58', '0', '20051206 13:06:58', '0')
SET identity_insert [GROUPS] off
GO
/* Data for table HELPMENU */
SET identity_insert [HELPMENU] on

INSERT [HELPMENU] ([ID], [MENUID], [HELPFILE]) VALUES (1, 46, 'mailcenter_home.htm')
INSERT [HELPMENU] ([ID], [MENUID], [HELPFILE]) VALUES (2, 7, 'admin_home.htm')
INSERT [HELPMENU] ([ID], [MENUID], [HELPFILE]) VALUES (3, 16, 'archive_home.htm')
INSERT [HELPMENU] ([ID], [MENUID], [HELPFILE]) VALUES (4, 55, 'reports_home.htm')
INSERT [HELPMENU] ([ID], [MENUID], [HELPFILE]) VALUES (5, 57, 'catalogue_home.htm')
INSERT [HELPMENU] ([ID], [MENUID], [HELPFILE]) VALUES (6, 25, 'crm_home.htm')
SET identity_insert [HELPMENU] off
GO
/* Data for table NEWMENU */
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (2, 1, 'Agenda', 'ViewAgenda();', 0, 25, 10, '|1|14|15|16|17|13|', 0, 1, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (6, 2, 'Visualizza Fax', 'faxbox.aspx?m=5&dgb=1', 0, 5, 0, '|0|', 0, 1, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (7, 3, 'Admin', 'Default.aspx?m=7&dgb=1', 0, 0, 900, '|1|13|14|15|16|17|', 1, 1, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (8, 4, 'Utenti', 'NewUser.aspx?m=7&dgb=1', 0, 7, 0, '|1|4|', 0, 1, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (10, 5, 'Appunti', 'crm_notes.aspx?m=25&dgb=1', 0, 25, 120, '|1|13|14|15|16|17|', 0, 1, 'CRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (11, 6, 'Messaggi', 'Base_Messages.aspx?m=25&dgb=1', 0, 25, 130, '|1|13|14|15|16|17|', 0, 1, 'CRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (12, 7, 'Esci', 'default.aspx?logoff=true', 0, 0, 1000, '|1|13|14|15|16|17|', 1, 1, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (13, 8, 'WorkTime', NULL, 0, 0, 0, '|0|', 1, 0, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (14, 9, 'Nuova comunicazione', 'worktime.aspx?action=NEW&m=13&dgb=1', 0, 13, 0, '|0|', 0, 0, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (15, 10, 'Situazione comunicazioni', 'worktime.aspx?action=LIST&m=13&dgb=1', 0, 13, 0, '|0|', 0, 0, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (16, 11, 'Archivio', 'datastorage.aspx?m=16&dgb=1', 0, 0, 90, '|0|', 1, 1, 'DataStorage', 0, 2)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (17, 12, 'Crea WorkSpace', 'WorkSpace.aspx?workspace=new&m=16&dgb=1', 0, 16, 0, '|1|4|', 0, 0, NULL, 0, 2)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (18, 13, 'Apre WorkSpace', 'DocFlow.aspx?m=16&dgb=1', 0, 16, 0, '|1|4|', 0, 0, NULL, 0, 2)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (19, 14, 'Categorie DocFlow', 'Admin_Categorie.aspx?m=7&dgb=1', 0, 0, 0, '|0|', 0, 0, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (20, 15, 'Uffici', 'Offices.aspx?m=7&dgb=1', 0, 7, 0, '|1|', 0, 0, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (21, 16, 'Gruppi', 'Groups.aspx?m=7&dgb=1', 0, 7, 0, '|1|', 0, 0, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (22, 17, 'Menu', 'Menu.aspx?m=7&dgb=1', 0, 7, 0, '|1|', 0, 1, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (23, 18, 'To Do List(OLD)', 'todolist.aspx?m=25&dgb=1', 0, 25, 140, '|1|13|14|15|16|17|', 0, 0, 'CRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (24, 19, 'Gestione Account', 'NewUser.aspx?m=7&mo=1&dgb=1', 0, 7, 0, '|1|13|', 0, 1, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (25, 20, 'CRM', 'default.aspx?m=25&dgb=1', 0, 0, 2, '|1|13|', 1, 1, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (26, 21, 'Admin Caratteristiche', 'matricecaratteristiche.aspx?m=25&dgb=1', 0, 25, 110, '|0|', 0, 0, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (27, 22, 'Prodotti', 'prodotti.aspx?m=25&dgb=1', 0, 25, 90, '|0|', 0, 0, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (29, 24, 'Aziende', 'CRM_Companies.aspx?m=25&dgb=1', 0, 25, 20, '|1|13|', 0, 1, 'CRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (31, 25, 'Contatti', 'Base_Contacts.aspx?m=25&dgb=1', 0, 25, 30, '|1|13|14|15|16|17|', 0, 1, 'CRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (33, 27, 'Azienda', 'Company.aspx?m=7&dgb=1', 0, 7, 0, '|1|', 0, 1, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (35, 29, 'Cerca', 'kbview.aspx?m=34&dgb=1', 0, 34, 0, '|1|', 0, 0, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (36, 30, 'Campi Liberi', 'FreeFields.aspx?m=7&dgb=1', 0, 7, 0, '|1|', 0, 0, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (37, 31, 'Opportunit&agrave;', 'CRM_Opportunity.aspx?m=25&dgb=1', 0, 25, 50, '|1|', 0, 1, 'CRM', 0, 1)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (38, 32, 'Attivit&agrave;', 'AllActivity.aspx?m=25&dgb=1', 0, 25, 60, NULL, 0, 1, 'WorkingCRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (39, 33, 'Admin Articoli', 'KBEdit.aspx?m=34&dgb=1', 0, 34, 0, '|1|', 0, 0, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (40, 34, 'Competitor', 'CRM_Competitor.aspx?m=25&dgb=1', 0, 25, 70, '|1|', 0, 0, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (41, 35, 'Reminder', 'CRM_Reminder.aspx?m=25&dgb=1', 0, 25, 80, NULL, 0, 1, 'CRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (42, 61, 'Rapporti', 'QBDefault.aspx?m=55&dgb=1', 0, 55, 255, NULL, 0, 1, 'CRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (43, 37, 'ToDoList', 'CRM_ToDoList.aspx?m=25&dgb=1', 0, 25, 150, NULL, 0, 0, 'CRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (44, 38, 'Categorie', 'AdminFolder.aspx?m=16&dgb=1', 0, 16, 10, '|1|', 0, 1, 'DataStorage', 0, 2)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (45, 39, 'Documenti', 'datastorage.aspx?m=16&dgb=1', 0, 16, 1, NULL, 0, 1, 'DataStorage', 0, 2)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (46, 40, 'Mail center', 'MailCenter.aspx?m=46&dgb=1', 0, 0, 3, '|0|', 1, 1, 'MailingList', 0, 1)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (47, 41, 'Importa Dati', 'loadcsv.aspx?m=7&dgb=1', 0, 7, 99, NULL, 0, 1, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (48, 42, 'Liste', 'Lists.aspx?m=7&dgb=1', 0, 7, 100, NULL, 0, 0, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (49, 43, 'Record eliminati', 'limbo.aspx?m=7&dgb=1', 0, 7, 101, NULL, 0, 1, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (50, 44, 'Cronologia', 'chronology.aspx?m=25&dgb=1', 0, 25, 170, NULL, 0, 0, 'WorkingCRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (51, 56, 'Mailing List', 'editor.aspx?m=46&dgb=1', 0, 46, 1, NULL, 0, 1, 'MailingList', 0, 1)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (52, 57, 'Mail events', 'MailEvents.aspx?m=46&dgb=1', 0, 46, 2, NULL, 0, 1, 'MailingList', 1, 1)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (53, 47, 'Lead', 'CRM_Lead.aspx?m=25&dgb=1', 0, 25, 40, NULL, 0, 1, 'CRM', 0, 1)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (55, 36, 'Rapporti', 'DashBoard.aspx?m=55&dgb=1', 0, 0, 250, NULL, 1, 1, 'Dash', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (56, 49, 'Colpo d''occhio', 'DashBoard.aspx?m=55&dgb=1', 0, 55, 251, NULL, 0, 1, 'Dash', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (57, 50, 'Catalogo', 'CatalogProducts.aspx?m=57&dgb=1', 0, 0, 800, NULL, 1, 1, 'Catalog', 0, 4)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (58, 51, 'Categorie', 'CatalogCategories.aspx?m=57&dgb=1', 0, 57, 1, NULL, 0, 1, 'Catalog', 0, 4)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (61, 54, 'Valute', 'CurrencyAdmin.aspx?m=7&dgb=1', 0, 7, 102, NULL, 0, 0, 'Admin', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (64, 60, 'HomePage', 'default.aspx?m=25&dgb=1', 0, 25, -1, NULL, 0, 1, NULL, 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (63, 59, 'Mail personale', 'webmail.aspx?m=46&dgb=1', 0, 46, 3, NULL, 0, 1, 'mailinglist/webmail', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (65, 36, 'Report', 'QBFixed.aspx?m=55&dgb=1', 0, 55, 253, NULL, 0, 1, 'CRM', 0, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (66, 62, 'Quick', 'Quick.aspx?m=25&dgb=1', 0, 25, 150, NULL, 0, 1, 'quick', 1, 1)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (59, 52, 'Prodotti', 'CatalogProducts.aspx?m=57&dgb=1', 0, 57, 2, NULL, 0, 1, 'Catalog', 0, 4)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (67, 63, 'ERP', 'quotelist.aspx?m=67&dgb=1', 0, 0, 850, NULL, 1, 1, 'erp', 1, 4)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (68, 64, 'Config ERP', 'erpConfiguration.aspx?m=7&dgb=1', 0, 7, 1, NULL, 0, 1, 'admin', 1, 0)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (69, 53, 'Preventivi', 'quotelist.aspx?m=67&dgb=1', 0, 67, 2, NULL, 0, 1, 'erp', 1, 4)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (72, 67, 'Ordini', 'orderlist.aspx?m=67&dgb=1', 0, 67, 3, NULL, 0, 1, 'erp', 1, 4)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (73, 68, 'Listino', 'CatalogList.aspx?m=57&dgb=1', 0, 57, 3, NULL, 0, 1, 'catalog', 1, 4)
INSERT [NEWMENU] ([ID], [RMVALUE], [VOICE], [LINK], [LASTMENU], [PARENTMENU], [SORTORDER], [ACCESSGROUP], [MENUTITLE], [ACTIVE], [FOLDER], [MODE], [MODULES]) VALUES (79, 73, 'Gestione listini', 'CatalogListManagment.aspx?m=57&dgb=1', 0, 57, 4, NULL, 0, 1, 'catalog', 0, 4)
/* Data for table OFFICES */
SET identity_insert [OFFICES] on

INSERT [OFFICES] ([ID], [OFFICE], [CREATEDDATE], [CREATEDBYID], [LASTMODIFIEDDATE], [LASTMODIFIEDBYID]) VALUES (1, 'Main office', '20051206 13:06:58', '0', '20051206 13:06:58', '0')
SET identity_insert [OFFICES] off
GO
/* Data for table QB_CUSTOMERQUERY */
SET identity_insert [QB_CUSTOMERQUERY] on

INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('41', 'Scheda Azienda', '|1|', '2', NULL, '1', '20040415 14:56:26', NULL, 'Fixed1', NULL, '0', NULL)
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('42', 'Scheda Contatto', '|1|', '2', NULL, '1', '20040416 9:42:53', NULL, 'Fixed2', NULL, '0', NULL)
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('43', 'Lista Contatti Per Azienda', '|1|', '2', NULL, '1', '20040416 11:23:10', NULL, 'Fixed3', NULL, '0', NULL)
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('44', 'Attivita Per Azienda', '|1|', '2', NULL, '1', '20040416 17:29:32', NULL, 'Fixed4', NULL, '0', NULL)
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('45', 'Scheda Azienda', '|1|', '2', NULL, '1', '20040416 18:02:54', NULL, 'Fixed5', NULL, '0', NULL)
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('222', 'Scheda Lead', '|1|', '2', NULL, '1', '20040708 15:05:30', NULL, 'Fixed6', NULL, '0', NULL)
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('278', 'Aziende WAP', '|1|', '2', NULL, '1', '20041004 11:42:19', NULL, 'Fixed7', 1, '0', NULL)
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('279', 'Contatti WAP', '|1|', '2', NULL, '1', '20041004 11:43:27', NULL, 'Fixed8', 2, '0', NULL)
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('556', 'lista delle attività relative ad una opportunità in un dato periodo ', '|1|', '6', NULL, '79', '20050406 11:03:57', '15|116|0', 'Fixed39', NULL, '0', '53|54')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('612', 'Lista delle aziende in una o più città', '|1|', '3', NULL, '79', '20050415 12:41:30', '1|8|0', 'Fixed9', NULL, '0', '15|16')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('618', 'Lista delle aziende in base ad uno o più tipi azienda. ', '|1|', '3', NULL, '79', '20050415 15:07:08', '1|26|1', 'Fixed10', NULL, '0', '23|24')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('620', 'Lista delle aziende appartenenti ad uno o più settori', '|1|', '3', NULL, '79', '20050415 15:08:19', '1|27|1', 'Fixed11', NULL, '0', '21|22')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('621', 'Lista delle aziende in una o più provincie', '|1|', '3', NULL, '79', '20050415 15:09:04', '1|9|0', 'Fixed12', NULL, '0', '19|20')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('622', 'Lista delle aziende suddivise per commerciale che le segue. ', '|1|', '3', NULL, '79', '20050415 15:09:34', '1|68|5', 'Fixed13', NULL, '0', '17|18')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('623', 'Lista delle aziende in base alla valutazione data dal commerciale.', '|1|', '3', NULL, '79', '20050415 15:10:56', '1|28|1', 'Fixed14', NULL, '0', '25|26')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('624', 'Lista delle aziende appartenenti ad una o più categorie.', '|1|', '3', NULL, '1', '20050415 15:20:38', '1|54|2', 'Fixed15', NULL, '0', '13|14')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('627', 'Lista delle attività relative ad una opportunità', '|1|', '6', NULL, '79', '20050415 15:35:09', '3|104|0', 'Fixed38', NULL, '0', '49|50')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('630', 'Lista delle attività eseguite da un commerciale in un dato periodo di tempo in ordine cronologico.', '|1|', '5', NULL, '79', '20050415 15:41:27', '4|71|8', 'Fixed25', NULL, '0', '5|6')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('631', 'Lista delle attività suddivise per tipo.', '|1|', '5', NULL, '79', '20050415 15:42:45', '4|56|1', 'Fixed26', NULL, '0', '7|8')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('633', 'Lista delle attività in base alla loro priorità.', '|1|', '5', NULL, '1', '20050415 15:54:35', '4|64|13', 'Fixed27', NULL, '0', '3|4')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('635', 'Lista delle attività svolte in un dato periodo.', '|1|', '5', NULL, '79', '20050415 16:37:15', '4|71|8', 'Fixed28', NULL, '0', '1|2')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('663', 'Lista leads in base alla nazione.', '|1|', '4', NULL, '79', '20050418 17:12:58', '12|79|0', 'Fixed16', NULL, '0', '33|34')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('672', 'Lista dei leads in base alla valutazione ', '|1|', '4', NULL, '79', '20050418 17:44:40', '12|96|1', 'Fixed17', NULL, '0', '43|44')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('674', 'Lista dei leads in base all''interesse', '|1|', '4', NULL, '79', '20050418 17:46:16', '12|97|1', 'Fixed18', NULL, '0', '31|32')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('677', 'Lista delle opportunità di un''azienda', '|1|', '6', NULL, '79', '20050418 17:51:50', '14|111|0', 'Fixed31', NULL, '0', '55|56')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('681', 'Lista delle opportunità appartenenti ad un commerciale.', '|1|', '6', NULL, '79', '20050420 17:11:43', '', 'Fixed32', NULL, '0', '59|60')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('699', 'Lista delle opportunità con reddito maggiore a:', '|1|', '6', NULL, '79', '20050421 12:37:12', '', 'Fixed33', NULL, '2', '61|62')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('700', 'Lista opportunità con confronto tra il reddito previsto e quello realmente conseguito.', '|1|', '6', NULL, '79', '20050421 12:39:27', '', 'Fixed34', NULL, '2', '63|64')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('707', 'Lista dei Leads suddivisi per settore al quale appartengono.', '|1|', '4', NULL, '79', '20050421 15:31:31', '12|102|1', 'Fixed19', NULL, '0', '39|40')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('708', 'Lista dei leads di una data città.', '|1|', '4', NULL, '79', '20050421 15:31:52', '12|76|0', 'Fixed20', NULL, '0', '29|30')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('716', 'Lista delle opportunità suddivise per commerciale con l''analisi del reddito', '|1|', '6', NULL, '79', '20050421 15:38:09', '', 'Fixed36', NULL, '0', '45|46')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('717', 'Aziende in opportunità con relativo reddito acquisito.', '|1|', '6', NULL, '79', '20050421 15:38:33', '', 'Fixed37', NULL, '0', '47|48')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('721', 'Lista delle attività svolte suddivise per azienda.', '|1|', '5', NULL, '79', '20050421 15:48:46', '4|60|0', 'Fixed29', NULL, '3', '9|10')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('724', 'Lista delle aziende di un''opportunità in fase decisionale', '|1|', '6', NULL, '1', '20050421 16:07:24', '', 'Fixed30', NULL, '2', '11|12')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('725', 'Lista dei leads in base all''origine di provenienza', '|1|', '4', NULL, '1', '20050421 16:12:37', '12|98|1', 'Fixed21', NULL, '0', '35|36')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('726', 'Lista leads in base allo stato della trattativa', '|1|', '4', NULL, '1', '20050421 16:16:48', '12|95|1', 'Fixed22', NULL, '0', '41|42')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('727', 'Lista dei leads di un commerciale.', '|1|', '4', NULL, '1', '20050421 16:20:18', '12|94|5', 'Fixed23', NULL, '0', '37|38')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('728', 'Lista delle attività svolte suddivise per contatti.', '|1|', '5', NULL, '1', '20050421 16:23:54', '4|61|3', 'Fixed24', NULL, '0', '27|28')
INSERT [QB_CUSTOMERQUERY] ([ID], [DESCRIPTION], [GROUPS], [QUERYTYPE], [EXTENDEDWHERE], [CREATEDBYID], [CREATEDDATE], [GROUPBY], [TITLE], [FROMWAP], [CATEGORY], [RM]) VALUES ('731', 'Lista delle opportunità legate ad uno o più leads.', '|1|', '6', NULL, '1', '20050422 11:31:43', '', 'Fixed35', NULL, '0', '57|58')
SET identity_insert [QB_CUSTOMERQUERY] off
GO
/* Data for table QB_CUSTOMERQUERYFIELDS */
SET identity_insert [QB_CUSTOMERQUERYFIELDS] on

INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('109', '42', 2, 31, 1, NULL, '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('110', '42', 2, 30, 1, NULL, '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('111', '42', 2, 32, 1, NULL, '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('112', '42', 2, 33, 1, NULL, '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('113', '42', 2, 34, 1, NULL, '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('114', '42', 2, 35, 1, NULL, '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('115', '42', 2, 36, 1, NULL, '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('116', '42', 2, 37, 1, NULL, '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('117', '42', 2, 38, 1, NULL, '9,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('118', '42', 2, 39, 1, NULL, '10,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('119', '42', 2, 40, 1, NULL, '11,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('120', '42', 2, 41, 1, NULL, '12,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('121', '42', 2, 42, 1, NULL, '13,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('122', '42', 2, 43, 1, NULL, '14,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('123', '42', 2, 44, 1, NULL, '15,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('124', '42', 2, 45, 1, NULL, '16,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('125', '42', 2, 46, 1, NULL, '17,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('126', '42', 2, 47, 1, NULL, '18,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('127', '42', 2, 48, 1, NULL, '19,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('128', '42', 2, 49, 1, NULL, '20,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('129', '42', 2, 50, 1, NULL, '21,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('130', '42', 2, 51, 1, NULL, '22,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('131', '42', 2, 52, 1, NULL, '23,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('132', '42', 2, 53, 1, NULL, '24,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('133', '42', -2, 10, 1, NULL, '25,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('134', '43', 1, 1, 0, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('135', '43', 2, 31, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('136', '43', 2, 30, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('137', '43', 2, 46, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('138', '43', 2, 49, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('139', '43', 2, 48, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('140', '43', 2, 45, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('141', '44', 1, 1, 0, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('142', '44', 4, 61, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('143', '44', 4, 58, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('144', '44', 4, 62, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('145', '44', 4, 63, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('146', '44', 4, 65, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('147', '44', 4, 66, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('148', '44', 4, 67, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('149', '44', 4, 59, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('150', '45', 1, 1, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('151', '45', 1, 4, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('152', '45', 1, 5, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('153', '45', 1, 7, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('154', '45', 1, 8, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('155', '45', 1, 9, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('156', '45', 1, 10, 1, NULL, NULL)
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('985', '222', 12, 72, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('986', '222', 12, 73, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('987', '222', 12, 74, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('988', '222', 12, 141, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('989', '222', 12, 75, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('990', '222', 12, 76, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('991', '222', 12, 77, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('992', '222', 12, 78, 1, '', '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('993', '222', 12, 79, 1, '', '9,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('994', '222', 12, 80, 1, '', '10,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('995', '222', 12, 81, 1, '', '11,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('996', '222', 12, 82, 1, '', '12,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('997', '222', 12, 83, 1, '', '13,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('998', '222', 12, 84, 1, '', '14,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('999', '222', 12, 85, 1, '', '15,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1000', '222', 12, 86, 1, '', '16,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1001', '222', 12, 87, 1, '', '17,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1002', '222', 12, 88, 1, '', '18,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1003', '222', 12, 89, 1, '', '19,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1004', '222', 12, 91, 1, '', '20,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1005', '222', 12, 92, 1, '', '21,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1006', '222', 12, 93, 1, '', '22,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1007', '222', 12, 94, 1, '', '23,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1008', '222', 12, 95, 1, '', '24,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1009', '222', 12, 96, 1, '', '25,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1010', '222', 12, 97, 1, '', '26,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1011', '222', 12, 98, 1, '', '27,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1012', '222', 12, 99, 1, '', '28,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1013', '222', 12, 100, 1, '', '29,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1014', '222', 12, 101, 1, '', '30,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1015', '222', 12, 102, 1, '', '31,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1016', '222', 12, 103, 1, '', '32,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1329', '278', 1, 1, 1, '', '1,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1330', '278', 1, 7, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1331', '278', 1, 8, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1332', '278', 1, 9, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1333', '278', 1, 10, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1334', '278', 1, 3, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1335', '278', 1, 4, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1336', '278', 1, 5, 1, '', '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1337', '279', 2, 30, 1, '', '1,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1338', '279', 2, 31, 1, '', '2,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1339', '279', 2, 33, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1340', '279', 2, 34, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1341', '279', 2, 35, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1342', '279', 2, 37, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1343', '279', 2, 36, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1344', '279', 2, 46, 1, '', '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1345', '279', 2, 49, 1, '', '9,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1346', '279', 2, 48, 1, '', '10,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('1347', '279', 2, 45, 1, '', '11,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('2792', '556', 3, 104, 1, '', '1,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('2793', '556', 4, 55, 1, '', '2,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('2794', '556', 4, 71, 1, '', '3,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('2795', '556', 4, 56, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('2796', '556', 4, 58, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('2797', '556', 4, 59, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('2798', '556', 15, 116, 1, '', '7,1,1,1,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3162', '41', 1, 1, 1, NULL, '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3163', '41', 1, 2, 1, NULL, '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3164', '41', 1, 3, 1, NULL, '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3165', '41', 1, 4, 1, NULL, '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3166', '41', 1, 5, 1, NULL, '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3167', '41', 1, 6, 1, NULL, '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3168', '41', 1, 7, 1, NULL, '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3169', '41', 1, 8, 1, NULL, '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3170', '41', 1, 9, 1, NULL, '9,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3171', '41', 1, 165, 1, NULL, '10,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3172', '41', 1, 10, 1, NULL, '11,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3173', '41', 1, 26, 1, NULL, '12,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3174', '41', 1, 27, 1, NULL, '13,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3175', '41', 1, 28, 1, NULL, '14,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3176', '41', 1, 29, 1, NULL, '15,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3177', '41', 1, 163, 1, NULL, '16,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3178', '41', 1, 68, 1, NULL, '17,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3179', '41', 1, 54, 1, NULL, '18,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3180', '41', 1, 11, 1, NULL, '19,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3181', '41', 1, 12, 1, NULL, '20,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3182', '41', 1, 13, 1, NULL, '21,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3183', '41', 1, 166, 1, NULL, '22,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3184', '41', 1, 14, 1, NULL, '23,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3185', '41', 1, 15, 1, NULL, '24,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3186', '41', 1, 16, 1, NULL, '25,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3187', '41', 1, 17, 1, NULL, '26,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3188', '41', 1, 18, 1, NULL, '27,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3189', '41', 1, 19, 1, NULL, '28,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3190', '41', 1, 20, 1, NULL, '29,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3191', '41', 1, 167, 1, NULL, '30,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3192', '41', 1, 21, 1, NULL, '31,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3193', '41', 1, 22, 1, NULL, '32,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3194', '41', 1, 23, 1, NULL, '33,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3195', '41', 1, 24, 1, NULL, '34,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3196', '41', 1, 25, 1, NULL, '35,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3197', '612', 1, 8, 1, '', '1,1,1,1,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3198', '612', 1, 1, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3199', '612', 1, 7, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3200', '612', 1, 10, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3201', '612', 1, 9, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3236', '618', 1, 26, 1, '', '1,1,1,1,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3237', '618', 1, 1, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3238', '618', 1, 7, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3239', '618', 1, 10, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3240', '618', 1, 8, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3241', '618', 1, 9, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3242', '618', 1, 68, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3249', '620', 1, 1, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3250', '620', 1, 7, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3251', '620', 1, 10, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3252', '620', 1, 8, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3253', '620', 1, 9, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3254', '620', 1, 27, 1, '', '6,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3255', '621', 1, 1, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3256', '621', 1, 7, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3257', '621', 1, 10, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3258', '621', 1, 8, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3259', '621', 1, 9, 1, '', '5,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3260', '622', 1, 68, 1, '', '1,1,1,1,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3261', '622', 1, 1, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3262', '622', 1, 7, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3263', '622', 1, 10, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3264', '622', 1, 8, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3265', '622', 1, 9, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3266', '623', 1, 1, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3267', '623', 1, 7, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3268', '623', 1, 10, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3269', '623', 1, 8, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3270', '623', 1, 9, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3271', '623', 1, 28, 1, '', '6,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3272', '624', 1, 54, 1, '', '1,1,1,1,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3273', '624', 1, 1, 1, '', '2,1,1,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3274', '624', 1, 7, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3275', '624', 1, 10, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3276', '624', 1, 8, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3277', '624', 1, 9, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3294', '627', 3, 104, 1, '', '1,1,1,1,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3295', '627', 3, 106, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3296', '627', 4, 56, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3297', '627', 4, 60, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3298', '627', 4, 195, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3299', '627', 4, 58, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3300', '627', 4, 59, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3301', '627', 4, 61, 1, '', '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3316', '630', 4, 56, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3317', '630', 4, 60, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3318', '630', 4, 195, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3319', '630', 4, 55, 1, '', '4,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3320', '630', 4, 58, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3321', '630', 4, 59, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3322', '630', 4, 71, 1, '', '7,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3323', '631', 4, 56, 1, '', '1,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3324', '631', 4, 60, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3325', '631', 4, 195, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3326', '631', 4, 55, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3327', '631', 4, 58, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3328', '631', 4, 59, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3329', '631', 4, 61, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3339', '633', 4, 56, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3340', '633', 4, 60, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3341', '633', 4, 195, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3342', '633', 4, 55, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3343', '633', 4, 58, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3344', '633', 4, 59, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3345', '633', 4, 61, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3346', '633', 4, 63, 1, '', '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3347', '633', 4, 64, 1, '', '9,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3357', '635', 4, 71, 1, '', '1,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3358', '635', 4, 56, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3359', '635', 4, 60, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3360', '635', 4, 195, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3361', '635', 4, 55, 1, '', '5,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3362', '635', 4, 58, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3363', '635', 4, 59, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3364', '635', 4, 64, 1, '', '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3365', '635', 4, 63, 1, '', '9,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3561', '663', 12, 141, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3562', '663', 12, 72, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3563', '663', 12, 73, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3564', '663', 12, 75, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3565', '663', 12, 78, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3566', '663', 12, 76, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3567', '663', 12, 77, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3568', '663', 12, 79, 1, '', '8,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3569', '663', 12, 83, 1, '', '9,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3570', '663', 12, 84, 1, '', '10,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3571', '663', 12, 82, 1, '', '11,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3626', '672', 12, 141, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3627', '672', 12, 73, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3628', '672', 12, 72, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3629', '672', 12, 96, 1, '', '4,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3639', '674', 12, 141, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3640', '674', 12, 73, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3641', '674', 12, 72, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3642', '674', 12, 97, 1, '', '4,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3656', '677', 14, 111, 1, '', '1,1,1,1,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3657', '677', 3, 104, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3658', '677', 3, 105, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3659', '677', 3, 106, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3674', '681', 3, 104, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3675', '681', 3, 105, 1, '', '2,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3676', '681', 3, 106, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3724', '699', 3, 104, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3725', '699', 3, 108, 1, '', '2,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3726', '700', 3, 104, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3727', '700', 3, 108, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3728', '700', 3, 110, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3751', '707', 12, 141, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3752', '707', 12, 73, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3753', '707', 12, 72, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3754', '707', 12, 76, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3755', '707', 12, 94, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3756', '707', 12, 102, 1, '', '6,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3757', '708', 12, 72, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3758', '708', 12, 73, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3759', '708', 12, 141, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3760', '708', 12, 75, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3761', '708', 12, 76, 1, '', '5,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3762', '708', 12, 83, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3763', '708', 12, 84, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3764', '708', 12, 82, 1, '', '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3789', '716', 3, 105, 1, '', '1,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3790', '716', 3, 104, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3791', '716', 3, 106, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3792', '716', 3, 108, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3793', '716', 3, 109, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3794', '716', 3, 110, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3795', '717', 14, 111, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3796', '717', 14, 160, 1, '', '2,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3797', '717', 14, 115, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3823', '721', 4, 61, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3824', '721', 4, 60, 1, '', '2,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3825', '721', 4, 55, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3826', '721', 4, 56, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3827', '721', 4, 58, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3828', '721', 4, 59, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3829', '721', 4, 63, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3830', '721', 4, 64, 1, '', '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3843', '724', 14, 122, 1, '', '1,1,0,1,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3844', '724', 14, 111, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3845', '724', 3, 104, 1, '', '3,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3846', '725', 12, 141, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3847', '725', 12, 73, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3848', '725', 12, 72, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3849', '725', 12, 98, 1, '', '4,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3850', '726', 12, 141, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3851', '726', 12, 73, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3852', '726', 12, 72, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3853', '726', 12, 95, 1, '', '4,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3854', '726', 12, 96, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3855', '726', 12, 97, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3856', '726', 12, 94, 1, '', '7,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3857', '727', 12, 141, 1, '', '1,1,0,1,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3858', '727', 12, 73, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3859', '727', 12, 72, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3860', '727', 12, 75, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3861', '727', 12, 78, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3862', '727', 12, 76, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3863', '727', 12, 77, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3864', '727', 12, 79, 1, '', '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3865', '727', 12, 94, 1, '', '9,1,1,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3866', '728', 4, 61, 1, '', '1,1,1,1,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3867', '728', 4, 56, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3868', '728', 4, 57, 1, '', '3,1,0,0,1')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3869', '728', 4, 60, 1, '', '4,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3870', '728', 4, 55, 1, '', '5,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3871', '728', 4, 58, 1, '', '6,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3872', '728', 4, 59, 1, '', '7,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3873', '728', 4, 63, 1, '', '8,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3874', '728', 4, 64, 1, '', '9,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3883', '731', 3, 104, 1, '', '1,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3884', '731', 3, 105, 1, '', '2,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3885', '731', 3, 106, 1, '', '3,1,0,0,0')
INSERT [QB_CUSTOMERQUERYFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIELDVISIBLE], [COLUMNNAME], [OPTIONS]) VALUES ('3886', '731', 15, 116, 1, '', '4,1,0,1,1')
SET identity_insert [QB_CUSTOMERQUERYFIELDS] off
GO
/* Data for table QB_CUSTOMERQUERYPARAMFIELDS */
SET identity_insert [QB_CUSTOMERQUERYPARAMFIELDS] on

INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('20', '41', 1, 0, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('21', '42', 2, 0, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('22', '43', 1, 0, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('23', '44', 1, 0, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('24', '45', 1, 0, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('268', '222', 12, 0, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('306', '278', 1, 1, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('307', '279', 2, 30, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('308', '279', 2, 31, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('597', '556', 3, 104, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('598', '556', 4, 55, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('599', '556', 4, 71, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('659', '612', 1, 8, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('664', '618', 1, 26, '1')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('666', '620', 1, 27, '1')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('667', '621', 1, 9, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('668', '622', 1, 68, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('669', '623', 1, 28, '1')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('670', '624', 1, 54, '2')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('673', '627', 3, 104, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('678', '630', 4, 55, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('679', '630', 4, 71, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('680', '631', 4, 56, '1')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('682', '633', 4, 64, '0')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('686', '635', 4, 71, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('687', '635', 4, 55, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('709', '663', 12, 79, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('720', '672', 12, 96, '4')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('722', '674', 12, 97, '8')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('725', '677', 14, 111, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('728', '681', 3, 105, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('749', '699', 3, 108, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('754', '707', 12, 102, '1')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('755', '708', 12, 76, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('760', '716', 3, 105, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('761', '717', 14, 160, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('765', '721', 4, 60, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('768', '724', 14, 122, '11')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('769', '724', 3, 104, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('770', '725', 12, 98, '11')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('771', '726', 12, 95, '1')
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('772', '726', 12, 94, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('773', '727', 12, 94, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('774', '728', 4, 57, NULL)
INSERT [QB_CUSTOMERQUERYPARAMFIELDS] ([ID], [IDQUERY], [IDTABLE], [IDFIELD], [FIXEDVALUE]) VALUES ('777', '731', 15, 116, NULL)
SET identity_insert [QB_CUSTOMERQUERYPARAMFIELDS] off
GO
/* Data for table QB_CUSTOMERQUERYTABLES */
SET identity_insert [QB_CUSTOMERQUERYTABLES] on

INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('21', '41', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('22', '41', -1, 0)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('23', '42', 2, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('24', '42', -2, 0)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('25', '43', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('26', '43', 2, 0)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('27', '44', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('28', '44', 4, 0)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('29', '45', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('301', '222', 12, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('392', '278', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('393', '279', 2, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('829', '556', 15, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('830', '556', 3, 0)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('831', '556', 4, 0)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('923', '612', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('929', '618', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('931', '620', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('932', '621', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('933', '622', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('934', '623', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('935', '624', 1, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('940', '627', 3, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('941', '627', 4, 0)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('944', '630', 4, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('945', '631', 4, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('947', '633', 4, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('949', '635', 4, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('984', '663', 12, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('993', '672', 12, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('995', '674', 12, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('998', '677', 14, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('999', '677', 3, 0)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1004', '681', 3, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1033', '699', 3, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1034', '700', 3, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1039', '707', 12, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1040', '708', 12, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1045', '716', 3, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1046', '717', 14, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1050', '721', 4, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1053', '724', 14, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1054', '724', 3, 0)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1055', '725', 12, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1056', '726', 12, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1057', '727', 12, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1058', '728', 4, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1063', '731', 15, 1)
INSERT [QB_CUSTOMERQUERYTABLES] ([ID], [IDQUERY], [IDTABLE], [MAINTABLE]) VALUES ('1064', '731', 3, 0)
SET identity_insert [QB_CUSTOMERQUERYTABLES] off
GO









/* Data for table QB_DROPDOWNPARAMS */
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (26, 'ContactType', 'K_ID', 'ContactType', 'lang', '1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (27, 'CompanyType', 'K_ID', 'Description', 'lang', '1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (28, 'ContactEstimate', 'K_ID', 'Estimate', 'lang', '1', NULL, NULL, 'FieldOrder', NULL, NULL, NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (54, 'CRM_ContactCategories', 'ID', 'Description', NULL, NULL, 'FlagPersonal', 'CreatedByID', NULL, NULL, NULL, NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (56, 'CRM_ActivityType', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (62, 'CRMWorkingClassification', 'ID', 'Description', NULL, NULL, NULL, NULL, NULL, 'type=1', '1', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (95, 'CRM_LeadDescription', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=1', '1', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (96, 'CRM_LeadDescription', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=2', '2', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (97, 'CRM_LeadDescription', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=3', '3', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (98, 'CRM_LeadDescription', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=4', '4', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (99, 'CRM_LeadDescription', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=5', '5', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (102, 'CompanyType', 'K_ID', 'Description', 'lang', '1', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (121, 'CRM_OpportunityTableType', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=1', '1', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (122, 'CRM_OpportunityTableType', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=2', '2', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (123, 'CRM_OpportunityTableType', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=3', '3', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (124, 'CRM_OpportunityTableType', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=1', '1', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (125, 'CRM_OpportunityTableType', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=2', '2', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (126, 'CRM_OpportunityTableType', 'K_ID', 'Description', 'lang', NULL, NULL, NULL, NULL, 'type=3', '3', NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (197, 'CRM_OppLostReasons', 'ID', 'Description', NULL, 'CustomerId=0 or CustomerId={CUSTID}', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [QB_DROPDOWNPARAMS] ([IDRIF], [REFTABLE], [VALUEFIELD], [TEXTFIELD], [LANGFIELD], [P1], [P2], [P3], [P4], [P5], [P6], [P7], [P8]) VALUES (198, 'CRM_OppLostReasons', 'ID', 'Description', NULL, 'CustomerId=0 or CustomerId={CUSTID}', NULL, NULL, NULL, NULL, NULL, NULL, NULL)
/* Data for table QB_FIXEDDROPDOWNPARAMS */
INSERT [QB_FIXEDDROPDOWNPARAMS] ([IDRIF], [DROPVALUE], [RMVALUE]) VALUES ('63', '0', '128')
INSERT [QB_FIXEDDROPDOWNPARAMS] ([IDRIF], [DROPVALUE], [RMVALUE]) VALUES ('63', '1', '129')
INSERT [QB_FIXEDDROPDOWNPARAMS] ([IDRIF], [DROPVALUE], [RMVALUE]) VALUES ('63', '2', '130')
INSERT [QB_FIXEDDROPDOWNPARAMS] ([IDRIF], [DROPVALUE], [RMVALUE]) VALUES ('64', '0', '131')
INSERT [QB_FIXEDDROPDOWNPARAMS] ([IDRIF], [DROPVALUE], [RMVALUE]) VALUES ('64', '1', '132')
INSERT [QB_FIXEDDROPDOWNPARAMS] ([IDRIF], [DROPVALUE], [RMVALUE]) VALUES ('64', '2', '133')
/* Data for table QB_JOIN */
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (1, 2, 1, 'CompanyID', 'ID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (2, 1, 2, 'ID', 'CompanyID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (3, 5, 1, 'ID', 'CompanyTypeID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (4, 6, 1, 'ID', 'ContactTypeID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (5, 7, 1, 'K_ID', 'Estimate', '2', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (6, 8, 1, 'UID', 'OwnerID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (7, 8, 4, 'UID', 'OwnerID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (8, 2, 4, 'ID', 'ReferrerID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (9, 1, 4, 'ID', 'CompanyID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (10, 10, 4, 'ID', 'Classification', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (11, 11, 4, 'K_ID', 'Type', '2', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (12, 4, 1, 'CompanyID', 'ID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (13, 4, 2, 'ReferrerID', 'ID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (14, 1, 12, 'ID', 'AssociatedCompany', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (15, 2, 12, 'ID', 'AssociatedContact', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (18, 3, 12, 'ID', 'AssociatedOpportunity', '1', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (19, 8, 12, 'UID', 'LeadOwner', '0', '1')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (20, 13, 12, 'ID', 'Status', '0', '1')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (21, 13, 12, 'ID', 'Rating', '0', '2')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (22, 13, 12, 'ID', 'ProductInterest', '0', '3')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (23, 13, 12, 'ID', 'PotentialRevenue', '0', '4')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (24, 13, 12, 'ID', 'Source', '0', '5')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (25, 5, 12, 'K_ID', 'Industry', '2', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (26, 8, 12, 'UID', 'SalesPerson', '0', '2')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (27, 1, 14, 'ID', 'ContactID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (28, 3, 14, 'ID', 'OpportunityID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (29, 14, 3, 'OpportunityID', 'ID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (30, 3, 15, 'ID', 'OpportunityID_L', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (31, 15, 3, 'OpportunityID_L', 'ID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (32, 16, 14, 'ID', 'StatusID', '0', '1')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (33, 16, 14, 'ID', 'RatingID', '0', '2')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (34, 16, 14, 'ID', 'ProbabilityID', '0', '3')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (35, 16, 15, 'ID', 'StatusID_L', '0', '1')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (36, 16, 15, 'ID', 'RatingID_L', '0', '2')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (37, 16, 15, 'ID', 'ProbabilityID_L', '0', '3')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (38, 8, 3, 'UID', 'OwnerID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (39, 12, 15, 'ID', 'ContactID_L', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (40, 4, 3, 'OpportunityID', 'ID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (41, 3, 4, 'ID', 'OpportunityID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (42, 8, 14, 'UID', 'SalesPerson', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (43, 8, 15, 'UID', 'SalesPerson_L', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (44, 4, 12, 'LeadID', 'ID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (44, 12, 4, 'LeadID', 'ID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (45, 14, 4, 'ContactID', 'CompanyID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (47, 15, 4, 'ContactID', 'LeadID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (51, 4, 15, 'LeadID', 'ContactID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (52, 4, 14, 'CompanyID', 'ContactID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (53, 4, 12, 'ID', 'LeadID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (54, 17, 14, 'ID', 'LOSTREASON', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (55, 17, 15, 'ID', 'LOSTREASON', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (56, 14, 17, 'LOSTREASON', 'ID', '0', '0')
INSERT [QB_JOIN] ([ID], [FIRSTTABLEID], [SECONDTABLEID], [FIRSTFIELD], [SECONDFIELD], [TYPE], [ASTABLE]) VALUES (57, 15, 17, 'LOSTREASON', 'ID', '0', '0')

/* Data for table QB_LOCKARRAY */
INSERT [QB_LOCKARRAY] ([IDTABLE], [LOCKTABLES]) VALUES ('-3', ',''1'',''2'',''12'',')
INSERT [QB_LOCKARRAY] ([IDTABLE], [LOCKTABLES]) VALUES ('-2', ',''3'',''12'',')
INSERT [QB_LOCKARRAY] ([IDTABLE], [LOCKTABLES]) VALUES ('-1', ',''3'',''12'',')
INSERT [QB_LOCKARRAY] ([IDTABLE], [LOCKTABLES]) VALUES ('1', ',''3'',''12'',')
INSERT [QB_LOCKARRAY] ([IDTABLE], [LOCKTABLES]) VALUES ('2', ',''3'',''12'',')
INSERT [QB_LOCKARRAY] ([IDTABLE], [LOCKTABLES]) VALUES ('3', ',''1'',''2'',''12'',')
INSERT [QB_LOCKARRAY] ([IDTABLE], [LOCKTABLES]) VALUES ('12', ',''3'',''1'',''2'',')
INSERT [QB_LOCKARRAY] ([IDTABLE], [LOCKTABLES]) VALUES ('14', ',''1'',''2'',''12'',')
INSERT [QB_LOCKARRAY] ([IDTABLE], [LOCKTABLES]) VALUES ('15', ',''1'',''2'',''12'',')
/* Data for table TIMEZONES */
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Australian Central Standard Time', 'ACST', 570, '9.5', 'Mid-Australia, Adelaide', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Australian Eastern Standard Time', 'AEST', 660, '11', 'Solomon Islands, Kuril Islands', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Alaska Standard Time', 'AKST', -540, '-9', 'Anchorage, Fairbanks', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Atlantic Standard Time', 'AST ', -240, '-4', 'La Paz, Santiago, Aruba, Labrador, Asuncion, Dominican Republic, Santo Domingo, Puerto Rico, San Juan, Barbados', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Australian Western Standard Time', 'AWST', 480, '8', 'Shanghai, Manila, Taipei, Hong Kong, Perth, Mongolia, Beijing', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Central Europe Time', 'CET ', 60, '1', 'Rome, Florence, Budapest, Berlin, Paris, Prague, Brussels, Vienna, Amsterdam, Warsaw, Oslo, Madrid, Zurich, Belgrade, Stockholm, Tripoli, Casablanca', '1', '20050327 0:00:00', '20051031 0:00:00', 60)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Central Standard Time', 'CST ', -360, '-6', 'Dallas, Chicago, Kansas City, New Orleans, Nashville, Nicaragua, Honduras, Costa Rica, El Salvador, Guatemala, Mexico City, Guadalajara, Winnipeg', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Eastern Europe Time', 'EET ', 120, '2', 'Botswana, Bulgaria, Cyprus, Cairo, Helsinki, Athens, Jerusalem, Tel Aviv, Amman, Bucharest, Cape Town, Istanbul, Zambia, Zimbabwe, Kiev, Minsk', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Eastern Standard Time', 'EST ', -300, '-5', 'Kennebunkport, Washington, New York, Atlanta, Toronto, Havana, Bogota, Lima, Ecuador, Panama ', '0', '20050403 0:00:00', '20051030 0:00:00', 60)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Greenwich Mean Time', 'GMT ', 0, '0', 'Greenwich, London, Glasgow, Dublin, Lisbon, Iceland, Reykjavik, U.T.C., Mali, Liberia', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Hawaiian Standard Time', 'HST ', -600, '-10', 'Aleutian Islands, Hawaii (Honolulu, Maui)', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Mountain Standard Time', 'MST ', -420, '-7', 'Denver, Cheyenne, Salt Lake City, Des Moines, Flagstaff, Edmonton, Calgary, Acapulco', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Newfoundland Standard Time', 'NST ', -210, '-3.5', 'New Foundland, St. Johns', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Pacific Standard Time', 'PST ', -480, '-8', 'Santa Cruz, Los Angeles, San Francisco, Vancouver, Seattle, Tijuana, Mexicali', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 1', 'TZ1 ', 720, '12', 'Fiji, New Zealand, Auckland, Marshall Islands, Petropavlovsk', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 10', 'TZ10', 330, '5.5', 'India (Udaipur, New Delhi, Calcutta)', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 11', 'TZ11', 300, '5', 'Maldives, Pakistan (Karachi, Lahore)', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 12', 'TZ12', 270, '4.5', 'Afghanistan (Kabul, Qandahar)', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 13', 'TZ13', 240, '4', 'Oman, U.A.E., Azerbaijan, Armenia', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 14', 'TZ14', 210, '3.5', 'Iran, Tehran', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 15', 'TZ15', 180, '3', 'Bahrain, Ethiopia, Baghdad, Kenya, Kuwait, Qatar, Saudi Arabia, Tanzania, Uganda, Moscow, St. Petersburg', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 19', 'TZ19', -60, '-1', 'Cape Verde', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 21', 'TZ21', -180, '-3', 'Asuncion, Canelone, Buenos Aires, Rio de Janeiro, Thule', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 30', 'TZ30', -660, '-11', 'American Samoa, Western Samoa', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 5', 'TZ5 ', 540, '9', 'Kyoto,Tokyo, Seoul', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 7', 'TZ7 ', 420, '7', 'Jakarta, Bangkok, Vietnam, Singapore, Malaysia, Cambodia, Laos', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 8', 'TZ8 ', 390, '6.5', 'Burma (Rangoon, Mandalay) ', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('TimeZone 9', 'TZ9 ', 360, '6', 'Bangladesh, Kazakhstan', '0', NULL, NULL, 0)
INSERT [TIMEZONES] ([NAME], [SHORTNAME], [MTIMEZONE], [HTIMEZONE], [CITY], [DTZ], [DAYLIGHTSAVINGSTART], [DAYLIGHTSAVINGEND], [MDAYLIGHT]) VALUES ('Western Europe Time', 'WET ', 600, '10', 'Sydney, Guam, Saipan, Melbourne, New Guinea, Vladivostok, Micronesia', '0', NULL, NULL, 0)
/* Data for table TUSTENA_DATA */
SET identity_insert [TUSTENA_DATA] on

INSERT [TUSTENA_DATA] ([ID], [COMPANYNAME], [LICENCE], [PHONE], [FAX], [EMAIL], [WEBSITE], [ADDRESS], [CITY], [PROVINCE], [REGION], [STATE], [ZIPCODE], [CREATEDDATE], [ACTIVE], [TESTING], [MAXUSER], [ADMINGROUPID], [PHONENORMALIZE], [CUSTOMTYPES], [LASTACCESS], [TESTINGDAYS], [TAXIDENTIFICATIONNUMBER], [VATID], [ESTIMATEDDATEDAYS], [DEBUGMODE], [EXPIRATIONDATE], [FROMWEB], [GUID], [PIN], [DEFAULTWEBUSER], [IDAGENDA], [DATASTORAGECAPACITY], [SMSCREDIT], [SMSORIGIN], [LINKFORVOIP], [INTERNATIONALPREFIX], [DISKSPACE], [WIZARD], [LOGO], [MODULES]) VALUES (1, 'Tustena Custom Company', '', '', '', '', '', '', '', '', '', '', 'May 19 2006 12:10PM', '20060519 12:10:06', 1, 1, 20, '1', NULL, 0, '20060809 18:58:08', 30, '', '', 90, 1, NULL, NULL, 'e30df889-6390-4dff-ba54-5d9137c4757d', 99999, '1', NULL, 20, 9, NULL, 'test://', '', 30, 0, NULL, 127)
SET identity_insert [TUSTENA_DATA] off
GO
/*-----END SCRIPT------*/

ALTER TABLE [dbo].[ACCOUNT] WITH NOCHECK ADD 
	CONSTRAINT [PK_ACCOUNT] PRIMARY KEY  CLUSTERED 
	(
		[UID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ACTIVITYMOVELOG] WITH NOCHECK ADD 
	CONSTRAINT [PK_ACTIVITYMOVELOG] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ADDEDFIELDS] WITH NOCHECK ADD 
	CONSTRAINT [PK_ADDEDFIELDS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ADDEDFIELDS_CROSS] WITH NOCHECK ADD 
	CONSTRAINT [PK_ADDEDFIELDS_CROSS] PRIMARY KEY  CLUSTERED 
	(
		[PKEY]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ADDEDFIELD_DROPDOWN] WITH NOCHECK ADD 
	CONSTRAINT [PK_ADDEDFIELD_DROPDOWN] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BASE_CALENDAR] WITH NOCHECK ADD 
	CONSTRAINT [PK_BASE_CALENDAR] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BASE_COMPANIES] WITH NOCHECK ADD 
	CONSTRAINT [PK_BASE_CONTACTS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BASE_CONTACTS] WITH NOCHECK ADD 
	CONSTRAINT [PK_BASE_REFERRING] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BASE_CONTACTS_LINKS] WITH NOCHECK ADD 
	CONSTRAINT [PK_BASE_CONTACTS_LINKS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BASE_EVENTS] WITH NOCHECK ADD 
	CONSTRAINT [PK_BASE_EVENTS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BASE_MESSAGES] WITH NOCHECK ADD 
	CONSTRAINT [PK_BASE_MESSAGES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[BASE_NOTES] WITH NOCHECK ADD 
	CONSTRAINT [PK_BASE_NOTES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CATALOGCATEGORIES] WITH NOCHECK ADD 
	CONSTRAINT [PK_CATALOGCATEGORIES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CATALOGPRICELISTDESCRIPTION] WITH NOCHECK ADD 
	CONSTRAINT [PK_CATALOGPRICELISTDESCRIPTION] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CATALOGPRODUCTPRICE] WITH NOCHECK ADD 
	CONSTRAINT [PK_CATALOGPRODUCTPRICE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CATALOGPRODUCTS] WITH NOCHECK ADD 
	CONSTRAINT [PK_CATALOGPRODUCTS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CELEBRATION] WITH NOCHECK ADD 
	CONSTRAINT [PK_CELEBRATION] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[COMPANYMENU] WITH NOCHECK ADD 
	CONSTRAINT [PK_COMPANYMENU] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[COMPANYTYPE] WITH NOCHECK ADD 
	CONSTRAINT [PK_COMPANYTYPE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CONTACTESTIMATE] WITH NOCHECK ADD 
	CONSTRAINT [PK_CONTACTESTIMATE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CONTACTTYPE] WITH NOCHECK ADD 
	CONSTRAINT [PK_CONTACTTYPE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRMWORKINGCLASSIFICATION] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRMWORKINGCLASSIFICATION] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_ACTIVITYTYPE] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_ACTIVITYTYPE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_ADDRESSES] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_ADDRESSES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_BILL] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_BILL] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_BILLROWS] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_CONTACTPURCHASE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_COMPETITORPRODUCTS] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_COMPETITORPRODUCTS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_CONTACTCATEGORIES] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_CONTACTCATEGORIES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_CROSSCOMPANYPRODUCT] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_CROSSCOMPANYPRODUCT] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_CROSSCONTACTCOMPETITOR] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_CROSSCONTACTCOMPETITOR] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_CROSSLEAD] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_CROSSLEAD] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_CROSSOPPORTUNITY] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_CROSSOPPORTUNITY] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_CROSSOPPORTUNITYREFERRING] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_CROSSOPPORTUNITYREFERRING] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_LEADDESCRIPTION] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_LEADDESCRIPTION] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_LEADS] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_LEADS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_OPPLOSTREASONS] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_OPPLOSTREASONS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITY] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_OPPORTUNITY] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYCOMPETITOR] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_OPPORTUNITYCOMPETITOR] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYCOMPETITORPRODUCTS] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_OPPORTUNITYCOMPETITORPRODUCTS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYCONTACT] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_OPPORTUNITYCONTACT] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYCUSTOMTABLETYPE] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_OPPORTUNITYCUSTOMTABLETYPE] PRIMARY KEY  CLUSTERED 
	(
		[K_ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYPARTNERS] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_OPPORTUNITYPARTNERS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYTABLETYPE] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_OPPORTUNITYTABLETYPE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_OPPPRODUCTROWS] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_OPPPRODUCTROWS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_PHASE] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_PHASE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_PRODUCTSCAT_MATRIX] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_PRODUCTSCAT_MATRIX] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_PRODUCTSGROUPS] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_PRODUCTSGROUPS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_REFERRERCATEGORIES] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_REFERRERCATEGORIES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_REMINDER] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_REMINDER] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_TODOLIST] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_TODOLIST] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CRM_WORKACTIVITY] WITH NOCHECK ADD 
	CONSTRAINT [PK_CRM_WORKACTIVITY] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[CURRENCYTABLE] WITH NOCHECK ADD 
	CONSTRAINT [PK_CURRENCYTABLE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[DEFAULTVALUES] WITH NOCHECK ADD 
	CONSTRAINT [PK_DEFAULTVALUES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ESTIMATEDROWS] WITH NOCHECK ADD 
	CONSTRAINT [PK_ESTIMATEDROWS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ESTIMATELANGUAGES] WITH NOCHECK ADD 
	CONSTRAINT [PK_ESTIMATELANGUAGES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ESTIMATES] WITH NOCHECK ADD 
	CONSTRAINT [PK_ESTIMATES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[FILECROSSTABLES] WITH NOCHECK ADD 
	CONSTRAINT [PK_FILECROSSTABLES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[FILEMANAGER] WITH NOCHECK ADD 
	CONSTRAINT [PK_FILEMANAGER] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[FILESCATEGORIES] WITH NOCHECK ADD 
	CONSTRAINT [PK_FILESCATEGORIES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[FOLDERSIZE] WITH NOCHECK ADD 
	CONSTRAINT [PK_FOLDERSIZE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[GROUPS] WITH NOCHECK ADD 
	CONSTRAINT [PK_GROUPS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[HELPMENU] WITH NOCHECK ADD 
	CONSTRAINT [PK_HELPMENU] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[INVOICES] WITH NOCHECK ADD 
	CONSTRAINT [PK_INVOICES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[LASTCONTACT] WITH NOCHECK ADD 
	CONSTRAINT [PK_LASTCONTACT] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[LINKS] WITH NOCHECK ADD 
	CONSTRAINT [PK_LINKS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[LOCALUSER] WITH NOCHECK ADD 
	CONSTRAINT [PK_LOCALUSER] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[LOGINLOG] WITH NOCHECK ADD 
	CONSTRAINT [PK_LOGINLOG] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[MAILEVENTS] WITH NOCHECK ADD 
	CONSTRAINT [PK_MAILEVENTS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[MENUMAP] WITH NOCHECK ADD 
	CONSTRAINT [PK_FIRSTTIMEMENU] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_ATTACHMENT] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_ATTACHMENT] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_AUTH] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_AUTH] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_AUTHLOG] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_AUTHLOG] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_CATEGORIES] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_CATEGORIES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_COMPANIES] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_COMPANIES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_CONTACTS] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_CONTACTS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_DESCRIPTION] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_DESCRIPTION] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_FIXEDPARAMS] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_FIXEDPARAMS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_LEAD] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_LEAD] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_LOG] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_LOG] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_MAIL] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_MAIL] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ML_REMOVEDFROM] WITH NOCHECK ADD 
	CONSTRAINT [PK_ML_REMOVEDFROM] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[OFFICES] WITH NOCHECK ADD 
	CONSTRAINT [PK_OFFICES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ORDERROWS] WITH NOCHECK ADD 
	CONSTRAINT [PK_ORDERROWS_1] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ORDERS] WITH NOCHECK ADD 
	CONSTRAINT [PK_ORDERS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[PAYMENTLIST] WITH NOCHECK ADD 
	CONSTRAINT [PK_PAYMENTLIST] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[PRODUCTCHARACTERISTICS] WITH NOCHECK ADD 
	CONSTRAINT [PK_PRODUCTCHARACTERISTICS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[PRODUCTS] WITH NOCHECK ADD 
	CONSTRAINT [PK_PRODUCTS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QB_ALL_FIELDS] WITH NOCHECK ADD 
	CONSTRAINT [PK_QB_ALL_FIELDS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QB_ALL_TABLES] WITH NOCHECK ADD 
	CONSTRAINT [PK_QB_ALL_TABLES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QB_CATEGORIES] WITH NOCHECK ADD 
	CONSTRAINT [PK_QB_CATEGORIES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERY] WITH NOCHECK ADD 
	CONSTRAINT [PK_QB_CUSTOMERQUERY] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERYFIELDS] WITH NOCHECK ADD 
	CONSTRAINT [PK_QB_CUSTOMERQUERYFIELDS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERYFREEFIELDS] WITH NOCHECK ADD 
	CONSTRAINT [PK_QB_CUSTOMERQUERYFREEFIELDS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERYPARAMFIELDS] WITH NOCHECK ADD 
	CONSTRAINT [PK_QB_CUSTOMERQUERYPARAMFIELDS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERYTABLES] WITH NOCHECK ADD 
	CONSTRAINT [PK_QB_CUSTOMERQUERYTABLE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QB_DROPDOWNPARAMS] WITH NOCHECK ADD 
	CONSTRAINT [PK_QB_DROPDOWNPARAMS] PRIMARY KEY  CLUSTERED 
	(
		[IDRIF]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QB_LOCKARRAY] WITH NOCHECK ADD 
	CONSTRAINT [PK_QB_LOCKARRAY] PRIMARY KEY  CLUSTERED 
	(
		[IDTABLE]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QUICKLOG] WITH NOCHECK ADD 
	CONSTRAINT [PK_QUICKLOG] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QUOTEDOCUMENT] WITH NOCHECK ADD 
	CONSTRAINT [PK_QUOTEDOCUMENT] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QUOTENUMBERS] WITH NOCHECK ADD 
	CONSTRAINT [PK_QUOTENUMBERS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QUOTEROWS] WITH NOCHECK ADD 
	CONSTRAINT [PK_QUOTEROWS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QUOTES] WITH NOCHECK ADD 
	CONSTRAINT [PK_QUOTES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[QUOTESHIPMENT] WITH NOCHECK ADD 
	CONSTRAINT [PK_QUOTESHIPMENT] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RECENT] WITH NOCHECK ADD 
	CONSTRAINT [PK_RECENT] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RECURRENCE] WITH NOCHECK ADD 
	CONSTRAINT [PK_RECURRENCE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[REPORTREFERENCE] WITH NOCHECK ADD 
	CONSTRAINT [PK_REPORTREFERENCE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[SCOREDESCRIPTION] WITH NOCHECK ADD 
	CONSTRAINT [PK_SCOREDESCRIPTION] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[SCORELOG] WITH NOCHECK ADD 
	CONSTRAINT [PK_SCORELOG] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[SCOREVALUES] WITH NOCHECK ADD 
	CONSTRAINT [PK_SCOREVALUES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[SURVEYDB] WITH NOCHECK ADD 
	CONSTRAINT [PK_SURVEYDB] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[SURVEYRESPONSES] WITH NOCHECK ADD 
	CONSTRAINT [PK_SURVEYRESPONSES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TAXVALUES] WITH NOCHECK ADD 
	CONSTRAINT [PK_TAXVALUES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKETNOTRESOLVEDREASON] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKETNOTRESOLVEDREASON] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKET_AREA] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKET_AREA] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKET_MAIN] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKET_MAIN] PRIMARY KEY  CLUSTERED 
	(
		[TICKETID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKET_MOVELOG] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKET_MOVELOG] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKET_PROGRESS] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKET_PROGRESS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKET_SCHEDULE] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKET_SCHEDULE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKET_SLA] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKET_SLA] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKET_SLA_CUSTOMER] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKET_SLA_CUSTOMER] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKET_SUPPORTLOG] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKET_SUPPORTLOG] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKET_TYPE] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKET_TYPE] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TICKET_USER] WITH NOCHECK ADD 
	CONSTRAINT [PK_TICKETUSER] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TODOLIST] WITH NOCHECK ADD 
	CONSTRAINT [PK_TODOLIST] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TUSTENADB] WITH NOCHECK ADD 
	CONSTRAINT [PK_TUSTENADB] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TUSTENA_DATA] WITH NOCHECK ADD 
	CONSTRAINT [PK_EWORK_CUSTOMER] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[USAGELOG] WITH NOCHECK ADD 
	CONSTRAINT [PK_USAGELOG] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[VERSION] WITH NOCHECK ADD 
	CONSTRAINT [PK_VERSION] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[VIEWSTATEMANAGER] WITH NOCHECK ADD 
	CONSTRAINT [PK_VIEWSTATEMANAGER] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WEBGATEPARAMS] WITH NOCHECK ADD 
	CONSTRAINT [PK_WEBGATEPARAMS] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ZONES] WITH NOCHECK ADD 
	CONSTRAINT [PK_ZONES] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ACCOUNT] ADD 
	CONSTRAINT [DF_ACCOUNT_ACCESSLEVEL] DEFAULT (0) FOR [ACCESSLEVEL],
	CONSTRAINT [DF_ACCOUNT_ACTIVE] DEFAULT (1) FOR [ACTIVE],
	CONSTRAINT [DF_ACCOUNT_ISMANAGER] DEFAULT (0) FOR [ISMANAGER],
	CONSTRAINT [DF_ACCOUNT_ISEMPLOYEE] DEFAULT (0) FOR [ISEMPLOYEE],
	CONSTRAINT [DF_ACCOUNT_OFFICEID] DEFAULT (0) FOR [OFFICEID],
	CONSTRAINT [DF_ACCOUNT_MANAGERID] DEFAULT (0) FOR [MANAGERID],
	CONSTRAINT [DF_ACCOUNT_WORKDAYS] DEFAULT (62) FOR [WORKDAYS],
	CONSTRAINT [DF_ACCOUNT_SELFCONTACTID] DEFAULT ((-1)) FOR [SELFCONTACTID],
	CONSTRAINT [DF_ACCOUNT_DIARYACCOUNT] DEFAULT ('|') FOR [DIARYACCOUNT],
	CONSTRAINT [DF_ACCOUNT_STRETCHMENU] DEFAULT (0) FOR [STRETCHMENU],
	CONSTRAINT [DF_ACCOUNT_OFFICEACCOUNT] DEFAULT ('|') FOR [OFFICEACCOUNT],
	CONSTRAINT [DF_ACCOUNT_TIMEZONE] DEFAULT ('CET') FOR [TIMEZONE],
	CONSTRAINT [DF_ACCOUNT_ENABLEPERSCONTACT] DEFAULT (0) FOR [ENABLEPERSCONTACT],
	CONSTRAINT [DF_ACCOUNT_PERSISTLOGIN] DEFAULT (0) FOR [PERSISTLOGIN],
	CONSTRAINT [DF__ACCOUNT__CREATED__08A03ED0] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__ACCOUNT__CREATED__09946309] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__ACCOUNT__LASTMOD__0A888742] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__ACCOUNT__LASTMOD__0B7CAB7B] DEFAULT (0) FOR [LASTMODIFIEDBYID],
	CONSTRAINT [DF_ACCOUNT_FLAGNOTIFYAPPOINTMENT] DEFAULT (0) FOR [FLAGNOTIFYAPPOINTMENT],
	CONSTRAINT [DF_ACCOUNT_PAGING] DEFAULT (20) FOR [PAGING],
	CONSTRAINT [DF_ACCOUNT_FULLSCREEN] DEFAULT (0) FOR [FULLSCREEN],
	CONSTRAINT [DF_ACCOUNT_VIEWBIRTHDATE] DEFAULT (0) FOR [VIEWBIRTHDATE],
	CONSTRAINT [DF_ACCOUNT_CULTURE] DEFAULT ('it-IT') FOR [CULTURE],
	CONSTRAINT [DF_ACCOUNT_STATE] DEFAULT (0) FOR [STATE],
	CONSTRAINT [DF_ACCOUNT_LASTLOGIN] DEFAULT (getdate()) FOR [LASTLOGIN],
	CONSTRAINT [DF_ACCOUNT_SESSIONTIMEOUT] DEFAULT (20) FOR [SESSIONTIMEOUT],
	CONSTRAINT [DF_ACCOUNT_FIRSTDAYOFWEEK] DEFAULT (1) FOR [FIRSTDAYOFWEEK],
	CONSTRAINT [DF_ACCOUNT_TIMEZONEINDEX] DEFAULT (110) FOR [TIMEZONEINDEX],
	CONSTRAINT [DF_ACCOUNT_LASTNEWS] DEFAULT (getdate()) FOR [LASTNEWS]
GO

 CREATE  INDEX [HIND_480056796_1A_6A_7A] ON [dbo].[ACCOUNT]([UID], [Name], [SURNAME]) ON [PRIMARY]
GO

 CREATE  INDEX [HIND_480056796_1A] ON [dbo].[ACCOUNT]([UID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ACTIVITYMOVELOG] ADD 
	CONSTRAINT [DF_ACTIVITYMOVELOG_ACTIONTYPE] DEFAULT (0) FOR [ACTIONTYPE],
	CONSTRAINT [DF_ACTIVITYMOVELOG_MOVEDATE] DEFAULT (getdate()) FOR [MOVEDATE]
GO

ALTER TABLE [dbo].[ADDEDFIELDS] ADD 
	CONSTRAINT [DF_ADDEDFIELDS_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_ADDEDFIELDS_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_ADDEDFIELDS_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_ADDEDFIELDS_LASTMODIFIEDBYID] DEFAULT (0) FOR [LASTMODIFIEDBYID],
	CONSTRAINT [DF_ADDEDFIELDS_VIEWORDER] DEFAULT (0) FOR [VIEWORDER]
GO

ALTER TABLE [dbo].[ADDEDFIELDS_CROSS] ADD 
	CONSTRAINT [DF_ADDEDFIELDS_CROSS_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_ADDEDFIELDS_CROSS_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_ADDEDFIELDS_CROSS_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_ADDEDFIELDS_CROSS_LASTMODIFIEDBYID] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[ADDEDFIELD_DROPDOWN] ADD 
	CONSTRAINT [DF_ADDEDFIELD_DROPDOWN_RMVALUE] DEFAULT (0) FOR [RMVALUE],
	CONSTRAINT [DF_ADDEDFIELD_DROPDOWN_QUERYTYPE] DEFAULT (0) FOR [QUERYTYPE]
GO

ALTER TABLE [dbo].[BASE_CALENDAR] ADD 
	CONSTRAINT [DF_BASE_CALENDAR_CONFIRMATION] DEFAULT (0) FOR [CONFIRMATION],
	CONSTRAINT [DF_BASE_CALENDAR_PLACE] DEFAULT (1) FOR [PLACE],
	CONSTRAINT [DF_BASE_CALENDAR_RECURRID] DEFAULT (0) FOR [RECURRID],
	CONSTRAINT [DF_BASE_CALENDAR_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_BASE_CALENDAR_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_BASE_CALENDAR_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_BASE_CALENDAR_LASTMODIFIEDBYID] DEFAULT (0) FOR [LASTMODIFIEDBYID],
	CONSTRAINT [DF_BASE_CALENDAR_SENCONDIDOWNER] DEFAULT ((-1)) FOR [SENCONDIDOWNER]
GO

ALTER TABLE [dbo].[BASE_COMPANIES] ADD 
	CONSTRAINT [DF_BASE_CONTACTS_BILLED] DEFAULT (0) FOR [BILLED],
	CONSTRAINT [DF_BASE_CONTACTS_OWNERID] DEFAULT (0) FOR [OWNERID],
	CONSTRAINT [DF_BASE_CONTACTS_FLAGGLOBALORPERSONAL] DEFAULT (0) FOR [FLAGGLOBALORPERSONAL],
	CONSTRAINT [DF_BASE_CONTACTS_LASTACTIVITY] DEFAULT (0) FOR [LASTACTIVITY],
	CONSTRAINT [DF_BASE_CONTACTS_LIMBO] DEFAULT (0) FOR [LIMBO],
	CONSTRAINT [DF_BASE_CONTACTS_INSERTFROMCRM] DEFAULT (1) FOR [INSERTFROMCRM],
	CONSTRAINT [DF_BASE_CONTACTS_EVALUATION] DEFAULT (0) FOR [EVALUATION],
	CONSTRAINT [DF_BASE_CONTACTS_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_BASE_CONTACTS_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_BASE_CONTACTS_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_BASE_CONTACTS_LASTMODIFIEDBYID] DEFAULT (0) FOR [LASTMODIFIEDBYID],
	CONSTRAINT [DF_BASE_CONTACTS_COMPETITOR] DEFAULT (0) FOR [COMPETITOR],
	CONSTRAINT [DF_BASE_CONTACTS_MLFLAG] DEFAULT (0) FOR [MLFLAG],
	CONSTRAINT [DF_BASE_CONTACTS_NOCONTACT] DEFAULT (0) FOR [NOCONTACT],
	CONSTRAINT [DF_BASE_COMPANIES_COMMERCIALZONE] DEFAULT (0) FOR [COMMERCIALZONE]
GO

 CREATE  INDEX [COMPANYNAME] ON [dbo].[BASE_COMPANIES]([COMPANYNAME]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BASE_CONTACTS] ADD 
	CONSTRAINT [DF_BASE_REFERRING_ACTIVE] DEFAULT (1) FOR [ACTIVE],
	CONSTRAINT [DF_BASE_REFERRING_FLAGGLOBALORPERSONAL] DEFAULT (0) FOR [FLAGGLOBALORPERSONAL],
	CONSTRAINT [DF_BASE_REFERRING_LASTACTIVITY] DEFAULT (0) FOR [LASTACTIVITY],
	CONSTRAINT [DF_BASE_REFERRING_LIMBO] DEFAULT (0) FOR [LIMBO],
	CONSTRAINT [DF__BASE_CONT__CREAT__1CA7377D] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__BASE_CONT__CREAT__1D9B5BB6] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__BASE_CONT__LASTM__1E8F7FEF] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__BASE_CONT__LASTM__1F83A428] DEFAULT (0) FOR [LASTMODIFIEDBYID],
	CONSTRAINT [DF_BASE_REFERRING_MLFLAG] DEFAULT (0) FOR [MLFLAG],
	CONSTRAINT [DF_BASE_REFERRING_NOCONTACT] DEFAULT (0) FOR [NOCONTACT],
	CONSTRAINT [DF_BASE_CONTACTS_COMMERCIALZONE] DEFAULT (0) FOR [COMMERCIALZONE]
GO

 CREATE  INDEX [Name] ON [dbo].[BASE_CONTACTS]([Name]) ON [PRIMARY]
GO

 CREATE  INDEX [SURNAME] ON [dbo].[BASE_CONTACTS]([SURNAME]) ON [PRIMARY]
GO

 CREATE  INDEX [BASE_REFERRING32] ON [dbo].[BASE_CONTACTS]([COMPANYID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[BASE_EVENTS] ADD 
	CONSTRAINT [DF_BASE_EVENTS_GLOBALEVENT] DEFAULT (0) FOR [GLOBALEVENT],
	CONSTRAINT [DF_BASE_EVENTS_RECURRID] DEFAULT (0) FOR [RECURRID],
	CONSTRAINT [DF_BASE_EVENTS_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_BASE_EVENTS_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_BASE_EVENTS_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_BASE_EVENTS_LASTMODIFIEDBYID] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[BASE_MESSAGES] ADD 
	CONSTRAINT [DF__BASE_MESS__CREAT__33366271] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__BASE_MESS__CREAT__342A86AA] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__BASE_MESS__LASTM__351EAAE3] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__BASE_MESS__LASTM__3612CF1C] DEFAULT (0) FOR [LASTMODIFIEDBYID],
	CONSTRAINT [DF_BASE_MESSAGES_DELETESENDER] DEFAULT (1) FOR [DELETESENDER],
	CONSTRAINT [DF_BASE_MESSAGES_DELETERECEIVING] DEFAULT (1) FOR [DELETERECEIVING],
	CONSTRAINT [DF_BASE_MESSAGES_READED] DEFAULT (0) FOR [READED],
	CONSTRAINT [DF_BASE_MESSAGES_INOUT] DEFAULT (0) FOR [INOUT]
GO

ALTER TABLE [dbo].[BASE_NOTES] ADD 
	CONSTRAINT [DF_BASE_NOTES_FLAGGLOBAL] DEFAULT (1) FOR [FLAGGLOBAL],
	CONSTRAINT [DF_BASE_NOTES_GROUPS] DEFAULT ('|1|') FOR [GROUPS],
	CONSTRAINT [DF_BASE_NOTES_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_BASE_NOTES_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_BASE_NOTES_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_BASE_NOTES_LASTMODIFIEDBYID] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[CATALOGCATEGORIES] ADD 
	CONSTRAINT [DF_CATALOGCATEGORIES_PARENTID] DEFAULT (0) FOR [PARENTID]
GO

ALTER TABLE [dbo].[CATALOGPRICELISTDESCRIPTION] ADD 
	CONSTRAINT [DF_CATALOGPRICELISTDESCRIPTION_PERCENTAGE] DEFAULT (0) FOR [PERCENTAGE],
	CONSTRAINT [DF_CATALOGPRICELISTDESCRIPTION_DISCOUNT] DEFAULT (0) FOR [INCREASE]
GO

ALTER TABLE [dbo].[CATALOGPRODUCTPRICE] ADD 
	CONSTRAINT [DF_CATALOGPRODUCTPRICE_UNITPRICE] DEFAULT (0) FOR [UNITPRICE],
	CONSTRAINT [DF_CATALOGPRODUCTPRICE_COST] DEFAULT (0) FOR [COST],
	CONSTRAINT [DF_CATALOGPRODUCTPRICE_VAT] DEFAULT (0) FOR [VAT]
GO

ALTER TABLE [dbo].[CATALOGPRODUCTS] ADD 
	CONSTRAINT [DF_CATALOGPRODUCTS_QTA] DEFAULT (1) FOR [QTA],
	CONSTRAINT [DF_CATALOGPRODUCTS_QTABLISTER] DEFAULT (0) FOR [QTABLISTER],
	CONSTRAINT [DF_CATALOGPRODUCTS_UNITPRICE] DEFAULT (0) FOR [UNITPRICE],
	CONSTRAINT [DF_CATALOGPRODUCTS_ACTIVE] DEFAULT (1) FOR [ACTIVE],
	CONSTRAINT [DF_CATALOGPRODUCTS_COST] DEFAULT (0) FOR [COST],
	CONSTRAINT [DF_CATALOGPRODUCTS_PUBLISH] DEFAULT (0) FOR [PUBLISH],
	CONSTRAINT [DF_CATALOGPRODUCTS_PRINTDESCRIPTION] DEFAULT (0) FOR [PRINTDESCRIPTION],
	CONSTRAINT [DF_CATALOGPRODUCTS_STOCK] DEFAULT (0) FOR [STOCK]
GO

ALTER TABLE [dbo].[CELEBRATION] ADD 
	CONSTRAINT [DF_CELEBRATION_YEARS] DEFAULT (0) FOR [YEARS]
GO

ALTER TABLE [dbo].[COMPANYMENU] ADD 
	CONSTRAINT [DF_COMPANYMENU_MENUID] DEFAULT (0) FOR [MENUID],
	CONSTRAINT [DF_COMPANYMENU_GROUPSID] DEFAULT ('|0|') FOR [ACCESSGROUP]
GO

ALTER TABLE [dbo].[COMPANYTYPE] ADD 
	CONSTRAINT [DF_COMPANYTYPE_K_ID] DEFAULT (0) FOR [K_ID],
	CONSTRAINT [DF_COMPANYTYPE_TYPE] DEFAULT (0) FOR [TYPE],
	CONSTRAINT [DF_COMPANYTYPE_LANG] DEFAULT ('it-IT') FOR [LANG],
	CONSTRAINT [DF_COMPANYTYPE_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_COMPANYTYPE_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_COMPANYTYPE_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_COMPANYTYPE_LASTMODIFIEDBYID] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[CONTACTESTIMATE] ADD 
	CONSTRAINT [DF_CONTACTESTIMATE_FIELDORDER] DEFAULT (0) FOR [FIELDORDER],
	CONSTRAINT [DF_CONTACTESTIMATE_K_ID] DEFAULT (0) FOR [K_ID],
	CONSTRAINT [DF_CONTACTESTIMATE_LANG] DEFAULT ('it') FOR [LANG]
GO

ALTER TABLE [dbo].[CONTACTTYPE] ADD 
	CONSTRAINT [DF_CONTACTTYPE_LANG] DEFAULT ('it-IT') FOR [LANG]
GO

ALTER TABLE [dbo].[CRMWORKINGCLASSIFICATION] ADD 
	CONSTRAINT [DF_CRMWORKINGCLASSIFICATION_DROPPOSITION] DEFAULT (0) FOR [DROPPOSITION],
	CONSTRAINT [DF_CRMWORKINGCLASSIFICATION_LANG] DEFAULT ('it') FOR [LANG]
GO

ALTER TABLE [dbo].[CRM_ACTIVITYTYPE] ADD 
	CONSTRAINT [DF_CRM_ACTIVITYTYPE_K_ID] DEFAULT (0) FOR [K_ID]
GO

ALTER TABLE [dbo].[CRM_ADDRESSES] ADD 
	CONSTRAINT [DF_CRM_ADDRESSES_TYPE] DEFAULT (0) FOR [TYPE]
GO

ALTER TABLE [dbo].[CRM_BILL] ADD 
	CONSTRAINT [DF_CRM_BILL_BILLINGDATE] DEFAULT (getdate()) FOR [BILLINGDATE],
	CONSTRAINT [DF_CRM_BILL_TOTALPRICE] DEFAULT (0) FOR [TOTALPRICE],
	CONSTRAINT [DF_CRM_BILL_PAYMENT] DEFAULT (0) FOR [PAYMENT],
	CONSTRAINT [DF_CRM_BILL_NROWS] DEFAULT (0) FOR [NROWS],
	CONSTRAINT [DF_CRM_BILL_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_CRM_BILL_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_CRM_BILL_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_CRM_BILL_OWNERID] DEFAULT (0) FOR [OWNERID]
GO

ALTER TABLE [dbo].[CRM_BILLROWS] ADD 
	CONSTRAINT [DF_CRM_CONTACTPURCHASE_COMPANYID] DEFAULT (0) FOR [BILLID],
	CONSTRAINT [DF_CRM_CONTACTPURCHASE_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_CRM_CONTACTPURCHASE_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_CRM_CONTACTPURCHASE_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_CRM_CONTACTPURCHASE_LASTMODIFIEDBYID] DEFAULT (0) FOR [LASTMODIFIEDBYID],
	CONSTRAINT [DF_CRM_BILLROWS_PRODUCTID] DEFAULT (0) FOR [PRODUCTID]
GO

ALTER TABLE [dbo].[CRM_COMPETITORPRODUCTS] ADD 
	CONSTRAINT [DF_CRM_COMPETITORPRODUCTS_PRICE] DEFAULT (0) FOR [PRICE]
GO

ALTER TABLE [dbo].[CRM_CONTACTCATEGORIES] ADD 
	CONSTRAINT [DF_CRM_CONTACTCATEGORIES_FLAGPERSONAL] DEFAULT (0) FOR [FLAGPERSONAL],
	CONSTRAINT [DF_CRM_CONTACTCATEGORIES_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE]
GO

ALTER TABLE [dbo].[CRM_CROSSCOMPANYPRODUCT] ADD 
	CONSTRAINT [DF_CRM_CROSSCOMPANYPRODUCT_RELATION] DEFAULT (0) FOR [RELATION]
GO

ALTER TABLE [dbo].[CRM_CROSSCONTACTCOMPETITOR] ADD 
	CONSTRAINT [DF_CRM_CROSSCONTACTCOMPETITOR_CONTACTTYPE] DEFAULT (0) FOR [CONTACTTYPE]
GO

ALTER TABLE [dbo].[CRM_CROSSLEAD] ADD 
	CONSTRAINT [DF_CRM_CROSSLEAD_STATUS] DEFAULT (0) FOR [STATUS],
	CONSTRAINT [DF_CRM_CROSSLEAD_PRODUCTINTEREST] DEFAULT (0) FOR [PRODUCTINTEREST],
	CONSTRAINT [DF_CRM_CROSSLEAD_POTENTIALREVENUE] DEFAULT (0) FOR [POTENTIALREVENUE]
GO

ALTER TABLE [dbo].[CRM_CROSSOPPORTUNITY] ADD 
	CONSTRAINT [DF_CRM_CROSSOPPORTUNITY_TYPE] DEFAULT (0) FOR [TYPE],
	CONSTRAINT [DF_CRM_CROSSOPPORTUNITY_CONTACTTYPE] DEFAULT (0) FOR [CONTACTTYPE]
GO

ALTER TABLE [dbo].[CRM_CROSSOPPORTUNITYREFERRING] ADD 
	CONSTRAINT [DF_CRM_CROSSOPPORTUNITYREFERRING_REFERRERID] DEFAULT (0) FOR [REFERRERID],
	CONSTRAINT [DF_CRM_CROSSOPPORTUNITYREFERRING_COMPANYID] DEFAULT (0) FOR [COMPANYID],
	CONSTRAINT [DF_CRM_CROSSOPPORTUNITYREFERRING_OPPORTUNITYID] DEFAULT (0) FOR [OPPORTUNITYID],
	CONSTRAINT [DF_CRM_CROSSOPPORTUNITYREFERRING_CHARACTERID] DEFAULT (0) FOR [CHARACTERTEXT]
GO

ALTER TABLE [dbo].[CRM_LEADDESCRIPTION] ADD 
	CONSTRAINT [DF_CRM_LEADDESCRIPTION_K_ID] DEFAULT (0) FOR [K_ID],
	CONSTRAINT [DF_CRM_LEADDESCRIPTION_LANG] DEFAULT ('it') FOR [LANG],
	CONSTRAINT [DF_CRM_LEADDESCRIPTION_TYPE] DEFAULT (1) FOR [TYPE]
GO

ALTER TABLE [dbo].[CRM_LEADS] ADD 
	CONSTRAINT [DF_CRM_LEADS_LASTACTIVITY] DEFAULT (0) FOR [LASTACTIVITY],
	CONSTRAINT [DF_CRM_LEADS_LIMBO] DEFAULT (0) FOR [LIMBO],
	CONSTRAINT [DF__CRM_LEADS__CREAT__30AE302A] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__CRM_LEADS__CREAT__31A25463] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__CRM_LEADS__LASTM__3296789C] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__CRM_LEADS__LASTM__338A9CD5] DEFAULT (0) FOR [LASTMODIFIEDBYID],
	CONSTRAINT [DF_CRM_LEADS_ACTIVE] DEFAULT (1) FOR [ACTIVE],
	CONSTRAINT [DF_CRM_LEADS_NOCONTACT] DEFAULT (0) FOR [NOCONTACT],
	CONSTRAINT [DF_CRM_LEADS_COMMARCIALZONE] DEFAULT (0) FOR [COMMERCIALZONE]
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITY] ADD 
	CONSTRAINT [DF_CRM_OPPORTUNITY_OWNERID] DEFAULT (0) FOR [OWNERID],
	CONSTRAINT [DF_CRM_OPPORTUNITY_ADMINACCOUNT] DEFAULT ('|1|') FOR [ADMINACCOUNT],
	CONSTRAINT [DF_CRM_OPPORTUNITY_GROUPS] DEFAULT ('|0|') FOR [GROUPS],
	CONSTRAINT [DF_CRM_OPPORTUNITY_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_CRM_OPPORTUNITY_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_CRM_OPPORTUNITY_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_CRM_OPPORTUNITY_LASTMODIFIEDBYID] DEFAULT (0) FOR [LASTMODIFIEDBYID],
	CONSTRAINT [DF_CRM_OPPORTUNITY_EXPECTEDREVENUE] DEFAULT (0) FOR [EXPECTEDREVENUE],
	CONSTRAINT [DF_CRM_OPPORTUNITY_INCOMEPROBABILITY] DEFAULT (0) FOR [INCOMEPROBABILITY],
	CONSTRAINT [DF_CRM_OPPORTUNITY_AMOUNTCLOSED] DEFAULT (0) FOR [AMOUNTCLOSED],
	CONSTRAINT [DF_CRM_OPPORTUNITY_CURRENCY] DEFAULT (0) FOR [CURRENCY],
	CONSTRAINT [DF_CRM_OPPORTUNITY_CURRENCYCHANGE] DEFAULT (0) FOR [CURRENCYCHANGE]
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYCOMPETITOR] ADD 
	CONSTRAINT [DF_CRM_OPPORTUNITYCOMPETITOR_VALUTAZIONE] DEFAULT (0) FOR [EVALUATION]
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYCONTACT] ADD 
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_EXPECTEDREVENUE] DEFAULT (0) FOR [EXPECTEDREVENUE],
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_INCOMEPROBABILITY] DEFAULT (0) FOR [INCOMEPROBABILITY],
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_AMOUNTCLOSED] DEFAULT (0) FOR [AMOUNTCLOSED],
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_CONTACTTYPE] DEFAULT (0) FOR [CONTACTTYPE],
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_STARTDATE] DEFAULT (getdate()) FOR [STARTDATE],
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_ESIMATEDCLOSEDATE] DEFAULT (getdate()) FOR [ESTIMATEDCLOSEDATE],
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_ENDDATE] DEFAULT (getdate()) FOR [ENDDATE],
	CONSTRAINT [DF_CRM_OPPORTUNITYCONTACT_SALESPERSON] DEFAULT (0) FOR [SALESPERSON]
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYTABLETYPE] ADD 
	CONSTRAINT [DF_CRM_OPPORTUNITYTABLETYPE_K_ID] DEFAULT (0) FOR [K_ID],
	CONSTRAINT [DF_CRM_OPPORTUNITYTABLETYPE_LANG] DEFAULT ('it') FOR [LANG]
GO

ALTER TABLE [dbo].[CRM_OPPPRODUCTROWS] ADD 
	CONSTRAINT [DF_CRM_OPPPRODUCTROWS_QTA] DEFAULT (1) FOR [QTA],
	CONSTRAINT [DF_CRM_OPPPRODUCTROWS_UPRICE] DEFAULT (0) FOR [UPRICE],
	CONSTRAINT [DF_CRM_OPPPRODUCTROWS_NEWUPRICE] DEFAULT (0) FOR [NEWUPRICE],
	CONSTRAINT [DF_CRM_OPPPRODUCTROWS_LEADORCOMPANYID] DEFAULT (0) FOR [LEADORCOMPANYID],
	CONSTRAINT [DF_CRM_OPPPRODUCTROWS_TYPE] DEFAULT (0) FOR [TYPE]
GO

ALTER TABLE [dbo].[CRM_PHASE] ADD 
	CONSTRAINT [DF__CRM_PHASE__CREAT__7A672E12] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__CRM_PHASE__CREAT__7B5B524B] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__CRM_PHASE__LASTM__7C4F7684] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__CRM_PHASE__LASTM__7D439ABD] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[CRM_PRODUCTSCAT_MATRIX] ADD 
	CONSTRAINT [DF__CRM_PRODU__CREAT__7F2BE32F] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__CRM_PRODU__CREAT__00200768] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__CRM_PRODU__LASTM__01142BA1] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__CRM_PRODU__LASTM__02084FDA] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[CRM_PRODUCTSGROUPS] ADD 
	CONSTRAINT [DF__CRM_PRODU__CREAT__03F0984C] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__CRM_PRODU__CREAT__04E4BC85] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__CRM_PRODU__LASTM__05D8E0BE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__CRM_PRODU__LASTM__06CD04F7] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[CRM_REFERRERCATEGORIES] ADD 
	CONSTRAINT [DF_CRM_REFERRERCATEGORIES_FLAGPERSONAL] DEFAULT (0) FOR [FLAGPERSONAL],
	CONSTRAINT [DF_CRM_REFERRERCATEGORIES_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE]
GO

ALTER TABLE [dbo].[CRM_REMINDER] ADD 
	CONSTRAINT [DF_CRM_REMINDER_REMINDERDATE] DEFAULT (getdate()) FOR [REMINDERDATE],
	CONSTRAINT [DF_CRM_REMINDER_OPPORTUNITYID] DEFAULT (0) FOR [OPPORTUNITYID],
	CONSTRAINT [DF_CRM_REMINDER_ADVANCEREMIND] DEFAULT (0) FOR [ADVANCEREMIND]
GO

ALTER TABLE [dbo].[CRM_TODOLIST] ADD 
	CONSTRAINT [DF_CRM_TODOLIST_GROUPS] DEFAULT ('|0|') FOR [GROUPS],
	CONSTRAINT [DF_CRM_TODOLIST_EXPIRATIONDATE] DEFAULT (getdate()) FOR [EXPIRATIONDATE],
	CONSTRAINT [DF_CRM_TODOLIST_FLAGEXECUTED] DEFAULT (0) FOR [FLAGEXECUTED],
	CONSTRAINT [DF_CRM_TODOLIST_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_CRM_TODOLIST_CREATEDBY] DEFAULT (0) FOR [CREATEDBYID]
GO

ALTER TABLE [dbo].[CRM_WORKACTIVITY] ADD 
	CONSTRAINT [DF_CRM_ACTIVITY_GROUPS] DEFAULT ('|0|') FOR [GROUPS],
	CONSTRAINT [DF_CRM_ACTIVITY_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_CRM_ACTIVITY_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_CRM_ACTIVITY_LASTMODIFIEDDATE] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF_CRM_ACTIVITY_LASTMODIFIEDBYID] DEFAULT (0) FOR [LASTMODIFIEDBYID],
	CONSTRAINT [DF_CRM_ACTIVITY_INOUT] DEFAULT (0) FOR [INOUT],
	CONSTRAINT [DF_CRM_ACTIVITY_ACTIVITYDATE] DEFAULT (getdate()) FOR [ACTIVITYDATE],
	CONSTRAINT [DF_CRM_WORKACTIVITY_PARENTID] DEFAULT (0) FOR [PARENTID],
	CONSTRAINT [DF_CRM_WORKACTIVITY_TOBILL] DEFAULT (0) FOR [TOBILL],
	CONSTRAINT [DF_CRM_WORKACTIVITY_COMMERCIAL] DEFAULT (0) FOR [COMMERCIAL],
	CONSTRAINT [DF_CRM_WORKACTIVITY_TECHNICAL] DEFAULT (0) FOR [TECHNICAL],
	CONSTRAINT [DF_CRM_WORKACTIVITY_DURATION] DEFAULT (0) FOR [DURATION],
	CONSTRAINT [DF_CRM_WORKACTIVITY_TODO] DEFAULT (1) FOR [TODO],
	CONSTRAINT [DF_CRM_WORKACTIVITY_ORDERDATE] DEFAULT (getdate()) FOR [ORDERDATE]
GO

 CREATE  INDEX [IX_CRM_WORKACTIVITY] ON [dbo].[CRM_WORKACTIVITY]([PARENTID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CURRENCYTABLE] ADD 
	CONSTRAINT [DF_CURRENCYTABLE_CHANGE] DEFAULT (1) FOR [CHANGETOEURO],
	CONSTRAINT [DF_CURRENCYTABLE_CHANGETFROMEURO] DEFAULT (1) FOR [CHANGEFROMEURO]
GO

ALTER TABLE [dbo].[DEFAULTVALUES] ADD 
	CONSTRAINT [DF_DEFAULTVALUES_REFID] DEFAULT (0) FOR [REFID],
	CONSTRAINT [DF_DEFAULTVALUES_LANG] DEFAULT ('en') FOR [LANG]
GO

ALTER TABLE [dbo].[ESTIMATEDROWS] ADD 
	CONSTRAINT [DF_ESTIMATEDROWS_CATALOGID] DEFAULT (0) FOR [CATALOGID],
	CONSTRAINT [DF_ESTIMATEDROWS_QTA] DEFAULT (1) FOR [QTA],
	CONSTRAINT [DF_ESTIMATEDROWS_UPRICE] DEFAULT (0) FOR [UPRICE],
	CONSTRAINT [DF_ESTIMATEDROWS_NEWUPRICE] DEFAULT (0) FOR [NEWUPRICE]
GO

ALTER TABLE [dbo].[ESTIMATES] ADD 
	CONSTRAINT [DF_ESTIMATES_CURRENCY] DEFAULT (1) FOR [CURRENCY],
	CONSTRAINT [DF_ESTIMATES_CHANGE] DEFAULT (1) FOR [CHANGE],
	CONSTRAINT [DF_ESTIMATES_REDUCTION] DEFAULT (0) FOR [REDUCTION],
	CONSTRAINT [DF_ESTIMATES_STAGE] DEFAULT (1) FOR [STAGE]
GO

ALTER TABLE [dbo].[EVENTSCHEDULER] ADD 
	CONSTRAINT [DF_EVENTSCHEDULER_LASTEVENT] DEFAULT (getdate()) FOR [LASTEVENT]
GO

ALTER TABLE [dbo].[FILEMANAGER] ADD 
	CONSTRAINT [DF_FILEMANAGER_REMOTE] DEFAULT (0) FOR [REMOTE],
	CONSTRAINT [DF_FILEMANAGER_ISREVIEW] DEFAULT (0) FOR [ISREVIEW],
	CONSTRAINT [DF_FILEMANAGER_HAVEREVISION] DEFAULT (0) FOR [HAVEREVISION],
	CONSTRAINT [DF__FILEMANAG__CREAT__1332DBDC] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__FILEMANAG__CREAT__14270015] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__FILEMANAG__LASTM__151B244E] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__FILEMANAG__LASTM__160F4887] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[FILESCATEGORIES] ADD 
	CONSTRAINT [DF_FILESCATEGORIES_PARENTID] DEFAULT (0) FOR [PARENTID]
GO

ALTER TABLE [dbo].[GROUPS] ADD 
	CONSTRAINT [DF__GROUPS__CREATEDD__17F790F9] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__GROUPS__CREATEDB__18EBB532] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__GROUPS__LASTMODI__19DFD96B] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__GROUPS__LASTMODI__1AD3FDA4] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[INVOICES] ADD 
	CONSTRAINT [DF_INVOICES_PAID] DEFAULT (0) FOR [PAID]
GO

ALTER TABLE [dbo].[LASTCONTACT] ADD 
	CONSTRAINT [DF_LASTCONTACT_ACTIVITYDATE] DEFAULT (getdate()) FOR [ACTIVITYDATE]
GO

ALTER TABLE [dbo].[LINKS] ADD 
	CONSTRAINT [DF_LINKS_IDREF] DEFAULT (0) FOR [IDREF],
	CONSTRAINT [DF_LINKS_DYNAMIC] DEFAULT (0) FOR [DYNAMIC],
	CONSTRAINT [DF_LINKS_VIEWORDER] DEFAULT (0) FOR [VIEWORDER]
GO

ALTER TABLE [dbo].[LOGINLOG] ADD 
	CONSTRAINT [DF_LOGINLOG_ID] DEFAULT (newid()) FOR [ID],
	CONSTRAINT [DF_LOGINLOG_LOGINDATE] DEFAULT (getdate()) FOR [LOGINDATE]
GO

ALTER TABLE [dbo].[MAILEVENTS] ADD 
	CONSTRAINT [DF_MAILEVENTS_EVENTTYPE] DEFAULT (0) FOR [EVENTTYPE]
GO

ALTER TABLE [dbo].[MENUMAP] ADD 
	CONSTRAINT [DF_FIRSTTIMEMENU_FIRSTTIME] DEFAULT (0) FOR [FIRSTTIME]
GO

ALTER TABLE [dbo].[ML_AUTH] ADD 
	CONSTRAINT [DF_ML_AUTH_TABLEID] DEFAULT (0) FOR [TABLEID],
	CONSTRAINT [DF_ML_AUTH_FIELDID] DEFAULT (0) FOR [FIELDID],
	CONSTRAINT [DF_ML_AUTH_DATEAUTH] DEFAULT (getdate()) FOR [DATEAUTH]
GO

ALTER TABLE [dbo].[ML_AUTHLOG] ADD 
	CONSTRAINT [DF_ML_AUTHLOG_AUTHDATE] DEFAULT (getdate()) FOR [AUTHDATE],
	CONSTRAINT [DF_ML_AUTHLOG_AUTHTYPE] DEFAULT (0) FOR [AUTHTYPE]
GO

ALTER TABLE [dbo].[ML_CONTACTS] ADD 
	CONSTRAINT [DF_ML_CONTACTS] DEFAULT (0) FOR [PROVINCE]
GO

ALTER TABLE [dbo].[ML_DESCRIPTION] ADD 
	CONSTRAINT [DF_ML_DESCRIPTION_SMS] DEFAULT (0) FOR [SMS]
GO

ALTER TABLE [dbo].[ML_LEAD] ADD 
	CONSTRAINT [DF_ML_LEAD_PROVINCE] DEFAULT (0) FOR [PROVINCE]
GO

ALTER TABLE [dbo].[ML_LOG] ADD 
	CONSTRAINT [DF_ML_LOG_MAILNUMBER] DEFAULT (0) FOR [MAILNUMBER],
	CONSTRAINT [DF_ML_LOG_SENDDATE] DEFAULT (getdate()) FOR [SENDDATE]
GO

ALTER TABLE [dbo].[ML_MAIL] ADD 
	CONSTRAINT [DF_ML_MAIL_CATEGORYID] DEFAULT (0) FOR [CATEGORYID],
	CONSTRAINT [DF_ML_MAIL_WELCOME] DEFAULT (0) FOR [WELCOME]
GO

ALTER TABLE [dbo].[ML_REMOVEDFROM] ADD 
	CONSTRAINT [DF_ML_REMOVEDFROM_ABUSE] DEFAULT (0) FOR [ABUSE]
GO

ALTER TABLE [dbo].[NEWMENU] ADD 
	CONSTRAINT [DF_NEWMENU_MODULES] DEFAULT (0) FOR [MODULES]
GO

ALTER TABLE [dbo].[OFFICES] ADD 
	CONSTRAINT [DF__OFFICES__CREATED__2739D489] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__OFFICES__CREATED__282DF8C2] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__OFFICES__LASTMOD__29221CFB] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__OFFICES__LASTMOD__2A164134] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[ORDERROWS] ADD 
	CONSTRAINT [DF__ORDERROWS__CATAL__61BC4730] DEFAULT (0) FOR [CATALOGID],
	CONSTRAINT [DF__ORDERROWS__QTA__62B06B69] DEFAULT (1) FOR [QTA],
	CONSTRAINT [DF__ORDERROWS__UPRIC__63A48FA2] DEFAULT (0) FOR [UPRICE],
	CONSTRAINT [DF__ORDERROWS__NEWUP__6498B3DB] DEFAULT (0) FOR [NEWUPRICE]
GO

ALTER TABLE [dbo].[ORDERS] ADD 
	CONSTRAINT [DF__ORDERS__CURRENCY__59B045BD] DEFAULT (1) FOR [CURRENCY],
	CONSTRAINT [DF__ORDERS__CHANGE__5AA469F6] DEFAULT (1) FOR [CHANGE],
	CONSTRAINT [DF__ORDERS__REDUCTIO__5B988E2F] DEFAULT (0) FOR [REDUCTION],
	CONSTRAINT [DF__ORDERS__STAGE__5C8CB268] DEFAULT (1) FOR [STAGE],
	CONSTRAINT [DF__ORDERS__PAYMENTI__5D80D6A1] DEFAULT (0) FOR [PAYMENTID],
	CONSTRAINT [DF__ORDERS__OWNERID__5E74FADA] DEFAULT (0) FOR [OWNERID],
	CONSTRAINT [DF__ORDERS__CROSSID__5F691F13] DEFAULT (0) FOR [CROSSID],
	CONSTRAINT [DF__ORDERS__GRANDTOT__605D434C] DEFAULT (0) FOR [GRANDTOTAL],
	CONSTRAINT [DF__ORDERS__INCLUDEP__61516785] DEFAULT (0) FOR [INCLUDEPRODPDF],
	CONSTRAINT [DF__ORDERS__QUOTEDAT__62458BBE] DEFAULT (getdate()) FOR [QUOTEDATE],
	CONSTRAINT [DF__ORDERS__CREATEDD__6339AFF7] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_ORDERS_CREATEDBYID] DEFAULT (1) FOR [CREATEDBYID],
	CONSTRAINT [DF_ORDERS_ORIGINALQUOTE] DEFAULT (0) FOR [ORIGINALQUOTE],
	CONSTRAINT [DF_ORDERS_LIST] DEFAULT (0) FOR [LIST]
GO

ALTER TABLE [dbo].[PAYMENTLIST] ADD 
	CONSTRAINT [DF_PAYMENTLIST_VIEWORDER] DEFAULT (0) FOR [VIEWORDER],
	CONSTRAINT [DF_PAYMENTLIST_LANG] DEFAULT ('IT') FOR [LANG],
	CONSTRAINT [DF_PAYMENTLIST_K_ID] DEFAULT (0) FOR [K_ID]
GO

ALTER TABLE [dbo].[PRODUCTCHARACTERISTICS] ADD 
	CONSTRAINT [DF__PRODUCTCH__CREAT__2BFE89A6] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__PRODUCTCH__CREAT__2CF2ADDF] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__PRODUCTCH__LASTM__2DE6D218] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__PRODUCTCH__LASTM__2EDAF651] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[PRODUCTS] ADD 
	CONSTRAINT [DF__PRODUCTS__CREATE__30C33EC3] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__PRODUCTS__CREATE__31B762FC] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__PRODUCTS__LASTMO__32AB8735] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__PRODUCTS__LASTMO__339FAB6E] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[QB_ALL_FIELDS] ADD 
	CONSTRAINT [DF_QB_ALL_FIELDS_FIELDTYPE] DEFAULT (0) FOR [FIELDTYPE],
	CONSTRAINT [DF_QB_ALL_FIELDS_FLAGPARAM] DEFAULT (0) FOR [FLAGPARAM],
	CONSTRAINT [DF_QB_ALL_FIELDS_FIELDCAT_RMVALUE] DEFAULT (0) FOR [FIELDCAT_RMVALUE],
	CONSTRAINT [DF_QB_ALL_FIELDS_VIEWORDER] DEFAULT (0) FOR [VIEWORDER]
GO

ALTER TABLE [dbo].[QB_ALL_TABLES] ADD 
	CONSTRAINT [DF_QB_ALL_TABLES_SELECTABLE] DEFAULT (0) FOR [SELECTABLE],
	CONSTRAINT [DF_QB_ALL_TABLES_PARENT] DEFAULT (0) FOR [PARENT],
	CONSTRAINT [DF_QB_ALL_TABLES_MODULES] DEFAULT (0) FOR [MODULES]
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERY] ADD 
	CONSTRAINT [DF_QB_CUSTOMERQUERY_FOREXPORT] DEFAULT (0) FOR [QUERYTYPE],
	CONSTRAINT [DF_QB_CUSTOMERQUERY_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_QB_CUSTOMERQUERY_CATEGORY] DEFAULT (0) FOR [CATEGORY]
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERYFIELDS] ADD 
	CONSTRAINT [DF_QB_CUSTOMERQUERYFIELDS_FIELDVISIBLE] DEFAULT (1) FOR [FIELDVISIBLE]
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERYTABLES] ADD 
	CONSTRAINT [DF_QB_CUSTOMERQUERYTABLES_MAINTABLE] DEFAULT (0) FOR [MAINTABLE]
GO

ALTER TABLE [dbo].[QUOTENUMBERS] ADD 
	CONSTRAINT [DF_QUOTENUMBERS_NPROG] DEFAULT (1) FOR [NPROG],
	CONSTRAINT [DF_QUOTENUMBERS_CHECKDAY] DEFAULT (0) FOR [CHECKDAY],
	CONSTRAINT [DF_QUOTENUMBERS_CHECKMONTH] DEFAULT (0) FOR [CHECKMONTH],
	CONSTRAINT [DF_QUOTENUMBERS_CHECKYEAR] DEFAULT (0) FOR [CHECKYEAR],
	CONSTRAINT [DF_QUOTENUMBERS_TWODIGITYEAR] DEFAULT (1) FOR [TWODIGITYEAR],
	CONSTRAINT [DF_QUOTENUMBERS_CHECKCUSTOMERCODE] DEFAULT (0) FOR [CHECKCUSTOMERCODE],
	CONSTRAINT [DF_QUOTENUMBERS_NPROGSTART] DEFAULT (1) FOR [NPROGSTART],
	CONSTRAINT [DF_QUOTENUMBERS_NPROGRESTART] DEFAULT (0) FOR [NPROGRESTART],
	CONSTRAINT [DF_QUOTENUMBERS_DISABLED] DEFAULT (0) FOR [DISABLED],
	CONSTRAINT [DF_QUOTENUMBERS_LASTRESET] DEFAULT (getdate()) FOR [LASTRESET],
	CONSTRAINT [DF_QUOTENUMBERS_TYPE] DEFAULT (0) FOR [TYPE]
GO

ALTER TABLE [dbo].[QUOTEROWS] ADD 
	CONSTRAINT [DF_QUOTEROWS_CATALOGID] DEFAULT (0) FOR [CATALOGID],
	CONSTRAINT [DF_QUOTEROWS_QTA] DEFAULT (1) FOR [QTA],
	CONSTRAINT [DF_QUOTEROWS_UPRICE] DEFAULT (0) FOR [UPRICE],
	CONSTRAINT [DF_QUOTEROWS_NEWUPRICE] DEFAULT (0) FOR [NEWUPRICE]
GO

ALTER TABLE [dbo].[QUOTES] ADD 
	CONSTRAINT [DF_QUOTES_CURRENCY] DEFAULT (1) FOR [CURRENCY],
	CONSTRAINT [DF_QUOTES_CHANGE] DEFAULT (1) FOR [CHANGE],
	CONSTRAINT [DF_QUOTES_REDUCTION] DEFAULT (0) FOR [REDUCTION],
	CONSTRAINT [DF_QUOTES_STAGE] DEFAULT (1) FOR [STAGE],
	CONSTRAINT [DF_QUOTES_PAYMENTID] DEFAULT (0) FOR [PAYMENTID],
	CONSTRAINT [DF_QUOTES_OWNERID] DEFAULT (0) FOR [OWNERID],
	CONSTRAINT [DF_QUOTES_CROSSID] DEFAULT (0) FOR [CROSSID],
	CONSTRAINT [DF_QUOTES_GRANDTOTAL] DEFAULT (0) FOR [GRANDTOTAL],
	CONSTRAINT [DF_QUOTES_INCLUDEPRODPDF] DEFAULT (0) FOR [INCLUDEPRODPDF],
	CONSTRAINT [DF_QUOTES_QUOTEDATE] DEFAULT (getdate()) FOR [QUOTEDATE],
	CONSTRAINT [DF_QUOTES_SHIPDATE] DEFAULT (getdate()) FOR [SHIPDATE],
	CONSTRAINT [DF_QUOTES_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_QUOTES_CREATEDBYID] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF_QUOTES_LIST] DEFAULT (0) FOR [LIST]
GO

ALTER TABLE [dbo].[QUOTESHIPMENT] ADD 
	CONSTRAINT [DF_QUOTESHIPMENT_REQUIREDDATE] DEFAULT (0) FOR [REQUIREDDATE]
GO

ALTER TABLE [dbo].[RECENT] ADD 
	CONSTRAINT [DF_RECENT_TYPE] DEFAULT (0) FOR [TYPE],
	CONSTRAINT [DF_RECENT_LASTDATE] DEFAULT (getdate()) FOR [LASTDATE]
GO

ALTER TABLE [dbo].[RECURRENCE] ADD 
	CONSTRAINT [DF_RECURRENCE_STARTDATE] DEFAULT (getdate()) FOR [STARTDATE],
	CONSTRAINT [DF_RECURRENCE_VAR1] DEFAULT (0) FOR [VAR1],
	CONSTRAINT [DF_RECURRENCE_VAR2] DEFAULT (0) FOR [VAR2],
	CONSTRAINT [DF_RECURRENCE_VAR3] DEFAULT (0) FOR [VAR3],
	CONSTRAINT [DF_RECURRENCE_STANDARD] DEFAULT (0) FOR [STANDARD]
GO

ALTER TABLE [dbo].[SURVEYRESPONSES] ADD 
	CONSTRAINT [DF_SURVEYRESPONSES_LASTACCESS] DEFAULT (getdate()) FOR [LASTACCESS]
GO

ALTER TABLE [dbo].[TAXVALUES] ADD 
	CONSTRAINT [DF_TAXVALUES_TAXVALUE] DEFAULT (0) FOR [TAXVALUE],
	CONSTRAINT [DF_TAXVALUES_VIEWORDER] DEFAULT (0) FOR [VIEWORDER]
GO

ALTER TABLE [dbo].[TICKET_AREA] ADD 
	CONSTRAINT [DF_TICKET_AREA_OWNERID] DEFAULT (0) FOR [OWNERID]
GO

ALTER TABLE [dbo].[TICKET_MAIN] ADD 
	CONSTRAINT [DF_TICKET_MAIN_OWNER] DEFAULT (0) FOR [Owner],
	CONSTRAINT [DF_TICKET_MAIN_STATUS] DEFAULT (0) FOR [STATUS],
	CONSTRAINT [DF_TICKET_MAIN_OPENDATE] DEFAULT (getdate()) FOR [OPENDATE],
	CONSTRAINT [DF_TICKET_MAIN_TIMETOFIX] DEFAULT (0) FOR [TIMETOFIX],
	CONSTRAINT [DF_TICKET_MAIN_PRIORITY] DEFAULT (0) FOR [PRIORITY]
GO

ALTER TABLE [dbo].[TICKET_PROGRESS] ADD 
	CONSTRAINT [DF_TICKET_PROGRESS_TICKETIDPROG] DEFAULT (0) FOR [TICKETIDPROG]
GO

ALTER TABLE [dbo].[TICKET_SCHEDULE] ADD 
	CONSTRAINT [DF_TICKET_SCHEDULE_DAYOFWEEK] DEFAULT (0) FOR [DAYOFWEEK],
	CONSTRAINT [DF_TICKET_SCHEDULE_STARTMINUTE] DEFAULT (0) FOR [STARTMINUTE],
	CONSTRAINT [DF_TICKET_SCHEDULE_ENDMINUTE] DEFAULT (0) FOR [ENDMINUTE]
GO

ALTER TABLE [dbo].[TICKET_SLA] ADD 
	CONSTRAINT [DF_TICKET_SLA_TIMETOTAKE] DEFAULT (0) FOR [TIMETOTAKE],
	CONSTRAINT [DF_TICKET_SLA_TIMETORESOLVE] DEFAULT (0) FOR [TIMETORESOLVE],
	CONSTRAINT [DF_TICKET_SLA_TICKETSTATUS] DEFAULT (0) FOR [TICKETSTATUS],
	CONSTRAINT [DF_TICKET_SLA_TICKETPRIORITY] DEFAULT (0) FOR [TICKETPRIORITY],
	CONSTRAINT [DF_TICKET_SLA_TYPE] DEFAULT (0) FOR [TYPE]
GO

ALTER TABLE [dbo].[TICKET_SLA_CUSTOMER] ADD 
	CONSTRAINT [DF_TICKET_SLA_CUSTOMER_CONTACTTYPE] DEFAULT (0) FOR [CONTACTTYPE]
GO

ALTER TABLE [dbo].[TICKET_USER] ADD 
	CONSTRAINT [DF_TICKET_USER_TYPE] DEFAULT (0) FOR [TYPE]
GO

ALTER TABLE [dbo].[TIMEZONES] ADD 
	CONSTRAINT [DF_TIMEZONES_MDAYLIGHT] DEFAULT (0) FOR [MDAYLIGHT]
GO

ALTER TABLE [dbo].[TODOLIST] ADD 
	CONSTRAINT [DF_TODOLIST_PRIORITY] DEFAULT (0) FOR [PRIORITY],
	CONSTRAINT [DF_TODOLIST_FLAGEXECUTED] DEFAULT (0) FOR [FLAGEXECUTED],
	CONSTRAINT [DF_TODOLIST_FLAGRECURRENCE] DEFAULT (0) FOR [FLAGRECURRENCE],
	CONSTRAINT [DF__TODOLIST__CREATE__2A37081C] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF__TODOLIST__CREATE__2B2B2C55] DEFAULT (0) FOR [CREATEDBYID],
	CONSTRAINT [DF__TODOLIST__LASTMO__2C1F508E] DEFAULT (getdate()) FOR [LASTMODIFIEDDATE],
	CONSTRAINT [DF__TODOLIST__LASTMO__2D1374C7] DEFAULT (0) FOR [LASTMODIFIEDBYID]
GO

ALTER TABLE [dbo].[TOKENS] ADD 
	CONSTRAINT [DF_TOKENS_EXPIRE] DEFAULT (getdate()) FOR [EXPIRE]
GO

ALTER TABLE [dbo].[TUSTENA_DATA] ADD 
	CONSTRAINT [DF_EWORK_CUSTOMER_CREATEDDATE] DEFAULT (getdate()) FOR [CREATEDDATE],
	CONSTRAINT [DF_EWORK_CUSTOMER_ACTIVE] DEFAULT (1) FOR [ACTIVE],
	CONSTRAINT [DF_EWORK_CUSTOMER_TESTING] DEFAULT (1) FOR [TESTING],
	CONSTRAINT [DF_EWORK_CUSTOMER_MAXUSER] DEFAULT (20) FOR [MAXUSER],
	CONSTRAINT [DF_EWORK_CUSTOMER_ADMINGROUPMENU] DEFAULT (1) FOR [ADMINGROUPID],
	CONSTRAINT [DF_EWORK_CUSTOMER_PHONENORMALIZE] DEFAULT ('+##-####-#######') FOR [PHONENORMALIZE],
	CONSTRAINT [DF_EWORK_CUSTOMER_CUSTOMTYPES] DEFAULT (0) FOR [CUSTOMTYPES],
	CONSTRAINT [DF_EWORK_CUSTOMER_LASTACCESS] DEFAULT (getdate()) FOR [LASTACCESS],
	CONSTRAINT [DF_EWORK_CUSTOMER_TESTINGDAYS] DEFAULT (30) FOR [TESTINGDAYS],
	CONSTRAINT [DF_EWORK_CUSTOMER_ESTIMATEDDATEDAYS] DEFAULT (90) FOR [ESTIMATEDDATEDAYS],
	CONSTRAINT [DF_EWORK_CUSTOMER_DEBUGMODE] DEFAULT (0) FOR [DEBUGMODE],
	CONSTRAINT [DF_EWORK_CUSTOMER_DATASTORAGECAPACITY] DEFAULT (20) FOR [DATASTORAGECAPACITY],
	CONSTRAINT [DF_EWORK_CUSTOMER_SMSCREDIT] DEFAULT (0) FOR [SMSCREDIT],
	CONSTRAINT [DF_EWORK_CUSTOMER_DISKSPACE] DEFAULT (30) FOR [DISKSPACE],
	CONSTRAINT [DF_TUSTENA_COMPANIES_WIZARD] DEFAULT (0) FOR [WIZARD],
	CONSTRAINT [DF_TUSTENA_DATA_MODULES] DEFAULT (127) FOR [MODULES]
GO

ALTER TABLE [dbo].[USAGELOG] ADD 
	CONSTRAINT [DF_USAGELOG_DATELOG] DEFAULT (getdate()) FOR [DATELOG]
GO

ALTER TABLE [dbo].[VERSION] ADD 
	CONSTRAINT [DF_VERSION_MODIFIED] DEFAULT (0) FOR [MODIFIED],
	CONSTRAINT [DF_VERSION_MODIFYDATE] DEFAULT (getdate()) FOR [MODIFYDATE]
GO

ALTER TABLE [dbo].[VIEWSTATEMANAGER] ADD 
	CONSTRAINT [DF_VIEWSTATEMANAGER_LASTACCESS] DEFAULT (getdate()) FOR [LASTACCESS]
GO

ALTER TABLE [dbo].[WEBGATEPARAMS] ADD 
	CONSTRAINT [DF_WEBGATEPARAMS_NOTIFYBY] DEFAULT (0) FOR [NOTIFYBY],
	CONSTRAINT [DF_WEBGATEPARAMS_CATEGORYID] DEFAULT (0) FOR [CATEGORYID],
	CONSTRAINT [DF_WEBGATEPARAMS_PERSONALCATEGORYID] DEFAULT (0) FOR [PERSONALCATEGORYID]
GO

ALTER TABLE [dbo].[ZONES] ADD 
	CONSTRAINT [DF_ZONES_VIEWORDER] DEFAULT (0) FOR [VIEWORDER]
GO

ALTER TABLE [dbo].[ADDEDFIELDS_CROSS] ADD 
	CONSTRAINT [FK_ADDEDFIELDS_CROSS_ADDEDFIELDS] FOREIGN KEY 
	(
		[IDRIF]
	) REFERENCES [dbo].[ADDEDFIELDS] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[BASE_CONTACTS_LINKS] ADD 
	CONSTRAINT [FK_BASE_CONTACTS_LINKS_EWORK_CUSTOMER] FOREIGN KEY 
	(
		[COMPANYID]
	) REFERENCES [dbo].[TUSTENA_DATA] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[CRM_BILLROWS] ADD 
	CONSTRAINT [FK_CRM_BILLROWS_CRM_BILL] FOREIGN KEY 
	(
		[BILLID]
	) REFERENCES [dbo].[CRM_BILL] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[CRM_CROSSOPPORTUNITY] ADD 
	CONSTRAINT [FK_CRM_CROSSOPPORTUNITY_CRM_OPPORTUNITY] FOREIGN KEY 
	(
		[OPPORTUNITYID]
	) REFERENCES [dbo].[CRM_OPPORTUNITY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[CRM_CROSSOPPORTUNITYREFERRING] ADD 
	CONSTRAINT [FK_CRM_CROSSOPPORTUNITYREFERRING_CRM_OPPORTUNITY] FOREIGN KEY 
	(
		[OPPORTUNITYID]
	) REFERENCES [dbo].[CRM_OPPORTUNITY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYCOMPETITOR] ADD 
	CONSTRAINT [FK_CRM_OPPORTUNITYCOMPETITOR_CRM_OPPORTUNITY] FOREIGN KEY 
	(
		[OPPORTUNITYID]
	) REFERENCES [dbo].[CRM_OPPORTUNITY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYCOMPETITORPRODUCTS] ADD 
	CONSTRAINT [FK_CRM_OPPORTUNITYCOMPETITORPRODUCTS_CRM_OPPORTUNITY] FOREIGN KEY 
	(
		[OPPORTUNITYID]
	) REFERENCES [dbo].[CRM_OPPORTUNITY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYCONTACT] ADD 
	CONSTRAINT [FK_CRM_OPPORTUNITYCONTACT_CRM_OPPORTUNITY] FOREIGN KEY 
	(
		[OPPORTUNITYID]
	) REFERENCES [dbo].[CRM_OPPORTUNITY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[CRM_OPPORTUNITYPARTNERS] ADD 
	CONSTRAINT [FK_CRM_OPPORTUNITYPARTNERS_CRM_OPPORTUNITY] FOREIGN KEY 
	(
		[OPPORTUNITYID]
	) REFERENCES [dbo].[CRM_OPPORTUNITY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[CRM_OPPPRODUCTROWS] ADD 
	CONSTRAINT [FK_CRM_OPPPRODUCTROWS_CRM_OPPORTUNITY] FOREIGN KEY 
	(
		[OPPORTUNITYID]
	) REFERENCES [dbo].[CRM_OPPORTUNITY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[ESTIMATEDROWS] ADD 
	CONSTRAINT [FK_ESTIMATEDROWS_ESTIMATES] FOREIGN KEY 
	(
		[ESTIMATEID]
	) REFERENCES [dbo].[ESTIMATES] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[FILECROSSTABLES] ADD 
	CONSTRAINT [FK_FILECROSSTABLES_FILEMANAGER] FOREIGN KEY 
	(
		[IDFILE]
	) REFERENCES [dbo].[FILEMANAGER] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[ML_ATTACHMENT] ADD 
	CONSTRAINT [FK_ML_ATTACHMENT_ML_MAIL] FOREIGN KEY 
	(
		[MLID]
	) REFERENCES [dbo].[ML_MAIL] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[ML_COMPANIES] ADD 
	CONSTRAINT [FK_ML_COMPANIES_ML_DESCRIPTION] FOREIGN KEY 
	(
		[IDMAILINGLIST]
	) REFERENCES [dbo].[ML_DESCRIPTION] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[ML_CONTACTS] ADD 
	CONSTRAINT [FK_ML_CONTACTS_ML_DESCRIPTION] FOREIGN KEY 
	(
		[IDMAILINGLIST]
	) REFERENCES [dbo].[ML_DESCRIPTION] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[ML_FIXEDPARAMS] ADD 
	CONSTRAINT [FK_ML_FIXEDPARAMS_ML_DESCRIPTION] FOREIGN KEY 
	(
		[IDMAILINGLIST]
	) REFERENCES [dbo].[ML_DESCRIPTION] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[ML_LEAD] ADD 
	CONSTRAINT [FK_ML_LEAD_ML_DESCRIPTION] FOREIGN KEY 
	(
		[IDMAILINGLIST]
	) REFERENCES [dbo].[ML_DESCRIPTION] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[ML_LOG] ADD 
	CONSTRAINT [FK_ML_LOG_ML_MAIL] FOREIGN KEY 
	(
		[MAILID]
	) REFERENCES [dbo].[ML_MAIL] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[ML_REMOVEDFROM] ADD 
	CONSTRAINT [FK_ML_REMOVEDFROM_ML_DESCRIPTION] FOREIGN KEY 
	(
		[IDML]
	) REFERENCES [dbo].[ML_DESCRIPTION] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[ORDERROWS] ADD 
	CONSTRAINT [FK_ORDERROWS_ORDERS] FOREIGN KEY 
	(
		[ORDERID]
	) REFERENCES [dbo].[ORDERS] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERYFIELDS] ADD 
	CONSTRAINT [FK_QB_CUSTOMERQUERYFIELDS_QB_CUSTOMERQUERY] FOREIGN KEY 
	(
		[IDQUERY]
	) REFERENCES [dbo].[QB_CUSTOMERQUERY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERYFREEFIELDS] ADD 
	CONSTRAINT [FK_QB_CUSTOMERQUERYFREEFIELDS_QB_CUSTOMERQUERY] FOREIGN KEY 
	(
		[IDQUERY]
	) REFERENCES [dbo].[QB_CUSTOMERQUERY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERYPARAMFIELDS] ADD 
	CONSTRAINT [FK_QB_CUSTOMERQUERYPARAMFIELDS_QB_CUSTOMERQUERY] FOREIGN KEY 
	(
		[IDQUERY]
	) REFERENCES [dbo].[QB_CUSTOMERQUERY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[QB_CUSTOMERQUERYTABLES] ADD 
	CONSTRAINT [FK_QB_CUSTOMERQUERYTABLES_QB_CUSTOMERQUERY] FOREIGN KEY 
	(
		[IDQUERY]
	) REFERENCES [dbo].[QB_CUSTOMERQUERY] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[QUOTEROWS] ADD 
	CONSTRAINT [FK_QUOTEROWS_QUOTES] FOREIGN KEY 
	(
		[ESTIMATEID]
	) REFERENCES [dbo].[QUOTES] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[SCORELOG] ADD 
	CONSTRAINT [FK_SCORELOG_SCOREVALUES] FOREIGN KEY 
	(
		[IDVALUE]
	) REFERENCES [dbo].[SCOREVALUES] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[SCOREVALUES] ADD 
	CONSTRAINT [FK_SCOREVALUES_SCOREDESCRIPTION] FOREIGN KEY 
	(
		[IDDESCRIPTION]
	) REFERENCES [dbo].[SCOREDESCRIPTION] (
		[ID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[TICKET_MOVELOG] ADD 
	CONSTRAINT [FK_TICKET_MOVELOG_TICKET_MAIN1] FOREIGN KEY 
	(
		[TICKETID]
	) REFERENCES [dbo].[TICKET_MAIN] (
		[TICKETID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

ALTER TABLE [dbo].[TICKET_SUPPORTLOG] ADD 
	CONSTRAINT [FK_TICKET_SUPPORTLOG_TICKET_MAIN] FOREIGN KEY 
	(
		[TICKETID]
	) REFERENCES [dbo].[TICKET_MAIN] (
		[TICKETID]
	) ON DELETE CASCADE NOT FOR REPLICATION
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



/****** Oggetto: vista dbo.Admin_Customer_View    Data dello script: 30/08/2005 17.42.56 ******/
create VIEW ADMIN_CUSTOMER_VIEW
AS
SELECT DBO.WEBGATEPARAMS.WEBSITE AS WEBGATEWEBSITE, DBO.WEBGATEPARAMS.OWNERID AS WEBGATEOWNERID, 
               DBO.WEBGATEPARAMS.GROUPS AS WEBGATEGROUP, DBO.WEBGATEPARAMS.NOTIFYID AS WEBGATENOTIFYID, DBO.WEBGATEPARAMS.ID AS WEBGATEID, 
               ACCOUNT_2.Name + ' ' + ACCOUNT_2.SURNAME AS WEBGATEOWNER, ACCOUNT_1.Name + ' ' + ACCOUNT_1.SURNAME AS WEBGATENOTIFY, 
               DBO.GROUPS.DESCRIPTION AS WEBGATEGROUPDESCRIPTION, DBO.TUSTENA_DATA.*
FROM  DBO.WEBGATEPARAMS LEFT OUTER JOIN
               DBO.ACCOUNT ACCOUNT_1 ON DBO.WEBGATEPARAMS.NOTIFYID = ACCOUNT_1.UID LEFT OUTER JOIN
               DBO.ACCOUNT ACCOUNT_2 ON DBO.WEBGATEPARAMS.OWNERID = ACCOUNT_2.UID FULL OUTER JOIN
               DBO.GROUPS ON DBO.WEBGATEPARAMS.GROUPS = DBO.GROUPS.ID CROSS JOIN
               DBO.TUSTENA_DATA



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





/****** OGGETTO: VISTA DBO.COMPLETELEAD_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.42.59 ******/

CREATE VIEW DBO.COMPLETELEAD_VIEW
AS
SELECT     DBO.CRM_LEADS.*, DBO.CRM_CROSSLEAD.ASSOCIATEDCOMPANY AS ASSOCIATEDCOMPANY, DBO.CRM_CROSSLEAD.ASSOCIATEDCONTACT AS ASSOCIATEDCONTACT, 
                      DBO.CRM_CROSSLEAD.LEADOWNER AS LEADOWNER, DBO.CRM_CROSSLEAD.LEADID AS LEADID, DBO.CRM_CROSSLEAD.STATUS AS STATUS, 
                      DBO.CRM_CROSSLEAD.RATING AS RATING, DBO.CRM_CROSSLEAD.POTENTIALREVENUE AS POTENTIALREVENUE, 
                      DBO.CRM_CROSSLEAD.PRODUCTINTEREST AS PRODUCTINTEREST, DBO.CRM_CROSSLEAD.ESTIMATEDCLOSEDATE AS ESTIMATEDCLOSEDATE, 
                      DBO.CRM_CROSSLEAD.LEADCURRENCY AS LEADCURRENCY, DBO.CRM_CROSSLEAD.SOURCE AS SOURCE, DBO.CRM_CROSSLEAD.CAMPAIGN AS CAMPAIGN, 
                      DBO.CRM_CROSSLEAD.INDUSTRY AS INDUSTRY, DBO.CRM_CROSSLEAD.SALESPERSON AS SALESPERSON, 
                      DBO.CRM_CROSSLEAD.OTHEROPPORTUNIES AS ASSOCIATEDOPPORTUNITY
FROM         DBO.CRM_LEADS LEFT OUTER JOIN
                      DBO.CRM_CROSSLEAD ON DBO.CRM_LEADS.ID = DBO.CRM_CROSSLEAD.LEADID
WHERE     (DBO.CRM_LEADS.LIMBO = 0)




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





/****** OGGETTO: VISTA DBO.CRM_OPPORTUNITY_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.42.56 ******/

CREATE VIEW DBO.CRM_OPPORTUNITY_VIEW
AS
SELECT     DBO.ACCOUNT.NAME + ' ' + DBO.ACCOUNT.SURNAME AS OWNER, ACCOUNT_1.NAME + ' ' + ACCOUNT_1.SURNAME AS CREATEDBY, 
                      ACCOUNT_2.NAME + ' ' + ACCOUNT_2.SURNAME AS LASTMODIFIEDBY, DBO.CRM_OPPORTUNITY.*
FROM         DBO.CRM_OPPORTUNITY INNER JOIN
                      DBO.ACCOUNT ON DBO.CRM_OPPORTUNITY.OWNERID = DBO.ACCOUNT.UID INNER JOIN
                      DBO.ACCOUNT ACCOUNT_1 ON DBO.CRM_OPPORTUNITY.CREATEDBYID = ACCOUNT_1.UID INNER JOIN
                      DBO.ACCOUNT ACCOUNT_2 ON DBO.CRM_OPPORTUNITY.LASTMODIFIEDBYID = ACCOUNT_2.UID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





/****** OGGETTO: VISTA DBO.CRM_TODOLIST_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.42.56 ******/

CREATE VIEW DBO.CRM_TODOLIST_VIEW
AS
SELECT     DBO.CRM_TODOLIST.*, ACCOUNT_2.NAME + ' ' + ACCOUNT_2.SURNAME AS OWNERNAME, ACCOUNT_1.NAME + ' ' + ACCOUNT_1.SURNAME AS CREATEDBYNAME, 
                      ACCOUNT_2.NAME + ' ' + ACCOUNT_2.SURNAME AS MODIFIEDBYNAME, DBO.BASE_COMPANIES.COMPANYNAME AS COMPANYNAME, 
                      DBO.CRM_OPPORTUNITY.TITLE AS OPPORTUNITYTITLE
FROM         DBO.BASE_COMPANIES RIGHT OUTER JOIN
                      DBO.CRM_OPPORTUNITY RIGHT OUTER JOIN
                      DBO.CRM_TODOLIST LEFT OUTER JOIN
                      DBO.ACCOUNT AS ACCOUNT_2 ON DBO.CRM_TODOLIST.OWNERID = ACCOUNT_2.UID ON DBO.CRM_OPPORTUNITY.ID = DBO.CRM_TODOLIST.OPPORTUNITYID ON 
                      DBO.BASE_COMPANIES.ID = DBO.CRM_TODOLIST.COMPANYID LEFT OUTER JOIN
                      DBO.ACCOUNT AS ACCOUNT_1 ON DBO.CRM_TODOLIST.CREATEDBYID = ACCOUNT_1.UID LEFT OUTER JOIN
                      DBO.ACCOUNT AS ACCOUNT_3 ON DBO.CRM_TODOLIST.OWNERID = ACCOUNT_3.UID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE VIEW CRM_WORKACTIVITYSEARCH_VIEW
AS
SELECT     DBO.CRM_WORKACTIVITY.*, DBO.ACCOUNT.SURNAME + ' ' + DBO.ACCOUNT.Name AS OWNERNAME, DBO.BASE_COMPANIES.COMPANYNAME AS COMPANYNAME, 
                      DBO.BASE_CONTACTS.SURNAME + ' ' + DBO.BASE_CONTACTS.Name AS CONTACTNAME, ISNULL(DBO.CRM_LEADS.SURNAME, '') 
                      + ' ' + ISNULL(DBO.CRM_LEADS.Name, '') + ' ' + ISNULL(DBO.CRM_LEADS.COMPANYNAME, '') AS LEADNAME, ISNULL(DBO.BASE_COMPANIES.GROUPS, '') 
                      + ISNULL(DBO.BASE_CONTACTS.GROUPS, '') + ISNULL(DBO.CRM_LEADS.GROUPS, '') AS ACGROUP, ISNULL(DBO.CRM_LEADS.COMMERCIALZONE, 0) 
                      AS LEADCOMMERCIALZONE, ISNULL(DBO.BASE_CONTACTS.COMMERCIALZONE, 0) AS CONTACTCOMMERCIALZONE, 
                      ISNULL(DBO.BASE_COMPANIES.COMMERCIALZONE, 0) AS COMPANYCOMMERCIALZONE
FROM         DBO.CRM_WORKACTIVITY LEFT OUTER JOIN
                      DBO.CRM_LEADS ON DBO.CRM_WORKACTIVITY.LEADID = DBO.CRM_LEADS.ID LEFT OUTER JOIN
                      DBO.BASE_CONTACTS ON DBO.CRM_WORKACTIVITY.REFERRERID = DBO.BASE_CONTACTS.ID LEFT OUTER JOIN
                      DBO.BASE_COMPANIES ON DBO.CRM_WORKACTIVITY.COMPANYID = DBO.BASE_COMPANIES.ID LEFT OUTER JOIN
                      DBO.ACCOUNT ON DBO.CRM_WORKACTIVITY.OWNERID = DBO.ACCOUNT.UID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE VIEW CRM_WORKING_VIEW
AS
SELECT     DBO.CRM_WORKACTIVITY.*, DBO.ACCOUNT.SURNAME + ' ' + DBO.ACCOUNT.Name AS OWNERNAME, DBO.BASE_COMPANIES.COMPANYNAME AS COMPANYNAME, 
                      DBO.BASE_CONTACTS.SURNAME + ' ' + ISNULL(DBO.BASE_CONTACTS.Name, '') AS CONTACTNAME, CRM_WORKACTIVITY_1.SUBJECT AS PARENTSUBJECT, 
                      DBO.CRM_OPPORTUNITY.TITLE AS OPPORTUNITYDESCRIPTION, ISNULL(DBO.CRM_LEADS.SURNAME, '') + ' ' + ISNULL(DBO.CRM_LEADS.Name, '') 
                      + '-' + ISNULL(DBO.CRM_LEADS.COMPANYNAME, '') AS LEADNAME, ISNULL(DBO.CRM_LEADS.COMMERCIALZONE, 0) AS LEADCOMMERCIALZONE, 
                      ISNULL(DBO.BASE_CONTACTS.COMMERCIALZONE, 0) AS CONTACTCOMMERCIALZONE, ISNULL(DBO.BASE_COMPANIES.COMMERCIALZONE, 0) 
                      AS COMPANYCOMMERCIALZONE
FROM         DBO.CRM_WORKACTIVITY LEFT OUTER JOIN
                      DBO.CRM_LEADS ON DBO.CRM_WORKACTIVITY.LEADID = DBO.CRM_LEADS.ID LEFT OUTER JOIN
                      DBO.CRM_OPPORTUNITY ON DBO.CRM_WORKACTIVITY.OPPORTUNITYID = DBO.CRM_OPPORTUNITY.ID LEFT OUTER JOIN
                      DBO.CRM_WORKACTIVITY CRM_WORKACTIVITY_1 ON DBO.CRM_WORKACTIVITY.PARENTID = CRM_WORKACTIVITY_1.ID LEFT OUTER JOIN
                      DBO.BASE_CONTACTS ON DBO.CRM_WORKACTIVITY.REFERRERID = DBO.BASE_CONTACTS.ID LEFT OUTER JOIN
                      DBO.BASE_COMPANIES ON DBO.CRM_WORKACTIVITY.COMPANYID = DBO.BASE_COMPANIES.ID LEFT OUTER JOIN
                      DBO.ACCOUNT ON DBO.CRM_WORKACTIVITY.OWNERID = DBO.ACCOUNT.UID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO






/****** OGGETTO: VISTA DBO.LASTCONTACT_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.43.00 ******/

CREATE VIEW DBO.LASTCONTACT_VIEW
AS
SELECT     DBO.LASTCONTACT.*, DBO.CRM_WORKACTIVITY.OWNERID AS OWNERID, DBO.CRM_WORKACTIVITY.TYPE AS TYPE, DBO.CRM_WORKACTIVITY.REFERRERID AS REFERRERID,
                       DBO.CRM_WORKACTIVITY.SUBJECT AS SUBJECT, DBO.CRM_WORKACTIVITY.COMPANYID AS COMPANYID, DBO.CRM_WORKACTIVITY.OPPORTUNITYID AS OPPORTUNITYID, 
                      DBO.CRM_WORKACTIVITY.LEADID AS LEADID
FROM         DBO.LASTCONTACT LEFT OUTER JOIN
                      DBO.CRM_WORKACTIVITY ON DBO.LASTCONTACT.ACTIVITYID = DBO.CRM_WORKACTIVITY.ID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

create VIEW LEADFORMAIL_VIEW
AS
SELECT     DBO.CRM_LEADS.*, DBO.CRM_CROSSLEAD.INDUSTRY AS INDUSTRY
FROM         DBO.CRM_LEADS LEFT OUTER JOIN
                      DBO.CRM_CROSSLEAD ON DBO.CRM_LEADS.ID = DBO.CRM_CROSSLEAD.LEADID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO






/****** OGGETTO: VISTA DBO.QB_CONTACTS_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.42.57 ******/

CREATE VIEW DBO.QB_CONTACTS_VIEW
AS
SELECT     DBO.BASE_COMPANIES.*, DBO.ACCOUNT.NAME + ' ' + DBO.ACCOUNT.SURNAME AS OWNERNAME, DBO.CONTACTTYPE.CONTACTTYPE AS CONTACTTYPETXT, 
                      DBO.COMPANYTYPE.DESCRIPTION AS COMPANYTYPETXT, DBO.CONTACTESTIMATE.ESTIMATE AS ESTIMATETXT
FROM         DBO.BASE_COMPANIES LEFT OUTER JOIN
                      DBO.COMPANYTYPE ON DBO.BASE_COMPANIES.COMPANYTYPEID = DBO.COMPANYTYPE.ID LEFT OUTER JOIN
                      DBO.CONTACTTYPE ON DBO.BASE_COMPANIES.CONTACTTYPEID = DBO.CONTACTTYPE.ID LEFT OUTER JOIN
                      DBO.CONTACTESTIMATE ON DBO.BASE_COMPANIES.ESTIMATE = DBO.CONTACTESTIMATE.ID LEFT OUTER JOIN
                      DBO.ACCOUNT ON DBO.BASE_COMPANIES.OWNERID = DBO.ACCOUNT.UID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

create VIEW QUOTE_VIEW
AS
SELECT     DBO.CRM_WORKACTIVITY.*, DBO.ESTIMATES.Currency AS Currency, DBO.ESTIMATES.CHANGE AS CHANGE, DBO.ESTIMATES.EXPIRATIONDATE AS EXPIRATIONDATE, 
                      DBO.ESTIMATES.REDUCTION AS REDUCTION, DBO.ESTIMATES.STAGE AS STAGE, DBO.ESTIMATES.Number AS Number, DBO.ESTIMATES.ID AS QUOTEID
FROM         DBO.CRM_WORKACTIVITY INNER JOIN
                      DBO.ESTIMATES ON DBO.CRM_WORKACTIVITY.ID = DBO.ESTIMATES.ACTIVITYID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





/****** OGGETTO: VISTA DBO.SYNCRO_EMAILMATCH    DATA DELLO SCRIPT: 30/08/2005 17.42.58 ******/

CREATE VIEW DBO.SYNCRO_EMAILMATCH
AS
SELECT     DBO.BASE_CONTACTS.NAME + ' ' + DBO.BASE_CONTACTS.SURNAME AS NAME, DBO.BASE_COMPANIES.COMPANYNAME, DBO.BASE_CONTACTS.ID, 
                      DBO.BASE_CONTACTS.EMAIL
FROM         DBO.BASE_CONTACTS LEFT OUTER JOIN
                      DBO.BASE_COMPANIES ON DBO.BASE_CONTACTS.COMPANYID = DBO.BASE_COMPANIES.ID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

create VIEW TICKETINFO_VIEW
AS
SELECT     DBO.TICKET_MAIN.*, DBO.TICKET_AREA.AREA AS AREATXT, DBO.TICKET_TYPE.DESCRIPTION AS TYPETXT, 
                      DBO.ACCOUNT.Name + ' ' + DBO.ACCOUNT.SURNAME AS OWNERTXT
FROM         DBO.TICKET_MAIN LEFT OUTER JOIN
                      DBO.TICKET_AREA ON DBO.TICKET_MAIN.AREA = DBO.TICKET_AREA.ID LEFT OUTER JOIN
                      DBO.TICKET_TYPE ON DBO.TICKET_MAIN.TYPE = DBO.TICKET_TYPE.ID LEFT OUTER JOIN
                      DBO.ACCOUNT ON DBO.TICKET_MAIN.Owner = DBO.ACCOUNT.UID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO


/****** OGGETTO: VISTA DBO.TUSTENAMENU_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.42.58 ******/
CREATE VIEW dbo.TUSTENAMENU_VIEW
AS
SELECT     dbo.NEWMENU.ID, dbo.NEWMENU.VOICE, dbo.NEWMENU.LINK, dbo.NEWMENU.LASTMENU, dbo.NEWMENU.PARENTMENU, 
                      dbo.NEWMENU.SORTORDER, dbo.NEWMENU.MENUTITLE, dbo.COMPANYMENU.ACCESSGROUP, dbo.NEWMENU.FOLDER, dbo.NEWMENU.MODE, 
                      dbo.NEWMENU.RMVALUE, dbo.NEWMENU.ACTIVE, dbo.NEWMENU.MODULES
FROM         dbo.NEWMENU INNER JOIN
                      dbo.COMPANYMENU ON dbo.NEWMENU.ID = dbo.COMPANYMENU.MENUID
WHERE     (dbo.NEWMENU.ACTIVE = 1)


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

create VIEW VIEW_QUOTEMIGRATION
AS
SELECT     DBO.CRM_WORKACTIVITY.ID, DBO.ESTIMATES.ID AS ESTIMATEID, DBO.CRM_WORKACTIVITY.OWNERID, DBO.CRM_WORKACTIVITY.TYPE, 
                      DBO.CRM_WORKACTIVITY.REFERRERID, DBO.CRM_WORKACTIVITY.SUBJECT, DBO.CRM_WORKACTIVITY.DESCRIPTION, DBO.CRM_WORKACTIVITY.COMPANYID, 
                      DBO.CRM_WORKACTIVITY.GROUPS, DBO.CRM_WORKACTIVITY.ACTIVITYDATE, DBO.CRM_WORKACTIVITY.LEADID, 
                      DBO.ESTIMATES.EXPIRATIONDATE, DBO.ESTIMATES.REDUCTION, DBO.ESTIMATES.STAGE, DBO.ESTIMATES.number
FROM         DBO.CRM_WORKACTIVITY INNER JOIN
                      DBO.ESTIMATES ON DBO.CRM_WORKACTIVITY.ID = DBO.ESTIMATES.ACTIVITYID

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO






/****** OGGETTO: VISTA DBO.COMPETITORCROSSOPPORTUNITY_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.42.59 ******/

CREATE VIEW DBO.COMPETITORCROSSOPPORTUNITY_VIEW
AS
SELECT     DBO.CRM_OPPORTUNITYCOMPETITOR.OPPORTUNITYID, DBO.CRM_OPPORTUNITYCOMPETITOR.COMPETITORID, DBO.CRM_CROSSCONTACTCOMPETITOR.RELATION, 
                      DBO.CRM_CROSSCONTACTCOMPETITOR.CONTACTID, DBO.CRM_CROSSCONTACTCOMPETITOR.ID, DBO.CRM_CROSSCONTACTCOMPETITOR.CONTACTTYPE, 
                      DBO.BASE_COMPANIES.COMPANYNAME
FROM         DBO.CRM_CROSSCONTACTCOMPETITOR INNER JOIN
                      DBO.BASE_COMPANIES ON DBO.CRM_CROSSCONTACTCOMPETITOR.COMPETITORID = DBO.BASE_COMPANIES.ID RIGHT OUTER JOIN
                      DBO.CRM_OPPORTUNITYCOMPETITOR ON DBO.CRM_CROSSCONTACTCOMPETITOR.COMPETITORID = DBO.CRM_OPPORTUNITYCOMPETITOR.COMPETITORID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





/****** OGGETTO: VISTA DBO.CRM_CROSSOPPREF_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.42.58 ******/

CREATE VIEW DBO.CRM_CROSSOPPREF_VIEW
AS
SELECT     DBO.CRM_CROSSOPPORTUNITYREFERRING.*, DBO.BASE_CONTACTS.NAME + ' ' + DBO.BASE_CONTACTS.SURNAME AS REFERRERNAME, 
                      DBO.BASE_CONTACTS.LIMBO AS LIMBO
FROM         DBO.CRM_CROSSOPPORTUNITYREFERRING LEFT OUTER JOIN
                      DBO.BASE_CONTACTS ON DBO.CRM_CROSSOPPORTUNITYREFERRING.REFERRERID = DBO.BASE_CONTACTS.ID
WHERE     (DBO.BASE_CONTACTS.LIMBO = 0)




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO






/****** OGGETTO: VISTA DBO.DASH1_COMPANYINDUSTRY_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.42.59 ******/

CREATE VIEW DBO.DASH1_COMPANYINDUSTRY_VIEW
AS
SELECT     DBO.CRM_OPPORTUNITYCONTACT.OPPORTUNITYID, COMPANYTYPE.DESCRIPTION AS COMPANYINDUSTRY, DBO.BASE_COMPANIES.ID AS COMPANYID, 
                      ISNULL(DBO.BASE_COMPANIES.COMPANYTYPEID, 0) AS COMPANYTYPEID, DBO.CRM_OPPORTUNITYCONTACT.EXPECTEDREVENUE, 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE, DBO.CRM_OPPORTUNITYCONTACT.SALESPERSON, ISNULL(DBO.BASE_COMPANIES.INVOICINGSTATE, '') AS NATION, 
                      ISNULL(DBO.BASE_COMPANIES.INVOICINGCITY, '') AS CITY, ISNULL(DBO.BASE_COMPANIES.INVOICINGSTATEPROVINCE, '') AS PROVINCE, 
                      DBO.CRM_OPPORTUNITYCONTACT.INCOMEPROBABILITY, DBO.CRM_OPPORTUNITYCONTACT.AMOUNTCLOSED
FROM         DBO.CRM_OPPORTUNITYCONTACT LEFT OUTER JOIN
                      DBO.BASE_COMPANIES ON DBO.CRM_OPPORTUNITYCONTACT.CONTACTID = DBO.BASE_COMPANIES.ID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE = 0 LEFT OUTER JOIN
                      DBO.COMPANYTYPE COMPANYTYPE ON DBO.BASE_COMPANIES.COMPANYTYPEID = COMPANYTYPE.K_ID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO






/****** OGGETTO: VISTA DBO.DASH1_LEADINDUSTRY_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.43.00 ******/

CREATE VIEW DBO.DASH1_LEADINDUSTRY_VIEW
AS
SELECT     DBO.CRM_OPPORTUNITYCONTACT.OPPORTUNITYID, DBO.COMPANYTYPE.DESCRIPTION AS LEADINDUSTRY, ISNULL(DBO.CRM_CROSSLEAD.INDUSTRY, 0) AS INDUSTRY, 
                      DBO.CRM_CROSSLEAD.LEADID, DBO.CRM_OPPORTUNITYCONTACT.EXPECTEDREVENUE, DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE, 
                      DBO.CRM_OPPORTUNITYCONTACT.SALESPERSON, ISNULL(DBO.CRM_LEADS.STATE, '') AS NATION, ISNULL(DBO.CRM_LEADS.CITY, '') AS CITY, 
                      ISNULL(DBO.CRM_LEADS.PROVINCE, '') AS PROVINCE, DBO.CRM_OPPORTUNITYCONTACT.INCOMEPROBABILITY, DBO.CRM_OPPORTUNITYCONTACT.AMOUNTCLOSED
FROM         DBO.CRM_OPPORTUNITYCONTACT LEFT OUTER JOIN
                      DBO.CRM_CROSSLEAD ON DBO.CRM_OPPORTUNITYCONTACT.CONTACTID = DBO.CRM_CROSSLEAD.LEADID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE = 1 LEFT OUTER JOIN
                      DBO.COMPANYTYPE ON DBO.CRM_CROSSLEAD.INDUSTRY = DBO.COMPANYTYPE.K_ID LEFT OUTER JOIN
                      DBO.CRM_LEADS ON DBO.CRM_CROSSLEAD.LEADID = DBO.CRM_LEADS.ID




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO






/****** OGGETTO: VISTA DBO.OPPORTUNITYCOMPANY_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.43.00 ******/

CREATE VIEW DBO.OPPORTUNITYCOMPANY_VIEW
AS
SELECT     DBO.CRM_OPPORTUNITYCONTACT.CONTACTID, DBO.CRM_OPPORTUNITYCONTACT.NOTE,
                      DBO.CRM_OPPORTUNITYCONTACT.EXPECTEDREVENUE, DBO.CRM_OPPORTUNITYCONTACT.INCOMEPROBABILITY, DBO.CRM_OPPORTUNITYCONTACT.AMOUNTCLOSED, 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE, DBO.CRM_CROSSOPPORTUNITY.TABLETYPEID AS STATUSID, CRM_CROSSOPPORTUNITY_1.TABLETYPEID AS RATINGID, 
                      CRM_CROSSOPPORTUNITY_2.TABLETYPEID AS PROBABILITYID, DBO.CRM_OPPORTUNITYCONTACT.OPPORTUNITYID, DBO.CRM_OPPORTUNITYCONTACT.STARTDATE, 
                      DBO.CRM_OPPORTUNITYCONTACT.ESTIMATEDCLOSEDATE, DBO.CRM_OPPORTUNITYCONTACT.ENDDATE, DBO.CRM_OPPORTUNITYCONTACT.SALESPERSON, 
                      DBO.BASE_COMPANIES.COMPANYNAME, DBO.BASE_COMPANIES.PHONE, DBO.BASE_COMPANIES.FAX, DBO.BASE_COMPANIES.EMAIL, 
                      DBO.BASE_COMPANIES.INVOICINGADDRESS, DBO.BASE_COMPANIES.INVOICINGCITY, DBO.BASE_COMPANIES.INVOICINGSTATEPROVINCE, 
                      DBO.BASE_COMPANIES.INVOICINGSTATE, DBO.BASE_COMPANIES.INVOICINGZIPCODE, DBO.CRM_OPPORTUNITYCONTACT.LOSTREASON
FROM         DBO.CRM_OPPORTUNITYCONTACT LEFT OUTER JOIN
                      DBO.CRM_CROSSOPPORTUNITY ON DBO.CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = DBO.CRM_CROSSOPPORTUNITY.OPPORTUNITYID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTID = DBO.CRM_CROSSOPPORTUNITY.CONTACTID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE = DBO.CRM_CROSSOPPORTUNITY.CONTACTTYPE LEFT OUTER JOIN
                      DBO.CRM_CROSSOPPORTUNITY CRM_CROSSOPPORTUNITY_1 ON DBO.CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = CRM_CROSSOPPORTUNITY_1.OPPORTUNITYID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTID = CRM_CROSSOPPORTUNITY_1.CONTACTID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE = CRM_CROSSOPPORTUNITY_1.CONTACTTYPE LEFT OUTER JOIN
                      DBO.CRM_CROSSOPPORTUNITY CRM_CROSSOPPORTUNITY_2 ON DBO.CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = CRM_CROSSOPPORTUNITY_2.OPPORTUNITYID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTID = CRM_CROSSOPPORTUNITY_2.CONTACTID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE = CRM_CROSSOPPORTUNITY_2.CONTACTTYPE LEFT OUTER JOIN
                      DBO.BASE_COMPANIES ON DBO.CRM_OPPORTUNITYCONTACT.CONTACTID = DBO.BASE_COMPANIES.ID
WHERE     (DBO.CRM_CROSSOPPORTUNITY.TYPE = 1) AND (CRM_CROSSOPPORTUNITY_1.TYPE = 2) AND (CRM_CROSSOPPORTUNITY_2.TYPE = 3) AND 
                      (DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE = 0) OR
                      (DBO.CRM_CROSSOPPORTUNITY.TYPE IS NULL) AND (CRM_CROSSOPPORTUNITY_1.TYPE IS NULL) AND (CRM_CROSSOPPORTUNITY_2.TYPE IS NULL)


GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





/****** OGGETTO: VISTA DBO.OPPORTUNITYCOMPETITOR_VIEW    DATA DELLO SCRIPT: 30/08/2005 17.43.01 ******/

CREATE VIEW DBO.OPPORTUNITYCOMPETITOR_VIEW
AS
SELECT     DBO.BASE_COMPANIES.COMPANYNAME, DBO.BASE_COMPANIES.DESCRIPTION, DBO.CRM_OPPORTUNITYCOMPETITOR.ID, 
                      DBO.CRM_OPPORTUNITYCOMPETITOR.OPPORTUNITYID, DBO.CRM_OPPORTUNITYCOMPETITOR.COMPETITORID, DBO.CRM_OPPORTUNITYCOMPETITOR.STRENGTHS, 
                      DBO.CRM_OPPORTUNITYCOMPETITOR.WEAKNESSES, DBO.CRM_OPPORTUNITYCOMPETITOR.NOTE, DBO.BASE_COMPANIES.EVALUATION
FROM         DBO.CRM_OPPORTUNITYCOMPETITOR LEFT OUTER JOIN
                      DBO.BASE_COMPANIES ON DBO.CRM_OPPORTUNITYCOMPETITOR.COMPETITORID = DBO.BASE_COMPANIES.ID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

CREATE VIEW OPPORTUNITYLEAD_VIEW
AS
SELECT     DBO.CRM_OPPORTUNITYCONTACT.CONTACTID AS CONTACTID_L, DBO.CRM_OPPORTUNITYCONTACT.NOTE AS NOTE_L, 
                      DBO.CRM_OPPORTUNITYCONTACT.EXPECTEDREVENUE AS EXPECTEDREVENUE_L, DBO.CRM_OPPORTUNITYCONTACT.INCOMEPROBABILITY AS INCOMEPROBABILITY_L, 
                      DBO.CRM_OPPORTUNITYCONTACT.AMOUNTCLOSED AS AMOUNTCLOSED_L, DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE AS CONTACTTYPE_L, 
                      DBO.CRM_CROSSOPPORTUNITY.TABLETYPEID AS STATUSID_L, CRM_CROSSOPPORTUNITY_1.TABLETYPEID AS RATINGID_L, 
                      CRM_CROSSOPPORTUNITY_2.TABLETYPEID AS PROBABILITYID_L, DBO.CRM_OPPORTUNITYCONTACT.OPPORTUNITYID AS OPPORTUNITYID_L, 
                      DBO.CRM_OPPORTUNITYCONTACT.STARTDATE AS STARTDATE_L, DBO.CRM_OPPORTUNITYCONTACT.ESTIMATEDCLOSEDATE AS ESTIMATEDCLOSEDATE_L, 
                      DBO.CRM_OPPORTUNITYCONTACT.ENDDATE AS ENDDATE_L, DBO.CRM_OPPORTUNITYCONTACT.SALESPERSON AS SALESPERSON_L, 
                      ISNULL(DBO.CRM_LEADS.COMPANYNAME, '') + ' ' + ISNULL(DBO.CRM_LEADS.Name, '') + ' ' + ISNULL(DBO.CRM_LEADS.SURNAME, '') AS LEADNAME_L, 
                      DBO.CRM_LEADS.ADDRESS AS ADDRESS_L, DBO.CRM_LEADS.CITY AS CITY_L, DBO.CRM_LEADS.PROVINCE AS PROVINCE_L, 
                      DBO.CRM_LEADS.ZIPCODE AS ZIPCODE_L, DBO.CRM_LEADS.STATE AS STATE_L, DBO.CRM_LEADS.VATID AS VATID_L, 
                      DBO.CRM_LEADS.TAXIDENTIFICATIONNUMBER AS TAXIDENTIFICATIONNUMBER_L, DBO.CRM_LEADS.PHONE AS PHONE_L, DBO.CRM_LEADS.EMAIL AS EMAIL_L, 
                      DBO.CRM_LEADS.FAX AS FAX_L, DBO.CRM_LEADS.MOBILEPHONE AS MOBILEPHONE_L, DBO.CRM_LEADS.NOTES AS NOTES_L, 
                      DBO.CRM_LEADS.BIRTHPLACE AS BIRTHPLACE_L, DBO.CRM_LEADS.BIRTHDAY AS BIRTHDAY_L, DBO.CRM_OPPORTUNITYCONTACT.LOSTREASON
FROM         DBO.CRM_OPPORTUNITYCONTACT LEFT OUTER JOIN
                      DBO.CRM_CROSSOPPORTUNITY CRM_CROSSOPPORTUNITY_1 ON DBO.CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = CRM_CROSSOPPORTUNITY_1.OPPORTUNITYID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTID = CRM_CROSSOPPORTUNITY_1.CONTACTID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE = CRM_CROSSOPPORTUNITY_1.CONTACTTYPE LEFT OUTER JOIN
                      DBO.CRM_CROSSOPPORTUNITY CRM_CROSSOPPORTUNITY_2 ON DBO.CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = CRM_CROSSOPPORTUNITY_2.OPPORTUNITYID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTID = CRM_CROSSOPPORTUNITY_2.CONTACTID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE = CRM_CROSSOPPORTUNITY_2.CONTACTTYPE LEFT OUTER JOIN
                      DBO.CRM_CROSSOPPORTUNITY ON DBO.CRM_OPPORTUNITYCONTACT.OPPORTUNITYID = DBO.CRM_CROSSOPPORTUNITY.OPPORTUNITYID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTID = DBO.CRM_CROSSOPPORTUNITY.CONTACTID AND 
                      DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE = DBO.CRM_CROSSOPPORTUNITY.CONTACTTYPE LEFT OUTER JOIN
                      DBO.CRM_LEADS ON DBO.CRM_OPPORTUNITYCONTACT.CONTACTID = DBO.CRM_LEADS.ID
WHERE     (DBO.CRM_OPPORTUNITYCONTACT.CONTACTTYPE = 1) AND (DBO.CRM_CROSSOPPORTUNITY.TYPE = 1) AND (CRM_CROSSOPPORTUNITY_1.TYPE = 2) AND 
                      (CRM_CROSSOPPORTUNITY_2.TYPE = 3) OR
                      (DBO.CRM_CROSSOPPORTUNITY.TYPE IS NULL) AND (CRM_CROSSOPPORTUNITY_1.TYPE IS NULL) AND (CRM_CROSSOPPORTUNITY_2.TYPE IS NULL)

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO





/****** OGGETTO: VISTA DBO.RANK_VIEW    DATA DELLO SCRIPT: 13/12/2005 17.42.58 ******/
CREATE VIEW DBO.RANK_VIEW
AS
SELECT     SUM(DBO.SCOREDESCRIPTION.WEIGHT * DBO.SCOREVALUES.SCOREVALUE / DBO.SCOREVALUES.VOTENUMBER) + 100 AS RANK, DBO.SCOREVALUES.IDCROSS, 
                      DBO.SCOREVALUES.TYPE
FROM         DBO.SCOREVALUES RIGHT OUTER JOIN
                      DBO.SCOREDESCRIPTION ON DBO.SCOREVALUES.IDDESCRIPTION = DBO.SCOREDESCRIPTION.ID
GROUP BY DBO.SCOREVALUES.IDCROSS, DBO.SCOREVALUES.TYPE




GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.APPOINTMENTCALENDAR    SCRIPT DATE: 03/04/2006 16.24.37 ******/
CREATE PROCEDURE APPOINTMENTCALENDAR
(
    @MONTH  INT,
    @YEAR  INT,
    @OWNERID INT,	
    @LTZ INT,
    @DAYS VARCHAR(1000)  OUTPUT
)
AS

SELECT  @DAYS=  COALESCE(@DAYS + '|', '') + CAST(DAY(DATEADD(MI,@LTZ,STARTDATE)) AS VARCHAR(2))
FROM BASE_CALENDAR 
WHERE UID=@OWNERID AND MONTH(DATEADD(MI,@LTZ,STARTDATE))=@MONTH AND YEAR(DATEADD(MI,@LTZ,STARTDATE))=@YEAR 
GROUP BY STARTDATE
ORDER BY STARTDATE

SELECT @DAYS='|'+@DAYS+'|'



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.APPOINTMENTDETAIL    SCRIPT DATE: 03/04/2006 16.24.37 ******/
CREATE PROCEDURE APPOINTMENTDETAIL
(
    @MONTH  INT,
    @YEAR  INT,
    @DAY  INT,
    @OWNERID INT,	
    @LTZ INT
)
AS

SELECT CONVERT(VARCHAR(5), DATEADD(MI,@LTZ,STARTDATE),108) AS STARTHOUR, 
	CONVERT(VARCHAR(5), DATEADD(MI,@LTZ,ENDDATE),108) AS ENDHOUR,
	CONTACT
FROM BASE_CALENDAR 
WHERE UID=@OWNERID AND 
	MONTH(DATEADD(MI,@LTZ,STARTDATE))=@MONTH AND 
	YEAR(DATEADD(MI,@LTZ,STARTDATE))=@YEAR AND 
	DAY(DATEADD(MI,@LTZ,STARTDATE))=@DAY

ORDER BY STARTDATE



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.CHKGRPPERMISSION    SCRIPT DATE: 03/04/2006 16.24.37 ******/
CREATE PROCEDURE CHKGRPPERMISSION 
(
/*    @TABLEID  INT,
    @OWNERID BIGINT,
*/
    @REFID  BIGINT
)
AS
SELECT DISTINCT TOP 1 (DBO.GROUPDEPENDENCY.GROUPIDFATHER) AS GIDS
FROM DBO.GROUPDEPENDENCY INNER JOIN 
DBO.GROUPCROSS ON DBO.GROUPDEPENDENCY.GROUPIDSON = DBO.GROUPCROSS.IDGROUP
WHERE DBO.GROUPCROSS.IDREF = @REFID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OGGETTO: STORED PROCEDURE DBO.CHOBJOWNER    DATA DELLO SCRIPT: 30/08/2005 17.43.02 ******/
CREATE PROC CHOBJOWNER ( @USRNAME VARCHAR(20), @NEWUSRNAME VARCHAR(50))
AS
-- @USRNAME IS THE CURRENT USER
-- @NEWUSRNAME IS THE NEW USER
SET NOCOUNT ON
DECLARE @UID INT                   -- UID OF THE USER
DECLARE @OBJNAME VARCHAR(50)       -- OBJECT NAME OWNED BY USER
DECLARE @CURROBJNAME VARCHAR(50)   -- CHECKS FOR EXISTING OBJECT OWNED BY NEW USER 
DECLARE @OUTSTR VARCHAR(256)       -- SQL COMMAND WITH 'SP_CHANGEOBJECTOWNER'
SET @UID = USER_ID(@USRNAME)
DECLARE CHOBJOWNERCUR CURSOR STATIC
FOR
SELECT NAME FROM SYSOBJECTS WHERE UID = @UID
OPEN CHOBJOWNERCUR
IF @@CURSOR_ROWS = 0
BEGIN
  PRINT 'ERROR: NO OBJECTS OWNED BY ' + @USRNAME
  CLOSE CHOBJOWNERCUR
  DEALLOCATE CHOBJOWNERCUR
  RETURN 1
END
FETCH NEXT FROM CHOBJOWNERCUR INTO @OBJNAME
WHILE @@FETCH_STATUS = 0
BEGIN
  SET @CURROBJNAME = @NEWUSRNAME + "." + @OBJNAME
  IF (OBJECT_ID(@CURROBJNAME) > 0)
    PRINT 'WARNING *** ' + @CURROBJNAME + ' ALREADY EXISTS ***'
  SET @OUTSTR = "SP_CHANGEOBJECTOWNER '" + @USRNAME + "." + @OBJNAME + "','" + @NEWUSRNAME + "'"
  PRINT @OUTSTR
  PRINT 'GO'
  FETCH NEXT FROM CHOBJOWNERCUR INTO @OBJNAME
END
CLOSE CHOBJOWNERCUR
DEALLOCATE CHOBJOWNERCUR
SET NOCOUNT OFF
RETURN 0



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

create PROCEDURE CLONEQUOTE
(
@id bigint,
@userid bigint

)
as

declare @newid bigint

INSERT INTO QUOTES
	SELECT [Description], [Currency], [Change], GETDATE(), @userid,  [ExpirationDate], [ActivityID], [Reduction], [Stage], [Number], [PaymentID], [OwnerID], [CrossID], [CrossType], [Address], [ShipAddress], [City], [Province], [Nation], [ZIPCode], [GrandTotal], [Groups], 'Copy of: '+[Subject], [Subtotal], [TaxTotal], [Ship], [ManagerId], [Signaler], [ShipVat], [IncludeProdPdf], [QuoteDate], [ShipDate], GETDATE(), @userid, [ShipId], [List] FROM [Tustena].[dbo].[Quotes]
where ID = @id;select @newid=@@identity

INSERT INTO QUOTEROWS
	SELECT @newid, [CatalogID], [Qta], [Uprice], [NewUprice], [Description], [Description2], [Reduction], [Tax], [Cost], [UnitMeasure], [ListPrice], [ProductCode],[REALLISTPRICE] FROM [Tustena].[dbo].[QuoteRows]
where ESTIMATEID = @id

select @newid

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.COMPANY_USED_SIZE    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE COMPANY_USED_SIZE 
AS
DROP TABLE ##SIZETMP
SELECT * INTO ##SIZETMP FROM ACCOUNT 
EXEC SP_SPACEUSED ##SIZETMP
DROP TABLE ##SIZETMP



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.DAYCALENDAR    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE DAYCALENDAR
(
    @MONTH  INT,
    @YEAR  INT,
    @OWNERID INT,	
    @DAYS VARCHAR(1000)  OUTPUT
)
AS

SELECT  @DAYS=  COALESCE(@DAYS + '|', '') + CAST(DAY(STARTDATE) AS VARCHAR(2))
FROM BASE_CALENDAR
WHERE UID=@OWNERID AND MONTH(STARTDATE)=@MONTH AND YEAR(STARTDATE)=@YEAR
ORDER BY STARTDATE

SELECT  @DAYS=  COALESCE(@DAYS + '|', '') + CAST(DAY(STARTDATE) AS VARCHAR(2))
FROM BASE_EVENTS
WHERE UID=@OWNERID AND MONTH(STARTDATE)=@MONTH AND YEAR(STARTDATE)=@YEAR
ORDER BY STARTDATE

SELECT @DAYS='|'+@DAYS+'|'



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.GETCHILD    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE GETCHILD (@ID INT) AS
SET NOCOUNT ON
DECLARE @CHILDID INT
DECLARE @CHILDID2 INT
DECLARE @SUBJECT  VARCHAR(500)
DECLARE @SUBJECT2  VARCHAR(500)
DECLARE @TEMP TABLE(CHILDID INT, ID INT, SUBJECT VARCHAR(500))
SELECT TOP 1  @CHILDID = ID, @SUBJECT=SUBJECT  FROM CRM_WORKACTIVITY WHERE PARENTID = @ID

IF @@ROWCOUNT > 0
BEGIN
	SELECT TOP 1  @CHILDID = ID, @SUBJECT=SUBJECT  FROM CRM_WORKACTIVITY WHERE PARENTID = @ID
	WHILE @@ROWCOUNT > 0
		BEGIN
			SET @SUBJECT2 = @SUBJECT
			SET @CHILDID2 = @CHILDID
			INSERT @TEMP (CHILDID, ID,SUBJECT) SELECT  TOP 1 ID,PARENTID,@SUBJECT2  FROM CRM_WORKACTIVITY WHERE PARENTID = @CHILDID2
			SELECT  TOP 1 @CHILDID=ID, @SUBJECT=SUBJECT FROM CRM_WORKACTIVITY WHERE PARENTID = @CHILDID2 AND (SELECT COUNT(ID) FROM @TEMP WHERE ID=@CHILDID2) = 0
		END
		INSERT @TEMP (CHILDID, ID,SUBJECT) VALUES (0,@CHILDID,@SUBJECT )	
END

 SELECT ID,SUBJECT FROM @TEMP



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.GETCOMPANYMAIL    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE GETCOMPANYMAIL
(
   @MAILADDRESS     VARCHAR(500)
)
AS

SELECT 
 'A'+CAST(ID AS VARCHAR(10)) AS ID,
	REPLACE(REPLACE(REPLACE(ISNULL(COMPANYNAME,''),CHAR(13),''),CHAR(9),''),CHAR(10),'') + ' ('+EMAIL+')' AS EMAIL
 FROM BASE_COMPANIES
 WHERE LIMBO=0
AND EMAIL LIKE '%'+@MAILADDRESS +'%'



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO



/****** OGGETTO: STORED PROCEDURE DBO.GETCOMPANYPHONE    DATA DELLO SCRIPT: 30/08/2005 17.43.07 ******/
CREATE PROCEDURE GETCOMPANYPHONE (
    @PHONENUMBER   VARCHAR(500)
)
AS
SELECT 
 'A'+CAST(ID AS VARCHAR(10)) AS ID,
REPLACE(REPLACE(REPLACE(ISNULL(COMPANYNAME,''),CHAR(13),''),CHAR(9),''),CHAR(10),'') + CHAR(10) + INVOICINGADDRESS + ' ' + INVOICINGZIPCODE + ' ' + INVOICINGCITY + ' ' + INVOICINGSTATEPROVINCE + ' ' + INVOICINGSTATE AS PHONENUMBER
 FROM BASE_COMPANIES
 WHERE LIMBO=0 AND PHONE LIKE '%'+@PHONENUMBER +'%'



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.GETCONTACTMAIL    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE GETCONTACTMAIL
(    
    @MAILADDRESS     VARCHAR(500)
)
AS

SELECT 
 'C'+CAST(ID AS VARCHAR(10)) AS ID,
	REPLACE(REPLACE(REPLACE(ISNULL(SURNAME,'')+' '+ISNULL(NAME,''),CHAR(13),''),CHAR(9),''),CHAR(10),'') + ' ('+EMAIL+')' AS EMAIL
 FROM BASE_CONTACTS
 WHERE LIMBO=0
AND EMAIL LIKE '%'+@MAILADDRESS +'%'



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.GETLEADMAIL    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE GETLEADMAIL
(    
    @MAILADDRESS     VARCHAR(500)
)
AS

SELECT 
 'L'+CAST(ID AS VARCHAR(10)) AS ID,
	REPLACE(REPLACE(REPLACE(ISNULL(SURNAME,'')+' '+ISNULL(NAME,'')+'-'+ISNULL(COMPANYNAME,''),CHAR(13),''),CHAR(9),''),CHAR(10),'') + ' ('+EMAIL+')' AS EMAIL
 FROM CRM_LEADS
 WHERE LIMBO=0
AND EMAIL LIKE '%'+@MAILADDRESS +'%'



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.GETPARENT    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE GETPARENT (@ID INT) AS
SET NOCOUNT ON
DECLARE @PARENTID INT
DECLARE @ORD INT
SET @ORD=0
DECLARE @TEMP TABLE(PARENTID INT, ID INT,ORD INT, SUBJECT VARCHAR(500))
SELECT @PARENTID = PARENTID FROM CRM_WORKACTIVITY WHERE ID = @ID
WHILE @@ROWCOUNT > 0
BEGIN
SET @ORD=@ORD+1
	INSERT @TEMP (PARENTID, ID, ORD, SUBJECT)
		SELECT PARENTID, ID, @ORD, SUBJECT FROM CRM_WORKACTIVITY WHERE ID = @PARENTID
		SELECT @PARENTID=PARENTID FROM CRM_WORKACTIVITY WHERE ID = @PARENTID AND ID <> @ID
END
	 SELECT ID,SUBJECT FROM @TEMP ORDER BY ORD DESC



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.GETVISIBLECONTACT    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE GETVISIBLECONTACT 
(
@PGROUPS VARCHAR(8000), 
@POWNERID INT
)
AS
BEGIN
/* DROP TABLE #TCONTACTS */
CREATE TABLE #TCONTACTS
(
OCOMPANYID INT,
OCOMPANYNAME VARCHAR(100),
OTITLE VARCHAR(6),
ONAME VARCHAR(100),
OSURNAME VARCHAR(100),
OPHONE_1 VARCHAR(100),
OPHONE_2 VARCHAR(100),
OFAX VARCHAR(100),
OEMAIL VARCHAR(100),
OWEBSITE VARCHAR(250),
OGROUPS VARCHAR(1000),
OADDRESS_1 VARCHAR(250),
OADDRESS_2 VARCHAR(250),
OCITY_1 VARCHAR(250),
OCITY_2 VARCHAR(250),
OPROVINCE_1 VARCHAR(250),
OPROVINCE_2 VARCHAR(250),
OZIPCODE_1 VARCHAR(10),
OZIPCODE_2 VARCHAR(10),
OSTATE_1 VARCHAR(250),
OSTATE_2 VARCHAR(250),
OMOBILEPHONE_1 VARCHAR(250),
OMOBILEPHONE_2 VARCHAR(250),
OLIMBO BIT,
OVATID VARCHAR(250),
OTAXIDENTIFICATIONNUMBER VARCHAR(250),
OBUSINESSROLE VARCHAR(250),
OOWNERID INT,
ONOTES VARCHAR(1000),
OCREATEDDATE DATETIME,
OCREATEDBYID INT,
OLASTMODIFIEDDATE DATETIME,
OLASTMODIFIEDBYID INT,
OMLEMAIL  VARCHAR(50),
OSEX BIT,
OBIRTHDAY DATETIME
)
INSERT INTO #TCONTACTS 
(
OCOMPANYID,
OCOMPANYNAME,
OTITLE,
ONAME,
OSURNAME,
OVATID,
OTAXIDENTIFICATIONNUMBER,
OPHONE_1,
OMOBILEPHONE_1,
OMOBILEPHONE_2,
OBUSINESSROLE,
OFAX,
OEMAIL,
OSEX,
OBIRTHDAY,
OWEBSITE,
OGROUPS,
OADDRESS_1,
OCITY_1,
OPROVINCE_1,
OSTATE_1,
OZIPCODE_1,
OADDRESS_2,
OCITY_2,
OPROVINCE_2,
OSTATE_2,
OZIPCODE_2,
OPHONE_2,
ONOTES,
OOWNERID,
OLIMBO,
OCREATEDDATE,
OCREATEDBYID,
OLASTMODIFIEDDATE,
OLASTMODIFIEDBYID,
OMLEMAIL
)
SELECT 
  												
ID AS OCOMPANYID,                        
COMPANYNAME AS OCOMPANYNAME,                      
'' AS OTITLE,
'' AS ONAME,
'' AS OSURNAME,
'' AS OVATID,
'' AS OTAXIDENTIFICATIONNUMBER,                     
PHONE AS OPHONE_1,
'' AS OMOBILEPHONE_1,    
'' AS OMOBILEPHONE_2,
'' AS OBUSINESSROLE,
FAX AS OFAX,                              
EMAIL AS OEMAIL,    
'' AS OSEX,     
'' AS OBIRTHDAY,     
WEBSITE AS OWEBSITE,                          
GROUPS AS OGROUPS,                           
INVOICINGADDRESS AS OADDRESS_1,                 
INVOICINGCITY AS OCITY_1,                    
INVOICINGSTATEPROVINCE AS OPROVINCE_1,           
INVOICINGSTATE AS OSTATE_1,                   
INVOICINGZIPCODE AS OZIPCODE_1,                 
SHIPMENTADDRESS AS OADDRESS_2,                  
SHIPMENTCITY AS OCITY_2,                     
SHIPMENTSTATEPROVINCE AS OPROVINCE_2,            
SHIPMENTSTATE AS OSTATE_2,                    
SHIPMENTZIPCODE AS OZIPCODE_2,                  
SHIPMENTPHONE AS OPHONE_2,                    
DESCRIPTION AS ONOTES,                      
OWNERID AS OOWNERID,                          
LIMBO AS OLIMBO,
CREATEDDATE AS OCREATEDDATE,
CREATEDBYID AS OCREATEDBYID,
LASTMODIFIEDDATE AS OLASTMODIFIEDDATE,
LASTMODIFIEDBYID AS OLASTMODIFIEDBYID,
MLEMAIL AS OMLEMAIL

FROM CRM_COMPANIES WHERE 
(
	(LIMBO=0) 
AND
	( 
		(GROUPS LIKE '%' + @PGROUPS + '%')
	OR 
		(OWNERID=@POWNERID)
	)
)

SELECT * FROM #TCONTACTS
END



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.LOADVIEWSTATEMGR    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE LOADVIEWSTATEMGR (
    @SESSIONID  VARCHAR(55)
) AS
SELECT VIEWSTATE
	FROM VIEWSTATEMANAGER
	WHERE SESSIONID = @SESSIONID



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.NEW_REMINDERCALENDAR    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE NEW_REMINDERCALENDAR
(
    @MONTH  INT,
    @YEAR  INT,
    @OWNERID INT,	
    @LTZ INT,
    @DAYS VARCHAR(1000)  OUTPUT
)
AS

SELECT  @DAYS=  COALESCE(@DAYS + '|', '') + CAST(DAY(DATEADD(MI,@LTZ,REMINDERDATE)) AS VARCHAR(2))
FROM CRM_REMINDER 
WHERE OWNERID=@OWNERID AND MONTH(DATEADD(MI,@LTZ,REMINDERDATE))=@MONTH AND YEAR(DATEADD(MI,@LTZ,REMINDERDATE))=@YEAR 
GROUP BY REMINDERDATE
ORDER BY REMINDERDATE

SELECT @DAYS='|'+@DAYS+'|'



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

create PROCEDURE QUOTETOORDER
(
@id bigint,
@userid bigint

)
as

declare @newid bigint

INSERT INTO [Orders]
	SELECT [Description], [Currency], [Change], GETDATE(), @userid,  GETDATE(), [ActivityID], [Reduction], [Stage], [Number], [PaymentID], [OwnerID], [CrossID], [CrossType], [Address], [ShipAddress], [City], [Province], [Nation], [ZIPCode], [GrandTotal], [Groups], [Subject], [Subtotal], [TaxTotal], [Ship], [ManagerId], [Signaler], [ShipVat], [IncludeProdPdf], GETDATE(), null, GETDATE(), @userid ,@id, [ShipId], [List]
	FROM [Tustena].[dbo].[Quotes]
	where ID=@id;select @newid=@@identity

INSERT INTO [Tustena].[dbo].[OrderRows]([OrderID], [CatalogID], [Qta], [Uprice], [NewUprice], [Description], [Description2], [Reduction], [Tax], [Cost], [UnitMeasure], [ListPrice], [ProductCode],[REALLISTPRICE])
	SELECT @newid, [CatalogID], [Qta], [Uprice], [NewUprice], [Description], [Description2], [Reduction], [Tax], [Cost], [UnitMeasure], [ListPrice], [ProductCode] ,[REALLISTPRICE]
	FROM [Tustena].[dbo].[QuoteRows]
	where ESTIMATEID=@id

select @newid

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.REMINDERCALENDAR    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE REMINDERCALENDAR
(
    @MONTH  INT,
    @YEAR  INT,
    @OWNERID INT,	
    @LTZ INT,
    @DAYS VARCHAR(1000)  OUTPUT
)
AS

SELECT  @DAYS=  COALESCE(@DAYS + '|', '') + CAST(DAY(DATEADD(MI,@LTZ,REMINDERDATE)) AS VARCHAR(2))
FROM CRM_REMINDER 
WHERE OWNERID=@OWNERID AND MONTH(DATEADD(MI,@LTZ,REMINDERDATE))=@MONTH AND YEAR(DATEADD(MI,@LTZ,REMINDERDATE))=@YEAR 
ORDER BY REMINDERDATE

SELECT @DAYS='|'+@DAYS+'|'



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.STOPTESTING    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE STOPTESTING AS

UPDATE EWORK_CUSTOMER SET ACTIVE=0
WHERE TESTING=1 AND DATEDIFF ( D , CREATEDDATE , GETDATE() ) >TESTINGDAYS



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE UPDATEMENU as

DECLARE @AdminID bigint
DECLARE @MenuID int
DECLARE @Exist int
DECLARE cur CURSOR LOCAL FOR 
	select admingroupid from Tustena_Data 
	/*where testing=0 or (testing=1 and dateadd(dd,testingdays,createddate)>getdate()) order by id*/

open cur
 	FETCH NEXT FROM cur INTO @AdminID	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		DECLARE curmenu CURSOR LOCAL FOR
			select id from newmenu where active=1 order by parentmenu,id
				open curmenu
				 	FETCH NEXT FROM curmenu INTO @MenuID
					WHILE @@FETCH_STATUS = 0
					BEGIN
						select @Exist=count(*) from companymenu where  menuid= @MenuID
						if(@Exist<1)
							BEGIN 
								insert into companymenu (menuid,accessgroup) values (@MenuID,'|'+cast(@AdminID as varchar)+'|')								
							END
					FETCH NEXT FROM curmenu INTO @MenuID
					END
		close curmenu
		deallocate curmenu
		
		FETCH NEXT FROM cur INTO  @AdminID	
	END
close cur
DEALLOCATE cur

GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO



/****** OBJECT:  STORED PROCEDURE DBO.VIEWSTATEMGR    SCRIPT DATE: 03/04/2006 16.24.38 ******/
CREATE PROCEDURE VIEWSTATEMGR (
    @SESSIONID  VARCHAR(55),
    @USERID  INT,
    @PAGE  VARCHAR(50),
    @VIEWSTATE  TEXT
) AS
IF EXISTS (SELECT *
	FROM VIEWSTATEMANAGER
	WHERE SESSIONID = @SESSIONID AND PAGE = @PAGE)
	UPDATE VIEWSTATEMANAGER SET LASTACCESS=GETDATE(), VIEWSTATE=@VIEWSTATE WHERE SESSIONID = @SESSIONID AND PAGE = @PAGE
ELSE
	INSERT INTO VIEWSTATEMANAGER (PAGE,USERID, SESSIONID, VIEWSTATE,LASTACCESS) VALUES(@PAGE,@USERID,@SESSIONID,@VIEWSTATE,GETDATE())

DELETE FROM VIEWSTATEMANAGER WHERE LASTACCESS<GETDATE()-0.05



GO
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


COMMIT
go