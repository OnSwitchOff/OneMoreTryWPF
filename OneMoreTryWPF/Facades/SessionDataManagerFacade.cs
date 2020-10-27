using OneMoreTryWPF.ENUMs;
using OneMoreTryWPF.InvoiceService;
using OneMoreTryWPF.LocalService;
using OneMoreTryWPF.Models;
using OneMoreTryWPF.SessionService;
using OneMoreTryWPF.UploadInvoiceService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Text;
using System.Windows;

namespace OneMoreTryWPF.Facades
{
	static class SessionDataManagerFacade
	{
		private static string sessionId;

		private static string userTin = "760816300415";
		private static string userPassword = "Micr0!nvest";
		private static string userAuthCertPath = @"C:\Users\viktor.kassov\source\repos\ESF_kz\ESF_kz\bin\Debug\Сертификат\ИП Пинчук ВВ до 17.06.21\ИП Пинчук ВВ до 17.06.21\AUTH_RSA256_12fc440f2049f1b5b61765114f28e58ec67eccff.p12";
		private static string userAuthCertPin = "Aa123456";
		private static string userSignCertPath = @"C:\Users\viktor.kassov\source\repos\ESF_kz\ESF_kz\bin\Debug\Сертификат\ИП Пинчук ВВ до 17.06.21\ИП Пинчук ВВ до 17.06.21\RSA256_af8e6f8be023a8cc035198522a70ca7203a7059a.p12";
		private static string userSignCertPin = "Aa123456";
		private static User currentUser;
		private static profileInfo[] profileInfoList;

		private static InvoiceV2 currentInvoice;
		private static long currentInvoiceId;
		private static string currentReason;

		private static string invoiceSignature;
		private static string invoiceSignatureId;
		private static string invoiceSignatureIdWithReason;

		private static InvoiceDirection invoiceDirection;
		private static DateTime lastEventDate;
		private static long lastInvoiceId;
		private static int limit;

		

		private static bool fullInfoOnStatusChange;
		private static bool asc;
		private static DateTime dateTo;
		private static DateTime dateFrom;

		private static long[] selectedIdList;


		//QueryInvoice parameters
		private static QueryInvoiceCriteria queryInvoiceCriteria;

		internal static void setCriteria(Criteria criteria)
		{
			queryInvoiceCriteria = new QueryInvoiceCriteria();
			queryInvoiceCriteria.dateTo = criteria.dateTo;
			queryInvoiceCriteria.dateFrom = criteria.dateFrom;
			queryInvoiceCriteria.direction = criteria.direction;

			List<InvoiceStatus> invoiceStatuses = new List<InvoiceStatus>();

			if (criteria.isCreated)
			{
				invoiceStatuses.Add(InvoiceStatus.CREATED);
			}
			if(criteria.isDelivered)
			{
				invoiceStatuses.Add(InvoiceStatus.DELIVERED);
			}
			if (criteria.isCanceled)
			{
				invoiceStatuses.Add(InvoiceStatus.CREATED);
			}
			if (criteria.isRevoked)
			{
				invoiceStatuses.Add(InvoiceStatus.REVOKED);
			}
			if (criteria.isImported)
			{
				invoiceStatuses.Add(InvoiceStatus.IMPORTED);
			}
			if (criteria.isFailed)
			{
				invoiceStatuses.Add(InvoiceStatus.FAILED);
			}
			if (criteria.isDeleted)
			{
				invoiceStatuses.Add(InvoiceStatus.DELETED);
			}
			if (criteria.isDeclined)
			{
				invoiceStatuses.Add(InvoiceStatus.DECLINED);
			}
			if (criteria.isDraft)
			{
				invoiceStatuses.Add(InvoiceStatus.DRAFT);
			}
			if (criteria.isCanceledByOGD)
			{
				invoiceStatuses.Add(InvoiceStatus.CANCELED_BY_OGD);
			}

			queryInvoiceCriteria.invoiceStatusList = invoiceStatuses.ToArray();

			queryInvoiceCriteria.invoiceType = criteria.isOrdinary ? InvoiceService.InvoiceType.ORDINARY_INVOICE : criteria.isFixed ? InvoiceService.InvoiceType.FIXED_INVOICE : InvoiceService.InvoiceType.ADDITIONAL_INVOICE;
		}



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

