using Microinvest.Common;
using MicroinvestUtilityCenter;
using OneMoreTryWPF.Facades;
using OneMoreTryWPF.InvoiceService;
using OneMoreTryWPF.SignatureService;
using OneMoreTryWPF.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.Models
{
	public class InvoiceViewModel: INotifyPropertyChanged
	{
		//public ObservableCollection<ProductV2> Products { get; set; }

		private MyInvoiceInfo selectedInvoice;
		private string title;
		private InvoiceV2 invoice;
		/*private ProductSetV2 productSet;
		private SellerV2 seller;
		private CustomerV2 customer;*/
		private ProductV2 selectedProduct;
		private bool isEditable;
		private bool isValidSeller;
		private bool isValidCustomer;
		private bool godMode;
		private bool isValidProductSet;

		private InvoiceDirection direction;

		public InvoiceDirection Direction
		{
			get { return direction; }
			set
			{
				direction = value;
				OnPropertyChanged("Direction");
			}
		}

		private bool IsInbound = false;
		public bool isInbound
		{
			get { return IsInbound; }
			set
			{
				IsInbound = value;
				OnPropertyChanged("isInbound");
			}
		}

		private bool IsOutbound = false;
		public bool isOutbound
		{
			get { return IsOutbound; }
			set
			{
				IsOutbound = value;
				OnPropertyChanged("isOutbound");
			}
		}

		private bool IsFailed = false;
		public bool isFailed
		{
			get { return IsFailed; }
			set
			{
				IsFailed = value;
				OnPropertyChanged("isFailed");
			}
		}

		private bool IsCreated = false;
		public bool isCreated
		{
			get { return IsCreated; }
			set
			{
				IsCreated = value;
				OnPropertyChanged("isCreated");
			}
		}

		private bool IsCreatable = false;
		public bool isCreatable
		{
			get { return IsCreatable; }
			set
			{
				IsCreatable = value;
				OnPropertyChanged("isCreatable");
			}
		}

		private bool IsRevokable = false;
		public bool isRevokable
		{
			get { return IsRevokable; }
			set
			{
				IsRevokable = value;
				OnPropertyChanged("isRevokable");
			}
		}

		private bool IsDeclinable = false;
		public bool isDeclinable
		{
			get { return IsDeclinable; }
			set
			{
				IsDeclinable = value;
				OnPropertyChanged("isDeclinable");
			}
		}


		/*private int partnerId;
		public int PartnerId
		{
			get { return partnerId; }
			set
			{
				partnerId = value;
				if(PartnerId!=0)
				{
					Partner _partner = new Partner();
					_partner.Id = PartnerId;
					_partner.Name = SessionDataManagerFacade.getPartners().Select(String.Format("ID={0} ", PartnerId))[0]["Company"].ToString();
					Partner = _partner;
					if (selectedInvoice !=null)
					{
						selectedInvoice.myPartner = Partner;
					}
					
					IsValidSeller = true;
				}
				OnPropertyChanged("PartnerId");
			}
		}

		private int objectId;
		public int ObjectId
		{
			get { return objectId; }
			set
			{
				objectId = value;
				if (ObjectId != 0)
				{
					Object _obj = new Object();
					_obj.Id = ObjectId;
					_obj.Name = SessionDataManagerFacade.getObjects().Select(String.Format("ID={0} ", ObjectId))[0]["Name"].ToString();
					Object = _obj;
					if (selectedInvoice != null)
					{
						selectedInvoice.myObject = Object;
					}						
				}
				OnPropertyChanged("ObjectId");
			}
		}*/

		private Partner partner = new Partner();
		public Partner Partner
		{
			get { return partner; }
			set
			{
				partner = value;
				OnPropertyChanged("Partner");
			}
		}

		private Object obj = new Object();
		public Object Object
		{
			get { return obj; }
			set
			{
				obj = value;
				OnPropertyChanged("Object");
			}
		}

		public InvoiceV2 Invoice
		{
			get { return invoice; }
			set
			{
				invoice = value;
				ReCalcRowNumbers();////
				IsValidProductSet=ProductSetValidation();
				switch (Direction)
				{
					case InvoiceDirection.INBOUND:
						IsValidSeller = SellerValidation(invoice.sellers[0]);
						IsValidCustomer = true;
						break;
					case InvoiceDirection.OUTBOUND:
						IsValidSeller = true;
						IsValidCustomer = CustomerValidation(invoice.customers[0]);
						break;
					default:
						IsValidSeller = SellerValidation(invoice.sellers[0]);
						IsValidCustomer = CustomerValidation(invoice.customers[0]);
						break;
				}
				if(String.IsNullOrEmpty(Title) && invoice !=null)
				{
					Title = "Продажа №" + invoice.num;
				}
				OnPropertyChanged("Invoice");
			}
		}


		public string Title
		{
			get { return title; }
			set
			{
				title = value;
				OnPropertyChanged("Object");
			}
		}


		public MyInvoiceInfo SelectedInvoice
		{
			get { return selectedInvoice; }
			set
			{
				selectedInvoice = value;
				if (value != null)
				{
					isInbound = selectedInvoice.direction == InvoiceDirection.INBOUND;
					isOutbound = selectedInvoice.direction == InvoiceDirection.OUTBOUND;
					isCreated = selectedInvoice.invoiceStatus == InvoiceStatus.CREATED;
					isFailed = selectedInvoice.invoiceStatus == InvoiceStatus.FAILED;
					isDeclinable = selectedInvoice.invoice.invoiceType != ENUMs.InvoiceType.ORDINARY_INVOICE;
					isCreatable = selectedInvoice.invoiceStatus == InvoiceStatus.DRAFT;
					isRevokable = selectedInvoice.invoiceStatus != InvoiceStatus.DRAFT && selectedInvoice.invoiceStatus != InvoiceStatus.FAILED;
				
					if(selectedInvoice.invoiceStatus != InvoiceStatus.DRAFT)
					{
						Title = "ЭСФ №"+selectedInvoice.invoiceNumber;
					}					
				}
				OnPropertyChanged("SelectedInvoice");
			}
		}





		/*public ProductSetV2 ProductSet
		{
			get { return productSet; }
			set
			{
				productSet = value;
				OnPropertyChanged("ProductSet");
			}
		}*/

		/*public SellerV2 Seller
		{
			get { return seller; }
			set
			{
				seller = value;
				OnPropertyChanged("Seller");
			}
		}*/

		/*public CustomerV2 Customer
		{
			get { return customer; }
			set
			{
				customer = value;
				OnPropertyChanged("Customer");
			}
		}*/

		public ProductV2 SelectedProduct
		{
            get { return selectedProduct; }
			set
			{
				selectedProduct = value;
				OnPropertyChanged("SelectedProduct");
			}
        }
		public bool IsEditable
		{
			get { return isEditable; }
			set
			{
				isEditable = value;
				OnPropertyChanged("IsEditable");
			}
		}

		public bool GodMode
		{
			get { return godMode; }
			set
			{
				godMode = value;
				OnPropertyChanged("GodMode");
			}
		}

		public bool IsValidSeller
		{
			get { return isValidSeller; }
			set
			{
				isValidSeller = value;
				OnPropertyChanged("IsValidSeller");
			}
		}

		public bool IsValidCustomer
		{
			get { return isValidCustomer; }
			set
			{
				isValidCustomer = value;
				OnPropertyChanged("IsValidCustomer");
			}
		}

		public bool IsValidProductSet
		{
			get { return isValidProductSet; }
			set
			{
				isValidProductSet = value;
				OnPropertyChanged("IsValidProductSet");
			}
		}





		public InvoiceViewModel(MyInvoiceInfo myInvoiceInfo)
		{
			IsEditable = false;
			GodMode = false;
			IsValidSeller = false;
			IsValidCustomer = false;
			IsValidProductSet = false;
			SelectedInvoice = myInvoiceInfo;
			Direction = myInvoiceInfo.direction;
			Invoice = myInvoiceInfo.invoice;
			Object = myInvoiceInfo.myObject;			
			//ProductSet = new ProductSetV2();
			//ProductSet.products = SessionDataManagerFacade.GetRandomProducts();
			//Seller = new SellerV2();
			//Customer = new CustomerV2();
			
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		private RelayCommand addCommand;
		public RelayCommand AddCommand
		{
			get
			{
				return addCommand ??
				  (addCommand = new RelayCommand(obj =>
				  {
					  ProductV2 product = new ProductV2();
					  product.truOriginCode = 3;
					  product.rowNumber = Invoice.productSet.products.Count + 1;
					  Invoice.productSet.products.Add(product);
					  SelectedProduct = product;
				  }));
			}
		}

		private RelayCommand removeCommand;
		public RelayCommand RemoveCommand
		{
			get
			{
				return removeCommand ??
				  (removeCommand = new RelayCommand(obj =>
				  {
						while(selectedProduct!=null)
						{
							Invoice.productSet.products.Remove(SelectedProduct);
							ReCalcRowNumbers();							
						}
				  },
				  (obj)=> Invoice.productSet.products.Count>0));
			}
		}

		private RelayCommand addPartnerCommand;
		public RelayCommand AddPartnerCommand
		{
			get
			{
				return addPartnerCommand ??
				  (addPartnerCommand = new RelayCommand(obj =>
				  {
					  Partner.Id = SessionDataManagerFacade.AddRetailerToDB(Invoice);
					  SessionDataManagerFacade.RefreshPartners();
				  }));
			}
		}

		private RelayCommand selectPartnerCommand;
		public RelayCommand SelectPartnerCommand
		{
			get
			{
				return selectPartnerCommand ??
				  (selectPartnerCommand = new RelayCommand(obj =>
				  {
					  SelectPartnerWindow selectPartnerWindow = new SelectPartnerWindow();
					  if(selectPartnerWindow.ShowDialog()==true)
					  {
						  Partner.Id = ((SelectPartnerViewModel)selectPartnerWindow.DataContext).SelectedPartner.Id;
					  }
				  }));
			}
		}

		

		private RelayCommand selectObjectCommand;
		public RelayCommand SelectObjectCommand
		{
			get
			{
				return selectObjectCommand ??
				  (selectObjectCommand = new RelayCommand(obj =>
				  {
					  SelectObjectWindow selectObjectWindow = new SelectObjectWindow();
					  if (selectObjectWindow.ShowDialog() == true)
					  {
						  Object.Id = ((SelectObjectViewModel)selectObjectWindow.DataContext).SelectedObject.Id;
					  }
				  }));
			}
		}

		private RelayCommand addProductCommand;
		public RelayCommand AddProductCommand
		{
			get
			{
				return addProductCommand ??
				  (addProductCommand = new RelayCommand(obj =>
				  {
					  if(SessionDataManagerFacade.AddProductToDB(selectedProduct))
					  {
						  SessionDataManagerFacade.RefreshGoods();
						  IsValidProductSet = ProductSetValidation();
					  }
				  }));
			}
		}

		private void ReCalcRowNumbers()
		{
			for (int i = 0; i < Invoice.productSet.products.Count; i++)
			{
				Invoice.productSet.products[i].rowNumber = i+1;
			}
		}

		private bool ProductSetValidation()
		{
			bool flag = true;
			foreach (ProductV2 prod in Invoice.productSet.products)
			{
				prod.IsContained = ProductValidation(prod);
				flag = flag && prod.IsContained;
			}
			return flag;
		}

		private bool SellerValidation(SellerV2 seller)
		{
			Partner.Id = SessionDataManagerFacade.SearchInPartners(seller);
			return Partner.Id != 0;
		}

		private bool CustomerValidation(CustomerV2 customer)
		{
			Partner.Id = SessionDataManagerFacade.SearchInPartners(customer);
			return Partner.Id != 0;
		}

		private bool ProductValidation(ProductV2 prod)
		{
			return SessionDataManagerFacade.SearchInGoods(prod) !=0;
		}

		public void ClearFlags()
		{
			isInbound = false;
			isOutbound = false;
			isCreated = false;
			isFailed = false;
			isCreatable = false;
			isDeclinable = false;
			isRevokable = false;
		}

		private string Reason = String.Empty;
		public string reason
		{
			get { return Reason; }
			set
			{
				Reason = value;
				SessionDataManagerFacade.setReason(reason);
				OnPropertyChanged("reason");
			}
		}


		private RelayCommand confirmCommand;
		public RelayCommand ConfirmCommand
		{
			get
			{
				return confirmCommand ??
				  (confirmCommand = new RelayCommand(obj =>
				  {

					  /*long[] selectedIdList = { selectedInvoice.invoiceId };
					  SessionDataManagerFacade.setSelectedIdList(selectedIdList);
					  selectedInvoice.invoiceStatus = InvoiceServiceOperationsFacade.confirmInvoiceById() ? InvoiceStatus.DELIVERED:selectedInvoice.invoiceStatus;*/
					  SessionDataManagerFacade.AddDeliveryToDB(SelectedInvoice);
					  SessionDataManagerFacade.AddInvoiceToDB(SelectedInvoice);

				  }));
			}
		}







		private RelayCommand declineCommand;
		public RelayCommand DeclineCommand
		{
			get
			{
				return declineCommand ??
				  (declineCommand = new RelayCommand(obj =>
				  {
					  ReasonWindow reasonWindow = new ReasonWindow(reason);
					  if (reasonWindow.ShowDialog() == true)
					  {
						  reason = reasonWindow.reason;
						  long[] selectedIdList = { selectedInvoice.invoiceId };
						  SessionDataManagerFacade.setSelectedIdList(selectedIdList);
						  if (LocalServiceOperationFacade.GenerateIdWithReasonListSignature())
						  {
							  selectedInvoice.invoiceStatus = InvoiceServiceOperationsFacade.declineInvoiceById() ? InvoiceStatus.DECLINED : selectedInvoice.invoiceStatus;
						  }
					  }
				  }));
			}
		}


		private RelayCommand revokeCommand;
		public RelayCommand RevokeCommand
		{
			get
			{
				return revokeCommand ??
				  (revokeCommand = new RelayCommand(obj =>
				  {
					  ReasonWindow reasonWindow = new ReasonWindow(reason);
					  if (reasonWindow.ShowDialog() == true)
					  {
						  reason = reasonWindow.reason;
						  SessionDataManagerFacade.setCurrentInvoiceId(selectedInvoice.invoiceId);
						  if (LocalServiceOperationFacade.GenerateIdWithReasonListSignature())
						  {
							  if (InvoiceServiceOperationsFacade.RevokeInvoiceById())
							  {
								  selectedInvoice.invoiceStatus = InvoiceStatus.REVOKED;
								  isCreated = false;
							  }
						  }
					  }
				  }));
			}
		}


		private RelayCommand deleteCommand;
		public RelayCommand DeleteCommand
		{
			get
			{
				return deleteCommand ??
				  (deleteCommand = new RelayCommand(obj =>
				  {
					  long[] selectedIdList = { selectedInvoice.invoiceId };
					  SessionDataManagerFacade.setSelectedIdList(selectedIdList);
					  ListSignatureResponse listSignatureResponse = new ListSignatureResponse();
					  if (LocalServiceOperationFacade.GenerateIdListSignature())
					  {
						  selectedInvoice.invoiceStatus = InvoiceServiceOperationsFacade.DeleteInvoiceById() ? InvoiceStatus.DELETED : selectedInvoice.invoiceStatus;
					  }
				  }));
			}
		}

		private RelayCommand createCommand;
		public RelayCommand CreateCommand
		{
			get
			{
				return createCommand ??
				  (createCommand = new RelayCommand(obj =>
				  {
					  if (selectedInvoice != null)
					  {
						  SessionDataManagerFacade.setCurrentInvoice(selectedInvoice.invoice);
						  if (LocalServiceOperationFacade.GenerateInvoiceSignature())
						  {
							  if (UploadInvoiceServiceOperationFacade.SendInvoice())
							  {

								  QueryInvoiceResponse queryInvoiceResponse = new QueryInvoiceResponse();
								  InvoiceServiceOperationsFacade.QueryInvoiceById(out queryInvoiceResponse);
								  if (queryInvoiceResponse.invoiceInfoList[0].invoiceStatus != InvoiceStatus.FAILED)
								  {
									  selectedInvoice.invoiceNumber = queryInvoiceResponse.invoiceInfoList[0].registrationNumber;
									  if (SessionDataManagerFacade.AddInvoiceToDB(selectedInvoice))
									  {
										  selectedInvoice.invoiceStatus = InvoiceStatus.CREATED;
										  isCreated = true;
									  }
								  }
							  }
						  }
					  }
				  }));
			}
		}

		private RelayCommand selectedItemDoubleClickCommand;
		public RelayCommand SelectedItemDoubleClickCommand
		{
			get
			{
				return selectedItemDoubleClickCommand ??
				  (selectedItemDoubleClickCommand = new RelayCommand(obj =>
				  {
					  if (selectedProduct!= null)
					  {
						  ProductDetailsWindow productWindow = new ProductDetailsWindow(selectedProduct);
						  productWindow.ShowDialog();
					  }
				  }));
			}
		}



	}
}
