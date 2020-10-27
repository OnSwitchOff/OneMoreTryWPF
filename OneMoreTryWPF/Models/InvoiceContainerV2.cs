using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OneMoreTryWPF.Models
{
	[Serializable]
	public class invoiceContainerV2
	{
		//[XmlArray("invoiceSet")]
		[XmlArrayItem(ElementName = "invoice")]
		public ObservableCollection<InvoiceV2> invoiceSet;

		public invoiceContainerV2() { }
	}
}
