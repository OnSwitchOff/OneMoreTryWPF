using Microinvest.Common;
using MicroinvestUtilityCenter;
using MicroinvestUtilityCenter.Managers;
using MicroinvestUtilityCenter.Operations;
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
using System.Windows;
using System.Windows.Data;

namespace OneMoreTryWPF.Models
{
	public class InvoicesManagerViewModel : INotifyPropertyChanged
	{

		public ObservableCollection<MyInvoiceInfo> InvoiceInfos { get; set; }
		private MyInvoiceInfo selectedInvoice;
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
					Partner = selectedInvoice.myPartner;
					Object = selectedInvoice.myObject;
				}
				OnPropertyChanged("SelectedInvoice");				
			}
		}

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



		private Criteria criteria = new Criteria();
		public Criteria Criteria
		{
			get { return criteria; }
			set
			{
				criteria = value;
				OnPropertyChanged("Criteria");
			}
		}

		public InvoicesManagerViewModel() 
		{
			InvoiceInfos = new ObservableCollection<MyInvoiceInfo>();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		public void OnPropertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		

		private RelayCommand getInvoicesByCriteriaCommand;
		public RelayCommand GetInvoicesByCriteriaCommand
		{
			get
			{
				return getInvoicesByCriteriaCommand ??
				  (getInvoicesByCriteriaCommand = new RelayCommand(obj =>
				  {
					  SessionDataManagerFacade.setCriteria(Criteria);
					  QueryInvoiceResponse queryInvoiceResponse = new QueryInvoiceResponse();
					  InvoiceServiceOperationsFacade.QueryInvoice(out queryInvoiceResponse);

					  ClearFlags();
					  InvoiceInfos.Clear();
					  if (queryInvoiceResponse != null)
					  {
						  foreach (InvoiceInfo invoiceInfo in queryInvoiceResponse.invoiceInfoList)
						  {
							  MyInvoiceInfo myInvoiceInfo = new MyInvoiceInfo();
							  myInvoiceInfo.direction = Criteria.direction;
							  myInvoiceInfo.invoice = ParsingManager.getInvoiceFromBodyString(invoiceInfo.invoiceBody);
							  myInvoiceInfo.invoiceId = invoiceInfo.invoiceId;
							  myInvoiceInfo.inputDate = invoiceInfo.inputDate;
							  myInvoiceInfo.invoiceStatus = invoiceInfo.invoiceStatus;
							  myInvoiceInfo.cancelReason = invoiceInfo.cancelReason;
							  myInvoiceInfo.invoiceNumber = invoiceInfo.registrationNumber;
							  
							  if (myInvoiceInfo.direction == InvoiceDirection.INBOUND)
							  {
								  SessionDataManagerFacade.IsExistInvoiceInDb(myInvoiceInfo);
							  }							  
							  InvoiceInfos.Add(myInvoiceInfo);
						  }
					  }
				  }));
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
					  int index = InvoiceInfos.IndexOf(SelectedInvoice);
					  if (String.IsNullOrEmpty(SelectedInvoice.turnoverNum))
					  {						 
						  if (SessionDataManagerFacade.AddDeliveryToDB(SelectedInvoice) && SessionDataManagerFacade.AddInvoiceToDB(SelectedInvoice))
						  {
							  long[] selectedIdList = { selectedInvoice.invoiceId };
							  SessionDataManagerFacade.setSelectedIdList(selectedIdList);
							  selectedInvoice.invoiceStatus = InvoiceServiceOperationsFacade.confirmInvoiceById() ? InvoiceStatus.DELIVERED : selectedInvoice.invoiceStatus;

							  CollectionViewSource.GetDefaultView(InvoiceInfos).Refresh();
						  }
					  }
					 
					  

				  }, (o)=> selectedInvoice!=null && selectedInvoice.myObject.Id > 0 && selectedInvoice.myPartner.Id >0));
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
					  if(reasonWindow.ShowDialog()==true)
					  {
						  reason = reasonWindow.reason;
						  long[] selectedIdList = { selectedInvoice.invoiceId };
						  SessionDataManagerFacade.setSelectedIdList(selectedIdList);
						  if(LocalServiceOperationFacade.GenerateIdWithReasonListSignature())
						  {
							  selectedInvoice.invoiceStatus = InvoiceServiceOperationsFacade.declineInvoiceById() ? InvoiceStatus.DECLINED : selectedInvoice.invoiceStatus;
						  }
						  CollectionViewSource.GetDefaultView(InvoiceInfos).Refresh();
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
							  CollectionViewSource.GetDefaultView(InvoiceInfos).Refresh();
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
							selectedInvoice.invoiceStatus = InvoiceServiceOperationsFacade.DeleteInvoiceById()? InvoiceStatus.DELETED : selectedInvoice.invoiceStatus;
						  
						}
						CollectionViewSource.GetDefaultView(InvoiceInfos).Refresh();
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
					  if (selectedInvoice !=null)
					  {
						  int index = InvoiceInfos.IndexOf(selectedInvoice);
						  bool isDraft = selectedInvoice.invoiceStatus == InvoiceStatus.DRAFT;
						  MainWindow mainWindow = new MainWindow(selectedInvoice);
						  mainWindow.ShowDialog();

						  
						  MyInvoiceInfo invoiceI = new MyInvoiceInfo();
						  InvoiceViewModel IVM = ((InvoiceViewModel)mainWindow.DataContext);
						  invoiceI.invoice = IVM.Invoice;
						  invoiceI.invoiceStatus = IVM.SelectedInvoice.invoiceStatus;
						  invoiceI.direction = IVM.Direction;
						  invoiceI.myPartner = IVM.Partner;
						  invoiceI.myObject = IVM.Object;
						  invoiceI.invoiceNumber = selectedInvoice.invoiceNumber;
						  invoiceI.inputDate = selectedInvoice.inputDate;
						  invoiceI.cancelReason = selectedInvoice.cancelReason;
						  if (true)//isDraft
						  {							  
							  InvoiceInfos.Insert(index, invoiceI);
							  InvoiceInfos.RemoveAt(index+1);							 
						  }
						  else
						  {
							  InvoiceInfos.Add(invoiceI);
						  }						  
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
								if(queryInvoiceResponse.invoiceInfoList[0].invoiceStatus != InvoiceStatus.FAILED)
								{
									selectedInvoice.invoiceNumber = queryInvoiceResponse.invoiceInfoList[0].registrationNumber;
									selectedInvoice.inputDate = queryInvoiceResponse.invoiceInfoList[0].inputDate;
									if (SessionDataManagerFacade.AddInvoiceToDB(selectedInvoice))
									{
										selectedInvoice.invoiceStatus = InvoiceStatus.CREATED;
										isCreated = true;
									}
									 
								}
								CollectionViewSource.GetDefaultView(InvoiceInfos).Refresh();
							 }
						  }
					  }
				  }));
			}
		}

		

		private RelayCommand getOperationsByDatesCommand;
		public RelayCommand GetOperationsByDatesCommand
		{
			get
			{
				return getOperationsByDatesCommand ??
				  (getOperationsByDatesCommand = new RelayCommand(obj =>
				  {
					  InvoiceInfos.Clear();
					  foreach (MyInvoiceInfo invoiceInfo in SessionDataManagerFacade.GetOperationsInfoList(Criteria))
					  {
						  InvoiceInfos.Add(invoiceInfo);
					  }
					  CollectionViewSource.GetDefaultView(InvoiceInfos).Refresh();
				  }));
			}
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


		private RelayCommand selectPartnerCommand;
		public RelayCommand SelectPartnerCommand
		{
			get
			{
				return selectPartnerCommand ??
				  (selectPartnerCommand = new RelayCommand(obj =>
				  {
					  SelectPartnerWindow selectPartnerWindow = new SelectPartnerWindow();
					  if (selectPartnerWindow.ShowDialog() == true)
					  {
						  selectedInvoice.myPartner.Id = ((SelectPartnerViewModel)selectPartnerWindow.DataContext).SelectedPartner.Id;
					  }
				  }, (o) => selectedInvoice != null && (!selectedInvoice.IsValidCustomer || !selectedInvoice.IsValidSeller)));
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
						  selectedInvoice.myObject.Id = ((SelectObjectViewModel)selectObjectWindow.DataContext).SelectedObject.Id;
					  }
				  }, (o) => selectedInvoice != null));
			}
		}
	}
}
