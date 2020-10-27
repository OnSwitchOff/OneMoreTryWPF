using OneMoreTryWPF.UploadInvoiceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace OneMoreTryWPF.Facades
{
	static class UploadInvoiceServiceOperationFacade
	{
		static private UploadInvoiceServiceClient serviceClient;

		static private UploadInvoiceServiceClient getServiceClient()
		{
			if (serviceClient == null)
			{
				serviceClient = new UploadInvoiceServiceClient();
			}
			return serviceClient;
		}

		static internal bool SendInvoice()
		{
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
