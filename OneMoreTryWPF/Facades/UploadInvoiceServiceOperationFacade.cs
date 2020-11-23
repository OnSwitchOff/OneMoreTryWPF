using OneMoreTryWPF.UploadInvoiceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Windows;
using System.Xml;

namespace OneMoreTryWPF.Facades
{
	static class UploadInvoiceServiceOperationFacade
	{
		static private UploadInvoiceServiceClient serviceClient;

		static private UploadInvoiceServiceClient getServiceClient()
		{
			if (serviceClient == null)
			{
				string EndpointAddressString = ConfigManagerFacade.getUploadService_EndpointAddress();
				EndpointAddress endpointAdress = new EndpointAddress(EndpointAddressString);

				CustomBinding customBinding = new CustomBinding();

				HttpsTransportBindingElement transport = new HttpsTransportBindingElement();
				transport.MaxReceivedMessageSize = ConfigManagerFacade.getInvoiceService_MaxReceivedMessageSize();
				transport.MaxBufferSize = ConfigManagerFacade.getInvoiceService_MaxBufferSize();
				transport.MaxBufferPoolSize = ConfigManagerFacade.getInvoiceService_MaxBufferPoolSize();
				transport.AllowCookies = ConfigManagerFacade.getInvoiceService_AllowCookies();

				TextMessageEncodingBindingElement encoding = new TextMessageEncodingBindingElement();
				XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();
				readerQuotas.MaxArrayLength = ConfigManagerFacade.getInvoiceService_readerQuotasMaxArrayLength();
				readerQuotas.MaxStringContentLength = ConfigManagerFacade.getInvoiceService_readerQuotasMaxStringContentLength();
				readerQuotas.MaxDepth = ConfigManagerFacade.getInvoiceService_readerQuotasMaxDepth();
				encoding.ReaderQuotas = readerQuotas;
				encoding.MessageVersion = MessageVersion.Soap11;

				customBinding.Elements.Add(encoding);
				customBinding.Elements.Add(transport);
				serviceClient = new UploadInvoiceServiceClient(customBinding, endpointAdress);
			}
			return serviceClient;
		}

		static internal bool SendInvoice()
		{
			SessionServiceOperationsFacade.StartSession();
			SyncInvoiceRequest syncInvoiceRequest = new SyncInvoiceRequest();
			syncInvoiceRequest.sessionId = SessionDataManagerFacade.getSessionId();
			syncInvoiceRequest.x509Certificate = SessionDataManagerFacade.getX509SignCertificate();
			syncInvoiceRequest.invoiceUploadInfoList = SessionDataManagerFacade.getInvoiceUploadInfoList();

			//SyncInvoiceResponse syncInvoiceResponse;
			object syncInvoiceResponse;
			try
			{
				syncInvoiceResponse = getServiceClient().syncInvoice(syncInvoiceRequest);
				return SessionDataManagerFacade.setCurrentInvoiceId((SyncInvoiceResponse)syncInvoiceResponse);
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}
	}
}
