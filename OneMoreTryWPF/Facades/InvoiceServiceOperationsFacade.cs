using OneMoreTryWPF.InvoiceService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace OneMoreTryWPF.Facades
{
	static class InvoiceServiceOperationsFacade
	{
		static private InvoiceServiceClient serviceClient;

		static private InvoiceServiceClient getServiceClient()
		{
			if (serviceClient == null)
			{
				string EndpointAddressString = ConfigManagerFacade.getInvoiceService_EndpointAddress();
				EndpointAddress invoiceServiceEndpointAddress = new EndpointAddress(EndpointAddressString);

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

				serviceClient = new InvoiceServiceClient(customBinding, invoiceServiceEndpointAddress);

			/*BasicHttpsBinding basicHttpBinding = new BasicHttpsBinding();
			basicHttpBinding.MaxReceivedMessageSize = ConfigManagerFacade.getInvoiceService_MaxReceivedMessageSize();
			basicHttpBinding.MaxBufferSize = ConfigManagerFacade.getInvoiceService_MaxBufferSize();
			basicHttpBinding.MaxBufferPoolSize = ConfigManagerFacade.getInvoiceService_MaxBufferPoolSize();
			basicHttpBinding.AllowCookies = ConfigManagerFacade.getInvoiceService_AllowCookies();*/
			}

			return serviceClient;
		}

		static public bool EnterpriseValidation()
		{
			SessionServiceOperationsFacade.StartSession();
			EnterpriseValidationRequest enterpriseValidationRequest = new EnterpriseValidationRequest();
			enterpriseValidationRequest.sessionId = SessionDataManagerFacade.getSessionId();

			EnterpriseKey enterpriseKey = new EnterpriseKey();
			enterpriseKey.tin = SessionDataManagerFacade.getUserTin();
			enterpriseKey.certificateNum = SessionDataManagerFacade.getCertificateNum();
			enterpriseKey.certificateSeries = SessionDataManagerFacade.getCertificateSeries();
			EnterpriseKey[] enterpriseKeyList = { enterpriseKey };
			enterpriseValidationRequest.enterpriseKeyList = enterpriseKeyList;

			EnterpriseValidationResponse enterpriseValidationResponse;
			try
			{
				enterpriseValidationResponse = getServiceClient().enterpriseValidation(enterpriseValidationRequest);
				switch (enterpriseValidationResponse.resultList[0].resultType)
				{
					case EnterpriseValidationResultType.SUCCESS:
						return true;
					case EnterpriseValidationResultType.TIN_ABSENT:
					case EnterpriseValidationResultType.CERTIFICATE_SERIES_OR_CERTIFICATE_NUM_ABSENT:
					case EnterpriseValidationResultType.BIK_ABSENT:
					case EnterpriseValidationResultType.BANK_NOT_FOUND:
					case EnterpriseValidationResultType.IIK_ABSENT:
					default:
						return false;
				}
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		static public bool QueryInvoiceById(out QueryInvoiceResponse queryInvoiceResponse)
		{
			SessionServiceOperationsFacade.StartSession();
			InvoiceByIdRequest invoiceByIdRequest = new InvoiceByIdRequest();
			invoiceByIdRequest.sessionId = SessionDataManagerFacade.getSessionId();
			invoiceByIdRequest.idList = SessionDataManagerFacade.getInvoiceIdList();

			//QueryInvoiceResponse queryInvoiceResponse;
			try
			{
				queryInvoiceResponse = getServiceClient().queryInvoiceById(invoiceByIdRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				queryInvoiceResponse = null;
				return false;
			}
		}

		static public bool QueryInvoiceSummaryById()
		{
			SessionServiceOperationsFacade.StartSession();
			InvoiceByIdRequest invoiceSummaryByIdRequest = new InvoiceByIdRequest();
			invoiceSummaryByIdRequest.sessionId = SessionDataManagerFacade.getSessionId();
			invoiceSummaryByIdRequest.idList = SessionDataManagerFacade.getInvoiceIdList();

			InvoiceSummaryResponse queryInvoiceSummaryResponse;
			try
			{
				queryInvoiceSummaryResponse = getServiceClient().queryInvoiceSummaryById(invoiceSummaryByIdRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		static public bool QueryInvoiceByKey()
		{
			SessionServiceOperationsFacade.StartSession();
			InvoiceByKeyRequest invoiceByKeyRequest = new InvoiceByKeyRequest();
			invoiceByKeyRequest.sessionId = SessionDataManagerFacade.getSessionId();
			invoiceByKeyRequest.invoiceKeyList = SessionDataManagerFacade.getinvoiceKeyList();
			invoiceByKeyRequest.direction = SessionDataManagerFacade.getDirection();

			QueryInvoiceResponse queryInvoiceByKeyResponse;
			try
			{
				queryInvoiceByKeyResponse = getServiceClient().queryInvoiceByKey(invoiceByKeyRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		static public bool QueryUpdates(out QueryInvoiceUpdateResponse queryInvoiceUpdateResponse)
		{
			SessionServiceOperationsFacade.StartSession();
			QueryInvoiceUpdateRequest queryInvoiceUpdateRequest = new QueryInvoiceUpdateRequest();
			queryInvoiceUpdateRequest.sessionId = SessionDataManagerFacade.getSessionId();
			queryInvoiceUpdateRequest.lastEventDate = SessionDataManagerFacade.getlastEventDate();
			queryInvoiceUpdateRequest.lastInvoiceId = SessionDataManagerFacade.getlastInvoiceId();
			queryInvoiceUpdateRequest.direction = SessionDataManagerFacade.getDirection();
			queryInvoiceUpdateRequest.limit = SessionDataManagerFacade.getlimit();
			queryInvoiceUpdateRequest.fullInfoOnStatusChange = SessionDataManagerFacade.getfullInfoOnStatusChange();

			//QueryInvoiceUpdateResponse queryInvoiceUpdateResponse;
			try
			{
				queryInvoiceUpdateResponse = getServiceClient().queryUpdates(queryInvoiceUpdateRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				queryInvoiceUpdateResponse = null;
				return false;
			}
		}

		static public bool QueryInvoiceSummaryByKey()
		{
			SessionServiceOperationsFacade.StartSession();
			InvoiceByKeyRequest invoiceSummaryByKeyRequest = new InvoiceByKeyRequest();
			invoiceSummaryByKeyRequest.sessionId = SessionDataManagerFacade.getSessionId();
			invoiceSummaryByKeyRequest.invoiceKeyList = SessionDataManagerFacade.getinvoiceKeyList();
			invoiceSummaryByKeyRequest.direction = SessionDataManagerFacade.getDirection();

			InvoiceSummaryResponse invoiceSummaryByKeyResponse;
			try
			{
				invoiceSummaryByKeyResponse = getServiceClient().queryInvoiceSummaryByKey(invoiceSummaryByKeyRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		static public bool QueryInvoice(out QueryInvoiceResponse queryInvoiceResponse)
		{
			SessionServiceOperationsFacade.StartSession();
			QueryInvoiceRequest queryInvoiceRequest = new QueryInvoiceRequest();
			queryInvoiceRequest.sessionId = SessionDataManagerFacade.getSessionId();
			queryInvoiceRequest.criteria = SessionDataManagerFacade.getQueryInvoiceCriteria();
			try
			{
				queryInvoiceResponse = getServiceClient().queryInvoice(queryInvoiceRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				queryInvoiceResponse = null;
				return true;
			}
		}

		static public bool RevokeInvoiceById()
		{
			SessionServiceOperationsFacade.StartSession();
			InvoiceByIdWithReasonRequest invoiceByIdWithReasonRequest = new InvoiceByIdWithReasonRequest();
			invoiceByIdWithReasonRequest.sessionId = SessionDataManagerFacade.getSessionId();
			invoiceByIdWithReasonRequest.signature = SessionDataManagerFacade.getInvoiceSignatureIdWithReason();
			invoiceByIdWithReasonRequest.x509Certificate = SessionDataManagerFacade.getX509SignCertificate();
			invoiceByIdWithReasonRequest.idWithReasonList = SessionDataManagerFacade.getInvoiceIdWithReasonsList_InvoiceService();

			TryChangeStatusResponse tryChangeStatusResponse;
			try
			{
				tryChangeStatusResponse = getServiceClient().revokeInvoiceById(invoiceByIdWithReasonRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		static public bool declineInvoiceById()
		{
			SessionServiceOperationsFacade.StartSession();
			InvoiceByIdWithReasonRequest invoiceByIdWithReasonRequest = new InvoiceByIdWithReasonRequest();
			invoiceByIdWithReasonRequest.sessionId = SessionDataManagerFacade.getSessionId();
			invoiceByIdWithReasonRequest.signature = SessionDataManagerFacade.getInvoiceSignatureIdWithReason();
			invoiceByIdWithReasonRequest.x509Certificate = SessionDataManagerFacade.getX509SignCertificate();
			invoiceByIdWithReasonRequest.idWithReasonList = SessionDataManagerFacade.getInvoiceIdWithReasonsList_InvoiceService();

			TryChangeStatusResponse tryChangeStatusResponse;
			try
			{
				tryChangeStatusResponse = getServiceClient().declineInvoiceById(invoiceByIdWithReasonRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		static public bool unrevokeInvoiceById()
		{
			SessionServiceOperationsFacade.StartSession();
			InvoiceByIdWithReasonRequest invoiceByIdWithReasonRequest = new InvoiceByIdWithReasonRequest();
			invoiceByIdWithReasonRequest.sessionId = SessionDataManagerFacade.getSessionId();
			invoiceByIdWithReasonRequest.signature = SessionDataManagerFacade.getInvoiceSignatureIdWithReason();
			invoiceByIdWithReasonRequest.x509Certificate = SessionDataManagerFacade.getX509SignCertificate();
			invoiceByIdWithReasonRequest.idWithReasonList = SessionDataManagerFacade.getInvoiceIdWithReasonsList_InvoiceService();

			TryChangeStatusResponse tryChangeStatusResponse;
			try
			{
				tryChangeStatusResponse = getServiceClient().unrevokeInvoiceById(invoiceByIdWithReasonRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		static public bool confirmInvoiceById()
		{
			SessionServiceOperationsFacade.StartSession();
			InvoiceByIdRequest invoiceByIdRequest = new InvoiceByIdRequest();
			invoiceByIdRequest.sessionId = SessionDataManagerFacade.getSessionId();
			invoiceByIdRequest.idList = SessionDataManagerFacade.getInvoiceIdList();

			InvoiceSummaryResponse invoiceSummaryResponse;
			try
			{
				invoiceSummaryResponse = getServiceClient().confirmInvoiceById(invoiceByIdRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		static public bool QueryInvoiceErrorById()
		{
			SessionServiceOperationsFacade.StartSession();
			InvoiceErrorByIdRequest invoiceErrorByIdRequest = new InvoiceErrorByIdRequest();
			invoiceErrorByIdRequest.sessionId = SessionDataManagerFacade.getSessionId();
			invoiceErrorByIdRequest.idList = SessionDataManagerFacade.getInvoiceIdList();

			InvoiceErrorByIdResponse invoiceErrorByIdResponse;
			try
			{
				invoiceErrorByIdResponse = getServiceClient().queryInvoiceErrorById(invoiceErrorByIdRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}

		static public bool queryInvoiceHistoryById()
		{
			SessionServiceOperationsFacade.StartSession();
			QueryInvoiceHistoryByIdRequest queryInvoiceHistoryByIdRequest = new QueryInvoiceHistoryByIdRequest();
			queryInvoiceHistoryByIdRequest.idList = SessionDataManagerFacade.getInvoiceIdList();
			queryInvoiceHistoryByIdRequest.sessionId = SessionDataManagerFacade.getSessionId();

			QueryInvoiceHistoryByIdResponse queryInvoiceHistoryByIdResponse;
			try
			{
				queryInvoiceHistoryByIdResponse = getServiceClient().queryInvoiceHistoryById(queryInvoiceHistoryByIdRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}


		static public bool DeleteInvoiceById()
		{
			SessionServiceOperationsFacade.StartSession();
			DeleteInvoiceByIdRequest deleteInvoiceByIdRequest = new DeleteInvoiceByIdRequest();
			deleteInvoiceByIdRequest.idList = SessionDataManagerFacade.getInvoiceIdList();
			deleteInvoiceByIdRequest.sessionId = SessionDataManagerFacade.getSessionId();
			deleteInvoiceByIdRequest.signature = SessionDataManagerFacade.getInvoiceSignatureId();
			deleteInvoiceByIdRequest.x509Certificate = SessionDataManagerFacade.getX509SignCertificate();

			DeleteInvoiceByIdResponse deleteInvoiceByIdResponse;
			try
			{
				deleteInvoiceByIdResponse = getServiceClient().deleteInvoiceById(deleteInvoiceByIdRequest);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
		}
	}
}

