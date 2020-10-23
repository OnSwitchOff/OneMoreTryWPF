using OneMoreTryWPF.ENUMs;
using OneMoreTryWPF.InvoiceService;
using OneMoreTryWPF.Models;
using OneMoreTryWPF.SessionService;
using OneMoreTryWPF.UploadInvoiceService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Facades
{
	static class SessionDataManagerFacade
	{
		internal static ObservableCollection<ProductV2> GetRandomProducts()
		{
			ObservableCollection<ProductV2> list = new ObservableCollection<ProductV2>();
			for (int i = 1; i <= 10; i++)
			{
				ProductV2 p = new ProductV2();
				p.additional = "additional" + i;
				p.catalogTruId = "catalogTruId" + i;
				p.description = "description" + i;
				p.exciseAmount = i * 100;
				p.exciseRate = 10;
				p.kpvedCode = "kpvedcode";
				p.ndsAmount = i * 110;
				p.ndsRate = 10;
				p.priceWithoutTax = i * 1000;
				p.priceWithTax = 1.1f * 1.1f * i * 1000;
				p.productDeclaration = "productDecl" + i;
				p.productNumberInDeclaration = "productNumberInDec" + i;
				p.quantity = "1";
				p.rowNumber = i;
				p.tnvedName = "tnvedName";
				p.truOriginCode = (i - 1) % 6 + 1;
				p.turnoverSize = i * 1000;
				p.unitCode = "unitCOde" + i;
				p.unitNomenclature = "UnitNimenclature" + i;
				p.unitPrice = 1000;
				list.Add(p);
			}
			return list;
		}

		internal static bool setInvoiceSignature(LocalService.SignatureResponse signatureResponse)
		{
			throw new NotImplementedException();
		}

		internal static string[] getInvoiceBodies()
		{
			throw new NotImplementedException();
		}

		internal static LocalService.InvoiceIdWithReason[] getInvoiceIdWithReasonsList_LocalService()
		{
			throw new NotImplementedException();
		}

		internal static string getSignCertificatePin()
		{
			throw new NotImplementedException();
		}

		internal static bool setInvoiceSignatureIdWithReason(LocalService.ListSignatureResponse listSignatureResponse)
		{
			throw new NotImplementedException();
		}

		internal static string getSignCertificatePath()
		{
			throw new NotImplementedException();
		}

		internal static bool setInvoiceId(SyncInvoiceResponse syncInvoiceResponse)
		{
			throw new NotImplementedException();
		}

		internal static invoiceUploadInfo[] getInvoiceUploadInfoList()
		{
			throw new NotImplementedException();
		}

		internal static ObservableCollection<UserStatus> GetRandomSellerStatuses()
		{
			ObservableCollection<UserStatus> statuses = new ObservableCollection<UserStatus>();

			foreach (SellerType status in (SellerType[])Enum.GetValues(typeof(SellerType)))
			{
				UserStatus tmp = new UserStatus();
				tmp.type = status;
				tmp.isChecked = false;
				statuses.Add(tmp);
			}
			return statuses;
		}

		internal static bool setInvoiceSignatureId(LocalService.ListSignatureResponse listSignatureResponse)
		{
			throw new NotImplementedException();
		}

		internal static ObservableCollection<UserStatus> GetRandomCustomerStatuses()
		{
			ObservableCollection<UserStatus> statuses = new ObservableCollection<UserStatus>();

			foreach (CustomerType status in (CustomerType[])Enum.GetValues(typeof(CustomerType)))
			{
				UserStatus tmp = new UserStatus();
				tmp.type = status;
				tmp.isChecked = false;
				statuses.Add(tmp);
			}
			return statuses;
		}

		internal static string getPassword()
		{
			throw new NotImplementedException();
		}

		internal static string getUserName()
		{
			throw new NotImplementedException();
		}

		internal static string GetNewInvoiceNum()
		{
			return "666";
		}

		internal static string GetOperatorFullName()
		{
			return "Operator Viktotorovich";
		}

		internal static DateTime GetTurnoverDate()
		{
			return DateTime.Now;
		}

		internal static ObservableCollection<CustomerV2> GetCustomers()
		{
			ObservableCollection<CustomerV2> customers = new ObservableCollection<CustomerV2>();
			CustomerV2 customer = new CustomerV2();
			customers.Add(customer);
			return customers;
		}

		internal static string getX509AuthCertificate()
		{
			throw new NotImplementedException();
		}

		internal static ProductSetV2 GetProductSet()
		{
			ProductSetV2 set = new ProductSetV2();
			set.products = SessionDataManagerFacade.GetRandomProducts();
			return set;
		}

		internal static void setSessionId(string sessionId)
		{
			throw new NotImplementedException();
		}

		internal static string getSessionId()
		{
			throw new NotImplementedException();
		}

		internal static string getSellerTin()
		{
			throw new NotImplementedException();
		}

		internal static string getCertificateNum()
		{
			throw new NotImplementedException();
		}

		internal static string getCertificateSeries()
		{
			throw new NotImplementedException();
		}

		internal static ObservableCollection<SellerV2> GetSellers()
		{
			ObservableCollection <SellerV2> sellers = new ObservableCollection<SellerV2>();
			SellerV2 seller = new SellerV2();
			sellers.Add(seller);
			return sellers;
		}

		internal static void clearSessionData()
		{
			throw new NotImplementedException();
		}

		internal static long[] getInvoiceIdList()
		{
			throw new NotImplementedException();
		}

		internal static InvoiceKey[] getinvoiceKeyList()
		{
			throw new NotImplementedException();
		}

		internal static InvoiceDirection getDirection()
		{
			throw new NotImplementedException();
		}

		internal static DateTime getlastEventDate()
		{
			throw new NotImplementedException();
		}

		internal static long getlastInvoiceId()
		{
			throw new NotImplementedException();
		}

		internal static int getlimit()
		{
			throw new NotImplementedException();
		}

		internal static bool getfullInfoOnStatusChange()
		{
			throw new NotImplementedException();
		}

		internal static QueryInvoiceCriteria getQueryInvoiceCriteria()
		{
			throw new NotImplementedException();
		}

		internal static string getInvoiceSignatureIdWithReason()
		{
			throw new NotImplementedException();
		}

		internal static bool isEmptySessionId()
		{
			throw new NotImplementedException();
		}

		internal static string getX509SignCertificate()
		{
			throw new NotImplementedException();
		}

		internal static InvoiceIdWithReason[] getInvoiceIdWithReasonsList_InvoiceService()
		{
			throw new NotImplementedException();
		}

		internal static string getInvoiceSignatureId()
		{
			throw new NotImplementedException();
		}

		internal static void setCurrentUserData(User user)
		{
			throw new NotImplementedException();
		}

		internal static void setCurrentUserProfilesData(profileInfo[] profileInfoList)
		{
			throw new NotImplementedException();
		}
	}
}