		internal static bool setInvoiceSignature(SignatureResponse signatureResponse)
		{
			invoiceSignature = signatureResponse.invoiceHashList[0].signature;
			return true;
		}

		internal static string[] getInvoiceBodies()
		{
			string[] invoiceBodies = { getInvoiceBodyString()};
			return invoiceBodies;
		}

		private static string getInvoiceBodyString()
		{
			return ParsingManager.getInvoiceBodyString(getCurrentInvoice());
		}

		private static InvoiceV2 getCurrentInvoice()
		{
			return currentInvoice;
		}

		private static void setCurrentInvoice(InvoiceV2 invoice)
		{
			currentInvoice = invoice;
		}

		internal static LocalService.InvoiceIdWithReason[] getInvoiceIdWithReasonsList_LocalService()
		{
			LocalService.InvoiceIdWithReason invoiceIdWithReason = new LocalService.InvoiceIdWithReason();
			invoiceIdWithReason.id = getInvoiceId();
			invoiceIdWithReason.reason = getReason();
			LocalService.InvoiceIdWithReason[] invoiceIdWithReasonsList = { invoiceIdWithReason };
			return invoiceIdWithReasonsList;
		}

		private static string getReason()
		{
			return currentReason;
		}

		internal static bool setReason(string reason)
		{
			currentReason = reason;
			return true;
		}

		private static long getInvoiceId()
		{
			return currentInvoiceId;
		}

		internal static string getSignCertificatePin()
		{
			return userSignCertPin;
		}

		internal static bool setInvoiceSignatureIdWithReason(ListSignatureResponse listSignatureResponse)
		{
			invoiceSignatureIdWithReason = listSignatureResponse.signature;
			return true;
		}

		internal static string getSignCertificatePath()
		{
			return userSignCertPath;
		}

		internal static bool setCurrentInvoiceId(long id)
		{
			currentInvoiceId = id;
			return true;
		}

		internal static bool setCurrentInvoiceId(SyncInvoiceResponse syncInvoiceResponse)
		{
			if (syncInvoiceResponse.acceptedSet.Length > 0)
			{
				currentInvoiceId = syncInvoiceResponse.acceptedSet[0].id;
				return true;
			}
			else
			{
				string str = "";
				for (int i = 0; i < syncInvoiceResponse.declinedSet.Length; i++)
				{
					for (int j = 0; j < syncInvoiceResponse.declinedSet[i].errors.Length; j++)
					{
						str += syncInvoiceResponse.declinedSet[i].errors[j].text + "\n";
					}
					str += "\n";
				}
				MessageBox.Show(str);
				return false;
			}
		}

		internal static invoiceUploadInfo[] getInvoiceUploadInfoList()
		{
			invoiceUploadInfo InvoiceUploadInfo = new invoiceUploadInfo();
			InvoiceUploadInfo.invoiceBody = getInvoiceBodyString();
			InvoiceUploadInfo.version = ConfigManagerFacade.getESFVersion();
			InvoiceUploadInfo.signature = getInvoiceSignature();
			InvoiceUploadInfo.signatureType = getSignatureType();
			invoiceUploadInfo[] invoiceUploadInfoList = { InvoiceUploadInfo };
			return invoiceUploadInfoList;
		}

		private static UploadInvoiceService.SignatureType getSignatureType()
		{
			return UploadInvoiceService.SignatureType.COMPANY;
		}

