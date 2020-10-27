using OneMoreTryWPF.Facades;
using OneMoreTryWPF.InvoiceService;
using OneMoreTryWPF.LocalService;
using OneMoreTryWPF.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

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
				if (value!=null)
				{
					isInbound = selectedInvoice.direction == InvoiceDirection.INBOUND;
					isOutbound = selectedInvoice.direction == InvoiceDirection.OUTBOUND;
					isCreated = selectedInvoice.invoiceStatus == InvoiceStatus.CREATED;
					isFailed = selectedInvoice.invoiceStatus == InvoiceStatus.FAILED;
					isDeclinable = selectedInvoice.invoice.invoiceType != ENUMs.InvoiceType.ORDINARY_INVOICE;
					isCreatable = String.IsNullOrEmpty(selectedInvoice.invoiceId.ToString());
				}				
				OnPropertyChanged("SelectedInvoice");
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
							  myInvoiceInfo.invoice = ParsingManager.getInvoiceFromBodyString(invoiceInfo.invoiceBody);
							  myInvoiceInfo.invoiceId = invoiceInfo.invoiceId;
							  myInvoiceInfo.inputDate = invoiceInfo.inputDate;
							  myInvoiceInfo.invoiceStatus = invoiceInfo.invoiceStatus;
							  myInvoiceInfo.cancelReason = invoiceInfo.cancelReason;
							  myInvoiceInfo.direction = Criteria.direction;
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
					  long[] selectedIdList = { selectedInvoice.invoiceId };
					  SessionDataManagerFacade.setSelectedIdList(selectedIdList);
					  selectedInvoice.invoiceStatus = InvoiceServiceOperationsFacade.confirmInvoiceById() ? InvoiceStatus.DELIVERED:selectedInvoice.invoiceStatus;
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
					  if(reasonWindow.ShowDialog()==true)
					  {
						  reason = reasonWindow.reason;
						  long[] selectedIdList = { selectedInvoice.invoiceId };
						  SessionDataManagerFacade.setSelectedIdList(selectedIdList);
						  if(LocalServiceOperationFacade.GenerateIdWithReasonListSignature())
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
							selectedInvoice.invoiceStatus = InvoiceServiceOperationsFacade.DeleteInvoiceById()? InvoiceStatus.DELETED : selectedInvoice.invoiceStatus;
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
					  if(selectedInvoice !=null)
					  {
						  MainWindow mainWindow = new MainWindow(selectedInvoice);
						  mainWindow.ShowDialog();
						  MyInvoiceInfo invoiceI = new MyInvoiceInfo();
						  invoiceI.invoice = ((InvoiceViewModel)mainWindow.DataContext).Invoice;
						  invoiceI.invoiceStatus = InvoiceStatus.DRAFT;
						  invoiceI.direction = InvoiceDirection.OUTBOUND;
						  InvoiceInfos.Add(invoiceI);
					  }
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
		}
	}
}
