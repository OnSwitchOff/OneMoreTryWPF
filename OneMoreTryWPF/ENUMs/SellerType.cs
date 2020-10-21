using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneMoreTryWPF.ENUMs
{
	//Тип поставщика (B 10)
	[Serializable]
	public enum SellerType
	{
		COMMITTENT,//Комитент
		BROKER,//Комиссионер
		FORWARDER,//Экспедитор
		LESSOR,//Лизингодатель
		JOINT_ACTIVITY_PARTICIPANT,//Участник договора совместной деятельности
		SHARING_AGREEMENT_PARTICIPANT,//Участник СРП или сделки, заключенной в рамках СРП
		EXPORTER,//Экспортёр
		TRANSPORTER,//Международный перевозчик
		PRINCIPAL,//Доверитель
	}
}