		private static string getInvoiceSignature()
		{
			return invoiceSignature;
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

		internal static bool setInvoiceSignatureId(ListSignatureResponse listSignatureResponse)
		{
			invoiceSignatureId = listSignatureResponse.signature;
			return true;
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
			return userPassword;
		}

		internal static string getUserName()
		{
			return userTin;
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
			X509Certificate2 userAuthCertX509 = new X509Certificate2();
			userAuthCertX509.Import(System.IO.File.ReadAllBytes(userAuthCertPath), userAuthCertPin, X509KeyStorageFlags.MachineKeySet);
			string userAuthCertString = System.Convert.ToBase64String(userAuthCertX509.GetRawCertData());
			return userAuthCertString;
		}

		internal static ProductSetV2 GetProductSet()
		{
			ProductSetV2 set = new ProductSetV2();
			set.products = SessionDataManagerFacade.GetRandomProducts();
			return set;
		}

		internal static void setSessionId(string id)
		{
			sessionId = id;
		}

		internal static string getSessionId()
		{			
			return sessionId;
		}

		internal static string getUserTin()
		{
			return userTin;
		}

		internal static string getCertificateNum()
		{
			return currentUser.taxpayer.certificateNum;
		}

		internal static string getCertificateSeries()
		{
			return currentUser.taxpayer.certificateSeries;
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
			long[] idList = getSelectedIdList();
			return idList;
		}

		private static long[] getSelectedIdList()
		{
			return selectedIdList;
		}

		internal static void setSelectedIdList(long[] list)
		{
			selectedIdList = list;
		}

		internal static InvoiceKey[] getinvoiceKeyList()
		{
			InvoiceKey invoiceKey = new InvoiceKey();
			invoiceKey.date = currentInvoice.date;
			invoiceKey.num = currentInvoice.num;
			InvoiceKey[] keyList = { invoiceKey };
			return keyList;
		}

		internal static InvoiceDirection getDirection()
		{
			return invoiceDirection;
		}

		internal static void SetDirection(InvoiceDirection direction)
		{
			invoiceDirection = direction;
		}

		internal static DateTime getlastEventDate()
		{
			return lastEventDate;
		}

		internal static long getlastInvoiceId()
		{
			return lastInvoiceId;
		}

		internal static int getlimit()
		{
			return limit;
		}

		internal static bool getfullInfoOnStatusChange()
		{
			return fullInfoOnStatusChange;
		}

		internal static QueryInvoiceCriteria getQueryInvoiceCriteria()
		{
			return queryInvoiceCriteria;
		}

		private static InvoiceService.InvoiceStatus[] getInvoiceStatusList()
		{
			throw new NotImplementedException();
		}

		private static bool getAsc()
		{
			return asc;
		}

		private static DateTime getDateTo()
		{
			return dateTo;
		}

		private static DateTime getDateFrom()
		{
			return dateFrom;
		}

		internal static string getInvoiceSignatureIdWithReason()
		{
			return invoiceSignatureIdWithReason;
		}

		internal static bool isEmptySessionId()
		{
			return String.IsNullOrEmpty(sessionId);
		}

		internal static string getX509SignCertificate()
		{
			X509Certificate2 userSignCertX509 = new X509Certificate2();
			userSignCertX509.Import(System.IO.File.ReadAllBytes(userSignCertPath), userSignCertPin, X509KeyStorageFlags.MachineKeySet);
			string userSignCertString = Convert.ToBase64String(userSignCertX509.GetRawCertData());
			return userSignCertString;
		}

		internal static InvoiceService.InvoiceIdWithReason[] getInvoiceIdWithReasonsList_InvoiceService()
		{
			InvoiceService.InvoiceIdWithReason invoiceIdWithReason = new InvoiceService.InvoiceIdWithReason();
			invoiceIdWithReason.id = getInvoiceId();
			invoiceIdWithReason.reason = getReason();
			InvoiceService.InvoiceIdWithReason[] invoiceIdWithReasonsList = { invoiceIdWithReason };
			return invoiceIdWithReasonsList;
		}

		internal static string getInvoiceSignatureId()
		{
			return invoiceSignatureId;
		}

		internal static void setCurrentUserData(User user)
		{
			currentUser = user;
		}

		internal static void setCurrentUserProfilesData(profileInfo[] profileInfo)
		{
			profileInfoList = profileInfo;
		}

		
	}
}
