using Microinvest.Common;
using MicroinvestUtilityCenter;
using MicroinvestUtilityCenter.Operations;
using OneMoreTryWPF.ENUMs;
using OneMoreTryWPF.InvoiceService;
using OneMoreTryWPF.SignatureService;
using OneMoreTryWPF.Models;
using OneMoreTryWPF.SessionService;
using OneMoreTryWPF.UploadInvoiceService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Channels;
using System.Text;
using System.Windows;
using static Microinvest.Common.CommonModule;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;
using Microinvest.WarehousePro.Entities;


namespace OneMoreTryWPF.Facades
{
	public static class SessionDataManagerFacade
	{
		private static string key { get;} = "A!9HHhi%XjjYY4YP2@Nob009X";

		private static string sessionId;

		private static string userTin; //"760816300415";
		private static string userPassword;// = "Micr0!nvest";


		private static string userAuthCertPath;
		private static string userAuthCertPin;
		private static string userSignCertPath = @"C:\Users\viktor.kassov\source\repos\ESF_kz\ESF_kz\bin\Debug\Сертификат\ИП Пинчук ВВ до 17.06.21\ИП Пинчук ВВ до 17.06.21\RSA256_af8e6f8be023a8cc035198522a70ca7203a7059a.p12";
		private static string userSignCertPin = "Aa123456";

		private static SessionService.User currentUser;
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


		private static DataTable Goods;
		private static DataTable Partners;
		private static DataTable Objects;



		//QueryInvoice parameters
		private static QueryInvoiceCriteria queryInvoiceCriteria;

		internal static void setUserPassword(string password)
		{
			userPassword = Encrypt(password,key);
			var x = Decrypt(userPassword,key);
		}

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

		

		internal static void FillObjectsByGroup(ObjectGroup selectedGroup, ref ObservableCollection<Models.Object> result)
		{
			if (selectedGroup == null) return;
			foreach (Models.Object item in selectedGroup.Objects)
			{
				result.Add(item);
			}
			if (selectedGroup.ObjectGr == null) return;
			foreach (ObjectGroup item in selectedGroup.ObjectGr)
			{
				FillObjectsByGroup(item, ref result);
			}
		}

		internal static string GetRetailUserName()
		{
			return "Служебный партнер";
		}

