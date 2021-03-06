USE BILLING
GO
DROP TABLE MST_INVOICE_SERIES
DROP TABLE MST_ITEM
DROP TABLE MST_MENU
DROP TABLE MST_PARTY
DROP TABLE MST_PAYMENT_TYPE
DROP TABLE MST_SETUP
DROP TABLE MST_TAX
DROP TABLE MST_USER
DROP TABLE MST_USER_RIGHTS
DROP TABLE TRN_INVOICE_DETAIL
DROP TABLE TRN_INVOICE_HEADER
DROP TABLE TRN_INVOICE_TAX
DROP TABLE MST_BANK
DROP TABLE MST_COMPANY
DROP TABLE MST_FAYEAR

CREATE TABLE [dbo].[TRN_INVOICE_TAX](
	[INVOICE_TAX_ID] [int] IDENTITY(1,1),
	[INVOICE_HEADER_ID] [int] NULL,
	[TAX_ID] [int] NULL,
	[TAX_RATE] [money] NULL,
	[TAX_AMOUNT] [money] NULL
)
CREATE TABLE [dbo].[TRN_INVOICE_HEADER](
	[INVOICE_HEADER_ID] [int] IDENTITY(1,1),
	[BRANCH_ID] [int] NULL,
	[FAYEAR_ID] [int] NULL,
	[COMPANY_ID] [int] NULL,
	[PARTY_ID] [int] NULL,
	[INVOICE_SERIES_ID] [int] NULL,
	[INVOICE_NO] [nvarchar](50) NULL,
	[INVOICE_DATE] [datetime] NULL,
	[TRAN_TYPE] [nvarchar](4) NULL,
	[SUB_TYPE] [nvarchar](10) NULL,
	[CHALLAN_REF_NO] [nvarchar](50) NULL,
	[ORDER_NO] [nvarchar](50) NULL,
	[TOTAL_AMOUNT] [money] NULL,
	[DISCOUNT_RATE] [money] NULL,
	[DISCOUNT_AMOUNT] [money] NULL,
	[AMOUNT_WITH_DISCOUNT] [money] NULL,
	[TAX_TOTAL_AMOUNT] [money] NULL,
	[AMOUNT_WITH_TAX] [money] NULL,
	[ADDITIONAL_COST_TEXT] [nvarchar](50) NULL,
	[ADDITIONAL_COST] [money] NULL,
	[AMOUNT_WITH_ADDITIONAL_COST] [money] NULL,
	[ROUNDED_OFF] [money] NULL,
	[NET_AMOUNT] [money] NULL,
	[NET_AMOUNT_INWORDS] [nvarchar](200) NULL,
	[REMARKS] [nvarchar](200) NULL,
	[USER_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[DISCOUNT_TEXT] [nvarchar](200) NULL,
	[PAYMENT_TYPE_ID] [int] NULL,
	[BANK_ID] [int] NULL,
	[REFERENCE_NO] [nvarchar](20) NULL,
	[ACCOUNT_ENTRY_DATE] [datetime] NULL,
	[BANK_STATEMENT_DATE] [datetime] NULL,
	[CANCELLATION_FLAG] [int] NULL
)

CREATE TABLE [dbo].[TRN_INVOICE_DETAIL](
	[INVOICE_DETAIL_ID] [int]  IDENTITY(1,1),
	[INVOICE_HEADER_ID] [int] NULL,
	[ITEM_ID] [int] NULL,
	[QUANTITY] [int] NULL,
	[RATE] [money] NULL,
	[AMOUNT] [money] NULL,
	[REMARKS] [nvarchar](200) NULL
)

CREATE TABLE [dbo].[MST_USER_RIGHTS](
	[USER_ID] [int] NULL,
	[MENU_ID] [int] NULL
)

CREATE TABLE [dbo].[MST_USER](
	[USER_ID] [int]  IDENTITY(1,1),
	[BRANCH_ID] [int] NULL,
	[USER_CODE] [nvarchar](20) NULL,
	[DISPLAY_NAME] [nvarchar](50) NULL,
	[LOGIN_ID] [nvarchar](50) NULL,
	[USER_PASSWORD] [nvarchar](50) NULL,
	[USER_CATEGORY] [int] NULL
)

CREATE TABLE [dbo].[MST_TAX](
	[TAX_ID] [int]  IDENTITY(1,1),
	[BRANCH_ID] [int] NULL,
	[TAX_DESC] [nvarchar](50) NULL,
	[TAX_RATE] [money] NULL,
	[USER_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL
)

CREATE TABLE [dbo].[MST_SETUP](
	[BRANCH_ID] [int] NULL,
	[BRANCH_CODE] [nvarchar](50) NULL,
	[BRANCH_NAME] [nvarchar](50) NULL,
	[COMPANY_NAME] [nvarchar](50) NULL,
	[ADDRESS] [nvarchar](200) NULL,
	[CITY] [nvarchar](50) NULL,
	[PIN] [nvarchar](50) NULL,
	[CONTACT_NO] [nvarchar](50) NULL,
	[EMAIL_ID] [nvarchar](50) NULL,
	[WEB_SITE] [nvarchar](50) NULL,
	[START_DATE] [datetime] NULL,
	[LOCK_DATE] [datetime] NULL,
	[SAVE_CONFIRMATION_MSG] [int] NULL
)

CREATE TABLE [dbo].[MST_PAYMENT_TYPE](
	[PAYMENT_TYPE_ID] [int] NULL,
	[PAYMENT_TYPE_DESCRIPTION] [nvarchar](30) NULL,
	[PAYMENT_TYPE_GROUP] [int] NULL
) 

CREATE TABLE [dbo].[MST_PARTY](
	[PARTY_ID] [int]  IDENTITY(1,1),
	[BRANCH_ID] [int] NULL,
	[PARTY_NAME] [nvarchar](100) NULL,
	[ADDRESS1] [nvarchar](50) NULL,
	[ADDRESS2] [nvarchar](50) NULL,
	[ADDRESS3] [nvarchar](50) NULL,
	[CITY] [nvarchar](50) NULL,
	[PIN] [nvarchar](50) NULL,
	[CONTACT_PERSON] [nvarchar](50) NULL,
	[MOBILE_NO] [nvarchar](50) NULL,
	[PHONE_NO] [nvarchar](50) NULL,
	[FAX] [nvarchar](50) NULL,
	[EMAIL_ID] [nvarchar](50) NULL,
	[USER_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL
)

CREATE TABLE [dbo].[MST_MENU](
	[MENU_ID] [int] NULL,
	[MENU_GROUP_CODE] [nvarchar](50) NULL,
	[MENU_GROUP_NAME] [nvarchar](50) NULL,
	[MENU_SLNO] [nvarchar](50) NULL,
	[MENU_NAME] [nvarchar](50) NULL,
	[MENU_DESC] [nvarchar](50) NULL,
	[MENU_SUB_DESC_1] [nvarchar](50) NULL,
	[MENU_VISIBILITY] [int] NULL
) 

CREATE TABLE [dbo].[MST_ITEM](
	[ITEM_ID] [int]  IDENTITY(1,1),
	[BRANCH_ID] [int] NULL,
	[COMPANY_ID] [int] NULL,
	[ITEM_NAME] [nvarchar](50) NULL,
	[RATE] [money] NULL,
	[UNIT] [nvarchar](50) NULL,
	[USER_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL
) 

CREATE TABLE [dbo].[MST_INVOICE_SERIES](
	[INVOICE_SERIES_ID] [int]  IDENTITY(1,1),
	[BRANCH_ID] [int] NULL,
	[COMPANY_ID] [int] NULL,
	[PREFIX] [nvarchar](50) NULL,
	[START_NO] [int] NULL,
	[LAST_NO] [int] NULL,
	[LAST_DATE] [datetime] NULL,
	[HEADER_DISPLAY_TEXT] [nvarchar](50) NULL,
	[USER_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL
) 

CREATE TABLE [dbo].[MST_FAYEAR](
	[FAYEAR_ID] [int] NULL,
	[FA_BEG_DATE] [datetime] NULL,
	[FA_END_DATE] [datetime] NULL,
	[FA_LOCK_DATE] [datetime] NULL
) 

CREATE TABLE [dbo].[MST_COMPANY](
	[COMPANY_ID] [int]  IDENTITY(1,1),
	[BRANCH_ID] [int] NULL,
	[COMPANY_NAME] [nvarchar](50) NULL,
	[ADDRESS1] [nvarchar](50) NULL,
	[ADDRESS2] [nvarchar](50) NULL,
	[ADDRESS3] [nvarchar](50) NULL,
	[CITY] [nvarchar](50) NULL,
	[PIN] [nvarchar](50) NULL,
	[CONTACT_NO] [nvarchar](50) NULL,
	[FAX] [nvarchar](50) NULL,
	[EMAIL_ID] [nvarchar](50) NULL,
	[WEB_SITE] [nvarchar](50) NULL,
	[CONTACT_PERSON] [nvarchar](50) NULL,
	[SIGNATORY] [nvarchar](50) NULL,
	[VAT_NO] [nvarchar](50) NULL,
	[CST_NO] [nvarchar](50) NULL,
	[SERVICE_TAX_NO] [nvarchar](50) NULL,
	[PAN] [nvarchar](50) NULL,
	[USER_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL
) 

CREATE TABLE [dbo].[MST_BANK](
	[BANK_ID] [int] NULL,
	[BANK_NAME] [nvarchar](10) NULL
)