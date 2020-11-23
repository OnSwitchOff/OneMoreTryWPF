using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Facades
{
	static class ConfigManagerFacade
	{
		private static string InvoiceService_EndpointAddress = "https://test3.esf.kgd.gov.kz:8443/esf-web/ws/api1/InvoiceService?wsdl";
		private static string SessionService_EndpointAddress = "https://test3.esf.kgd.gov.kz:8443/esf-web/ws/api1/SessionService?wsdl";
		private static string UploadService_EndpointAddress = "https://test3.esf.kgd.gov.kz:8443/esf-web/ws/api1/UploadInvoiceService?wsdl";
		private static string LocalService_EndpointAddress = "http://localhost:6666/LocalService";

		internal static string getUploadService_EndpointAddress()
		{
			return UploadService_EndpointAddress;
		}

		private static long InvoiceService_MaxReceivedMessageSize = 20000000;

		internal static string getLocalService_EndpointAddress()
		{
			return LocalService_EndpointAddress;
		}

		private static int InvoiceService_MaxBufferSize = 20000000;
		private static bool IsSessionServiceConfigChanged = false;
		private static bool IsInvoiceServiceConfigChanged = false;
		private static string ESFVersion = "InvoiceV2";
		private static long InvoiceService_MaxBufferPoolSize = 20000000;
		private static int InvoiceService_readerQuotasMaxArrayLength = 20000000;
		private static int InvoiceService_readerQuotasMaxStringContentLength = 20000000;
		private static bool InvoiceService_AllowCookies = true;
		private static int InvoiceService_readerQuotasMaxDepth = 32;
		private static int SessionService_MaxReceivedMessageSize = 20000000;

		internal static string getInvoiceService_EndpointAddress()
		{
			return InvoiceService_EndpointAddress;
		}

		internal static long getInvoiceService_MaxReceivedMessageSize()
		{
			return InvoiceService_MaxReceivedMessageSize;
		}

		internal static bool isSessionServiceConfigChanged()
		{
			return IsSessionServiceConfigChanged;
		}

		internal static int getInvoiceService_MaxBufferSize()
		{
			return InvoiceService_MaxBufferSize;
		}

		internal static bool isInvoiceServiceConfigChanged()
		{
			return IsInvoiceServiceConfigChanged;
		}

		internal static string getESFVersion()
		{
			return ESFVersion;
		}

		internal static long getInvoiceService_MaxBufferPoolSize()
		{
			return InvoiceService_MaxBufferPoolSize;
		}

		internal static int getInvoiceService_readerQuotasMaxArrayLength()
		{
			return InvoiceService_readerQuotasMaxArrayLength;
		}

		internal static int getInvoiceService_readerQuotasMaxStringContentLength()
		{
			return InvoiceService_readerQuotasMaxStringContentLength;
		}

		internal static bool getInvoiceService_AllowCookies()
		{
			return InvoiceService_AllowCookies;
		}

		internal static int getInvoiceService_readerQuotasMaxDepth()
		{
			return InvoiceService_readerQuotasMaxDepth;
		}


		internal static long getSessionService_MaxReceivedMessageSize()
		{
			return SessionService_MaxReceivedMessageSize;
		}
		
		internal static string getSessionService_EndpointAddress()
		{
			return SessionService_EndpointAddress;
		}
	}
}