		internal static void FillPartnersByGroup(PartnerGroup selectedGroup, ref ObservableCollection<Models.Partner> result)
		{
			if (selectedGroup == null)	return;
			foreach (Models.Partner item in selectedGroup.Partners)
			{
				result.Add(item);
			}
			if (selectedGroup.PartnerGr == null) return;
			foreach(PartnerGroup item in selectedGroup.PartnerGr)
			{
				FillPartnersByGroup(item, ref result);
			}
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

		internal static void setCurrentInvoice(InvoiceV2 invoice)
		{
			currentInvoice = invoice;
		}

		internal static SignatureService.InvoiceIdWithReason[] getInvoiceIdWithReasonsList_LocalService()
		{
			SignatureService.InvoiceIdWithReason invoiceIdWithReason = new SignatureService.InvoiceIdWithReason();
			invoiceIdWithReason.id = getInvoiceId();
			invoiceIdWithReason.reason = getReason();
			SignatureService.InvoiceIdWithReason[] invoiceIdWithReasonsList = { invoiceIdWithReason };
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

		internal static long getInvoiceId()
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
				long[] selectedIdList = { currentInvoiceId };
				SessionDataManagerFacade.setSelectedIdList(selectedIdList);
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
			return Decrypt(userPassword,key);
		}

		internal static string getUserName()
		{
			return userTin;
		}

		internal static void setUserTin(string tin)
		{
			userTin = tin;
		}

		internal static string GetNewInvoiceNum()
		{
			return "666";
		}

		internal static string GetOperatorFullName()
		{
			return currentUser.taxpayer.nameRu;
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
			userAuthCertX509.Import(System.IO.File.ReadAllBytes(userAuthCertPath), Decrypt(userAuthCertPin,key), X509KeyStorageFlags.MachineKeySet);
			string userAuthCertString = System.Convert.ToBase64String(userAuthCertX509.GetRawCertData());
			return userAuthCertString;
		}

		internal static ProductSetV2 GetProductSet()
		{
			ProductSetV2 set = new ProductSetV2();
			//set.products = SessionDataManagerFacade.GetRandomProducts();
			//SessionDataManagerFacade.GetRandomProducts();
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

		internal static void setCurrentUserData(SessionService.User user)
		{
			currentUser = user;
		}

		internal static void setCurrentUserProfilesData(profileInfo[] profileInfo)
		{
			profileInfoList = profileInfo;
		}


		internal static ObservableCollection<MyInvoiceInfo> GetOperationsInfoList(Criteria criteria)
		{
			ObservableCollection<MyInvoiceInfo> resultlist = new ObservableCollection<MyInvoiceInfo>();

			string dateFrom = criteria.dateFrom > criteria.salesLimitDate ? criteria.dateFrom.ToString("yyyy-MM-dd") : criteria.salesLimitDate.ToString("yyyy-MM-dd");
			string dateTo = criteria.dateTo.ToString("yyyy-MM-dd");
			DataTable data = GlobalData.WHPro.dbApp.ExecuteDataset(
				@"SELECT ot.RU as [operType] , FORMAT(o.Date,'dd.MM.yyyy') as [turnoverDate],  o.Acct as [num], 
					u.Name2 as [operatorFullName],
					p.Company2 as [customerName],  p.Address2 as [customerAddress], p.Bulstat as [Bulstat],
					d.InvoiceNumber as [invoiceNumber],d.InvoiceDate as [invoiceDate],
					obj.Name as [objectName], obj.ID as [objectId],
					ROUND(SUM(o.Qtty*o.PriceOut),2) as [totalPriceWithTax]
				FROM Operations o
					LEFT JOIN Documents d ON o.Acct = d.Acct
					INNER JOIN OperationType ot ON o.OperType = ot.ID
					INNER JOIN Users u ON o.OperatorID = u.ID 
					INNER JOIN Partners p ON o.PartnerID = p.ID
					INNER JOIN Objects obj ON o.ObjectID = obj.ID
				WHERE o.Sign = -1 and o.Date >= '" + dateFrom+ "' and o.Date  <= '" +dateTo+
				"' GROUP BY ot.RU,o.Date,o.Acct,u.Name2,p.Company2,p.Address2,p.Bulstat, d.InvoiceNumber,d.invoiceDate, obj.Name, obj.ID"
				).Tables[0];

			DataTable ostatki = GlobalData.WHPro.dbApp.ExecuteDataset(
				@"Select * 
				from Operations o 
				Where  OperType = 3001 And o.Qtty>0
				Order by o.UserRealTime ASC").Tables[0];

			
			foreach (DataRow row in data.Rows)
			{
				bool flag = row["invoiceNumber"] == System.DBNull.Value;
				if (flag)
				{
					DataTable productsTable = GlobalData.WHPro.dbApp.ExecuteDataset(@"SELECT * from Operations Where OperType = 2 AND Acct =" + row["num"] ).Tables[0];
					ProductSetV2 productSet = new ProductSetV2();				
					foreach (DataRow pRow in productsTable.Rows)
					{
						var totalOstatkiByProduct = ostatki.Compute("SUM(Qtty)", "GoodID = " + pRow["GoodID"].ToString());
						double needQtty = (double)pRow["Qtty"];
						if (totalOstatkiByProduct == System.DBNull.Value || needQtty> double.Parse(totalOstatkiByProduct.ToString()))
						{
							flag = false;
							break;
						}
						
						float ndsRate = float.Parse(GlobalData.WHPro.dbApp.ExecuteScalar(@"Select VATValue From Goods g  INNER JOIN VATGroups v ON v.ID = g.TaxGroup WHERE g.ID = '" + pRow["GoodID"].ToString() + "'").ToString());

						DataRow GoodRow = GlobalData.WHPro.dbApp.ExecuteDataset(@"SELECT * from Goods Where ID=" + pRow["GoodID"]).Tables[0].Rows[0];

						DataRow[] ostatkiByproduct = ostatki.Select("Qtty>0 And GoodID = " + pRow["GoodID"]);


						for (int i = 0; needQtty > 0; i++)
						{
							ProductV2 prod = new ProductV2();
							prod.IsContained = true;
							prod.additional = String.Empty;
							//prod.catalogTruId
							prod.description = GoodRow["Name"].ToString();
							prod.unitPrice = float.Parse(pRow["PriceOut"].ToString());
							double ostatokPoDeklaracii = double.Parse(ostatkiByproduct[i]["Qtty"].ToString());
							if (needQtty < ostatokPoDeklaracii)
							{
								ostatokPoDeklaracii -= needQtty;
								ostatkiByproduct[i]["Qtty"] = ostatokPoDeklaracii;
								prod.quantity = needQtty.ToString();
								needQtty = 0;
							}
							else
							{
								needQtty -= ostatokPoDeklaracii;
								prod.quantity = ostatokPoDeklaracii.ToString();
								ostatokPoDeklaracii = 0;
								ostatkiByproduct[i]["Qtty"] = 0;
							}

							prod.priceWithTax = (float)Math.Round((float)prod.unitPrice * float.Parse(prod.quantity),2);
							prod.ndsRate = ndsRate;
							prod.ndsAmount = (float)Math.Round(prod.priceWithTax * ndsRate / (100+ndsRate),2);
							prod.turnoverSize = prod.priceWithTax - prod.ndsAmount;

							prod.exciseRate = float.Parse(ostatkiByproduct[i]["Note"].ToString().Split(']')[1].Trim('['));
							prod.exciseAmount = (float)Math.Round(prod.turnoverSize * prod.exciseRate / (100+prod.exciseRate),2);
							prod.priceWithoutTax = prod.turnoverSize - prod.exciseAmount;
							/*prod.priceWithoutTax = (float)prod.unitPrice * float.Parse(prod.quantity);
							prod.exciseRate = float.Parse(ostatkiByproduct[i]["Note"].ToString().Split('[')[1].Trim(']'));
							prod.exciseAmount = prod.priceWithoutTax * prod.exciseRate / 100; 

							prod.turnoverSize = prod.priceWithoutTax + prod.exciseAmount;
							prod.ndsRate = ndsRate;
							prod.ndsAmount = prod.turnoverSize * ndsRate / 100;
							prod.priceWithTax = prod.priceWithoutTax + prod.ndsAmount + prod.exciseAmount;*/


							prod.productDeclaration = ostatkiByproduct[i]["Lot"].ToString();
							prod.productNumberInDeclaration = ostatkiByproduct[i]["SrcDocID"].ToString();
							prod.rowNumber = i + 1;
							prod.tnvedName = GoodRow["Name"].ToString();
							prod.truOriginCode = int.Parse(ostatkiByproduct[i]["Note"].ToString().Split(']')[2].Trim('['));
							prod.unitCode = ostatkiByproduct[i]["Note"].ToString().Split(']')[0].Trim('[');
							prod.unitNomenclature = getUnitNomenclature(GoodRow["Measure1"].ToString());

							productSet.products.Add(prod);
						}
						productSet.totalPriceWithoutTax = (float)Math.Round(productSet.products.Sum(item => item.priceWithoutTax), 2);
						productSet.totalExciseAmount = (float)Math.Round(productSet.products.Sum(item => item.exciseAmount), 2);
						productSet.totalTurnoverSize = (float)Math.Round(productSet.products.Sum(item => item.turnoverSize), 2);
						productSet.totalNdsAmount = (float)Math.Round(productSet.products.Sum(item => item.ndsAmount), 2);
						productSet.totalPriceWithTax = (float)Math.Round(productSet.products.Sum(item => item.priceWithTax), 2);
					}

					if(flag)
					{
						MyInvoiceInfo info = new MyInvoiceInfo();
						info.direction = InvoiceDirection.OUTBOUND;
						InvoiceV2 invoice = new InvoiceV2();
						invoice.productSet = productSet;
						invoice.turnoverDate = row["turnoverDate"].ToString();
						invoice.customers[0].address = row["customerAddress"].ToString();
						invoice.customers[0].name = row["customerName"].ToString();
						invoice.customers[0].tin = row["Bulstat"].ToString().Trim();
						invoice.sellers[0].address = currentUser.taxpayer.addressRu;
						invoice.sellers[0].name = currentUser.taxpayer.nameRu;
						invoice.sellers[0].tin = currentUser.taxpayer.tin;
						if (invoice.customers[0].name == "Служебный партнер")
						{
							invoice.customers[0].statuses[8].isChecked = true;
						}

						info.invoice = invoice;
						info.invoice.num = row["num"].ToString();
						info.cancelReason = row["operType"].ToString();
						/*object inv = row["invoiceNumber"];
						info.invoiceId = inv == System.DBNull.Value ? 0 : long.Parse(inv.ToString());*/
						info.invoiceNumber = row["invoiceNumber"].ToString();
						info.turnoverDate = row["turnoverDate"].ToString();
						info.turnoverNum = row["num"].ToString();
						info.totalPriceWithTax = float.Parse(row["totalPriceWithTax"].ToString());
						info.customer = invoice.customers[0].name;
						//info.customerTin = row["bin"].ToString();
						//info.customerAddress = row["customerAddress"].ToString();
						info.seller = invoice.sellers[0].name;
						info.invoiceStatus = InvoiceStatus.DRAFT;
						info.direction = InvoiceDirection.OUTBOUND;
						info.myObject = new Models.Object();
						info.myObject.Name = row["objectName"].ToString();
						info.myObject.Id = int.Parse(row["objectId"].ToString());
						resultlist.Add(info);
					}					
				}				
			}
			return resultlist;
		}


		public static MIOperation SelectedOperType { get; set; } = MIOperation.NoOperation;

		internal static WarehouseProOperation InitializeWHOperation()
		{
			WarehouseProOperation warehouseProOperation = null;


			if (SelectedOperType == MIOperation.Purchase)
			{
				warehouseProOperation = new DeliveryOperation(GlobalData.WHPro);
				((DeliveryOperation)warehouseProOperation).Init(GlobalData.WHPro.partners.Partner, GlobalData.WHPro.objects.Object);
			}

			if (SelectedOperType == MIOperation.Order)
			{
				warehouseProOperation = new OrderOperation(GlobalData.WHPro);
				((OrderOperation)warehouseProOperation).Init(GlobalData.WHPro.partners.Partner, GlobalData.WHPro.objects.Object);
			}

			if (warehouseProOperation == null)
			{
				warehouseProOperation = new DeliveryOperation(GlobalData.WHPro);
				((DeliveryOperation)warehouseProOperation).Init(GlobalData.WHPro.partners.Partner, GlobalData.WHPro.objects.Object);
			}

			return warehouseProOperation;
		}

		internal static int AddRetailerToDB(InvoiceV2 invoice)
		{
			SellerV2 seller = invoice.sellers[0];
			CommonModule.TPartner partner = new CommonModule.TPartner();
			partner.Address = seller.address;
			partner.DisplayAddress = partner.Address;
			partner.Company = seller.name;
			partner.DisplayCompany = partner.Company;
			partner.BankAcct = seller.iik;
			partner.BankCode = seller.bik;
			partner.BankName = seller.bank;
			partner.Bulstat = seller.tin;
			partner.Type = PartnerType.Supplier;
			partner.PriceGroup = GoodPriceGroup.RetailPrice;
			partner.GroupID = -1;
			partner.Phone = string.Empty;
			partner.DisplayPhone = partner.Phone;
			partner.City = string.Empty;
			partner.DisplayCity = partner.City;
			partner.IsVeryUsed = false;
			partner.Code = " ";
			partner.MOL = " ";
			partner.DisplayMOL = partner.MOL;
			partner.TaxNo = " ";

			GlobalData.WHPro.partners.Partner = partner;
			GlobalData.WHPro.partners.Validate();
			GlobalData.WHPro.partners.Add(true);
			return GlobalData.WHPro.partners.Partner.ID;			
		}

		internal static bool AddProductToDB(ProductV2 product)
		{
			CommonModule.TGood item = new CommonModule.TGood();
			item.BarCode1 = string.Empty;
			item.Measure1 = getMeasure(product.unitNomenclature);
			item.Measure2 = string.Empty;
			item.Ratio = 1;
			item.Catalog3 = string.Empty;
			item.Type = CommonModule.GoodType.Standard;
			item.PriceIn = (product.unitPrice==null)?0:(double)product.unitPrice;			
			item.GroupID = -1;
			item.Name = !String.IsNullOrEmpty(product.description)?product.description:product.tnvedName;
			item.DisplayName = item.Name;
			item.Code = (GlobalData.WHPro.goods.GetMaxCode() + 1).ToString();
			item.BarCode3 = " ";
			item.Catalog1 = " ";
			item.Catalog2 = " ";
			item.Description = " ";
			item.VATGroupID = 1;
			GlobalData.WHPro.goods.Good = item;
			GlobalData.WHPro.goods.Validate();
			GlobalData.WHPro.goods.Add(true);
			return GlobalData.WHPro.goods.Good.ID != 0;
		}

		private static string getMeasure(string unitNomenclature)
		{
			switch (unitNomenclature)
			{
				case"166":
					return "кг.";
				case "868":
					return "бут.";
				case "112":
					return "л.";
				case "006":
					return "м.";
				default:
					return "шт.";
			}
		}

		private static string getUnitNomenclature(string measure)
		{
			string code = "796";
			switch (measure)
			{
				case "кг.":
				case "кг":
					code = "166";
					break;

				case "шт.":
				case "шт":
					code = "796";
					break;

				case "бут.":
				case "бут":
					code = "868";
					break;

				case "л.":
				case "л":
					code = "112";
					break;
				case "м.":
				case "м":
					code = "006";
					break;
			}
			return code;
		}


		internal static bool AddInvoiceToDB(MyInvoiceInfo selectedInvoice)
		{
			string Acct = selectedInvoice.invoice.num;			
			string[] tmp = selectedInvoice.invoice.date.Split('.');
			string InvoiceDate = tmp[2] + "-" + tmp[1] + "-" + tmp[0] + " 00:00:00";
			int OperType = selectedInvoice.direction == InvoiceDirection.OUTBOUND ? 2 : 1;
			string InvoiceNumber = selectedInvoice.invoiceNumber;
			int DocumentType = 0;
			tmp = selectedInvoice.invoice.turnoverDate.Split('.');
			string TaxDate = tmp[2] + "-" + tmp[1] + "-" + tmp[0] + " 00:00:00";

			try
			{
				string query = String.Format(
				@"INSERT INTO Documents(Acct,InvoiceNumber,OperType,InvoiceDate,DocumentType,PaymentType, TaxDate) Values('{0}','{1}','{2}','{3}','{4}',ISNULL((SELECT TOP(1) [Type] FROM Payments WHere Acct = '{0}' AND Mode = '1' AND OperType = '{2}'),'1'),'{5}')",
				Acct, InvoiceNumber, OperType, InvoiceDate, DocumentType, TaxDate);
				GlobalData.WHPro.dbApp.ExecuteNonQuery(query);
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				return false;
			}
			
		}



		internal static bool AddDeliveryToDB(MyInvoiceInfo selectedInvoice)
		{
			WarehouseProOperation operation = SessionDataManagerFacade.InitializeWHOperation();
			long acct = 0;

			Goods = GlobalData.WHPro.goods.NomenclatureDataSet.Tables[0];

			int PartnerID = selectedInvoice.myPartner.Id;
			int ObjectID = selectedInvoice.myObject.Id;
			/*if(PartnerID == 0)
			{
				SellerV2 seller = selectedInvoice.invoice.sellers[0];
				CommonModule.TPartner partner = new CommonModule.TPartner();
				partner.Address = seller.address;
				partner.DisplayAddress = partner.Address;
				partner.Company = seller.name;
				partner.DisplayCompany = partner.Company;
				partner.BankAcct = seller.iik;
				partner.BankCode = seller.bik;
				partner.BankName = seller.bank;
				partner.Bulstat = seller.tin;	
				partner.Type = PartnerType.Supplier ;
				partner.PriceGroup = GoodPriceGroup.RetailPrice;
				partner.GroupID = -1;
				partner.Phone = string.Empty;
				partner.DisplayPhone = partner.Phone;
				partner.City = string.Empty;
				partner.DisplayCity = partner.City;
				partner.IsVeryUsed = false;
				partner.Code = " ";
				partner.MOL = " ";
				partner.DisplayMOL = partner.MOL;
				partner.TaxNo = " ";

				GlobalData.WHPro.partners.Partner = partner;
			    GlobalData.WHPro.partners.Validate();
				GlobalData.WHPro.partners.Add(true);
				PartnerID = GlobalData.WHPro.partners.Partner.ID;			
			}*/


			operation.baseOperation.DocDate = DateTime.Now;
			operation.baseOperation.OperatorID = 1;
			operation.baseOperation.ObjectID = ObjectID;
			operation.baseOperation.PartnerID = PartnerID;


			foreach (ProductV2 product in selectedInvoice.invoice.productSet.products)
			{

				int GoodID = product.truOriginCode > 2 ? getGoodIdByName(product.description) : getGoodIdByName(product.tnvedName);
				CommonModule.TGood item = new CommonModule.TGood();
				GlobalData.WHPro.goods.Find(GoodID);
				item = GlobalData.WHPro.goods.Good;
				double price = product.unitPrice == null ? 0 : (double)product.unitPrice;
				double Qtty = String.IsNullOrEmpty(product.quantity) ? 1 : double.Parse(product.quantity);
				operation.AddByID(id: GoodID, qtty: Qtty, discount: 0, priceOut: item.PriceOut, priceIn: price);
			}

			if (operation.baseOperation.Validate())
			{
				//GlobalData.WHPro.dbApp.BeginTransaction();
				if (operation.Save())
				{
					acct = operation.DocNumber;
					selectedInvoice.invoice.num = acct.ToString();
					selectedInvoice.turnoverNum = acct.ToString();


					foreach (ProductV2 product in selectedInvoice.invoice.productSet.products)
					{
						int GoodID = product.truOriginCode > 2 ? getGoodIdByName(product.description) : getGoodIdByName(product.tnvedName);
						GlobalData.WHPro.dbApp.ExecuteNonQuery(
							@"UPDATE Operations
							SET Note ='[" + product.kpvedCode + "][" + product.exciseRate + "]["+product.truOriginCode+"]', Lot='" + product.productDeclaration
							+ "', SrcDocID='" + product.productNumberInDeclaration + "' WHERE GoodID = " + GoodID + " And Acct =" + acct + " And OperType = 1");
						DataSet tmp = GlobalData.WHPro.dbApp.ExecuteDataset(
							@"Insert into Operations([OperType],[Acct],[GoodID],[PartnerID],[ObjectID],[OperatorID],[Qtty],[Sign],[PriceIn],[PriceOut],[VATIn],[VATOut]
								  ,[Discount],[CurrencyID],[CurrencyRate],[Date],[Lot],[LotID],[Note],[SrcDocID],[UserID],[UserRealTime])
							Select '3001',[Acct],[GoodID],[PartnerID],[ObjectID],[OperatorID],[Qtty],'0',[PriceIn],[PriceOut],[VATIn],[VATOut]
								  ,[Discount],[CurrencyID],[CurrencyRate],[Date],[Lot],[LotID],[Note],[SrcDocID],[UserID],[UserRealTime]
							FROM [Kazakhstan].[dbo].[Operations]
							WHERE GoodID = " + GoodID + " And Acct =" + acct);
					}
					return true;
				}
			}
			return false;
		}

		public static int getPartnerIdByName(string seller)
		{

			DataRow[] rows = getPartners().Select(String.Format("Company = '{0}' OR Company2 = '{0}'", seller));
			return rows.Length == 0? 0: (int)rows[0]["ID"];
		}

		public static int getGoodIdByName(string productName)
		{
			DataRow[] rows = getGoods().Select(String.Format("Name = '{0}' OR Name2 = '{0}'", productName));
			return rows.Length == 0 ? 0 : (int)rows[0]["ID"];
		}

		internal static int SearchInPartners(SellerV2 seller)
		{
			if (seller.name != SessionDataManagerFacade.GetRetailUserName())
			{
				if (seller.tin == null) return 0;
				int id = getPartnerIdByTin(seller.tin);
				if (id != 0)
				{
					return id;
				}
			}
			return getPartnerIdByName(seller.name);
		}

		internal static int SearchInPartners(CustomerV2 customer)
		{
			if(customer.name != SessionDataManagerFacade.GetRetailUserName())
			{
				if (customer.tin == null) return 0;
				int id = getPartnerIdByTin(customer.tin);
				if (id != 0)
				{
					return id;
				}
			}			
			return getPartnerIdByName(customer.name);
		}

		internal static int SearchInGoods(ProductV2 product)
		{
			int id = 0;
			if (!String.IsNullOrEmpty(product.description))
			{
				id = getGoodIdByName(product.description);
			}
			else
			{
				if(!String.IsNullOrEmpty(product.tnvedName))
				{
					id = getGoodIdByName(product.tnvedName); 
				}
			}
			return id;			
		}


		public static int getPartnerIdByTin(string tin)
		{
			DataRow[] rows = getPartners().Select(String.Format("Bulstat = '{0}' OR TaxNo = '{0}'  OR BankVATName = '{0}'", tin));
			return rows.Length == 0 ? 0 : (int)rows[0]["ID"];
		}

		

		public static DataTable RefreshPartners()
		{
			Partners = GlobalData.WHPro.partners.NomenclatureDataSet.Tables[0];
			return Partners;
		}
		public static DataTable getPartners()
		{
			if(Partners == null)
			{
				Partners = GlobalData.WHPro.partners.NomenclatureDataSet.Tables[0];
			}
			return Partners;			
		}

		public static DataTable RefreshObjects()
		{
			Objects = GlobalData.WHPro.objects.NomenclatureDataSet.Tables[0];		
			return Objects;
		}
		public static DataTable getObjects()
		{
			if (Objects == null)
			{
				Objects = GlobalData.WHPro.objects.NomenclatureDataSet.Tables[0];
			}
			return Objects;
		}


		public static DataTable RefreshGoods()
		{
			Goods = GlobalData.WHPro.goods.NomenclatureDataSet.Tables[0];
			return Goods;
		}
		public static DataTable getGoods()
		{
			if (Goods == null)
			{
				Goods = GlobalData.WHPro.goods.NomenclatureDataSet.Tables[0];
			}
			return Goods;
		}

		public static ObservableCollection<Models.Partner> getPartnersByGroupId(int id)
		{
			ObservableCollection<Models.Partner> result = new ObservableCollection<Models.Partner>();
			DataRow[] rows = getPartners().Select(String.Format("iif(GroupID<0,GroupID*-1,GroupID)={0} ", id));
			foreach(DataRow row in rows)
			{
				Models.Partner tmp = new Models.Partner();
				tmp.Id = (int)row["ID"];
				tmp.Name = row["Company"].ToString();
				result.Add(tmp);
			}
			return result;
		}
		internal static ObservableCollection<Models.Object> getObjectsByGroupId(int id)
		{
			ObservableCollection<Models.Object> result = new ObservableCollection<Models.Object>();
			DataRow[] rows = getObjects().Select(String.Format("iif(GroupID<0,GroupID*-1,GroupID)={0} ", id));
			foreach (DataRow row in rows)
			{
				Models.Object tmp = new Models.Object();
				tmp.Id = (int)row["ID"];
				tmp.Name = row["Name"].ToString();
				result.Add(tmp);
			}
			return result;
		}

		public static string Encrypt(string plainText)
		{
			if (plainText == null)
			{
				return null;
			}			

			// Get the bytes of the string
			var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
			var passwordBytes = Encoding.UTF8.GetBytes(key);

			// Hash the password with SHA256
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

			return Convert.ToBase64String(bytesEncrypted);
		}

		public static string Encrypt(string plainText, string password)
		{
			if (plainText == null)
			{
				return null;
			}

			if (password == null)
			{
				password = String.Empty;
			}

			// Get the bytes of the string
			var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
			var passwordBytes = Encoding.UTF8.GetBytes(password);

			// Hash the password with SHA256
			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			var bytesEncrypted = Encrypt(bytesToBeEncrypted, passwordBytes);

			return Convert.ToBase64String(bytesEncrypted);
		}

		public static string Decrypt(string encryptedText, string password)
		{
			if (encryptedText == null)
			{
				return null;
			}

			if (password == null)
			{
				password = String.Empty;
			}

			// Get the bytes of the string
			var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
			var passwordBytes = Encoding.UTF8.GetBytes(password);

			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

			return Encoding.UTF8.GetString(bytesDecrypted);
		}

		public static string Decrypt(string encryptedText)
		{
			if (encryptedText == null)
			{
				return null;
			}

			// Get the bytes of the string
			var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
			var passwordBytes = Encoding.UTF8.GetBytes(key);

			passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

			var bytesDecrypted = Decrypt(bytesToBeDecrypted, passwordBytes);

			return Encoding.UTF8.GetString(bytesDecrypted);
		}

		private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
		{
			byte[] encryptedBytes = null;

			// Set your salt here, change it to meet your flavor:
			// The salt bytes must be at least 8 bytes.
			var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

			using (MemoryStream ms = new MemoryStream())
			{
				using (RijndaelManaged AES = new RijndaelManaged())
				{
					var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

					AES.KeySize = 256;
					AES.BlockSize = 128;
					AES.Key = key.GetBytes(AES.KeySize / 8);
					AES.IV = key.GetBytes(AES.BlockSize / 8);

					AES.Mode = CipherMode.CBC;

					using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
						cs.Close();
					}

					encryptedBytes = ms.ToArray();
				}
			}

			return encryptedBytes;
		}

		private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
		{
			byte[] decryptedBytes = null;

			// Set your salt here, change it to meet your flavor:
			// The salt bytes must be at least 8 bytes.
			var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

			using (MemoryStream ms = new MemoryStream())
			{
				using (RijndaelManaged AES = new RijndaelManaged())
				{
					var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

					AES.KeySize = 256;
					AES.BlockSize = 128;
					AES.Key = key.GetBytes(AES.KeySize / 8);
					AES.IV = key.GetBytes(AES.BlockSize / 8);
					AES.Mode = CipherMode.CBC;

					using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
						cs.Close();
					}

					decryptedBytes = ms.ToArray();
				}
			}

			return decryptedBytes;
		}

		internal static void setAuthCertPath(string authCertificatePath)
		{
			userAuthCertPath = authCertificatePath;
		}

		internal static string getAuthCertPin()
		{
			return userAuthCertPin;
		}

		internal static void setAuthCertPin(string pin)
		{
			userAuthCertPin = Encrypt(pin,key);
		}

		internal static void SaveConfigKey(string key, string value)
		{
			GlobalData.WHPro.dbApp.ExecuteNonQuery(@"Insert INTO Configuration ([Key], Value) VALUES  ('" + key +"','" + value + "')");
		}

		internal static string LoadLastAuthCertPath()
		{
			var result = GlobalData.WHPro.dbApp.ExecuteScalar(@"SELECT Value From Configuration c Where c.[Key] = 'InvoiceKZ_lastAuthCertPath'");
			return result==null? String.Empty:Decrypt(result.ToString());
		}

		internal static string LoadLastAuthCertPin()
		{
			var result = GlobalData.WHPro.dbApp.ExecuteScalar(@"SELECT Value From Configuration c Where c.[Key] = 'InvoiceKZ_lastAuthCertPin'");
			return result == null ? String.Empty : Decrypt(result.ToString());
		}

		internal static string LoadLastUserPassword()
		{
			var result = GlobalData.WHPro.dbApp.ExecuteScalar(@"SELECT Value From Configuration c Where c.[Key] = 'InvoiceKZ_lastUserPassword'");
			return result == null ? String.Empty : Decrypt(result.ToString());
		}


		internal static void IsExistInvoiceInDb(MyInvoiceInfo myInvoiceInfo)
		{
			var Acct = GlobalData.WHPro.dbApp.ExecuteScalar(@"SELECT TOP(1) Acct FROM Documents WHERE InvoiceNumber = '"+myInvoiceInfo.invoiceNumber+"'");
			if(Acct != DBNull.Value && Acct != null)
			{
				myInvoiceInfo.turnoverNum = Acct.ToString();
			}
			else
			{
				myInvoiceInfo.turnoverNum = string.Empty;
			}
		}

	}

	
}
