﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.ENUMs
{
	public enum InvoiceType
	{
		ORDINARY_INVOICE = 1,//Основной ЭСФ
		FIXED_INVOICE = 2,//Исправленный ЭСФ (A 4)
		ADDITIONAL_INVOICE = 3//Дополнительный ЭСФ (A 5)
	}
}
