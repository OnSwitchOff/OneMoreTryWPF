﻿using OneMoreTryWPF.ENUMs;
using OneMoreTryWPF.Models;
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

		internal static ObservableCollection<UserStatus> GetRandomSellerStatuses()
		{
			ObservableCollection<UserStatus> statuses = new ObservableCollection<UserStatus>();

			foreach (SellerType status in (SellerType[])Enum.GetValues(typeof(SellerType)))
			{
				UserStatus tmp = new UserStatus();
				tmp.type = status;
				tmp.isChecked = true;
				statuses.Add(tmp);
			}
			return statuses;
		}

		internal static ObservableCollection<UserStatus> GetRandomCustomerStatuses()
		{
			ObservableCollection<UserStatus> statuses = new ObservableCollection<UserStatus>();

			foreach (CustomerType status in (CustomerType[])Enum.GetValues(typeof(CustomerType)))
			{
				UserStatus tmp = new UserStatus();
				tmp.type = status;
				tmp.isChecked = true;
				statuses.Add(tmp);
			}
			return statuses;
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

		internal static ProductSetV2 GetProductSet()
		{
			ProductSetV2 set = new ProductSetV2();
			set.products = SessionDataManagerFacade.GetRandomProducts();
			return set;
		}

		internal static ObservableCollection<SellerV2> GetSellers()
		{
			ObservableCollection <SellerV2> sellers = new ObservableCollection<SellerV2>();
			SellerV2 seller = new SellerV2();
			sellers.Add(seller);
			return sellers;
		}
	}
}
