using OneMoreTryWPF.ENUMs;
using OneMoreTryWPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OneMoreTryWPF.Facades
{
	static class ParsingManager
	{
		static DateTime zeroDate = new DateTime();
		internal static float ParseStringToFLoat(string str)
		{
			return float.Parse(str);
		}



		internal static T ParseStringToEnum<T>(string str) where T : Enum
		{
			return (T)Enum.Parse(typeof(T), str);
		}

		internal static string getInvoiceBodyString(InvoiceV2 invoice)
		{
			string result;
			XNamespace a = "abstractInvoice.esf";
			XNamespace v2 = "v2.esf";
			XAttribute xA = new XAttribute(XNamespace.Xmlns + "a", a.NamespaceName);
			XAttribute xV2 = new XAttribute(XNamespace.Xmlns + "v2", v2.NamespaceName);
			XElement xInvoice = new XElement(v2 + "invoice", xA, xV2);

			if (invoice.date != "01.01.0001")
			{
				XElement date = new XElement("date", invoice.date);
				xInvoice.Add(date);
			}

			XElement invoiceType = new XElement("invoiceType", invoice.invoiceType);
			xInvoice.Add(invoiceType);

			if(!String.IsNullOrEmpty(invoice.num))
			{
				XElement num = new XElement("num", invoice.num);
				xInvoice.Add(num);
			}

			if (!String.IsNullOrEmpty(invoice.operatorFullname))
			{
				XElement operatorFullname = new XElement("operatorFullname", invoice.operatorFullname);
				xInvoice.Add(operatorFullname);
			}

			if (invoice.relatedInvoice != null)
			{
				XElement relatedInvoice = GetRelatedInvoiceXElement(invoice.relatedInvoice);
				xInvoice.Add(relatedInvoice);
			}

			if (invoice.turnoverDate != "01.01.0001")
			{
				XElement turnoverDate = new XElement("turnoverDate", invoice.turnoverDate);
				xInvoice.Add(turnoverDate);
			}

			if (!String.IsNullOrEmpty(invoice.addInf))
			{
				XElement addInf = new XElement("addInf", invoice.addInf);
				xInvoice.Add(addInf);
			}

			if(invoice.consignee != null)
			{
				XElement consignee = GetConsigneeXElement(invoice.consignee);
				xInvoice.Add(consignee);
			}

			if (invoice.consignor != null)
			{
				XElement consignor = GetConsignorXElement(invoice.consignor);
				xInvoice.Add(consignor);
			}

			if (!String.IsNullOrEmpty(invoice.customerAgentAddress))
			{
				XElement customerAgentAddress = new XElement("customerAgentAddress", invoice.customerAgentAddress);
				xInvoice.Add(customerAgentAddress);
			}

			if (invoice.customerAgentDocDate != "01.01.0001")
			{
				XElement customerAgentDocDate = new XElement("customerAgentDocDate", invoice.customerAgentDocDate);
				xInvoice.Add(customerAgentDocDate);
			}

			if (!String.IsNullOrEmpty(invoice.customerAgentDocNum))
			{
				XElement customerAgentDocNum = new XElement("customerAgentDocNum", invoice.customerAgentDocNum);
				xInvoice.Add(customerAgentDocNum);
			}

			if (!String.IsNullOrEmpty(invoice.customerAgentName))
			{
				XElement customerAgentName = new XElement("customerAgentName", invoice.customerAgentName);
				xInvoice.Add(customerAgentName);
			}

			if (!String.IsNullOrEmpty(invoice.customerAgentTin))
			{
				XElement customerAgentTin = new XElement("customerAgentName", invoice.customerAgentTin);
				xInvoice.Add(customerAgentTin);
			}

			if(invoice.customerParticipants != null)
			{
				if (invoice.customerParticipants.Count>0)
				{
					XElement customerParticipants = new XElement("customerParticipants");
					foreach (ParticipantV2 participant  in invoice.customerParticipants)
					{
						XElement XParticipant = GetParticipantXElement(participant);
						customerParticipants.Add(participant);
					}
					xInvoice.Add(customerParticipants);
				}				
			}

			if (invoice.customers != null)
			{
				if (invoice.customers.Count > 0)
				{
					XElement customers = new XElement("customers");
					foreach (CustomerV2 customer in invoice.customers)
					{
						XElement XCustomer = GetCustomerXElement(customer);
						customers.Add(XCustomer);
					}
					xInvoice.Add(customers);
				}
			}

			if (invoice.datePaper != "01.01.0001")
			{
				XElement datePaper = new XElement("datePaper", invoice.datePaper);
				xInvoice.Add(datePaper);
			}

			if (invoice.deliveryDocDate != "01.01.0001")
			{
				XElement deliveryDocDate = new XElement("deliveryDocDate", invoice.deliveryDocDate);
				xInvoice.Add(deliveryDocDate);
			}

			if (!String.IsNullOrEmpty(invoice.deliveryDocNum))
			{
				XElement deliveryDocNum = new XElement("deliveryDocNum", invoice.deliveryDocNum);
				xInvoice.Add(deliveryDocNum);
			}

			if(invoice.deliveryTerm != null)
			{
				XElement deliveryTerm = GetDeliveryTermXElement(invoice.deliveryTerm);
				xInvoice.Add(deliveryTerm);
			}

			if(invoice.productSet != null)
			{
				XElement productSet = GetProductSetXElement(invoice.productSet);
				xInvoice.Add(productSet);
			}

			if (invoice.publicOffice != null)
			{
				XElement publicOffice = GetPublicOfficeXElement(invoice.publicOffice);
				xInvoice.Add(publicOffice);
			}

			if (invoice.reasonPaper !=null)
			{
				XElement reasonPaper = new XElement("reasonPaper", invoice.reasonPaper);
				xInvoice.Add(reasonPaper);
			}

			if (!String.IsNullOrEmpty(invoice.sellerAgentAddress))
			{
				XElement sellerAgentAddress = new XElement("sellerAgentAddress", invoice.sellerAgentAddress);
				xInvoice.Add(sellerAgentAddress);
			}

			if (invoice.sellerAgentDocDate != "01.01.0001")
			{
				XElement sellerAgentDocDate = new XElement("sellerAgentDocDate", invoice.sellerAgentDocDate);
				xInvoice.Add(sellerAgentDocDate);
			}

			if (!String.IsNullOrEmpty(invoice.sellerAgentDocNum))
			{
				XElement sellerAgentDocNum = new XElement("sellerAgentDocNum", invoice.sellerAgentDocNum);
				xInvoice.Add(sellerAgentDocNum);
			}

			if (!String.IsNullOrEmpty(invoice.sellerAgentName))
			{
				XElement sellerAgentName = new XElement("sellerAgentName", invoice.sellerAgentName);
				xInvoice.Add(sellerAgentName);
			}

			if (!String.IsNullOrEmpty(invoice.sellerAgentTin))
			{
				XElement sellerAgentTin = new XElement("sellerAgentTin", invoice.sellerAgentTin);
				xInvoice.Add(sellerAgentTin);
			}

			if (invoice.sellerParticipants != null)
			{
				if (invoice.sellerParticipants.Count > 0)
				{
					XElement sellerParticipants = new XElement("sellerParticipants");
					foreach (ParticipantV2 participant in invoice.sellerParticipants)
					{
						XElement XParticipant = GetParticipantXElement(participant);
						sellerParticipants.Add(participant);
					}
					xInvoice.Add(sellerParticipants);
				}
			}

			if (invoice.sellers != null)
			{
				if (invoice.sellers.Count > 0)
				{
					XElement sellers = new XElement("sellers");
					foreach (SellerV2 seller in invoice.sellers)
					{
						XElement XSeller = GetSellerXElement(seller);
						sellers.Add(XSeller);
					}
					xInvoice.Add(sellers);
				}
			}

			//XElement num = new XElement("num", "123");
			//xInvoice.Add(num);

			/*foreach (PropertyInfo pi in typeof(InvoiceV2).GetProperties())
			{
				object value = pi.GetValue(invoice,null);
				XElement stringEl1 = new XElement(pi.Name, value);
				xInvoice.Add(stringEl1);				

				/*if (value != null)
				{
					switch (fi.FieldType.ToString())
					{
						case "System.String":
							if (String.IsNullOrEmpty(value.ToString()))
							{
								XElement stringEl = new XElement(fi.Name, value);
								xInvoice.Add(stringEl);
							}
							break;
						case "System.DateTime":
							if ((DateTime)value != zeroDate)
							{
								XElement dateEl = new XElement(fi.Name, ((DateTime)value).ToString("dd.MM.yyyy"));
								xInvoice.Add(dateEl);
							}
							break;
						default:
							Regex objectRegex = new Regex(@"^ESF_kz[.]");
							bool isObject = objectRegex.IsMatch(fi.FieldType.ToString());
							Regex listRegex = new Regex(@"System[.]Collections[.]Generic[.]List`1[[]ESF_kz[.]\w*[]]$");
							bool isList = listRegex.IsMatch(fi.FieldType.ToString());
							if (isObject)
							{
								Regex typeRegex = new Regex(@".*Type$");
								if (typeRegex.IsMatch(fi.FieldType.ToString()))
								{
									if ((int)value != 0)
									{
										XElement enumEl = new XElement(fi.Name, value.ToString());
										xInvoice.Add(enumEl);
									}
								}
								else
								{
									var attrs = fi.GetCustomAttributes(true);
									Match m2 = Regex.Match(fi.FieldType.ToString(), "^ESF_kz[.](.*?)$");
									string tagName = m2.Groups[1].ToString().Replace("V2", "");
									tagName = fi.Name;
									xInvoice.Add(getXmlStringByObject(value, tagName));
								}
							}
							else if (isList)
							{
								string lisTagName = fi.Name;
								string itemTagName = "item";
								foreach (var attr in fi.GetCustomAttributes(true))
								{
									if (attr.GetType().ToString() == "System.Xml.Serialization.XmlArrayItemAttribute")
									{
										itemTagName = ((XmlArrayItemAttribute)attr).ElementName;
									}
								}
								object listEl = getXmlStringByList(value, lisTagName, itemTagName);
								if (listEl != null)
								{
									xInvoice.Add(listEl);
								}
							}
							break;
					}
				}
			}*/

			result = xInvoice.ToString();
			return result;
		}

		private static XElement GetSellerXElement(SellerV2 seller)
		{
			XElement XSeller = new XElement("seller");

			if (!String.IsNullOrEmpty(seller.address))
			{
				XElement address = new XElement("address", seller.address);
				XSeller.Add(address);
			}

			if (!String.IsNullOrEmpty(seller.bank))
			{
				XElement bank = new XElement("bank", seller.bank);
				XSeller.Add(bank);
			}

			if (!String.IsNullOrEmpty(seller.bik))
			{
				XElement bik = new XElement("bik", seller.bik);
				XSeller.Add(bik);
			}

			if (!String.IsNullOrEmpty(seller.branchTin))
			{
				XElement branchTin = new XElement("branchTin", seller.branchTin);
				XSeller.Add(branchTin);
			}

			if (!String.IsNullOrEmpty(seller.certificateNum))
			{
				XElement certificateNum = new XElement("certificateNum", seller.certificateNum);
				XSeller.Add(certificateNum);
			}

			if (!String.IsNullOrEmpty(seller.certificateSeries))
			{
				XElement certificateSeries = new XElement("certificateSeries", seller.certificateSeries);
				XSeller.Add(certificateSeries);
			}

			if (!String.IsNullOrEmpty(seller.iik))
			{
				XElement iik = new XElement("iik", seller.iik);
				XSeller.Add(iik);
			}

			if (seller.isBranchNonResident)
			{
				XElement isBranchNonResident = new XElement("isBranchNonResident", seller.isBranchNonResident);
				XSeller.Add(isBranchNonResident);
			}

			if (!String.IsNullOrEmpty(seller.kbe))
			{
				XElement kbe = new XElement("kbe", seller.kbe);
				XSeller.Add(kbe);
			}

			if (!String.IsNullOrEmpty(seller.name))
			{
				XElement name = new XElement("name", seller.name);
				XSeller.Add(name);
			}

			if (!String.IsNullOrEmpty(seller.reorganizedTin))
			{
				XElement reorganizedTin = new XElement("reorganizedTin", seller.reorganizedTin);
				XSeller.Add(reorganizedTin);
			}

			if (seller.shareParticipation > 0 && seller.shareParticipation < 1)
			{
				XElement shareParticipation = new XElement("shareParticipation", seller.shareParticipation);
				XSeller.Add(shareParticipation);
			}

			if (seller.statuses != null)
			{
				if (seller.statuses.Count > 0)
				{
					XElement statuses = new XElement("statuses");
					bool flag = false;
					foreach (UserStatus status in seller.statuses)
					{
						object XStatus = GetStatusXElement(status);
						if (XStatus != null)
						{
							statuses.Add((XElement)XStatus);
							flag = true;
						}
					}
					if (flag)
					{
						XSeller.Add(statuses);
					}
				}
			}

			if (!String.IsNullOrEmpty(seller.tin))
			{
				XElement tin = new XElement("tin", seller.tin);
				XSeller.Add(tin);
			}

			if (!String.IsNullOrEmpty(seller.trailer))
			{
				XElement trailer = new XElement("trailer", seller.trailer);
				XSeller.Add(trailer);
			}

			return XSeller;
		}

		private static XElement GetPublicOfficeXElement(PublicOffice publicOffice)
		{
			XElement XPublicOffice = new XElement("publicOffice");
			if (!String.IsNullOrEmpty(publicOffice.bik))
			{
				XElement bik = new XElement("bik", publicOffice.bik);
				XPublicOffice.Add(bik);
			}
			if (!String.IsNullOrEmpty(publicOffice.iik))
			{
				XElement iik = new XElement("iik", publicOffice.iik);
				XPublicOffice.Add(iik);
			}
			if (!String.IsNullOrEmpty(publicOffice.payPurpose))
			{
				XElement payPurpose = new XElement("payPurpose", publicOffice.payPurpose);
				XPublicOffice.Add(payPurpose);
			}
			if (!String.IsNullOrEmpty(publicOffice.productCode))
			{
				XElement productCode = new XElement("productCode", publicOffice.productCode);
				XPublicOffice.Add(productCode);
			}
			return XPublicOffice;
		}

		private static XElement GetProductSetXElement(ProductSetV2 productSet)
		{
			XElement XProductSet = new XElement("productSet");
			if (!String.IsNullOrEmpty(productSet.currencyCode))
			{
				XElement currencyCode = new XElement("currencyCode", productSet.currencyCode);
				XProductSet.Add(currencyCode);
			}
			if (productSet.currencyRate != null)
			{
				XElement currencyRate = new XElement("currencyRate ", productSet.currencyRate);
				XProductSet.Add(currencyRate);
			}
			if (productSet.ndsRateType == NdsRateType.WITHOUT_NDS_NOT_KZ)
			{
				XElement ndsRateType = new XElement("ndsRateType", NdsRateType.WITHOUT_NDS_NOT_KZ);
				XProductSet.Add(ndsRateType);
			}
			if(productSet.products != null)
			{
				if(productSet.products.Count >0)
				{
					XElement products = new XElement("products");
					foreach (ProductV2 prod in productSet.products)
					{
						XElement product = GetProductXElement(prod);
						products.Add(product);
					}
					XProductSet.Add(products);
				}
			}
			XElement totalExciseAmount = new XElement("totalExciseAmount", productSet.totalExciseAmount);
			XProductSet.Add(totalExciseAmount);
			XElement totalNdsAmount = new XElement("totalNdsAmount", productSet.totalNdsAmount);
			XProductSet.Add(totalNdsAmount);
			XElement totalPriceWithTax = new XElement("totalPriceWithTax", productSet.totalPriceWithTax);
			XProductSet.Add(totalPriceWithTax);
			XElement totalPriceWithoutTax = new XElement("totalPriceWithoutTax", productSet.totalPriceWithoutTax);
			XProductSet.Add(totalPriceWithoutTax);
			XElement totalTurnoverSize = new XElement("totalTurnoverSize", productSet.totalTurnoverSize);
			XProductSet.Add(totalTurnoverSize);

			return XProductSet;
		}

		private static XElement GetProductXElement(ProductV2 prod)
		{
			XElement product = new XElement("product");
			if (!String.IsNullOrEmpty(prod.additional))
			{
				XElement additional = new XElement("additional", prod.additional);
				product.Add(additional);
			}


			XElement catalogTruId = String.IsNullOrEmpty(prod.catalogTruId) ? new XElement("catalogTruId", 1) : new XElement("catalogTruId", prod.catalogTruId);
			product.Add(catalogTruId);
			
			if (!String.IsNullOrEmpty(prod.description))
			{
				XElement description = new XElement("description", prod.description);
				product.Add(description);
			}
			if (prod.exciseAmount != 0)
			{ 
				XElement exciseAmount = new XElement("exciseAmount", prod.exciseAmount);
				product.Add(exciseAmount);
			}
			if(prod.exciseRate != 0)
			{
				XElement exciseRate = new XElement("exciseRate", prod.exciseRate);
				product.Add(exciseRate);
			}

			if (!String.IsNullOrEmpty(prod.kpvedCode))
			{
				XElement kpvedCode = new XElement("kpvedCode", prod.kpvedCode);
				product.Add(kpvedCode);
			}

			XElement ndsAmount = new XElement("ndsAmount", prod.ndsAmount);
			product.Add(ndsAmount);
			if (prod.ndsRate != 0)
			{
				XElement ndsRate = new XElement("ndsRate", prod.ndsRate);
				product.Add(ndsRate);
			}
			XElement priceWithTax = new XElement("priceWithTax", prod.priceWithTax);
			product.Add(priceWithTax);
			XElement priceWithoutTax = new XElement("priceWithoutTax", prod.priceWithoutTax);
			product.Add(priceWithoutTax);

			if (!String.IsNullOrEmpty(prod.productDeclaration))
			{
				XElement productDeclaration = new XElement("productDeclaration", prod.productDeclaration);
				product.Add(productDeclaration);
			}
			if (!String.IsNullOrEmpty(prod.productNumberInDeclaration))
			{
				XElement productNumberInDeclaration = new XElement("productNumberInDeclaration", prod.productNumberInDeclaration);
				product.Add(productNumberInDeclaration);
			}
			if (!String.IsNullOrEmpty(prod.quantity))
			{
				XElement quantity = new XElement("quantity", prod.quantity);
				product.Add(quantity);
			}
			if (!String.IsNullOrEmpty(prod.tnvedName))
			{
				XElement tnvedName = new XElement("tnvedName", prod.tnvedName);
				product.Add(tnvedName);
			}
			XElement truOriginCode = new XElement("truOriginCode", prod.truOriginCode);
			product.Add(truOriginCode);
			XElement turnoverSize = new XElement("turnoverSize", prod.turnoverSize);
			product.Add(turnoverSize);
			if (!String.IsNullOrEmpty(prod.unitCode))
			{
				XElement unitCode = new XElement("unitCode", prod.unitCode);
				product.Add(unitCode);
			}
			if (!String.IsNullOrEmpty(prod.unitNomenclature))
			{
				XElement unitNomenclature = new XElement("unitNomenclature", prod.unitNomenclature);
				product.Add(unitNomenclature);
			}
			if (prod.unitPrice != null)
			{
				XElement unitPrice = new XElement("unitPrice", prod.unitPrice);
				product.Add(unitPrice);
			}
			return product;
		}

		private static XElement GetDeliveryTermXElement(DeliveryTermV2 deliveryTerm)
		{
			XElement XDeliveryTerm = new XElement("deliveryTerm");
			if (deliveryTerm.contractDate != "01.01.0001")
			{
				XElement contractDate = new XElement("contractDate", deliveryTerm.contractDate);
				XDeliveryTerm.Add(contractDate);
			}
			if(!String.IsNullOrEmpty(deliveryTerm.contractNum))
			{
				XElement contractNum = new XElement("contractNum", deliveryTerm.contractNum);
				XDeliveryTerm.Add(contractNum);
			}
			if (!String.IsNullOrEmpty(deliveryTerm.deliveryConditionCode))
			{
				XElement deliveryConditionCode = new XElement("deliveryConditionCode", deliveryTerm.deliveryConditionCode);
				XDeliveryTerm.Add(deliveryConditionCode);
			}
			if (!String.IsNullOrEmpty(deliveryTerm.destination))
			{
				XElement destination = new XElement("destination", deliveryTerm.destination);
				XDeliveryTerm.Add(destination);
			}
			XElement hasContract = new XElement("hasContract", deliveryTerm.hasContract);
			XDeliveryTerm.Add(hasContract);

			if (!String.IsNullOrEmpty(deliveryTerm.term))
			{
				XElement term = new XElement("term", deliveryTerm.term);
				XDeliveryTerm.Add(term);
			}

			if (!String.IsNullOrEmpty(deliveryTerm.transportTypeCode))
			{
				XElement transportTypeCode = new XElement("transportTypeCode", deliveryTerm.transportTypeCode);
				XDeliveryTerm.Add(transportTypeCode);
			}
			if (!String.IsNullOrEmpty(deliveryTerm.warrant))
			{
				XElement warrant = new XElement("warrant", deliveryTerm.warrant);
				XDeliveryTerm.Add(warrant);
			}
			if (deliveryTerm.warrantDate != "01.01.0001")
			{
				XElement warrantDate = new XElement("warrantDate", deliveryTerm.warrantDate);
				XDeliveryTerm.Add(warrantDate);
			}
			return XDeliveryTerm;
		}

		private static XElement GetCustomerXElement(CustomerV2 customer)
		{
			XElement XCustomer = new XElement("customer");

			if (!String.IsNullOrEmpty(customer.address))
			{
				XElement address = new XElement("address", customer.address);
				XCustomer.Add(address);
			}

			if (!String.IsNullOrEmpty(customer.branchTin))
			{
				XElement branchTin = new XElement("branchTin", customer.branchTin);
				XCustomer.Add(branchTin);
			}

			if (!String.IsNullOrEmpty(customer.countryCode))
			{
				XElement countryCode = new XElement("countryCode", customer.countryCode);
				XCustomer.Add(countryCode);
			}

			if (!String.IsNullOrEmpty(customer.name))
			{
				XElement name = new XElement("name", customer.name);
				XCustomer.Add(name);
			}

			if (!String.IsNullOrEmpty(customer.reorganizedTin))
			{
				XElement reorganizedTin = new XElement("reorganizedTin", customer.reorganizedTin);
				XCustomer.Add(reorganizedTin);
			}

			if (customer.shareParticipation > 0 && customer.shareParticipation<1)
			{
				XElement shareParticipation = new XElement("shareParticipation", customer.shareParticipation);
				XCustomer.Add(shareParticipation);
			}

			if (customer.statuses != null)
			{
				if (customer.statuses.Count>0)
				{
					XElement statuses = new XElement("statuses");
					bool flag = false;
					foreach (UserStatus status in customer.statuses)
					{
						object XStatus = GetStatusXElement(status);
						if(XStatus != null)
						{
							statuses.Add((XElement)XStatus);
							flag = true;
						}
					}
					if(flag)
					{
						XCustomer.Add(statuses);
					}					
				}
			}

			if (!String.IsNullOrEmpty(customer.tin))
			{
				XElement tin = new XElement("tin", customer.tin);
				XCustomer.Add(tin);
			}

			if (!String.IsNullOrEmpty(customer.trailer))
			{
				XElement trailer = new XElement("trailer", customer.trailer);
				XCustomer.Add(trailer);
			}

			return XCustomer;
		}

		private static object GetStatusXElement(UserStatus status)
		{
			object XStatus = status.isChecked? new XElement("status", status.type) : null;
			return XStatus;
		}

		private static XElement GetParticipantXElement(ParticipantV2 participant)
		{
			XElement XParticipant = new XElement("Participant");
			if(participant.productShares !=null)
			{
				if(participant.productShares.Count>0)
				{
					XElement productShares = new XElement("productShares");
					foreach (ProductShare productShare in participant.productShares)
					{
						XElement share = GetShareXElement(productShare);
						productShares.Add(share);
					}
					XParticipant.Add(productShares);
				}
			}
			if (!String.IsNullOrEmpty(participant.reorganizedTin))
			{
				XElement reorganizedTin = new XElement("reorganizedTin", participant.reorganizedTin);
				XParticipant.Add(reorganizedTin);
			}
			if (!String.IsNullOrEmpty(participant.tin))
			{
				XElement tin = new XElement("tin", participant.tin);
				XParticipant.Add(tin);
			}
			return XParticipant;
		}

		private static XElement GetShareXElement(ProductShare productShare)
		{
			XElement share = new XElement("share");

			if (!String.IsNullOrEmpty(productShare.additional))
			{
				XElement additional = new XElement("additional", productShare.additional);
				share.Add(additional);
			}

			XElement exciseAmount = new XElement("exciseAmount", productShare.exciseAmount);
			share.Add(exciseAmount);

			XElement ndsAmount = new XElement("ndsAmount", productShare.ndsAmount);
			share.Add(ndsAmount);

			XElement priceWithTax = new XElement("priceWithTax", productShare.priceWithTax);
			share.Add(priceWithTax);

			XElement priceWithoutTax = new XElement("priceWithoutTax", productShare.priceWithoutTax);
			share.Add(priceWithoutTax);

			XElement productNumber = new XElement("productNumber", productShare.productNumber);
			share.Add(productNumber);

			if (!String.IsNullOrEmpty(productShare.quantity))
			{
				XElement quantity = new XElement("quantity", productShare.quantity);
				share.Add(quantity);
			}				

			XElement turnoverSize = new XElement("turnoverSize", productShare.turnoverSize);
			share.Add(turnoverSize);

			return share;
		}

		private static XElement GetConsigneeXElement(ConsigneeV2 consignee)
		{
			XElement XConsignee = new XElement("consignee");
			if (!String.IsNullOrEmpty(consignee.address))
			{
				XElement address = new XElement("address", consignee.address);
				XConsignee.Add(address);
			}
			if (!String.IsNullOrEmpty(consignee.countryCode))
			{
				XElement countryCode = new XElement("countryCode", consignee.countryCode);
				XConsignee.Add(countryCode);
			}
			if (!String.IsNullOrEmpty(consignee.name))
			{
				XElement name = new XElement("name", consignee.name);
				XConsignee.Add(name);
			}
			if (!String.IsNullOrEmpty(consignee.tin))
			{
				XElement tin = new XElement("tin", consignee.tin);
				XConsignee.Add(tin);
			}
			return XConsignee;
		}

		private static XElement GetConsignorXElement(Consignor consignor)
		{
			XElement XConsignee = new XElement("consignor");
			if (!String.IsNullOrEmpty(consignor.address))
			{
				XElement address = new XElement("address", consignor.address);
				XConsignee.Add(address);
			}
			if (!String.IsNullOrEmpty(consignor.name))
			{
				XElement name = new XElement("name", consignor.name);
				XConsignee.Add(name);
			}
			if (!String.IsNullOrEmpty(consignor.tin))
			{
				XElement tin = new XElement("tin", consignor.tin);
				XConsignee.Add(tin);
			}
			return XConsignee;
		}

		private static XElement GetRelatedInvoiceXElement(RelatedInvoice relatedInvoice)
		{

			XElement XRelatedInvoice =  new XElement("relatedInvoice");
			if (relatedInvoice.date != "01.01.0001")
			{
				XElement relatedInvoiceDate = new XElement("date", relatedInvoice.date);
				XRelatedInvoice.Add(relatedInvoiceDate);
			}
			if (!String.IsNullOrEmpty(relatedInvoice.num))
			{
				XElement relatedInvoiceNum = new XElement("num", relatedInvoice.num);
				XRelatedInvoice.Add(relatedInvoiceNum);
			}
			if (!String.IsNullOrEmpty(relatedInvoice.registrationNumber))
			{
				XElement relatedInvoiceRegistrationNumber = new XElement("registrationNumber", relatedInvoice.registrationNumber);
				XRelatedInvoice.Add(relatedInvoiceRegistrationNumber);
			}				
			return XRelatedInvoice;
		}

		private static object getXmlStringByList(object value, string tagName, string itemTagName)
		{
			XElement listEl = null;

			//Type itemType = value.GetType().GetGenericArguments()[0];

			int count = (int)value.GetType().GetProperty("Count").GetValue(value, null);
			if (count > 0)
			{
				listEl = new XElement(tagName);
				//string tag = 
				if (tagName == "statuses")
				{
					for (int i = 0; i < count; i++)
					{
						object[] index = { i };
						object item = value.GetType().GetProperty("Item").GetValue(value, index);
						XElement statusEl = new XElement("status", item);
						listEl.Add(statusEl);
					}
				}
				else
				{
					for (int i = 0; i < count; i++)
					{
						object[] index = { i };
						object item = value.GetType().GetProperty("Item").GetValue(value, index);

						string itemTag = itemTagName;
						listEl.Add(getXmlStringByObject(item, itemTag));
					}
				}
			}
			return listEl;
		}

		private static XElement getXmlStringByObject(object value, string tagName)
		{
			List<string> hideIfZeroTagList = getFloatExclusionList();

			XElement classEl = new XElement(tagName);

			foreach (FieldInfo fi in value.GetType().GetFields())
			{
				object fieldValue = value.GetType().GetField(fi.Name).GetValue(value);
				if (fieldValue != null)
				{
					switch (fi.FieldType.ToString())
					{
						case "System.String":
							if (fieldValue != "")
							{
								XElement stringEl = new XElement(fi.Name, fieldValue);
								classEl.Add(stringEl);
							}
							break;
						case "System.DateTime":
							if ((DateTime)fieldValue != zeroDate)
							{
								XElement dateEl = new XElement(fi.Name, ((DateTime)fieldValue).ToString("dd.MM.yyyy"));
								classEl.Add(dateEl);
							}
							break;
						case "System.Single":
							if (!(hideIfZeroTagList.Contains(fi.Name) && (float)fieldValue == 0))
							{
								XElement floatEl = new XElement(fi.Name, fieldValue);
								classEl.Add(floatEl);
							}
							break;
						case "System.Boolean":
							if (!(fi.Name == "isBranchNonResident" && (bool)fieldValue == false))
							{
								XElement boolEl = new XElement(fi.Name, fieldValue);
								classEl.Add(boolEl);
							}
							break;
						case "System.Int32":
							if (!(hideIfZeroTagList.Contains(fi.Name) && (int)fieldValue == 0))
							{
								XElement intEl = new XElement(fi.Name, fieldValue);
								classEl.Add(intEl);
							}
							break;
						default:
							Regex objectRegex = new Regex(@"^ESF_kz[.]");
							bool isObject = objectRegex.IsMatch(fi.FieldType.ToString());
							Regex listRegex = new Regex(@"System[.]Collections[.]Generic[.]List`1[[]ESF_kz[.]\w*[]]$");
							bool isList = listRegex.IsMatch(fi.FieldType.ToString());
							if (isObject)
							{
								Regex typeRegex = new Regex(@".*(Type)|(Code)$");

								if (typeRegex.IsMatch(fi.FieldType.ToString()))
								{

									if ((int)fieldValue != 0)
									{
										XElement enumEl = new XElement(fi.Name, fieldValue.ToString());
										classEl.Add(enumEl);
									}
								}
								else
								{
									//Match m2 = Regex.Match(fi.FieldType.ToString(), "^ESF_kz[.](.*?)$");
									//string tag = m2.Groups[1].ToString().Replace("V2", "");
									string tag = fi.Name;
									classEl.Add(getXmlStringByObject(fieldValue, tag));
								}
							}
							else if (isList)
							{
								string lisTagName = fi.Name;
								string itemTagName = "item";
								foreach (var attr in fi.GetCustomAttributes(true))
								{
									if (attr.GetType().ToString() == "System.Xml.Serialization.XmlArrayItemAttribute")
									{
										itemTagName = ((XmlArrayItemAttribute)attr).ElementName;
									}
								}
								object listEl = getXmlStringByList(fieldValue, lisTagName, itemTagName);
								if (listEl != null)
								{
									classEl.Add(listEl);
								}
							}
							break;
					}
				}
			}

			return classEl;
		}

		private static List<string> getFloatExclusionList()
		{
			List<string> hideIfZeroTagList = new List<string>();
			hideIfZeroTagList.Add("shareParticipation");
			hideIfZeroTagList.Add("currencyRate");
			hideIfZeroTagList.Add("exciseAmount");
			hideIfZeroTagList.Add("exciseRate");
			hideIfZeroTagList.Add("ndsRate");
			hideIfZeroTagList.Add("quantity");
			hideIfZeroTagList.Add("unitPrice");
			return hideIfZeroTagList;
		}

		/*internal static invoiceContainerV2 ParseInvoiceConatinerV2XML(XmlDocument xDoc)
		{
			XmlElement xRoot = xDoc.DocumentElement;//invoiceContainer
			invoiceContainerV2 invoiceContainer = new invoiceContainerV2();
			invoiceContainer.invoiceSet = new List<InvoiceV2>();
			XmlNode xInvoiceSet = xRoot.FirstChild;//invoiceSet

			foreach (XmlNode invoice in xInvoiceSet)
			{
				invoiceContainer.invoiceSet.Add(ParseInvoiceBody(invoice));
			}
			return invoiceContainer;
		}*/


		internal static InvoiceV2 getInvoiceFromBodyString(string invoiceBody)
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(invoiceBody);
			XmlNode newNode = doc.DocumentElement;
			InvoiceV2 invoice = ParseInvoiceBody(newNode);
			return invoice;
		}

		internal static InvoiceV2 ParseInvoiceBody(XmlNode invoiceXml)
		{
			InvoiceV2 invoiceV2 = new InvoiceV2();
			if (invoiceXml.Name == "v2:invoice")
			{
				foreach (XmlNode item in invoiceXml)
				{
					if (item.InnerText != "")
					{
						switch (item.Name)
						{
							case "date":
								try
								{
									invoiceV2.date = item.InnerText;
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "invoiceType":
								try
								{
									invoiceV2.invoiceType = (InvoiceType)ParseInvoiceType(item.InnerText);
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "num":
								try
								{
									invoiceV2.num = item.InnerText;
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "operatorFullname":
								try
								{
									invoiceV2.operatorFullname = item.InnerText;
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "relatedInvoice":
								try
								{
									invoiceV2.relatedInvoice = ParseRelatedInvoice(item);
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "turnoverDate":
								try
								{
									invoiceV2.turnoverDate = item.InnerText;
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "addInf":
								try
								{
									invoiceV2.addInf = item.InnerText;
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "consignee":
								try
								{
									invoiceV2.consignee = ParseConsignee(item);
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}

								break;
							case "consignor":
								try
								{
									invoiceV2.consignor = ParseConsignor(item);
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "customerAgentAddress":
								invoiceV2.customerAgentAddress = item.InnerText;
								break;
							case "customerAgentDocDate":
								try
								{
									invoiceV2.customerAgentDocDate = item.InnerText;
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "customerAgentDocNum":
								invoiceV2.customerAgentDocNum = item.InnerText;
								break;
							case "customerAgentName":
								invoiceV2.customerAgentName = item.InnerText;
								break;
							case "customerAgentTin":
								invoiceV2.customerAgentTin = item.InnerText;
								break;
							case "customerParticipants":
								try
								{
									invoiceV2.customerParticipants = ParseCustomerParticipants(item);
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "customers":
								invoiceV2.customers = ParseCustomers(item);
								break;
							case "datePaper":
								try
								{
									invoiceV2.datePaper = item.InnerText;
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "deliveryDocDate":
								try
								{
									invoiceV2.deliveryDocDate = item.InnerText;
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "deliveryDocNum":
								invoiceV2.deliveryDocNum = item.InnerText;
								break;
							case "deliveryTerm":
								try
								{
									invoiceV2.deliveryTerm = ParseDeliveryTerm(item);
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}

								break;
							case "productSet":
								try
								{
									invoiceV2.productSet = ParseProductSet(item);
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}

								break;
							case "publicOffice":
								try
								{
									invoiceV2.publicOffice = ParsePublicOffice(item);
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "reasonPaper":
								invoiceV2.reasonPaper = (PaperReasonType)Enum.Parse(typeof(PaperReasonType), item.InnerText);
								break;
							case "sellerAgentAddress":
								invoiceV2.sellerAgentAddress = item.InnerText;
								break;
							case "sellerAgentDocDate":
								try
								{
									invoiceV2.sellerAgentDocDate = item.InnerText;
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "sellerAgentDocNum":
								invoiceV2.sellerAgentDocNum = item.InnerText;
								break;
							case "sellerAgentName":
								invoiceV2.sellerAgentName = item.InnerText;
								break;
							case "sellerAgentTin":
								invoiceV2.sellerAgentTin = item.InnerText;
								break;
							case "sellerParticipants":
								try
								{
									invoiceV2.sellerParticipants = ParseSellerParticipants(item);
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							case "sellers":
								try
								{
									invoiceV2.sellers = ParseSellers(item);
								}
								catch (Exception e)
								{
									//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
								}
								break;
							default:
								break;
						}
					}
				}
			}
			return invoiceV2;
		}

		private static ObservableCollection<SellerV2> ParseSellers(XmlNode item)
		{
			ObservableCollection<SellerV2> sellers = new ObservableCollection<SellerV2>();
			foreach (XmlNode seller in item)
			{
				SellerV2 sellerV2 = ParseSeller(seller);
				sellers.Add(sellerV2);
			}
			return sellers;
		}


		private static SellerV2 ParseSeller(XmlNode seller)
		{
			SellerV2 sellerV2 = new SellerV2();
			foreach (XmlNode subitem in seller)
			{
				if (subitem.InnerText != "")
				{
					switch (subitem.Name)
					{
						case "address":
							sellerV2.address = subitem.InnerText;
							break;
						case "bank":
							sellerV2.bank = subitem.InnerText;
							break;
						case "bik":
							sellerV2.bik = subitem.InnerText;
							break;
						case "branchTin":
							sellerV2.branchTin = subitem.InnerText;
							break;
						case "certificateNum":
							sellerV2.certificateNum = subitem.InnerText;
							break;
						case "certificateSeries":
							sellerV2.certificateSeries = subitem.InnerText;
							break;
						case "iik":
							sellerV2.iik = subitem.InnerText;
							break;
						case "isBranchNonResident":
							sellerV2.isBranchNonResident = bool.Parse(subitem.InnerText);
							break;
						case "kbe":
							sellerV2.kbe = subitem.InnerText;
							break;
						case "name":
							sellerV2.name = subitem.InnerText;
							break;
						case "reorganizedTin":
							sellerV2.reorganizedTin = subitem.InnerText;
							break;
						case "shareParticipation":
							sellerV2.shareParticipation = float.Parse(subitem.InnerText);
							break;
						case "statuses":
							sellerV2.statuses = ParseSellerStatuses(subitem);
							break;
						case "tin":
							sellerV2.tin = subitem.InnerText;
							break;
						case "trailer":
							sellerV2.trailer = subitem.InnerText;
							break;
						default:
							break;
					}
				}
			}
			return sellerV2;
		}

		private static ObservableCollection<UserStatus> ParseSellerStatuses(XmlNode node)
		{
			List<SellerType> list = new List<SellerType>();

			foreach (XmlNode status in node)
			{
				var result = ParseType(status.InnerText, typeof(SellerType));
				if (result != null)
				{
					list.Add((SellerType)result);
				}
			}

			ObservableCollection<UserStatus> statuses = new ObservableCollection<UserStatus>();

			foreach (SellerType status in (SellerType[])Enum.GetValues(typeof(SellerType)))
			{
				UserStatus tmp = new UserStatus();
				tmp.type = status;
				tmp.isChecked = list.Contains(status);
				statuses.Add(tmp);
			}
			return statuses;
		}

		private static object ParseType(string innerText, Type type)
		{
			try
			{
				return Enum.Parse(type, innerText);
			}
			catch (Exception)
			{
				return null;
			}
		}

		private static ObservableCollection<ParticipantV2> ParseSellerParticipants(XmlNode item)
		{
			ObservableCollection<ParticipantV2> sellerParticipants = new ObservableCollection<ParticipantV2>();
			foreach (XmlNode participant in item)
			{
				ParticipantV2 sellerParticipant = ParseSellerParticipant(participant);
				sellerParticipants.Add(sellerParticipant);
			}
			return sellerParticipants;
		}

		private static ParticipantV2 ParseSellerParticipant(XmlNode participant)
		{
			ParticipantV2 sellerParticipant = new ParticipantV2();
			foreach (XmlNode node in participant)
			{
				switch (node.Name)
				{
					case "productShares":
						sellerParticipant.productShares = ParseProductShares(node);
						break;
					case "reorganizedTin":
						sellerParticipant.reorganizedTin = node.InnerText;
						break;
					case "tin":
						sellerParticipant.tin = node.InnerText;
						break;
					default:
						break;
				}
			}
			return sellerParticipant;
		}

		private static PublicOffice ParsePublicOffice(XmlNode item)
		{
			PublicOffice publicOffice = new PublicOffice();
			foreach (XmlNode node in item)
			{
				if (node.InnerText != "")
				{
					switch (node.Name)
					{
						case "bik":
							publicOffice.bik = node.InnerText;
							break;
						case "iik":
							publicOffice.iik = node.InnerText;
							break;
						case "payPurpose":
							publicOffice.payPurpose = node.InnerText;
							break;
						case "productCode":
							publicOffice.productCode = node.InnerText;
							break;
						default:
							break;
					}
				}
			}
			return publicOffice;
		}

		private static ProductSetV2 ParseProductSet(XmlNode item)
		{
			ProductSetV2 productSet = new ProductSetV2();
			foreach (XmlNode node in item)
			{
				if (node.InnerText != "")
				{
					switch (node.Name)
					{
						case "currencyCode":
							productSet.currencyCode = node.InnerText;
							break;
						case "currencyRate":
							productSet.currencyRate = float.Parse(node.InnerText);
							break;
						case "ndsRateType":
							productSet.ndsRateType = (NdsRateType)Enum.Parse(typeof(NdsRateType), node.InnerText);
							break;
						case "products":
							try
							{
								productSet.products = ParseProducts(node);
							}
							catch (Exception e)
							{
								//LogManagerFacade.ParsingXmlExeption(node.Name, node.Value, e);
							}
							break;
						case "totalExciseAmount":
							productSet.totalExciseAmount = float.Parse(node.InnerText);
							break;
						case "totalNdsAmount":
							productSet.totalNdsAmount = float.Parse(node.InnerText);
							break;
						case "totalPriceWithTax":
							productSet.totalPriceWithTax = float.Parse(node.InnerText);
							break;
						case "totalPriceWithoutTax":
							productSet.totalPriceWithoutTax = float.Parse(node.InnerText);
							break;
						case "totalTurnoverSize":
							productSet.totalTurnoverSize = float.Parse(node.InnerText);
							break;
						default:
							break;
					}
				}
			}
			return productSet;
		}

		private static ObservableCollection<ProductV2> ParseProducts(XmlNode node)
		{
			ObservableCollection<ProductV2> products = new ObservableCollection<ProductV2>();
			foreach (XmlNode product in node)
			{
				ProductV2 productV2 = ParseProduct(product);
				products.Add(productV2);
			}
			return products;
		}

		private static ProductV2 ParseProduct(XmlNode product)
		{
			ProductV2 productV2 = new ProductV2();
			foreach (XmlNode subnode in product)
			{
				switch (subnode.Name)
				{
					case "additional":
						productV2.additional = subnode.InnerText;
						break;
					case "catalogTruId":
						productV2.catalogTruId = subnode.InnerText;
						break;
					case "description":
						productV2.description = subnode.InnerText;
						break;
					case "exciseAmount":
						productV2.exciseAmount = float.Parse(subnode.InnerText);
						break;
					case "exciseRate":
						productV2.exciseRate = float.Parse(subnode.InnerText);
						break;
					case "kpvedCode":
						productV2.kpvedCode = subnode.InnerText;
						break;
					case "ndsAmount":
						productV2.ndsAmount = float.Parse(subnode.InnerText);
						break;
					case "ndsRate":
						productV2.ndsRate = int.Parse(subnode.InnerText);
						break;
					case "priceWithTax":
						productV2.priceWithTax = float.Parse(subnode.InnerText);
						break;
					case "priceWithoutTax":
						productV2.priceWithoutTax = float.Parse(subnode.InnerText);
						break;
					case "productDeclaration":
						productV2.productDeclaration = subnode.InnerText;
						break;
					case "productNumberInDeclaration":
						productV2.productNumberInDeclaration = subnode.InnerText;
						break;
					case "quantity":
						productV2.quantity = subnode.InnerText;
						break;
					case "tnvedName":
						productV2.tnvedName = subnode.InnerText;
						break;
					case "truOriginCode":
						productV2.truOriginCode = int.Parse(subnode.InnerText);
						break;
					case "turnoverSize":
						productV2.turnoverSize = float.Parse(subnode.InnerText);
						break;
					case "unitCode":
						productV2.unitCode = subnode.InnerText;
						break;
					case "unitNomenclature":
						productV2.unitNomenclature = subnode.InnerText;
						break;
					case "unitPrice":
						productV2.unitPrice = float.Parse(subnode.InnerText);
						break;
					default:
						break;
				}
			}
			return productV2;
		}

		private static DeliveryTermV2 ParseDeliveryTerm(XmlNode item)
		{
			DeliveryTermV2 deliveryTerm = new DeliveryTermV2();
			foreach (XmlNode node in item)
			{
				if (node.InnerText != "")
				{
					switch (node.Name)
					{
						case "contractDate":
							try
							{
								deliveryTerm.contractDate = node.InnerText;
							}
							catch (Exception e)
							{
								//LogManagerFacade.ParsingXmlExeption(node.Name, node.InnerText, e);
							}
							break;
						case "contractNum":
							deliveryTerm.contractNum = node.InnerText;
							break;
						case "deliveryConditionCode":
							deliveryTerm.deliveryConditionCode = node.InnerText;
							break;
						case "destination":
							deliveryTerm.destination = node.InnerText;
							break;
						case "hasContract":
							deliveryTerm.hasContract = bool.Parse(node.InnerText);
							break;
						case "term":
							deliveryTerm.term = node.InnerText;
							break;
						case "transportTypeCode":
							deliveryTerm.transportTypeCode = node.InnerText;
							break;
						case "warrant":
							deliveryTerm.warrant = node.InnerText;
							break;
						case "warrantDate":
							try
							{
								deliveryTerm.warrantDate = node.InnerText;
							}
							catch (Exception e)
							{
								//LogManagerFacade.ParsingXmlExeption(node.Name, node.InnerText, e);
							}
							break;
						default:
							break;
					}
				}
			}
			return deliveryTerm;
		}

		private static ObservableCollection<CustomerV2> ParseCustomers(XmlNode item)
		{
			ObservableCollection<CustomerV2> customers = new ObservableCollection<CustomerV2>();
			foreach (XmlNode customer in item)
			{
				CustomerV2 customerV2 = ParseCustomer(customer);
				customers.Add(customerV2);
			}
			return customers;
		}

		private static CustomerV2 ParseCustomer(XmlNode customer)
		{
			CustomerV2 customerV2 = new CustomerV2();
			foreach (XmlNode node in customer)
			{
				if (node.InnerText != "")
				{
					switch (node.Name)
					{
						case "address":
							customerV2.address = node.InnerText;
							break;
						case "branchTin":
							customerV2.branchTin = node.InnerText;
							break;
						case "countryCode":
							customerV2.countryCode = node.InnerText;
							break;
						case "name":
							customerV2.name = node.InnerText;
							break;
						case "reorganizedTin":
							customerV2.reorganizedTin = node.InnerText;
							break;
						case "shareParticipation":
							customerV2.shareParticipation = float.Parse(node.InnerText);
							break;
						case "statuses":
							try
							{
								customerV2.statuses = ParseCustomerStatuses(node);
							}
							catch (Exception e)
							{
								//LogManagerFacade.ParsingXmlExeption(node.Name, node.InnerText, e);
							}
							break;
						case "tin":
							customerV2.tin = node.InnerText;
							break;
						case "trailer":
							customerV2.trailer = node.InnerText;
							break;
						default:
							break;
					}
				}
			}
			return customerV2;
		}

		private static ObservableCollection<UserStatus> ParseCustomerStatuses(XmlNode node)
		{
			List<CustomerType> list = new List<CustomerType>();
		
			foreach (XmlNode status in node)
			{
				var result = ParseType(status.InnerText, typeof(CustomerType));
				if (result != null)
				{
					list.Add((CustomerType)result);
				}
			}

			ObservableCollection<UserStatus> statuses = new ObservableCollection<UserStatus>();

			foreach (CustomerType status in (CustomerType[])Enum.GetValues(typeof(CustomerType)))
			{
				UserStatus tmp = new UserStatus();
				tmp.type = status;
				tmp.isChecked = list.Contains(status);
				statuses.Add(tmp);
			}
			return statuses;
		}

		private static ObservableCollection<ParticipantV2> ParseCustomerParticipants(XmlNode item)
		{
			ObservableCollection<ParticipantV2> customerParticipants = new ObservableCollection<ParticipantV2>();
			foreach (XmlNode participant in item)
			{
				ParticipantV2 customerParticipant = ParseCustomerParticipant(participant);
				customerParticipants.Add(customerParticipant);
			}
			return customerParticipants;
		}

		private static ParticipantV2 ParseCustomerParticipant(XmlNode participant)
		{
			ParticipantV2 customerParticipant = new ParticipantV2();
			foreach (XmlNode node in participant)
			{
				switch (node.Name)
				{
					case "productShares":
						customerParticipant.productShares = ParseProductShares(node);
						break;
					case "reorganizedTin":
						customerParticipant.reorganizedTin = node.InnerText;
						break;
					case "tin":
						customerParticipant.tin = node.InnerText;
						break;
					default:
						break;
				}
			}
			return customerParticipant;
		}

		private static ObservableCollection<ProductShare> ParseProductShares(XmlNode node)
		{
			ObservableCollection<ProductShare> productShares = new ObservableCollection<ProductShare>();
			foreach (XmlNode share in node)
			{
				ProductShare productShare = ParseProductShare(share);
				productShares.Add(productShare);
			}
			return productShares;
		}

		private static ProductShare ParseProductShare(XmlNode share)
		{
			ProductShare productShare = new ProductShare();
			foreach (XmlNode subnode in share)
			{
				switch (subnode.Name)
				{
					case "additional":
						productShare.additional = subnode.InnerText;
						break;
					case "exciseAmount":
						productShare.exciseAmount = float.Parse(subnode.InnerText);
						break;
					case "ndsAmount":
						productShare.ndsAmount = float.Parse(subnode.InnerText);
						break;
					case "priceWithTax":
						productShare.priceWithTax = float.Parse(subnode.InnerText);
						break;
					case "priceWithoutTax":
						productShare.priceWithoutTax = float.Parse(subnode.InnerText);
						break;
					case "productNumber":
						productShare.productNumber = int.Parse(subnode.InnerText);
						break;
					case "quantity":
						productShare.quantity = subnode.InnerText;
						break;
					case "turnoverSize":
						productShare.turnoverSize = float.Parse(subnode.InnerText);
						break;
					default:
						break;
				}
			}
			return productShare;
		}

		private static Consignor ParseConsignor(XmlNode item)
		{
			Consignor consignor = new Consignor();
			foreach (XmlNode subItem in item)
			{
				if (subItem.InnerText != "")
				{
					switch (subItem.Name)
					{
						case "address":
							consignor.address = subItem.InnerText;
							break;
						case "name":
							consignor.name = subItem.InnerText;
							break;
						case "tin":
							consignor.tin = subItem.InnerText;
							break;
						default:
							break;
					}
				}
			}
			return consignor;
		}

		private static ConsigneeV2 ParseConsignee(XmlNode item)
		{
			ConsigneeV2 consignee = new ConsigneeV2();
			foreach (XmlNode subItem in item)
			{
				if (subItem.InnerText != "")
				{
					switch (subItem.Name)
					{
						case "address":
							consignee.address = subItem.InnerText;
							break;
						case "countryCode":
							consignee.countryCode = subItem.InnerText;
							break;
						case "name":
							consignee.name = subItem.InnerText;
							break;
						case "tin":
							consignee.tin = subItem.InnerText;
							break;
						default:
							break;
					}
				}
			}
			return consignee;
		}

		private static RelatedInvoice ParseRelatedInvoice(XmlNode relInvoice)
		{
			RelatedInvoice relatedInvoice = new RelatedInvoice();
			foreach (XmlNode item in relInvoice)
			{
				if (item.InnerText != "")
				{
					switch (item.Name)
					{
						case "date":
							try
							{
								relatedInvoice.date = item.InnerText;
							}
							catch (Exception e)
							{
								//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
							}
							break;
						case "num":
							try
							{
								relatedInvoice.num = item.InnerText;
							}
							catch (Exception e)
							{
								//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
							}
							break;
						case "registrationNumber":
							try
							{
								relatedInvoice.registrationNumber = item.InnerText;
							}
							catch (Exception e)
							{
								//LogManagerFacade.ParsingXmlExeption(item.Name, item.InnerText, e);
							}
							break;
						default:
							break;
					}
				}
			}
			return relatedInvoice;
		}

		private static object ParseInvoiceType(string innerText)
		{
			if (innerText == InvoiceType.ORDINARY_INVOICE.ToString())
				return InvoiceType.ORDINARY_INVOICE;
			else if (innerText == InvoiceType.ADDITIONAL_INVOICE.ToString())
				return InvoiceType.ADDITIONAL_INVOICE;
			else if (innerText == InvoiceType.FIXED_INVOICE.ToString())
				return InvoiceType.FIXED_INVOICE;
			else
				return null;
		}

		//Parse date in format dd.MM.yyyy
		private static object ParseDate(string innerText)
		{
			try
			{
				string[] tmp = new string[] { };
				int day, month, year;

				tmp = innerText.Split('.');
				day = int.Parse(tmp[0]);
				month = int.Parse(tmp[1]);
				year = int.Parse(tmp[2]);
				return new DateTime(year, month, day);
			}
			catch (Exception)
			{
				return null;
			}

		}
	}
}
